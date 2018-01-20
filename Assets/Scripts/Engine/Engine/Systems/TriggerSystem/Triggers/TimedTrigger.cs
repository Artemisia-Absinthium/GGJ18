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
#pragma warning disable 649
	[System.Serializable]
	[AddComponentMenu( "Engine/Systems/Trigger System/Triggers/Timed Trigger" )]
	public class TimedTrigger : TriggerBase
	{
		#region Fields
		[SerializeField]
		[Tooltip( "The time before the behaviour is triggered, in seconds" )]
		private float m_time;
		#endregion

		#region Methods
		public override void OnTrigger()
		{

		}

		void Start()
		{
			Invoke( "OnTrigger_internal", m_time );
		}

		public void CancelTrigger()
		{
			CancelInvoke( "OnTrigger_internal" );
		}
		#endregion
	}
#pragma warning restore 649
}
