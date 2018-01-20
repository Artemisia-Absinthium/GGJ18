﻿/*
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

namespace Engine
{
	[System.Serializable]
	public class ObjectPoolT<T> where T : new()
	{
		#region Members
		private Stack<T> m_elements;
		private System.Reflection.ConstructorInfo m_constructor;
		#endregion

		#region Constructors
		public ObjectPoolT()
		{
			m_elements = new Stack<T>();
			m_constructor = typeof( T ).GetConstructor( System.Type.EmptyTypes );
		}

		public ObjectPoolT( int _initialSize )
		{
			m_elements = new Stack<T>( _initialSize );
			m_constructor = typeof( T ).GetConstructor( System.Type.EmptyTypes );
		}
		#endregion

		#region Methods
		public T Unpool()
		{
			if ( m_elements.Count == 0 )
			{
				return new T();
			}
			T back = m_elements.Pop();
			m_constructor.Invoke( back, null );
			return back;
		}

		public T UnpoolAsIs()
		{
			if ( m_elements.Count == 0 )
			{
				return new T();
			}
			return m_elements.Pop();
		}

		public void Pool( ref T _element )
		{
			m_elements.Push( _element );
			_element = default( T );
		}
		#endregion
	}
}
