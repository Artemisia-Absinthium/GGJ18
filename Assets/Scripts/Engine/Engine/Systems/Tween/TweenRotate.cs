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
	[AddComponentMenu( "Engine/Systems/Tween/Tween Rotate" )]
	public class TweenRotate : TweenAction
	{
		#region Members
		private Quaternion m_origin = Quaternion.identity;
		private Quaternion m_target = Quaternion.identity;
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
			m_origin = transform.rotation;
			m_target = m_destination.rotation;
			m_tween = TweenManager.CreateTween( 0.0f, 1.0f, m_duration, m_ease );
			m_launched = true;
		}

		void Update()
		{
			if ( m_launched )
			{
				float value = m_tween.Value;
				if ( m_tween.Complete )
				{
					TweenManager.RemoveTween( m_tween.m_id );
					m_launched = false;
					transform.rotation = m_target;
				}
				else
				{
					transform.rotation = Quaternion.SlerpUnclamped( m_origin, m_target, value );
				}
			}
		}
		#endregion
	}
}
