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
using System.Linq;
using UnityEngine;

namespace Engine
{
	[System.Serializable]
	[AddComponentMenu( "Engine/Utilities/Optimization/Cache" )]
	public abstract class Cache<T> : MonoBehaviour where T : struct, System.IConvertible
	{
#if DEBUGGING
		#region Fields
		[SerializeField]
		[Tooltip( "Show cache in inspector" )]
		private bool m_showCachedObjects = false;
		[SerializeField]
		[Tooltip( "Cache content" )]
		private GameObject[] m_debugObjects = null;
		#endregion
#endif
		#region Members
		private GameObject[] m_cache;
		private static Cache<T> s_instance = null;
		#endregion

		#region Properties
		public static Cache<T> Instance
		{
			get
			{
				if ( s_instance == null )
				{
					s_instance = FindObjectOfType<Cache<T>>();
					Debug.Assert( s_instance );
					s_instance.m_cache = new GameObject[ System.Enum.GetValues( typeof( T ) ).Cast<int>().Max() + 1 ];
				}
				return s_instance;
			}
		}
		#endregion

		#region Methods
		void Awake()
		{
			if ( s_instance != null )
			{
				Destroy( this );
			}
			DontDestroyOnLoad( this );
			s_instance = this;
		}

#if DEBUGGING
		void Update()
		{
			if ( m_showCachedObjects )
			{
				m_showCachedObjects = false;
				m_debugObjects = new GameObject[ m_cache.Length ];
				int i = 0;
				foreach ( GameObject go in m_cache )
				{
					m_debugObjects[ i ] = go;
					++i;
				}
			}
		}
#endif
		public bool PutToCache( GameObject _go, T _name )
		{
			int index = System.Convert.ToInt32( _name );
			if ( m_cache[ index ] != null )
			{
				Debug.LogError( "Cache error - Value already exists: \"" + _name + "\"" );
				return false;
			}
			m_cache[ index ] = _go;
			return true;
		}

		public GameObject GetObject( T _name )
		{
			int index = System.Convert.ToInt32( _name );
			if ( index >= 0 && index < m_cache.Length )
			{
				return m_cache[ index ];
			}
			Debug.LogError( "Cache error - Unable to find value in cache: \"" + _name + "\"" );
			return null;
		}
		#endregion
	}
}
