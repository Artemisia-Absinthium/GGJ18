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
	[AddComponentMenu( "Engine/Systems/Trigger System/Behaviours/Invoke Behaviour" )]
	public class InvokeBehaviour : BehaviourBase
	{
		#region Fields
		[SerializeField]
		[Tooltip( "The MonoBehaviour script on wich to invoke the method" )]
		private MonoBehaviour m_script = null;
		[SerializeField]
		[Tooltip( "The method o invoke on the script" )]
		private string m_method = "Update";
		[SerializeField]
		[Tooltip( "Delay to call the method" )]
		private float m_invokeDelay = 0.0f;
		#endregion

		#region Methods
		public override void Trigger( TriggerBase _trigger, Object _data )
		{
			if ( m_script && ( m_method != null ) )
			{
				try
				{
					m_script.Invoke( m_method, m_invokeDelay >= 0.0f ? m_invokeDelay : 0.0f );
				}
				catch( System.Exception e)
				{
					Debug.Log( "Invoke failed: " + e );
				}
			}
		}
		#endregion
	}
#pragma warning restore 649
}
