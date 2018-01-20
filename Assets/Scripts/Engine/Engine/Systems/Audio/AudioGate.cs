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

namespace Engine
{
	[System.Serializable]
	[DisallowMultipleComponent]
	[AddComponentMenu( "Engine/Systems/Audio/Audio Gate" )]
	public class AudioGate : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		[Tooltip("The attenuation amount of the gate")]
		private float m_attenuation = 0.0f;
		[SerializeField]
		private AudioRoom m_audioRoomA = null;
		[SerializeField]
		private AudioRoom m_audioRoomB = null;
		#endregion

		#region Members
		private float m_invAttenuation = 1.0f;
		private AudioRoom[] m_roomsArray;
		private bool m_isDirty = true;
		#endregion

		#region Properties
		public float Attenuation
		{
			get { return m_attenuation; }
			set
			{
				m_attenuation = value;
				m_invAttenuation = 1.0f - m_attenuation;
			}
		}

		public float InvertAttenuation
		{
			get { return m_invAttenuation; }
		}
		#endregion

		#region Methods
		void Start()
		{
			if ( m_isDirty )
			{
				if ( m_roomsArray == null )
				{
					m_roomsArray = new AudioRoom[ 2 ];
				}
				m_roomsArray[ 0 ] = m_audioRoomA;
				m_roomsArray[ 1 ] = m_audioRoomB;
			}
		}

		public void AddRooms()
		{
			if ( m_audioRoomA )
			{
				m_audioRoomA.AddGate( this );
			}
			if ( m_audioRoomB )
			{
				m_audioRoomB.AddGate( this );
			}
		}

		public void SetRoomA( AudioRoom _room )
		{
			SetRoomInternal( ref m_audioRoomA, _room );
		}

		public void SetRoomB( AudioRoom _room )
		{
			SetRoomInternal( ref m_audioRoomB, _room );
		}

		private void SetRoomInternal( ref AudioRoom _toChange, AudioRoom _room )
		{
			if ( _toChange != null )
			{
				_toChange.RemoveGate( this );
			}
			_toChange = _room;
			_toChange.AddGate( this );
			m_isDirty = true;
		}

		public AudioRoom[] GetConnectedRooms()
		{
			Start();
			return m_roomsArray;
		}
		#endregion

#if DEBUGGING
		#region Gizmos
		private void OnDrawGizmoInternal( Color _colorBox, Color _colorLine )
		{
			if ( !enabled )
			{
				return;
			}
			Vector3 scale = transform.localScale;
			Transform t = transform;
			while ( t.parent != null )
			{
				t = t.parent;
				scale.x *= t.localScale.x;
				scale.y *= t.localScale.y;
				scale.z *= t.localScale.z;
			}
			Gizmos.color = _colorBox;
			Gizmos.DrawWireCube( transform.position, scale );

			Gizmos.color = _colorLine;
			if ( m_audioRoomA != null )
			{
				Gizmos.DrawLine( transform.position, m_audioRoomA.transform.position );
			}
			if ( m_audioRoomB != null )
			{
				Gizmos.DrawLine( transform.position, m_audioRoomB.transform.position );
			}
		}
		private void OnDrawGizmos()
		{
			OnDrawGizmoInternal( new Color( 0.1f, 1.0f, 0.2f, 0.5f ), new Color( 1.0f, .5f, 0.1f, 0.5f ) );
		}
		private void OnDrawGizmosSelected()
		{
			OnDrawGizmoInternal( new Color( 0.1f, 1.0f, 0.2f, 1.0f ), new Color( 1.0f, .5f, 0.1f, 1.0f ) );
		}
		#endregion
#endif
	}
}
