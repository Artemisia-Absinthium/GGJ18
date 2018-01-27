using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour {

    public enum EGameMusicManagerState
    {
        eMainMenu,
        eBaseVillage,
        eHappyCharacter,
        eSadCharacter
    };

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

    public int m_ActualMusic = 0;
    public int m_NextMusic;

	// Use this for initialization
	void Start () {

        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.playOnAwake = false;

        if(m_AudioSource.clip == null)
        {
            Debug.Log("No music in Music Manager's Audio Source");

            if(m_Musics.Length <= 0)
            {
                Debug.Log("No music configured in the game");
            }else
            {
                m_AudioSource.clip = m_Musics[0];
                m_ActualMusic = 0;
            }
            
        }

        m_AudioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(m_AudioSource.isPlaying == false)
        {
            m_AudioSource.Play();
        }

        if(m_SwitchState == EMusicSwitchState.eFadeOut)
        {
            m_AudioSource.volume -= 0.05f;
            if(m_AudioSource.volume <= 0.0f)
            {
                m_SwitchState = EMusicSwitchState.eFadeIn;
                m_AudioSource.Stop();
                m_AudioSource.clip = m_Musics[m_NextMusic];
                m_AudioSource.Play();
                m_ActualMusic = m_NextMusic;
            }
        }else if(m_SwitchState == EMusicSwitchState.eFadeIn)
        {
            m_AudioSource.volume += 0.05f;
            if (m_AudioSource.volume >= 1.0f)
            {
                m_SwitchState = EMusicSwitchState.eNone;
            }
        }

        if(Input.GetKeyDown(KeyCode.P) && m_SwitchState == EMusicSwitchState.eNone)
        {
            if(m_ActualMusic == 0)
            {
                ChangeMusic(1);
            }else
            {
                ChangeMusic(0);
            }
            
        }
	}

    //
    void ChangeMusic(int n)
    {
        if(m_AudioSource.isPlaying == false)
        {
            m_AudioSource.clip = m_Musics[n];
        }else
        {
            m_NextMusic = n;
            m_SwitchState = EMusicSwitchState.eFadeOut;
        }
    }


}
