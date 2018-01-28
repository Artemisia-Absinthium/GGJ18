using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour {

    public AudioClip[] m_ThunderSounds;

    public Light m_Light;

    public int m_LightingTime = 0;

	// Use this for initialization
	void Start () {
        m_Light.intensity = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Random.Range(0,500) == 5 && m_LightingTime <= 0)
        {
            m_LightingTime = Random.Range(40,200);
            int thunSound = Random.Range(0, m_ThunderSounds.Length);
            GetComponent<AudioSource>().clip = m_ThunderSounds[thunSound];
            GetComponent<AudioSource>().Play();
        }

        if(m_LightingTime > 0)
        {
            m_Light.intensity = 1.0f;
            m_LightingTime--;
        }

        if(m_LightingTime == 0)
        {
            m_Light.intensity = 0.0f;
        }

	}
}
