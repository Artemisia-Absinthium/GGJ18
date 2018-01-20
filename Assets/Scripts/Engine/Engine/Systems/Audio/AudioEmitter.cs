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
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
	[System.Serializable]
	[AddComponentMenu( "Engine/Systems/Audio/Audio Emitter" )]
	public class AudioEmitter : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		[Tooltip( "The audio source used for initialization" )]
		private AudioSource m_source = null;
		[SerializeField]
		[Tooltip( "Mute the source" )]
		private bool m_mute = false;
		#endregion

		#region Members
		private GameObject m_sourcePosition;
		private AudioSource m_sourceSource;
		private AudioRoom m_currentRoom;
		private bool m_muteState = false;
		#endregion

		#region Properties
		public AudioSource Source
		{
			get { return m_sourceSource; }
		}

		public bool Mute
		{
			get { return m_mute; }
			set { m_mute = value; }
		}
		#endregion

		#region Methods
		void Start()
		{
			Debug.Assert( m_source );

			m_sourcePosition = new GameObject( "AudioEmitter" );
			m_sourcePosition.transform.parent = transform;
			m_sourcePosition.transform.localPosition = Vector3.zero;
			m_sourcePosition.transform.localRotation = Quaternion.identity;
			m_sourcePosition.transform.localScale = Vector3.zero;
			m_sourceSource = m_sourcePosition.AddComponent<AudioSource>();
			Debug.Assert( m_sourceSource );

			m_sourceSource.bypassEffects = m_source.bypassEffects;
			m_sourceSource.bypassListenerEffects = m_source.bypassListenerEffects;
			m_sourceSource.bypassReverbZones = m_source.bypassReverbZones;
			m_sourceSource.clip = m_source.clip;
			m_sourceSource.dopplerLevel = m_source.dopplerLevel;
			m_sourceSource.enabled = m_source.enabled;
			m_sourceSource.ignoreListenerPause = m_source.ignoreListenerPause;
			m_sourceSource.ignoreListenerVolume = m_source.ignoreListenerVolume;
			m_sourceSource.loop = m_source.loop;
			m_sourceSource.minDistance = m_source.minDistance;
			m_sourceSource.maxDistance = m_source.maxDistance;
			m_sourceSource.mute = m_source.mute;
			m_sourceSource.outputAudioMixerGroup = m_source.outputAudioMixerGroup;
			m_sourceSource.panStereo = m_source.panStereo;
			m_sourceSource.pitch = m_source.pitch;
			m_sourceSource.priority = m_source.priority;
			m_sourceSource.reverbZoneMix = m_source.reverbZoneMix;
			m_sourceSource.rolloffMode = m_source.rolloffMode;
			m_sourceSource.spatialBlend = m_source.spatialBlend;
			m_sourceSource.spatialize = m_source.spatialize;
			m_sourceSource.spatializePostEffects = m_source.spatializePostEffects;
			m_sourceSource.spread = m_source.spread;
			m_sourceSource.time = m_source.time;
			m_sourceSource.timeSamples = m_source.timeSamples;
			m_sourceSource.velocityUpdateMode = m_source.velocityUpdateMode;
			m_sourceSource.volume = m_source.volume;

			AnimationCurve curveCustomRolloff = m_source.GetCustomCurve( AudioSourceCurveType.CustomRolloff );
			if ( curveCustomRolloff.length > 1 )
			{
				m_sourceSource.SetCustomCurve( AudioSourceCurveType.CustomRolloff, curveCustomRolloff );
			}
			AnimationCurve curveReverbZoneMix = m_source.GetCustomCurve( AudioSourceCurveType.ReverbZoneMix );
			if ( curveReverbZoneMix.length > 1 )
			{
				m_sourceSource.SetCustomCurve( AudioSourceCurveType.ReverbZoneMix, curveReverbZoneMix );
			}
			AnimationCurve curveSpatialBlend = m_source.GetCustomCurve( AudioSourceCurveType.SpatialBlend );
			if ( curveSpatialBlend.length > 1 )
			{
				m_sourceSource.SetCustomCurve( AudioSourceCurveType.SpatialBlend, curveSpatialBlend );
			}
			AnimationCurve curveSpread = m_source.GetCustomCurve( AudioSourceCurveType.Spread );
			if ( curveSpread.length > 1 )
			{
				m_sourceSource.SetCustomCurve( AudioSourceCurveType.Spread, curveSpread );
			}

			if ( m_source.isPlaying )
			{
				m_sourceSource.Play();
			}

			Destroy( m_source );

			m_source = m_sourceSource;
			m_mute = m_sourceSource.mute;
		}

		public void ComputeCurrentAudioRoom( AudioGraph _graph )
		{
			m_currentRoom = null;
			List<AudioRoom> rooms = _graph.GetAudioRooms();
			for ( int iRoom = 0; iRoom < rooms.Count; ++iRoom )
			{
				if ( rooms[ iRoom ].Contains( transform.position ) )
				{
					m_currentRoom = rooms[ iRoom ];
					return;
				}
			}
		}

		public void UpdateSource( float _distance, Vector3 _listenerPosition, Vector3 _position, float performances )
		{
			if ( _distance == Mathf.Infinity )
			{
				m_sourceSource.mute = true;
				m_muteState = true;
			}
			else
			{
				Vector3 newPosition = _listenerPosition + ( _position - _listenerPosition ).normalized * _distance;
				if ( m_muteState )
				{
					m_sourcePosition.transform.position = newPosition;
				}
				else
				{
					m_sourcePosition.transform.position = Vector3.Lerp( m_sourcePosition.transform.position, newPosition, performances );
				}
				m_muteState = false;
				m_sourceSource.mute = m_mute;
			}
		}

		public AudioRoom GetCurrentAudioRoom()
		{
			return m_currentRoom;
		}
		#endregion
	}
}
