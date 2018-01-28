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
	[AddComponentMenu( "Engine/Utilities/Optimization/Cache Object" )]
	public abstract class CacheObject<T> : MonoBehaviour where T : struct, System.IConvertible
	{
		#region Fields
		[SerializeField]
		[Tooltip( "The name of the object in cache" )]
		private T m_cacheValue;
		[SerializeField]
		[Tooltip( "Does this object resides in cache" )]
		private bool m_putInCache = true;
		[SerializeField]
		[Tooltip( "Is this object disabled ater being pushed in cache" )]
		private bool m_disableAfterAwake = false;
		#endregion

		#region Methods
		void Awake() // Set me after Cache in script execution order
		{
			if ( m_putInCache )
			{
				Cache<T>.Instance.PutToCache( gameObject, m_cacheValue );
			}
			if ( m_disableAfterAwake )
			{
				gameObject.SetActive( false );
			}
			Destroy( this );
		}
		#endregion
	}
#pragma warning restore 649
}
