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
using UnityEngine;
#if DEBUGGING
using UnityEditor;
#endif

namespace Engine
{
#pragma warning disable 649
	[System.Serializable]
	[AddComponentMenu( "Engine/Utilities/Optimization/Chunk" )]
	public class Chunk : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		[Tooltip("The bounding box containing the chunk")]
		private Vector3 m_boundingBox;
		[SerializeField]
		[Tooltip("Low resolution informations")]
		private ChunkInfo m_lowRes;
		[SerializeField]
		[Tooltip("Middle resolution informations")]
		private ChunkInfo m_midRes;
		[SerializeField]
		[Tooltip("High resolution informations")]
		private ChunkInfo m_highRes;
		[SerializeField]
		[Tooltip("Size of the transition zone to avoid loading flickering when at a limit position")]
		private float m_transitionZone = 5.0f;
		#endregion

		#region Members
		private Bounds m_bounds;
		private ChunkResolution m_currentResolution = ChunkResolution.Clip;
		private float m_cachedLowResMinViewDistance;
		private float m_cachedMidResMinViewDistance;
		private float m_cachedHighResMinViewDistance;
		private float m_cachedLowResMaxViewDistance;
		private float m_cachedMidResMaxViewDistance;
		private float m_cachedHighResMaxViewDistance;
		#endregion

		#region Properties
		public ChunkInfo LowRes
		{
			get { return m_lowRes; }
		}
		public ChunkInfo MidRes
		{
			get { return m_midRes; }
		}
		public ChunkInfo HighRes
		{
			get { return m_highRes; }
		}
		#endregion

		#region Methods
		void Start()
		{
			Debug.Assert( m_boundingBox.x > 0.0f && m_boundingBox.y > 0.0f && m_boundingBox.z > 0.0f );
			Debug.Assert( m_lowRes != null );
			Debug.Assert( m_lowRes.m_prefab != null );
			Debug.Assert( m_lowRes.m_viewDistance - m_midRes.m_viewDistance > m_transitionZone * 2.0f );
			Debug.Assert( m_midRes != null );
			Debug.Assert( m_midRes.m_prefab != null );
			Debug.Assert( m_midRes.m_viewDistance - m_highRes.m_viewDistance > m_transitionZone * 2.0f );
			Debug.Assert( m_highRes != null );
			Debug.Assert( m_highRes.m_prefab != null );
			Debug.Assert( m_highRes.m_viewDistance > m_transitionZone );

			m_bounds = new Bounds(
				transform.position,
				new Vector3( m_boundingBox.x, m_boundingBox.y, m_boundingBox.z ) );

			RecomputeDistances();
		}

		public void RecomputeDistances()
		{
			m_cachedLowResMinViewDistance = Mathf.Pow( m_lowRes.m_viewDistance - m_transitionZone, 2.0f );
			m_cachedMidResMinViewDistance = Mathf.Pow( m_midRes.m_viewDistance - m_transitionZone, 2.0f );
			m_cachedHighResMinViewDistance = Mathf.Pow( m_highRes.m_viewDistance - m_transitionZone, 2.0f );
			m_cachedLowResMaxViewDistance = Mathf.Pow( m_lowRes.m_viewDistance + m_transitionZone, 2.0f );
			m_cachedMidResMaxViewDistance = Mathf.Pow( m_midRes.m_viewDistance + m_transitionZone, 2.0f );
			m_cachedHighResMaxViewDistance = Mathf.Pow( m_highRes.m_viewDistance + m_transitionZone, 2.0f );
		}

