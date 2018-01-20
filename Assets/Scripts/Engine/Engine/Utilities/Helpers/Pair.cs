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
using System.Runtime.InteropServices;
using UnityEngine;

namespace Engine
{
	[System.Serializable]
	[ComVisible( true )]
	public class Pair<T, U>
	{
		#region Fields
		[SerializeField]
		[Tooltip( "First element of the pair" )]
		public T m_x;
		[SerializeField]
		[Tooltip( "Second element of the pair" )]
		public U m_y;
		#endregion

		#region Methods
		public Pair()
		{

		}
		public Pair( T _x, U _y )
		{
			m_x = _x;
			m_y = _y;
		}
		#endregion
	}

	[System.Serializable]
	[ComVisible( false )]
	public class BoolPair : Pair<bool, bool>
	{
		public BoolPair() : base() { }
		public BoolPair( bool _x, bool _y ) : base( _x, _y ) { }
	};
	[System.Serializable]
	[ComVisible( false )]
	public class IntPair : Pair<int, int>
	{
		public IntPair() : base() { }
		public IntPair( int _x, int _y ) : base( _x, _y ) { }
	};
	[System.Serializable]
	[ComVisible( false )]
	public class FloatPair : Pair<float, float>
	{
		public FloatPair() : base() { }
		public FloatPair( float _x, float _y ) : base( _x, _y ) { }
	};
	[System.Serializable]
	[ComVisible( false )]
	public class DoublePair : Pair<double, double>
	{
		public DoublePair() : base() { }
		public DoublePair( double _x, double _y ) : base( _x, _y ) { }
	};
	[System.Serializable]
	[ComVisible( false )]
	public class TransformPair : Pair<Transform, Transform>
	{
		public TransformPair() : base() { }
		public TransformPair( Transform _x, Transform _y ) : base( _x, _y ) { }
	};
	[System.Serializable]
	[ComVisible( false )]
	public class Vector3Pair : Pair<Vector3, Vector3>
	{
		public Vector3Pair() : base() { }
		public Vector3Pair( Vector3 _x, Vector3 _y ) : base( _x, _y ) { }
	};
}
