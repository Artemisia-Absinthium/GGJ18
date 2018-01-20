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
		private static Dictionary<uint, Tween> s_tweens;
		private static ObjectPoolT<Tween> s_tweenPool;
		private static uint s_id = 0;
		private static TweenManager s_instance = null;
		#endregion

		#region Methods
		void Start()
		{
			if ( s_instance != null )
			{
				Destroy( this );
			}
			DontDestroyOnLoad( this );
			s_tweens = new Dictionary<uint, Tween>();
			s_tweenPool = new ObjectPoolT<Tween>();
		}

		public static Tween CreateTween( float _startValue, float _endValue, float _duration, TweenEase _ease )
		{
			Tween t = s_tweenPool.Unpool();
			t.m_id = s_id;
			s_id++;
			t.m_ease = _ease;
			t.m_time = Time.time;
			t.m_duration = _duration;
			t.m_startValue = _startValue;
			t.m_endValue = _endValue;
			s_tweens.Add( t.m_id, t );
			return t;
		}

		public static Tween GetTween( uint _id )
		{
			Tween t;
			if ( s_tweens.TryGetValue( _id, out t ) )
			{
				return t;
			}
			return null;
		}

		public static void RemoveTween( uint _id )
		{
			Tween t = GetTween( _id );
			if ( t != null )
			{
				s_tweens.Remove( _id );
				s_tweenPool.Pool( ref t );
			}
		}
		#endregion
	}
}
