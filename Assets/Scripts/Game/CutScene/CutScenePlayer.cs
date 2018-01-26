/*
 * LICENCE
 */
using UnityEngine;
using System.Linq;

namespace Game
{
	public class CutScenePlayer : MonoBehaviour
	{
		[SerializeField]
		private MonoBehaviour m_adapterObject = null;

		public const int kMaxArgumentCount = 20;

		private int m_instanceCounter = -1;
		private int m_currentInstance = -1;
		private int m_lastInstance = -1;
		private CutSceneParser m_parser = null;
		private ICutSceneAdapterBase m_adapter = null;

		private bool m_isPlaying = false;
		private float m_timer = 0.0f;
		private CutScene m_currentCutScene = null;
		private int m_currentSnapshot = -1;
		private int m_nextSnapshot = -1;
		private bool m_start = false;

		private ICutSceneSupervisorBase m_currentSupervisor = null;

		private object[] m_arguments = new object[ kMaxArgumentCount + 1 ];

		private static CutScenePlayer s_instance = null;

		public static CutScenePlayer Instance
		{
			get { return s_instance; }
		}

		void Awake()
		{
			if ( s_instance != null )
			{
				Destroy( this );
				return;
			}
			DontDestroyOnLoad( this );
			s_instance = this;

			m_parser = new CutSceneParser();

			Debug.Assert( m_adapterObject != null );
			Debug.Assert( m_adapterObject.GetType().GetInterfaces().Contains( typeof( ICutSceneAdapterBase ) ) );
			m_adapter = ( ICutSceneAdapterBase )m_adapterObject;

			// Todo: read sdb and fill m_parser
			TextAsset sprites = Resources.Load<TextAsset>( "CutScene/SpriteDataBase" );
			Debug.Assert( sprites );

			string[] lines = sprites.text.Split( '\n' );

			foreach( string line in lines )
			{
				if ( line.Trim().Length == 0 )
				{
					continue;
				}
				string[] args = line.Trim().Split('|');
				Debug.Assert( args.Length == 2 );
				int rsFolder = args[ 1 ].IndexOf( "/Resources/" );
				string path = args[ 1 ].Substring( rsFolder >= 0 ? rsFolder + 11 : 0 );
				int ext = path.LastIndexOf( "." );
				path = path.Substring( 0, ext );

				m_parser.AddPicture( args[ 0 ], path );
			}

		}
		private void Update()
		{
			m_parser.Update();
			if ( m_isPlaying )
			{
				if ( m_nextSnapshot != -1 )
				{
					int old = m_currentSnapshot;
					m_currentSnapshot = m_nextSnapshot;
					m_nextSnapshot = -1;

					m_adapter.StartSnapshot( m_currentSnapshot );
					SetSnapshot( old, m_currentSnapshot );
				}
				else if ( m_start )
				{
					m_start = false;
					m_adapter.StartSnapshot( m_currentSnapshot );
					SetSnapshot( -1, m_currentSnapshot );
				}
				else
				{
					int transition = m_currentSnapshot;
					CutSceneSnapshot snap = m_currentCutScene[ m_currentSnapshot ];
					m_timer += Time.deltaTime;
					int choice;
					int overrideTransition = -1;
					bool doOverride = false;
					if ( ( snap.OutTime >= 0.0f ) && ( m_timer >= snap.OutTime ) )
					{
						if ( m_currentSupervisor != null )
						{
							m_adapter.EndSnapshot();
							transition = snap.TimeTarget;
							doOverride = m_currentSupervisor.OnCutSceneTransitionTime( m_currentCutScene.Name, m_currentSnapshot, transition, ref overrideTransition );
							m_timer = 0.0f;
						}
					}
					else if ( ( snap.IsOkForNext && m_adapter.ReceiveOk() ) )
					{
						if ( m_currentSupervisor != null )
						{
							m_adapter.EndSnapshot();
							transition = snap.OkTarget;
							doOverride = m_currentSupervisor.OnCutSceneTransitionOk( m_currentCutScene.Name, m_currentSnapshot, transition, ref overrideTransition );
							m_timer = 0.0f;
						}
					}
					else if ( snap.IsChoice && m_adapter.ReceiveChoice( out choice ) )
					{
						if ( m_currentSupervisor != null )
						{
							m_adapter.EndSnapshot();
							transition = snap.ChoiceTarget( choice );
							doOverride = m_currentSupervisor.OnCutSceneTransitionChoice( m_currentCutScene.Name, m_currentSnapshot, transition, choice, ref overrideTransition );
							m_timer = 0.0f;
						}
					}
					if ( transition != m_currentSnapshot )
					{
						if ( doOverride )
						{
							transition = overrideTransition;
						}
						if ( transition == -1 )
						{
							m_adapter.EndCutScene();
							m_lastInstance = m_currentInstance;
							m_currentInstance = -1;
							m_isPlaying = false;
							m_currentSupervisor = null;
						}
						else
						{
							m_nextSnapshot = transition;
						}
					}
				}
			}
		}

