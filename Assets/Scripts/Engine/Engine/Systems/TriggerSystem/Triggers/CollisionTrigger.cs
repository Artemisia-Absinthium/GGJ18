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
	[AddComponentMenu( "Engine/Systems/Trigger System/Triggers/Collision Trigger" )]
	[RequireComponent( typeof( Collider ) )]
	public class CollisionTrigger : TriggerBase
	{
		#region Fields
		[SerializeField]
		[Tooltip( "Set to false to handle collision enter event or to true to handle collision exit event" )]
		private bool m_onExit = false;
		[SerializeField]
		[TagSelector]
		[Tooltip( "Only trigger behaviour if other collider has this tag" )]
		private string m_triggeringTag = "";
		#endregion

		#region Methods
		public override void OnTrigger()
		{
			if ( m_allowExternalTriggering )
			{
				OnTrigger_internal();
			}
		}

		void OnCollisionEnter( Collision _collision )
		{
			string colliderTag = _collision.collider.tag;
			if ( !m_onExit && ( m_triggeringTag.Length == 0 || colliderTag == m_triggeringTag ) )
			{
				OnTrigger_internal( new CollisionTriggerDataWrapper( _collision ) );
			}
		}

		void OnCollisionExit( Collision _collision )
		{
			string colliderTag = _collision.collider.tag;
			if ( m_onExit && ( m_triggeringTag.Length == 0 || colliderTag == m_triggeringTag ) )
			{
				OnTrigger_internal( new CollisionTriggerDataWrapper( _collision ) );
			}
		}
		#endregion
	}
}
