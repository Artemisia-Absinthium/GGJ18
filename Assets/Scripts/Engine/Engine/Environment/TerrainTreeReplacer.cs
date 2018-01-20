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
	[RequireComponent( typeof( Terrain ) )]
	[AddComponentMenu( "Engine/Environment/Terrain Tree Replacer" )]
	public class TerrainTreeReplacer : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		[Tooltip("An array containing prefabs of trees that replace those on the terrain")]
		private GameObject[] m_replacers = null;
		[SerializeField]
		[Tooltip("The number of chunks to create on the terrain or set 0 for no chunks")]
		private IntPair m_chunkCount = null;
		#endregion

		#region Members
		private Terrain m_terrain = null;
		#endregion

		#region Methods
		void Start()
		{
			m_terrain = GetComponent<Terrain>();
			TerrainData data = m_terrain.terrainData;
			if ( m_replacers.Length != data.treePrototypes.Length )
			{
				Debug.Log( "Number of replacement trees should match tree prototype count" );
				Destroy( this );
				return;
			}

			bool useChunk = m_chunkCount.m_x > 0 && m_chunkCount.m_y > 0;

			GameObject[] roots = null;
			GameObject root = null;

			Vector3 terrainPosition = m_terrain.transform.position;

			if ( useChunk )
			{
				float deltaX = 1.0f / m_chunkCount.m_x;
				float deltaY = 1.0f / m_chunkCount.m_y;
				roots = new GameObject[ m_chunkCount.m_x * m_chunkCount.m_y ];
				for ( int iChunkY = 0; iChunkY < m_chunkCount.m_y; ++iChunkY )
				{
					for ( int iChunkX = 0; iChunkX < m_chunkCount.m_x; ++iChunkX )
					{
						GameObject chunk = new GameObject( "BatchRoot_" + iChunkX + "_" + iChunkY );
						Vector3 position = new Vector3(
							deltaX * 0.5f + iChunkX * deltaX,
							0.0f,
							deltaY * 0.5f + iChunkY * deltaY );

						position.x = position.x * data.size.x + terrainPosition.x;
						position.y = position.y * data.size.y + terrainPosition.y;
						position.z = position.z * data.size.z + terrainPosition.z;

						chunk.transform.position = position;
						chunk.transform.parent = transform;
						roots[ iChunkY * m_chunkCount.m_x + iChunkX ] = chunk;
					}
				}
			}
			else
			{
				root = new GameObject( "BatchRoot" );
				root.transform.parent = transform;
			}

			for ( int iTree = 0; iTree < data.treeInstanceCount; ++iTree )
			{
				GameObject replacer = m_replacers[ data.treeInstances[ iTree ].prototypeIndex ];
				if ( replacer != null )
				{
					TreeInstance treeInstance = data.treeInstances[ iTree ];

					Vector3 position = treeInstance.position;

					if ( useChunk )
					{
						root = roots[ GetChunkIndex( position.x, position.z ) ];
					}

					position.x = position.x * data.size.x + terrainPosition.x;
					position.y = position.y * data.size.y + terrainPosition.y;
					position.z = position.z * data.size.z + terrainPosition.z;

					GameObject newTreeInstance = Instantiate(
						replacer,
						position,
						Quaternion.Euler( 0.0f, treeInstance.rotation * Mathf.Rad2Deg, 0.0f ),
						root.transform );
					newTreeInstance.transform.localScale = new Vector3(
						treeInstance.widthScale,
						treeInstance.heightScale,
						treeInstance.widthScale );
				}
			}
			m_terrain.treeDistance = 0.0f;

			if ( useChunk )
			{
				for ( int iChunkY = 0; iChunkY < m_chunkCount.m_y; ++iChunkY )
				{
					for ( int iChunkX = 0; iChunkX < m_chunkCount.m_x; ++iChunkX )
					{
						GameObject chunk = roots[ iChunkY * m_chunkCount.m_x + iChunkX ];
						if ( chunk.transform.childCount > 0 )
						{
							StaticBatchingUtility.Combine( chunk );
						}
						else
						{
							Destroy( chunk );
						}
					}
				}
			}
			else
			{
				StaticBatchingUtility.Combine( root );
			}
			Destroy( this );
		}
		private int GetChunkIndex( float _x, float _y )
		{
			int res = ( int )( _y * m_chunkCount.m_y ) * m_chunkCount.m_x + ( int )( _x * m_chunkCount.m_x );
			if ( res < 0 )
			{
				return 0;
			}
			int maxRes = m_chunkCount.m_x * m_chunkCount.m_y;
			if ( res >= maxRes )
			{
				return maxRes - 1;
			}
			return res;
		}
		#endregion
	}
}
