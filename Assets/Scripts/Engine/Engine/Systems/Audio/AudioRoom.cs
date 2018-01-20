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
	[DisallowMultipleComponent]
	[AddComponentMenu( "Engine/Systems/Audio/Audio Room" )]
	public class AudioRoom : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		[Tooltip("The density of the atmosphere in the room")]
		private float m_density = 1.0f;
		#endregion

		#region Members
		private float m_invDensity = 1.0f;
		private List<AudioGate> m_gates;
		#endregion

		#region Properties
		public float Density
		{
			get { return m_density; }
			set
			{
				m_density = value >= 1.0f ? value : 1.0f;
				m_invDensity = 1.0f / m_density;
			}
		}

		public float InvertDensity
		{
			get { return m_invDensity; }
		}
		#endregion

		#region Methods
		void Awake()
		{
			m_gates = new List<AudioGate>();
			Density = m_density; // Apply property rules
		}

		public void AddGate( AudioGate _gate )
		{
			for ( int iGate = 0; iGate < m_gates.Count; ++iGate )
			{
				if ( m_gates[ iGate ] == _gate )
				{
					return;
				}
			}
			m_gates.Add( _gate );
		}

		public void RemoveGate( AudioGate _gate )
		{
			m_gates.Remove( _gate );
		}

		public List<AudioGate> GetConnectedGates()
		{
			return m_gates;
		}

		public bool Contains( Vector3 _point )
		{
			Vector3 scale = transform.localScale;
			Transform t = transform;
			while ( t.parent != null )
			{
				t = t.parent;
				scale.x *= t.localScale.x;
				scale.y *= t.localScale.y;
				scale.z *= t.localScale.z;
			}
			Bounds bounds = new Bounds( transform.position, scale );
			return bounds.Contains( _point );
		}
		#endregion

#if DEBUGGING
		#region Gizmos
		private void OnDrawGizmoInternal( Color _colorBox )
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
		}
		private void OnDrawGizmos()
		{
			OnDrawGizmoInternal( new Color( 1.0f, 0.0f, 0.8f, 0.5f ) );
		}
		private void OnDrawGizmosSelected()
		{
			OnDrawGizmoInternal( new Color( 1.0f, 0.0f, 0.8f, 1.0f ) );
		}
		#endregion
#endif
	}
}
