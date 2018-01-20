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
	[AddComponentMenu("Engine/Tests/Test Pool")]
	public class TestPool : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		private int numObject = 20000;
		#endregion

		#region Members
		private bool firstFrame = true;
		ObjectPoolT<TestFatObject> m_pool;
		#endregion

		#region Methods
		void Start()
		{
			m_pool = new ObjectPoolT<TestFatObject>( numObject );
		}
		
		void Update()
		{
			TestFatObject[] test = new TestFatObject[ numObject ];
			for ( int i = 0; i < numObject; ++i )
			{
				test[ i ] = m_pool.Unpool();
				if ( firstFrame )
				{
					firstFrame = false;
				}
				else
				{
					if ( test[ i ].a != 0.0 )
					{
						Debug.Log( "Test failed : " + test[ i ].a );
					}
				}
			}
			for ( int i = 0; i < numObject; ++i )
			{
				test[ i ].a = Time.time;
				m_pool.Pool( ref test[ i ] );
			}
		}
		#endregion
	}
}
