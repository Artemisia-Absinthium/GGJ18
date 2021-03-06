/*
 * LICENCE
 */
using UnityEngine;

public class GameMusicManager : MonoBehaviour
{

	private static GameMusicManager s_instance = null;
	public float m_SwitchSpeed = 0.01f;

	public enum EGameMusicManagerState
	{
		eNone = -1,
		eBaseVillage = 0,
		eLara = 1,
		eKozbee = 2,
		eLapin = 3,
		eFaucheuse = 4
	};

	public static GameMusicManager Instance
	{
		get { return s_instance; }
	}

	public EGameMusicManagerState m_StartMusicState = EGameMusicManagerState.eBaseVillage;

	public AudioClip[] m_Musics;

	public AudioSource m_AudioSource;

	public enum EMusicSwitchState
	{
		eFadeOut,
		eFadeIn,
		eNone
	};

	public EMusicSwitchState m_SwitchState = EMusicSwitchState.eNone;

	public EGameMusicManagerState m_ActualMusic = 0;
	public EGameMusicManagerState m_NextMusic = 0;

	// Use this for initialization
	void Start()
	{

		s_instance = this;

		m_AudioSource = GetComponent<AudioSource>();
		m_AudioSource.playOnAwake = false;

		if ( m_AudioSource.clip == null )
		{
			Debug.Log( "No music in Music Manager's Audio Source" );

			m_AudioSource.clip = m_Musics[ ( int )m_NextMusic ];
			m_ActualMusic = 0;

		}

		m_AudioSource.Play();
	}

	// Update is called once per frame
	void Update()
	{

		if ( m_ActualMusic == EGameMusicManagerState.eNone )
		{
			m_AudioSource.Pause();
		}

		if ( m_AudioSource.isPlaying == false && m_ActualMusic != EGameMusicManagerState.eNone )
		{
			m_AudioSource.Play();
		}

		if ( m_SwitchState == EMusicSwitchState.eFadeOut )
		{
			m_AudioSource.volume -= m_SwitchSpeed;
			if ( m_AudioSource.volume <= 0.0f )
			{
				m_SwitchState = EMusicSwitchState.eFadeIn;
				m_AudioSource.Pause();
				m_AudioSource.clip = m_Musics[ ( int )m_NextMusic ];
				m_AudioSource.Play();
				m_ActualMusic = m_NextMusic;
			}
		}
		else if ( m_SwitchState == EMusicSwitchState.eFadeIn )
		{
			m_AudioSource.volume += m_SwitchSpeed;
			if ( m_AudioSource.volume >= 1.0f )
			{
				m_SwitchState = EMusicSwitchState.eNone;
			}
		}

		if ( Input.GetKeyDown( KeyCode.P ) && m_SwitchState == EMusicSwitchState.eNone )
		{
			if ( m_ActualMusic == EGameMusicManagerState.eBaseVillage )
			{
				ChangeMusic( EGameMusicManagerState.eFaucheuse );
			}
			else
			{
				ChangeMusic( EGameMusicManagerState.eBaseVillage );
			}

		}
	}

	//
	public void ChangeMusic( EGameMusicManagerState music )
	{
		if ( m_AudioSource.isPlaying == false )
		{
			m_AudioSource.clip = m_Musics[ ( int )music ];
		}
		else
		{
			m_NextMusic = music;
			m_SwitchState = EMusicSwitchState.eFadeOut;
		}
	}


}