		public ChunkResolution Adapt( Vector3 _cameraPosition )
		{
			float distance = m_bounds.SqrDistance( _cameraPosition );
			if ( m_currentResolution != ChunkResolution.High && distance <= m_cachedHighResMinViewDistance )
			{
				SetResolution( ChunkResolution.High );
				return ChunkResolution.High;
			}
			else if ( m_currentResolution == ChunkResolution.High && distance <= m_cachedHighResMaxViewDistance )
			{
				return ChunkResolution.High;
			}
			if ( m_currentResolution != ChunkResolution.Middle && distance <= m_cachedMidResMinViewDistance )
			{
				SetResolution( ChunkResolution.Middle );
				return ChunkResolution.Middle;
			}
			else if ( m_currentResolution == ChunkResolution.Middle && distance <= m_cachedMidResMaxViewDistance )
			{
				return ChunkResolution.Middle;
			}
			if ( m_currentResolution != ChunkResolution.Low && distance <= m_cachedLowResMinViewDistance )
			{
				SetResolution( ChunkResolution.Low );
				return ChunkResolution.Low;
			}
			else if ( m_currentResolution == ChunkResolution.Low && distance <= m_cachedLowResMaxViewDistance )
			{
				return ChunkResolution.Low;
			}
			if ( m_currentResolution != ChunkResolution.Clip )
			{
				SetResolution( ChunkResolution.Clip );
			}
			return ChunkResolution.Clip;
		}

