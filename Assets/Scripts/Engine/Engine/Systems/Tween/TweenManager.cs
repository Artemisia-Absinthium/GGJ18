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
	[AddComponentMenu( "Engine/Systems/Tween/Tween Manager" )]
	public class TweenManager : MonoBehaviour
	{
		#region Members
		private Dictionary<uint, Tween> m_tweens;
		private ObjectPoolT<Tween> m_tweenPool;
		private uint m_id = 0;
		private static TweenManager s_instance = null;
		#endregion

		#region Properties
		public static TweenManager Instance
		{
			get { return s_instance; }
		}
		#endregion

		#region Methods
		void Start()
		{
			if ( s_instance != null )
			{
				Destroy( this );
				return;
			}
			DontDestroyOnLoad( this );
			s_instance = this;
			m_tweens = new Dictionary<uint, Tween>();
			m_tweenPool = new ObjectPoolT<Tween>();
		}

		public Tween CreateTween( float _startValue, float _endValue, float _duration, TweenEase _ease, bool _ignoreTimeScale = false )
		{
			Tween t = m_tweenPool.Unpool();
			t.m_id = m_id;
			m_id++;
			t.m_ease = _ease;
			t.m_time = _ignoreTimeScale ? Time.unscaledTime : Time.time;
			t.m_duration = _duration;
			t.m_startValue = _startValue;
			t.m_endValue = _endValue;
			t.m_ignoreScale = _ignoreTimeScale;
			m_tweens.Add( t.m_id, t );
			return t;
		}

		public Tween GetTween( uint _id )
		{
			Tween t;
			if ( m_tweens.TryGetValue( _id, out t ) )
			{
				return t;
			}
			return null;
		}

		public void RemoveTween( uint _id )
		{
			Tween t = GetTween( _id );
			if ( t != null )
			{
				m_tweens.Remove( _id );
				m_tweenPool.Pool( ref t );
			}
		}
		#endregion
	}
}
