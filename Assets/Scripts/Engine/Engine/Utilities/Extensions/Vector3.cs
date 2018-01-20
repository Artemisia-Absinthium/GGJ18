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
namespace Engine
{
	namespace Extensions
	{
		namespace Vector3
		{
			[System.Serializable]
			public static class Vector3Extension
			{
				public static UnityEngine.Vector2 XY( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector2( _in.x, _in.y );
				}
				public static UnityEngine.Vector2 XZ( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector2( _in.x, _in.z );
				}
				public static UnityEngine.Vector2 YZ( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector2( _in.z, _in.z );
				}
				public static UnityEngine.Vector2 YX( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector2( _in.y, _in.x );
				}
				public static UnityEngine.Vector2 ZX( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector2( _in.z, _in.x );
				}
				public static UnityEngine.Vector2 ZY( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector2( _in.z, _in.y );
				}

				public static UnityEngine.Vector3 MinusX( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector3( 0.0f, _in.y, _in.z );
				}
				public static UnityEngine.Vector3 MinusY( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector3( _in.x, 0.0f, _in.z );
				}
				public static UnityEngine.Vector3 MinusZ( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector3( _in.x, _in.y, 0.0f );
				}

				public static UnityEngine.Vector3 OnlyX( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector3( _in.x, 0.0f, 0.0f );
				}
				public static UnityEngine.Vector3 OnlyY( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector3( 0.0f, _in.y, 0.0f );
				}
				public static UnityEngine.Vector3 OnlyZ( this UnityEngine.Vector3 _in )
				{
					return new UnityEngine.Vector3( 0.0f, 0.0f, _in.z );
				}
			}
		}
	}
}
