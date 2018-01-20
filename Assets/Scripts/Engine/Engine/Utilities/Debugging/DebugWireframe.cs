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

#pragma warning disable 414

namespace Engine
{
	[System.Serializable]
	public class DebugWireframe : MonoBehaviour
	{
		#region Enums
		public enum DebugWireframeBehaviour
		{
			KEEP_RENDERER_UNCHANGED,
			DISABLE_RENDERER_ON_START,
			DESTROY_RENDERER_ON_START,
		}
		#endregion

		#region Properties
		[SerializeField]
		[Tooltip( "What to do with renderer on start" )]
		private DebugWireframeBehaviour m_behaviourOnStart = DebugWireframeBehaviour.DESTROY_RENDERER_ON_START;
		#endregion
		#region Methods
#if UNITY_EDITOR
		void OnPreRender()
		{
			GL.wireframe = true;
		}
		void OnPostRender()
		{
			GL.wireframe = false;
		}
#else
		void Start()
		{
			switch ( m_behaviourOnStart )
			{
			case DebugWireframeBehaviour.DISABLE_RENDERER_ON_START:
				{
					Renderer rendererComponent = GetComponent<Renderer>();
					if ( rendererComponent != null )
					{
						rendererComponent.enabled = false;
					}
				}
				break;
			case DebugWireframeBehaviour.DESTROY_RENDERER_ON_START:
				{
					Renderer rendererComponent = GetComponent<Renderer>();
					if ( rendererComponent != null )
					{
						Destroy( rendererComponent );
					}
				}
				break;
			}
		}
#endif
		#endregion
	}
}
