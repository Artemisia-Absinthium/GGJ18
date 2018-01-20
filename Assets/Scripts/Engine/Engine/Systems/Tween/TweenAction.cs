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
	public abstract class TweenAction : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		[Tooltip( "The destination of this object" )]
		protected Transform m_destination = null;
		[SerializeField]
		[Tooltip( "Time to complete the action" )]
		protected float m_duration = 0.0f;
		[SerializeField]
		[Tooltip( "Function used to ease the movement" )]
		protected TweenEase m_ease = TweenEase.NONE;
		[SerializeField]
		[Tooltip( "Restrict tweening on one two or three axis" )]
		protected Axis m_axis = Axis.AXIS_ALL;
		#endregion

		#region Methods
		public abstract void StartAction();
		#endregion
	}
}