		public void SetResolution( ChunkResolution _resolution )
		{
			m_currentResolution = _resolution;
		}
		#endregion

#if DEBUGGING
		#region Gizmos
		private void OnDrawGizmosDistanceCube( Vector3 _center, Vector3 _size, float _distance )
		{
			float distanceSqrt = Mathf.Cos( Mathf.PI / 4.0f ) * _distance;
			Vector3 up = Vector3.up * _distance;
			Vector3 right = Vector3.right * _distance;
			Vector3 forward = Vector3.forward * _distance;
			Vector3 upSqrt = new Vector3( 0.0f, distanceSqrt, 0.0f );
			Vector3 rightSqrt = new Vector3( distanceSqrt, 0.0f, 0.0f );
			Vector3 forwardSqrt = new Vector3( 0.0f, 0.0f, distanceSqrt );
			_size /= 2.0f;
			// Corners
			int zMult = -1;
			for ( int z = 0; z < 2; ++z )
			{
				int yMult = -1;
				for ( int y = 0; y < 2; ++y )
				{
					int xMult = -1;
					for ( int x = 0; x < 2; ++x )
					{
						Vector3 p = _center + new Vector3( _size.x * xMult, _size.y * yMult, _size.z * zMult );

						Vector3 p0 = p + up * yMult;
						Vector3 p1 = p + right * xMult;
						Vector3 p2 = p + forward * zMult;
						Vector3 p3 = p + upSqrt * yMult + rightSqrt * xMult;
						Vector3 p4 = p + upSqrt * yMult + forwardSqrt * zMult;
						Vector3 p5 = p + rightSqrt * xMult + forwardSqrt * zMult;

						Gizmos.DrawLine( p0, p3 );
						Gizmos.DrawLine( p0, p4 );
						Gizmos.DrawLine( p1, p3 );
						Gizmos.DrawLine( p1, p5 );
						Gizmos.DrawLine( p2, p4 );
						Gizmos.DrawLine( p2, p5 );
						Gizmos.DrawLine( p3, p4 );
						Gizmos.DrawLine( p3, p5 );
						Gizmos.DrawLine( p4, p5 );

						xMult *= -1;
					}
					yMult *= -1;
				}
				zMult *= -1;
			}
			// Sides
			int multA = -1;
			for ( int a = 0; a < 2; ++a )
			{
				int multB = -1;
				for ( int b = 0; b < 2; ++b )
				{
					Vector3 cA = _center + new Vector3( _size.x * multB, _size.y * multA, _size.z );
					Vector3 cB = _center + new Vector3( _size.x * multB, _size.y * multA, -_size.z );
					Vector3 c1 = cA + up * multA;
					Vector3 c2 = cA + right * multB;
					Vector3 c3 = cA + upSqrt * multA + rightSqrt * multB;
					Vector3 c4 = cB + up * multA;
					Vector3 c5 = cB + right * multB;
					Vector3 c6 = cB + upSqrt * multA + rightSqrt * multB;
					Gizmos.DrawLine( c1, c4 );
					Gizmos.DrawLine( c2, c5 );
					Gizmos.DrawLine( c3, c6 );

					multB *= -1;
				}
				multA *= -1;
			}
			multA = -1;
			for ( int a = 0; a < 2; ++a )
			{
				int multB = -1;
				for ( int b = 0; b < 2; ++b )
				{
					Vector3 cA = _center + new Vector3( _size.x * multB, _size.y, _size.z * multA );
					Vector3 cB = _center + new Vector3( _size.x * multB, -_size.y, _size.z * multA );
					Vector3 c1 = cA + forward * multA;
					Vector3 c2 = cA + right * multB;
					Vector3 c3 = cA + forwardSqrt * multA + rightSqrt * multB;
					Vector3 c4 = cB + forward * multA;
					Vector3 c5 = cB + right * multB;
					Vector3 c6 = cB + forwardSqrt * multA + rightSqrt * multB;
					Gizmos.DrawLine( c1, c4 );
					Gizmos.DrawLine( c2, c5 );
					Gizmos.DrawLine( c3, c6 );

					multB *= -1;
				}
				multA *= -1;
			}
			multA = -1;
			for ( int a = 0; a < 2; ++a )
			{
				int multB = -1;
				for ( int b = 0; b < 2; ++b )
				{
					Vector3 cA = _center + new Vector3( _size.x, _size.y * multA, _size.z * multB );
					Vector3 cB = _center + new Vector3( -_size.x, _size.y * multA, _size.z * multB );
					Vector3 c1 = cA + up * multA;
					Vector3 c2 = cA + forward * multB;
					Vector3 c3 = cA + upSqrt * multA + forwardSqrt * multB;
					Vector3 c4 = cB + up * multA;
					Vector3 c5 = cB + forward * multB;
					Vector3 c6 = cB + upSqrt * multA + forwardSqrt * multB;
					Gizmos.DrawLine( c1, c4 );
					Gizmos.DrawLine( c2, c5 );
					Gizmos.DrawLine( c3, c6 );

					multB *= -1;
				}
				multA *= -1;
			}
		}
		private void OnDrawGizmosInternal( bool selected, Color _colorBB, Color _colorLow, Color _colorMid, Color _colorHigh )
		{
			Gizmos.color = _colorBB;
			Gizmos.DrawWireCube( transform.position, m_boundingBox );
			if ( selected )
			{
				if ( m_lowRes.m_debugShowInEditor )
				{
					Gizmos.color = _colorLow;
					OnDrawGizmosDistanceCube( transform.position, m_boundingBox, m_lowRes.m_viewDistance );
				}
				if ( m_midRes.m_debugShowInEditor )
				{
					Gizmos.color = _colorMid;
					OnDrawGizmosDistanceCube( transform.position, m_boundingBox, m_midRes.m_viewDistance );
				}
				if ( m_highRes.m_debugShowInEditor )
				{
					Gizmos.color = _colorHigh;
					OnDrawGizmosDistanceCube( transform.position, m_boundingBox, m_highRes.m_viewDistance );
				}
			}
		}
		private void OnDrawGizmos()
		{
			OnDrawGizmosInternal( false,
				new Color( 0.2f, 0.2f, 0.2f, 0.5f ),
				new Color( 1.0f, 0.0f, 0.0f, 0.5f ),
				new Color( 0.0f, 1.0f, 0.0f, 0.5f ),
				new Color( 0.0f, 0.0f, 1.0f, 0.5f ) );
		}
		private void OnDrawGizmosSelected()
		{
			if ( Selection.activeTransform == transform )
			{
				OnDrawGizmosInternal( true,
					new Color( 0.1f, 0.1f, 0.1f, 1.0f ),
					new Color( 1.0f, 0.0f, 0.0f, 1.0f ),
					new Color( 0.0f, 1.0f, 0.0f, 1.0f ),
					new Color( 0.0f, 0.0f, 1.0f, 1.0f ) );
			}
			else
			{
				OnDrawGizmosInternal( true,
					new Color( 0.1f, 0.1f, 0.1f, 0.2f ),
					new Color( 1.0f, 0.0f, 0.0f, 0.2f ),
					new Color( 0.0f, 1.0f, 0.0f, 0.2f ),
					new Color( 0.0f, 0.0f, 1.0f, 0.2f ) );
			}
		}
		#endregion
#endif
	}
#pragma warning restore 649
}
