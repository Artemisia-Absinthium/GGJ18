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
	[AddComponentMenu( "Engine/Systems/Trigger System/Behaviours/Delayed Behaviour" )]
	public class DelayedBehaviour : BehaviourBase
	{
		#region Fields
		[SerializeField]
		[Tooltip( "The time between the triggering of this behaviour and the triggering of the target behaviour, in seconds" )]
		private float m_delay = 0.0f;
		[SerializeField]
		[Tooltip( "The behaviour to trig when delay is over" )]
		private BehaviourBase m_target = null;
		#endregion

		#region Members
		private Object m_data = null;
		private TriggerBase m_trigger = null;
		#endregion

		#region Methods
		public override void Trigger( TriggerBase _trigger, Object _data )
		{
			m_trigger = _trigger;
			m_data = _data;
			Invoke( "Delayed", m_delay >= 0.0f ? m_delay : 0.0f );
		}
		private void Delayed()
		{
			if ( m_target != null )
			{
				m_target.Trigger( m_trigger, m_data );
			}
		}
		#endregion
	}
}
