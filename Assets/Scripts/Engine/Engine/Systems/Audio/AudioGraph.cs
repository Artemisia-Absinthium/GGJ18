/*
 * MIT License
 * 
 * Copyright (c) 2017 Joseph Kieffer
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
	[System.Serializable]
	[AddComponentMenu( "Engine/Systems/Audio/Audio Graph" )]
	public class AudioGraph : MonoBehaviour
	{
		#region Fields
#if DEBUGGING
		[SerializeField]
		[Tooltip("Draw the connexions btween gates, sources and receiver")]
		private bool m_debugDraw = true;
#endif
		[SerializeField]
		[Tooltip("The receiver of the sounds")]
		private AudioReceiver m_receiver = null;
		[SerializeField]
		[Tooltip("The quality of the audio update rate")]
		private PerformanceModifier m_performances = PerformanceModifier.VERY_HIGH_QUALITY;
		[SerializeField]
		[Range( 0.0f, 1.0f )]
		[Tooltip("Factor with which sources ar lerped when moving")]
		private float m_emitterLerpFactor = 0.25f;
		#endregion

		#region Members
		private float[] m_dijDist = null;
		private int[] m_dijPrev = null;
		private int[] m_dijQ = null;
		private List<AudioGraphNode> m_nodes = null;
		private ObjectPoolT<AudioGraphNode> m_temporaryNodes = null;
		private ObjectPoolT<AudioGraphEdge> m_temporaryEdges = null;
		private ObjectPoolT<List<AudioGraphEdge>> m_temporaryEdgeList = null;
		private List<AudioEmitter> m_emitters = null;
		private List<AudioRoom> m_rooms = null;
		private AudioEmitter m_currentEmitter = null;
		private List<AudioGate> m_gatesPath = null;
		private Scheduler m_emitterScheduler = null;
		#endregion

		#region Properties
		public AudioReceiver Receiver
		{
			get { return m_receiver; }
			set { m_receiver = value; }
		}

		public PerformanceModifier Performances
		{
			get { return m_performances; }
			set { m_performances = value; }
		}

		public float EmitterLerpFactor
		{
			get { return m_emitterLerpFactor; }
			set
			{
				m_emitterLerpFactor = value < 0.0f ? 0.0f : ( value > 1.0f ? 1.0f : value );
			}
		}
		#endregion

		#region Methods
		void Start()
		{
			m_nodes = new List<AudioGraphNode>();
			m_temporaryNodes = new ObjectPoolT<AudioGraphNode>( 64 );
			m_temporaryEdges = new ObjectPoolT<AudioGraphEdge>( 256 );
			m_temporaryEdgeList = new ObjectPoolT<List<AudioGraphEdge>>( 64 );
			m_emitters = new List<AudioEmitter>( FindObjectsOfType<AudioEmitter>() );
			m_rooms = new List<AudioRoom>( FindObjectsOfType<AudioRoom>() );
			m_gatesPath = new List<AudioGate>();
			m_emitterScheduler = new Scheduler();

			AudioGate[] gatesToInitialize = FindObjectsOfType<AudioGate>();
			for ( int iGate = 0; iGate < gatesToInitialize.Length; ++iGate )
			{
				gatesToInitialize[ iGate ].AddRooms();
			}

			for ( int iRoom = 0; iRoom < m_rooms.Count; ++iRoom )
			{
				List<AudioGate> gates = m_rooms[ iRoom ].GetConnectedGates();
				for ( int iGateA = 0; iGateA < gates.Count; ++iGateA )
				{
					AudioGraphNode nodeA = null;
					int nodeAIndex = -1;
					for ( int iNode = 0; iNode < m_nodes.Count; ++iNode )
					{
						if ( m_nodes[ iNode ].gate == gates[ iGateA ] )
						{
							nodeA = m_nodes[ iNode ];
							nodeAIndex = iNode;
							break;
						}
					}
					if ( nodeA == null )
					{
						nodeA = new AudioGraphNode();
						nodeA.gate = gates[ iGateA ];
						nodeA.edges = new List<AudioGraphEdge>();
						nodeAIndex = m_nodes.Count;
						m_nodes.Add( nodeA );
					}
					for ( int iGateB = 0; iGateB < gates.Count; ++iGateB )
					{
						if ( gates[ iGateA ] == gates[ iGateB ] )
						{
							continue;
						}
						AudioGraphNode nodeB = null;
						int nodeBIndex = -1;
						for ( int iNode = 0; iNode < m_nodes.Count; ++iNode )
						{
							if ( m_nodes[ iNode ].gate == gates[ iGateB ] )
							{
								nodeB = m_nodes[ iNode ];
								nodeBIndex = iNode;
								break;
							}
						}
						if ( nodeB == null )
						{
							nodeB = new AudioGraphNode();
							nodeB.gate = gates[ iGateB ];
							nodeB.edges = new List<AudioGraphEdge>();
							nodeBIndex = m_nodes.Count;
							m_nodes.Add( nodeB );
						}
						AudioGraphEdge edge = null;
						for ( int iEdge = 0; iEdge < nodeA.edges.Count; ++iEdge )
						{
							if ( ( nodeA.edges[ iEdge ].a == nodeAIndex && nodeA.edges[ iEdge ].b == nodeBIndex ) ||
								( nodeA.edges[ iEdge ].a == nodeBIndex && nodeA.edges[ iEdge ].b == nodeAIndex ) )
							{
								edge = nodeA.edges[ iEdge ];
								break;
							}
						}
						if ( edge == null )
						{
							edge = new AudioGraphEdge();
							edge.a = nodeAIndex;
							edge.b = nodeBIndex;
							edge.cost = ComputeEdgeCost( nodeA.gate, nodeB.gate );
							nodeA.edges.Add( edge );
							nodeB.edges.Add( edge );
						}
					}
				}
			}
			m_dijDist = new float[ m_nodes.Count + 2 ];
			m_dijPrev = new int[ m_nodes.Count + 2 ];
			m_dijQ = new int[ m_nodes.Count + 2 ];

			Receiver = m_receiver;
		}

		void Update()
		{
			if ( m_receiver == null )
			{
				Debug.Assert( false );
				return;
			}

			int firstElement = 0;
			int lastElement = 0;
			m_emitterScheduler.Update( m_performances, m_emitters.Count, out firstElement, out lastElement );
			DoOcclusionPass( m_emitters, firstElement, lastElement );
		}

		public List<AudioRoom> GetAudioRooms()
		{
			return m_rooms;
		}

		public void AddEmitter( AudioEmitter _emitter )
		{
			if ( !m_emitters.Contains( _emitter ) )
			{
				m_emitters.Add( _emitter );
			}
		}

		public void RemoveEmitter( AudioEmitter _emitter )
		{
			m_emitters.Remove( _emitter );
		}

		public int FindNode( AudioGate _gate )
		{
			for ( int iNode = 0; iNode < m_nodes.Count; ++iNode )
			{
				if ( m_nodes[ iNode ].gate == _gate )
				{
					return iNode;
				}
			}
			return -1;
		}

		public void RegenerateGraph()
		{
			Start();
		}

		private float ComputeEdgeCost( AudioGate _a, AudioGate _b )
		{
			AudioRoom[] roomsA = _a.GetConnectedRooms();
			AudioRoom[] roomsB = _b.GetConnectedRooms();

			for ( int iRoomA = 0; iRoomA < roomsA.Length; ++iRoomA )
			{
				for ( int iRoomB = 0; iRoomB < roomsB.Length; ++iRoomB )
				{
					if ( roomsA[ iRoomA ] == roomsB[ iRoomB ] )
					{
						return Vector3.Distance( _a.transform.position, _b.transform.position ) * roomsA[ iRoomA ].Density;
					}
				}
			}

			return Mathf.Infinity;
		}

		private AudioGraphNode AppendTemporaryNode( Vector3 _position, List<AudioGate> _gates, AudioRoom _room )
		{
			AudioGraphNode n = m_temporaryNodes.Unpool();
			n.edges = m_temporaryEdgeList.UnpoolAsIs();
			n.edges.Clear();

			if ( _gates != null )
			{
				for ( int iGate = 0; iGate < _gates.Count; ++iGate )
				{
					int nodeIndex = FindNode( _gates[ iGate ] );
					if ( nodeIndex >= 0 )
					{
						AudioGraphEdge e = m_temporaryEdges.Unpool();
						e.a = nodeIndex;
						e.b = m_nodes.Count;
						e.cost = Vector3.Distance( _position, _gates[ iGate ].transform.position ) * _room.Density;
						m_nodes[ nodeIndex ].edges.Add( e );
						n.edges.Add( e );
					}
				}
			}

			m_nodes.Add( n );
			return n;
		}

		private void RemoveTemporaryNode()
		{
			AudioGraphNode n = m_nodes[ m_nodes.Count - 1 ];
			m_nodes.RemoveAt( m_nodes.Count - 1 );

			for ( int iEdge = 0; iEdge < n.edges.Count; ++iEdge )
			{
				AudioGraphEdge e = n.edges[ iEdge ];
				AudioGraphNode node = m_nodes[ e.a ];
				if ( node == n )
				{
					node = m_nodes[ e.b ];
				}
				if ( node != null )
				{
					for ( int iEdge2 = 0; iEdge2 < node.edges.Count; ++iEdge2 )
					{
						if ( node.edges[ iEdge2 ] == e )
						{
							node.edges.RemoveAt( iEdge2 );
							break;
						}
					}
				}
				m_temporaryEdges.Pool( ref e );
			}

			m_temporaryEdgeList.Pool( ref n.edges );

			m_temporaryNodes.Pool( ref n );
		}

		private void ComputeCurrentAudioRoom( AudioReceiver _receiver )
		{
			for ( int iRoom = 0; iRoom < m_rooms.Count; ++iRoom )
			{
				if ( m_rooms[ iRoom ].Contains( _receiver.transform.position ) )
				{
					_receiver.SetCurrentAudioRoom( m_rooms[ iRoom ] );
					return;
				}
			}
			_receiver.SetCurrentAudioRoom( null );
		}

		private AudioGraphNode AppendTemporaryListener()
		{
			AudioRoom listenerRoom = m_receiver.GetCurrentAudioRoom();
			if ( listenerRoom == null )
			{
				return AppendTemporaryNode( m_receiver.transform.position, null, null );
			}
			List<AudioGate> listenerGates = listenerRoom.GetConnectedGates();
			return AppendTemporaryNode( m_receiver.transform.position, listenerGates, listenerRoom );
		}

		private void RemoveTemporaryListener()
		{
			RemoveTemporaryNode();
		}

		private AudioGraphNode AppendTemporaryEmitter()
		{
			AudioRoom emitterRoom = m_currentEmitter.GetCurrentAudioRoom();
			if ( emitterRoom == null )
			{
				return AppendTemporaryNode( m_currentEmitter.transform.position, null, null );
			}
			List<AudioGate> emitterGates = emitterRoom.GetConnectedGates();
			return AppendTemporaryNode( m_currentEmitter.transform.position, emitterGates, emitterRoom );
		}

		private void RemoveTemporaryEmitter()
		{
			RemoveTemporaryNode();
		}

		private float FindPath( out List<AudioGate> _gatesPath )
		{
			_gatesPath = null;
			// Check if distance is less than maximum source earable distance
			float d = Vector3.Distance( m_receiver.transform.position, m_currentEmitter.transform.position );
			if ( d > m_currentEmitter.Source.maxDistance )
			{
				return Mathf.Infinity;
			}

			AudioRoom emitterRoom = m_currentEmitter.GetCurrentAudioRoom();
			AudioRoom listenerRoom = m_receiver.GetCurrentAudioRoom();

			if ( emitterRoom == null || listenerRoom == null )
			{
				return Mathf.Infinity;
			}

			// Check if both emitter and listener are in the same room
			if ( emitterRoom == listenerRoom )
			{
				return d * listenerRoom.Density;
			}

			AppendTemporaryEmitter();

			int listenerIndex = m_nodes.Count - 2;
			int emitterIndex = m_nodes.Count - 1;

#if DEBUGGING
			if ( m_debugDraw )
			{
				for ( int iNode = 0; iNode < m_nodes.Count; ++iNode )
				{
					foreach ( AudioGraphEdge e in m_nodes[ iNode ].edges )
					{
						if ( e.b == iNode )
						{
							if ( e.b == m_nodes.Count - 1 )
							{
								Debug.DrawLine( m_currentEmitter.transform.position, m_nodes[ e.a ].gate.transform.position, Color.green );
							}
							else if ( e.b == m_nodes.Count - 2 )
							{
								Debug.DrawLine( m_receiver.transform.position, m_nodes[ e.a ].gate.transform.position, Color.blue );
							}
							else if ( e.a < m_nodes.Count - 2 )
							{
								Debug.DrawLine( m_nodes[ e.a ].gate.transform.position, m_nodes[ e.b ].gate.transform.position, Color.red );
							}
						}
					}
				}
			}
#endif

			// Dijkstra
			int qSize = m_nodes.Count;

			for ( int iNode = 0; iNode < m_nodes.Count; ++iNode )
			{
				m_dijDist[ iNode ] = Mathf.Infinity;
				m_dijPrev[ iNode ] = -1;
				m_dijQ[ iNode ] = iNode;
			}

			m_dijDist[ emitterIndex ] = 0.0f;

			while ( qSize > 0 )
			{
				int x = 0;
				float xValue = m_dijDist[ m_dijQ[ 0 ] ];
				for ( int iNode = 1; iNode < qSize; ++iNode )
				{
					float newXValue = m_dijDist[ m_dijQ[ iNode ] ];
					if ( newXValue < xValue )
					{
						x = iNode;
						xValue = newXValue;
					}
				}
				--qSize;
				int xIndex = m_dijQ[ x ];
				if ( qSize > 0 )
				{
					m_dijQ[ x ] = m_dijQ[ qSize ];
				}
				x = xIndex;

				if ( x == listenerIndex )
				{
					_gatesPath = m_gatesPath;
					_gatesPath.Clear();
					int u = listenerIndex;
					float distance = m_dijDist[ u ];
					while ( m_dijPrev[ u ] >= 0 )
					{
						if ( u != listenerIndex && u != emitterIndex )
						{
							_gatesPath.Add( m_nodes[ u ].gate );
						}
						u = m_dijPrev[ u ];
					}
					RemoveTemporaryEmitter();
					return distance;
				}

				List<AudioGraphEdge> edges = m_nodes[ x ].edges;
				for ( int iEdge = 0; iEdge < edges.Count; ++iEdge )
				{
					int nodeIndex = edges[ iEdge ].a;
					if ( nodeIndex == x )
					{
						nodeIndex = edges[ iEdge ].b;
					}
					float alt = m_dijDist[ x ] + edges[ iEdge ].cost;
					if ( m_nodes[ x ].gate != null )
					{
						alt += m_nodes[ x ].gate.Attenuation * m_currentEmitter.Source.maxDistance;
					}
					if ( alt < m_dijDist[ nodeIndex ] )
					{
						m_dijDist[ nodeIndex ] = alt;
						m_dijPrev[ nodeIndex ] = x;
					}
				}
			}

			RemoveTemporaryEmitter();
			return Mathf.Infinity;
		}

		public void DoOcclusionPass( List<AudioEmitter> _emitters, int _first, int _last )
		{
			List<AudioGate> gates;

			ComputeCurrentAudioRoom( m_receiver );

			AppendTemporaryListener();

			_first = _first < 0 ? 0 : _first;
			_last = _last > _emitters.Count ? _emitters.Count : _last;

			for ( int iEmitter = _first; iEmitter < _last; ++iEmitter )
			{
				m_currentEmitter = _emitters[ iEmitter ];
				m_currentEmitter.ComputeCurrentAudioRoom( this );
				float distance = FindPath( out gates );
				if ( gates == null )
				{
					m_currentEmitter.UpdateSource(
						distance, m_receiver.transform.position,
						m_currentEmitter.transform.position,
						m_emitterLerpFactor * ( ( float )m_performances + 1.0f ) );
				}
				else
				{
					m_currentEmitter.UpdateSource(
						distance,
						m_receiver.transform.position,
						gates[ 0 ].transform.position,
						m_emitterLerpFactor * ( ( float )m_performances + 1.0f ) );
				}
			}

			RemoveTemporaryListener();
		}
		#endregion
	}
}
