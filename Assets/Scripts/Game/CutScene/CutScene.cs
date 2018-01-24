/*
 * LICENCE
 */
using UnityEngine;

namespace Game
{
	public class CutScene
	{
		private int m_input;
		private string m_name;
		private CutSceneSnapshot[] m_snapshots;

		public string Name { get { return m_name; } }
		public int Input { get { return m_input; } }
		public CutSceneSnapshot this[ int _index ]
		{
			get { return m_snapshots[ _index ]; }
		}

		public CutScene( string _name, CutSceneSnapshot[] _snapshots, int _input )
		{
			m_input = _input;
			m_name = _name;
			m_snapshots = new CutSceneSnapshot[ _snapshots.Length ];
			System.Array.Copy( _snapshots, m_snapshots, _snapshots.Length );
		}
	}
}
