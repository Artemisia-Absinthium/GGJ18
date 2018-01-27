/*
 * LICENCE
 */
using UnityEngine;

[System.Serializable]
public class Water : MonoBehaviour
{
	[SerializeField]
	private int m_size = 10;

	private Mesh m_mesh = null;

	void Start()
	{
		MeshFilter filter = GetComponent<MeshFilter>();
		Debug.Assert( filter );
		m_mesh = new Mesh();
		Vector3[] vertices = new Vector3[ m_size * m_size * 6 ];
		Vector3[] normals = new Vector3[ m_size * m_size * 6 ];
		Vector3 offset = -new Vector3( m_size / 2.0f, 0.0f, m_size / 2.0f );
		int[] triangles = new int[ m_size * m_size * 6 ];
		for ( int j = 0; j < m_size; ++j )
		{
			for ( int i = 0; i < m_size; ++i )
			{
				int x = i * 6;
				int y = j * 6 * m_size;
				vertices[ x + y ] = new Vector3( i, 0, j ) + offset;
				vertices[ x + y + 1 ] = new Vector3( i, 0, j + 1 ) + offset;
				vertices[ x + y + 2 ] = new Vector3( i + 1, 0, j + 1 ) + offset;
				vertices[ x + y + 3 ] = new Vector3( i, 0, j ) + offset;
				vertices[ x + y + 4 ] = new Vector3( i + 1, 0, j + 1 ) + offset;
				vertices[ x + y + 5 ] = new Vector3( i + 1, 0, j ) + offset;

				normals[ x + y ] = new Vector3( 0, 1, 0 );
				normals[ x + y + 1 ] = new Vector3( 0, 1, 0 );
				normals[ x + y + 2 ] = new Vector3( 0, 1, 0 );
				normals[ x + y + 3 ] = new Vector3( 0, 1, 0 );
				normals[ x + y + 4 ] = new Vector3( 0, 1, 0 );
				normals[ x + y + 5 ] = new Vector3( 0, 1, 0 );

				triangles[ x + y ] = x + y;
				triangles[ x + y + 1 ] = x + y + 1;
				triangles[ x + y + 2 ] = x + y + 2;
				triangles[ x + y + 3 ] = x + y + 3;
				triangles[ x + y + 4 ] = x + y + 4;
				triangles[ x + y + 5 ] = x + y + 5;
			}
		}
		m_mesh.vertices = vertices;
		m_mesh.normals = normals;
		m_mesh.triangles = triangles;
		m_mesh.RecalculateBounds();
		filter.mesh = m_mesh;
	}

	void Update()
	{
		Vector3[] v = m_mesh.vertices;
		Vector3[] n = m_mesh.normals;
		for ( int i = 0; i < v.Length; ++i )
		{
			Vector3 p = transform.TransformPoint( v[ i ] );
			v[ i ].y = Mathf.PerlinNoise( p.x + Time.time * 0.125f, p.z + Time.time * 0.2f ) * 0.4f - 0.2f;
			v[ i ].y += Mathf.PerlinNoise( p.x + Time.time * 0.4f, p.z + Time.time * 0.055f ) * 0.1f - 0.05f;
		}
		for ( int j = 0; j < m_size; ++j )
		{
			for ( int i = 0; i < m_size; ++i )
			{
				int x = i * 6;
				int y = j * 6 * m_size;
				Vector3 d1 = v[ x + y + 1 ] - v[ x + y ];
				Vector3 d2 = v[ x + y + 2 ] - v[ x + y ];
				Vector3 d3 = v[ x + y + 4 ] - v[ x + y + 3 ];
				Vector3 d4 = v[ x + y + 5 ] - v[ x + y + 3 ];
				Vector3 n1 = Vector3.Cross( d1, d2 );
				Vector3 n2 = Vector3.Cross( d3, d4 );
				n[ x + y ] = n1;
				n[ x + y + 1 ] = n1;
				n[ x + y + 2 ] = n1;
				n[ x + y + 3 ] = n2;
				n[ x + y + 4 ] = n2;
				n[ x + y + 5 ] = n2;
			}
		}
		m_mesh.vertices = v;
		m_mesh.normals = n;
		m_mesh.RecalculateBounds();
	}
}
