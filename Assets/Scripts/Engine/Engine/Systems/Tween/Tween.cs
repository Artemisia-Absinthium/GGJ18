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
	public class Tween
	{
		#region Members
		public uint m_id;
		public TweenEase m_ease;
		public float m_time;
		public float m_duration;
		public float m_startValue;
		public float m_endValue;
		public bool m_ignoreScale;
		#endregion

		#region Properties
		public uint ID
		{
			get { return m_id; }
		}
		public float Value
		{
			get
			{
				float time = m_ignoreScale ? Time.unscaledTime : Time.time;
				return TweenFunctions.Ease( m_ease, time - m_time, m_startValue, m_endValue - m_startValue, m_duration );
			}
		}
		public bool Complete
		{
			get
			{
				float time = m_ignoreScale ? Time.unscaledTime : Time.time;
				return time - m_time >= m_duration;
			}
		}
		#endregion
	}
}