		public bool IsPlaying()
		{
			return m_isPlaying;
		}
		public bool IsPlaying( int _instance )
		{
			if ( _instance < 0 )
			{
				return false;
			}
			return m_isPlaying && ( m_currentInstance == _instance );
		}
		public bool HasFinished( int _instance )
		{
			return ( _instance >= 0 ) && ( _instance <= m_lastInstance );
		}
		public int Play( string _cutSceneName, ICutSceneSupervisorBase _supervisor = null )
		{
			if ( m_isPlaying )
			{
				Debug.Log( "Error: CutScenePlayer is already playing" );
				return -1;
			}

			TextAsset cutFile = Resources.Load<TextAsset>( "CutScene/" + _cutSceneName );
			if ( cutFile == null )
			{
				Debug.Log( "Error: Unable to find CutScene: " + _cutSceneName );
				return -1;
			}

			m_currentCutScene = m_parser.Parse( cutFile.text );
			if ( m_currentCutScene == null )
			{
				Debug.Log( "Error: When parsing CutScene file" );
				return -1;
			}
			m_currentSnapshot = -1;

			m_currentSupervisor = _supervisor;

			int transition = -1;
			bool overrideTransition = false;
			if ( m_currentSupervisor != null )
			{
				overrideTransition = m_currentSupervisor.OnCutSceneTransitionOk( m_currentCutScene.Name, -1, m_currentCutScene.Input, ref transition );
			}
			if ( overrideTransition )
			{
				m_currentSnapshot = transition;
			}
			else
			{
				m_currentSnapshot = m_currentCutScene.Input;
			}
			m_start = true;
			m_adapter.StartCutScene( m_currentCutScene.Name );

			m_arguments = new object[ kMaxArgumentCount + 1 ];
			m_arguments[ 0 ] = '\n';

			m_isPlaying = true;
			++m_instanceCounter;
			m_currentInstance = m_instanceCounter;
			return m_instanceCounter;
		}
		public bool SetArgument( int _instance, int _argument, object _value )
		{
			if ( _instance != m_currentInstance )
			{
				return false;
			}
			if ( _argument <= 0 || _argument > kMaxArgumentCount ) // 0 is reserved for \n
			{
				return false;
			}
			m_arguments[ _argument ] = _value;
			return true;
		}

		private void SetSnapshot( int _previous, int _new )
		{
			CutSceneSnapshot snap = m_currentCutScene[ _new ];

			string format = string.Format( snap.Text, m_arguments );

			m_adapter.SetText( format, snap.IsChoice );
			if ( _previous == -1 )
			{
				m_adapter.SetSprite( snap.LeftPicture, 0 );
				m_adapter.SetSprite( snap.RightPicture, 1 );
				m_adapter.SetSprite( snap.CenterPicture, 2 );
			}
			else
			{
				if ( snap.LeftPicture != m_currentCutScene[ _previous ].LeftPicture )
				{
					m_adapter.SetSprite( snap.LeftPicture, 0 );
				}
				if ( snap.RightPicture != m_currentCutScene[ _previous ].RightPicture )
				{
					m_adapter.SetSprite( snap.RightPicture, 1 );
				}
				if ( snap.CenterPicture != m_currentCutScene[ _previous ].CenterPicture )
				{
					m_adapter.SetSprite( snap.CenterPicture, 2 );
				}
			}

			if ( snap.IsChoice )
			{
				if ( snap.ChoiceCount == 0 )
				{
					Debug.Log( "Error: No choices for a choice snapshot" );
				}
				string[] choices = new string[ snap.ChoiceCount ];
				for( int i = 0; i < snap.ChoiceCount; ++i )
				{
					choices[ i ] = string.Format( snap.Choice( i ), m_arguments );
				}
				m_adapter.SetChoices( choices );
			}
			else
			{
				m_adapter.SetChoices( null );
			}
			m_timer = 0.0f;
		}
	}
}
