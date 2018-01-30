/*
 * LICENCE
 */
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class CutSceneSnapshot
	{
		public class SpriteData
		{
			public Sprite Sprite;
			public string Name;
			public bool Speaker;
			public float Size;
		}
		private CutScene m_cutscene = null;
		private SpriteData m_left = null;
		private SpriteData m_right = null;
		private SpriteData m_center = null;
		private int m_speaker = -1;
		private Strings m_text = 0;
		private Strings[] m_choices = null;
		private bool m_outOk = false;
		private float m_outTime = -1.0f;
		private int m_okTarget = -1;
		private int m_timeTarget = -1;
		private int[] m_choiceTargets = null;
		private bool m_retargeted = false;

		public CutScene CutScene { get { return m_cutscene; } }
		public SpriteData LeftPicture { get { return m_left; } }
		public SpriteData RightPicture { get { return m_right; } }
		public SpriteData CenterPicture { get { return m_center; } }
		public int Speaker { get { return m_speaker; } }
		public string Text { get { return GameLocalizedStringManager.Instance.Get( m_text ); } }
		public bool IsChoice { get { return m_choices != null; } }
		public int ChoiceCount { get { return ( m_choices == null ) ? 0 : m_choices.Length; } }
		public string Choice( int _index )
		{
			if ( ( m_choices != null ) && ( _index >= 0 ) && ( _index < m_choices.Length ) )
			{
				return GameLocalizedStringManager.Instance.Get( m_choices[ _index ] );
			}
			return null;
		}
		public bool IsOkForNext { get { return m_outOk; } }
		public float OutTime { get { return m_outTime; } }
		public int OkTarget { get { return m_okTarget; } }
		public int TimeTarget { get { return m_timeTarget; } }
		public int ChoiceTarget( int _index )
		{
			if ( ( m_choiceTargets != null ) && ( _index >= 0 ) && ( _index < m_choiceTargets.Length ) )
			{
				return m_choiceTargets[ _index ];
			}
			return -1;
		}

		public CutSceneSnapshot( SpriteData[] _pictures, int _speaker, Strings _text, Strings[] _choices, bool _ok, float _time, int _okTarget, int _timeTarget, int[] _choiceTargets )
		{
			if ( _pictures != null )
			{
				if ( _pictures.Length > 0 )
				{
					m_left = _pictures[ 0 ];
					if ( _pictures.Length > 1 )
					{
						m_right = _pictures[ 1 ];
						if ( _pictures.Length > 2 )
						{
							m_center = _pictures[ 2 ];
						}
					}
				}
			}
			m_speaker = _speaker;
			m_text = _text;
			if ( _choices != null )
			{
				m_choices = new Strings[ _choices.Length ];
				System.Array.Copy( _choices, m_choices, _choices.Length );
			}
			m_choices = _choices;
			m_outOk = _ok;
			m_outTime = _time;
			m_okTarget = _okTarget;
			m_timeTarget = _timeTarget;
			if ( _choiceTargets != null )
			{
				m_choiceTargets = new int[ _choiceTargets.Length ];
				System.Array.Copy( _choiceTargets, m_choiceTargets, _choiceTargets.Length );
			}
		}

		public void Retarget( Dictionary<int, int> _retarget )
		{
			if ( m_retargeted )
			{
				return;
			}
			if ( m_okTarget >= 0 )
			{
				int retargeted = 0;
				if ( _retarget.TryGetValue( m_okTarget, out retargeted ) )
				{
					m_okTarget = retargeted;
				}
			}
			if ( m_timeTarget >= 0 )
			{
				int retargeted = 0;
				if ( _retarget.TryGetValue( m_timeTarget, out retargeted ) )
				{
					m_timeTarget = retargeted;
				}
			}
			if ( m_choiceTargets != null )
			{
				for ( int i = 0; i < m_choiceTargets.Length; ++i )
				{
					int retargeted = 0;
					if ( _retarget.TryGetValue( m_choiceTargets[ i ], out retargeted ) )
					{
						m_choiceTargets[ i ] = retargeted;
					}
				}
			}
			m_retargeted = true;
		}

		public void SetCutScene( CutScene _cutScene )
		{
			if ( ( m_cutscene == null ) && ( _cutScene != null ) )
			{
				m_cutscene = _cutScene;
			}
		}
	}
}
