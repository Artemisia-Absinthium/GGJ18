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
	[AddComponentMenu( "Engine/Systems/Tween/Tween Translate" )]
	public class TweenTranslate : TweenAction
	{
		#region Members
		private Vector3 m_origin = Vector3.zero;
		private Vector3 m_target = Vector3.zero;
		private Tween m_tween = null;
		private bool m_launched = false;
		#endregion

		#region Methods
		public override void StartAction()
		{
			Debug.Assert( m_destination );
			if ( !m_destination )
			{
				return;
			}
			m_origin = transform.position;
			m_target = m_destination.position;
			m_tween = TweenManager.Instance.CreateTween( 0.0f, 1.0f, m_duration, m_ease );
			m_launched = true;
		}

		void Update()
		{
			if ( m_launched )
			{
				float value = m_tween.Value;
				if ( m_tween.Complete )
				{
					TweenManager.Instance.RemoveTween( m_tween.ID );
					m_launched = false;
					switch ( m_axis )
					{
					case Axis.AXIS_X:
						transform.position = new Vector3( m_target.x, m_origin.y, m_origin.z );
						break;
					case Axis.AXIS_Y:
						transform.position = new Vector3( m_origin.x, m_target.y, m_origin.z );
						break;
					case Axis.AXIS_Z:
						transform.position = new Vector3( m_origin.x, m_origin.y, m_target.z );
						break;
					case Axis.AXIS_ALL:
						transform.position = m_target;
						break;
					case Axis.AXIS_ALL_BUT_X:
						transform.position = new Vector3( m_origin.x, m_target.y, m_target.z );
						break;
					case Axis.AXIS_ALL_BUT_Y:
						transform.position = new Vector3( m_target.x, m_origin.y, m_target.z );
						break;
					case Axis.AXIS_ALL_BUT_Z:
						transform.position = new Vector3( m_target.x, m_target.y, m_origin.z );
						break;
					}
				}
				else
				{
					switch ( m_axis )
					{
					case Axis.AXIS_X:
						transform.position = new Vector3( Mathf.LerpUnclamped( m_origin.x, m_target.x, value ), m_origin.y, m_origin.z );
						break;
					case Axis.AXIS_Y:
						transform.position = new Vector3( m_origin.x, Mathf.LerpUnclamped( m_origin.y, m_target.y, value ), m_origin.z );
						break;
					case Axis.AXIS_Z:
						transform.position = new Vector3( m_origin.x, m_origin.y, Mathf.LerpUnclamped( m_origin.z, m_target.z, value ) );
						break;
					case Axis.AXIS_ALL:
						transform.position = Vector3.LerpUnclamped( m_origin, m_target, value );
						break;
					case Axis.AXIS_ALL_BUT_X:
						transform.position = new Vector3( m_origin.x, Mathf.LerpUnclamped( m_origin.y, m_target.y, value ), Mathf.LerpUnclamped( m_origin.z, m_target.z, value ) );
						break;
					case Axis.AXIS_ALL_BUT_Y:
						transform.position = new Vector3( Mathf.LerpUnclamped( m_origin.x, m_target.x, value ), m_origin.y, Mathf.LerpUnclamped( m_origin.z, m_target.z, value ) );
						break;
					case Axis.AXIS_ALL_BUT_Z:
						transform.position = new Vector3( Mathf.LerpUnclamped( m_origin.x, m_target.x, value ), Mathf.LerpUnclamped( m_origin.y, m_target.y, value ), m_origin.z );
						break;
					}
				}
			}
		}
		#endregion
	}
}
