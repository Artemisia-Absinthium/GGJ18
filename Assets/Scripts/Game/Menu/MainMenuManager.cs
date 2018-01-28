/*
 * LICENCE
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game
{
	[System.Serializable]
	public class MainMenuManager : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		private Toggle f_english;
		[SerializeField]
		private Toggle f_french;
		[SerializeField]
		private Slider f_resolution;
		[SerializeField]
		private Toggle f_fullscreen;
		[SerializeField]
		private Slider f_anisotropicFiltering;
		[SerializeField]
		private Slider f_antiAliasing;
		[SerializeField]
		private Slider f_levelOfDetails;
		[SerializeField]
		private Slider f_textureQuality;
		[SerializeField]
		private Slider f_shadersQuality;
		[SerializeField]
		private Slider f_physicParticlesQuality;
		[SerializeField]
		private Toggle f_realtimeReflections;
		[SerializeField]
		private Slider f_shadowDistance;
		[SerializeField]
		private Slider f_shadowResolution;
		[SerializeField]
		private Slider f_shadowQuality;
		[SerializeField]
		private Slider f_particlesQuality;
		[SerializeField]
		private Toggle f_vsync;
		[SerializeField]
		private Slider f_generalVolume;
		[SerializeField]
		private Slider f_musicVolume;
		[SerializeField]
		private Slider f_effectsVolume;
		[SerializeField]
		private Slider f_voicesVolume;
		[SerializeField]
		private Toggle f_mute;
		[SerializeField]
		private Slider f_mouseX;
		[SerializeField]
		private Slider f_mouseY;
		[SerializeField]
		private Slider f_joystickX;
		[SerializeField]
		private Slider f_joystickY;
		[SerializeField]
		private Toggle f_invertX;
		[SerializeField]
		private Toggle f_invertY;

		[SerializeField]
		private TMPro.TextMeshProUGUI t_resolution;
		[SerializeField]
		private TextSwitcherGroup t_anisotropicFiltering;
		[SerializeField]
		private TextSwitcherGroup t_antiAliasing;
		[SerializeField]
		private TextSwitcherGroup t_levelOfDetails;
		[SerializeField]
		private TextSwitcherGroup t_textureQuality;
		[SerializeField]
		private TextSwitcherGroup t_shadersQuality;
		[SerializeField]
		private TextSwitcherGroup t_physicParticlesQuality;
		[SerializeField]
		private TextSwitcherGroup t_shadowDistance;
		[SerializeField]
		private TextSwitcherGroup t_shadowResolution;
		[SerializeField]
		private TextSwitcherGroup t_shadowQuality;
		[SerializeField]
		private TextSwitcherGroup t_particlesQuality;
		[SerializeField]
		private TMPro.TextMeshProUGUI t_generalVolume;
		[SerializeField]
		private TMPro.TextMeshProUGUI t_musicVolume;
		[SerializeField]
		private TMPro.TextMeshProUGUI t_effectsVolume;
		[SerializeField]
		private TMPro.TextMeshProUGUI t_voicesVolume;
		#endregion

		// General
		private Engine.LocalizedString.Lang m_tempLanguage;

		// Graphics
		private Resolution m_tempResolution;
		private bool m_tempFS;
		private AnisotropicFiltering m_tempAF;
		private Engine.AntiAliasingValue m_tempAA;
		private Engine.LevelOfDetailsValue m_tempLOD;
		private Engine.TextureQualityValue m_tempTQ;
		private Engine.ShadersQualityValue m_tempSQ;
		private Engine.PhysicParticleValue m_tempPPQ;
		private bool m_tempRR;
		private Engine.ShadowsDistanceValue m_tempDist;
		private ShadowResolution m_tempRes;
		private ShadowQuality m_tempQual;
		private Engine.ParticlesQualityValue m_tempPQ;
		private Engine.VSyncValue m_tempVS;

		// Sound
		private float m_tempGeneralVolume;
		private float m_tempMusicVolume;
		private float m_tempEffectsVolume;
		private float m_tempVoicesVolume;
		private bool m_tempMute;

		// Control
		private float m_tempSX;
		private float m_tempSY;
		private float m_tempJX;
		private float m_tempJY;
		private bool m_tempIX;
		private bool m_tempIY;

		private TMPro.TextMeshProUGUI m_graveyardText = null;
		private Slider m_graveyardSlider;
		private Toggle m_graveyardToggle;
		private TextSwitcherGroup m_graveyardTextSwitcherGroup;

		public void ResetValues()
		{
			// General
			ApplyGeneral();

			Engine.UserGraphicOptions opt = Engine.UserManager.Instance.Current.Options.Graphic;
			Screen.SetResolution( m_tempResolution.width, m_tempResolution.height, m_tempFS, m_tempResolution.refreshRate );
			opt.Width = m_tempResolution.width;
			opt.Height = m_tempResolution.height;
			opt.RefreshRate = m_tempResolution.refreshRate;
			opt.FullScreen = m_tempFS;

			// Graphics
			// anisotropicFiltering
			opt.AnisotropicFiltering = m_tempAF;

			// AntiAliasing
			QualitySettings.antiAliasing = ( int )m_tempAA;
			opt.AntiAliasing = m_tempAA;
			
			//LevelOfDetails
			QualitySettings.lodBias = ( float )m_tempLOD * 0.01f;
			opt.LevelOfDetails = m_tempLOD;
			
			//TextureQuality
			QualitySettings.masterTextureLimit = ( int )m_tempTQ;
			opt.TextureQuality = m_tempTQ;
			
			//ShadersQuality
			QualitySettings.pixelLightCount = ( int )m_tempSQ;
			opt.ShadersQuality = m_tempSQ;

			//PhysicParticle
			QualitySettings.particleRaycastBudget = ( int )m_tempPPQ;
			opt.PhysicParticle = m_tempPPQ;
			
			//RealtimeReflections
			QualitySettings.realtimeReflectionProbes = m_tempRR;
			opt.RealtimeReflections = m_tempRR;
			
			//ShadowsDistance
			QualitySettings.shadowDistance = ( float )m_tempDist;
			opt.ShadowsDistance = m_tempDist;

			//ShadowResolution
			QualitySettings.shadowResolution = m_tempRes;
			opt.ShadowResolution = m_tempRes;
			
			//ShadowQuality
			QualitySettings.shadows = m_tempQual;
			opt.ShadowQuality = m_tempQual;

			//ParticlesQuality
			QualitySettings.softParticles = m_tempPQ == Engine.ParticlesQualityValue.HighQuality;
			opt.ParticlesQuality = m_tempPQ;

			//VSync
			QualitySettings.vSyncCount = ( int )m_tempVS;
			opt.VSync = m_tempVS;

			//
			/// Sounds
			Engine.UserSoundOptions optSound = Engine.UserManager.Instance.Current.Options.Sound;

			//GeneralVolume

			//MusicVolume
			optSound.MusicVolume = m_tempMusicVolume;
			
			//EffectsVolume
			optSound.EffectsVolume = m_tempEffectsVolume;
			
			//VoicesVolum
			optSound.VoicesVolume = m_tempVoicesVolume;
			
			//IsMute
			optSound.IsMute = m_tempMute;

			//
			/// Controles
			Engine.UserCommandOptions optCtrl = Engine.UserManager.Instance.Current.Options.Command;
			// MouseXSensibility
				optCtrl.MouseXSensibility = m_tempSX;
			// MouseYSensibility
				optCtrl.MouseYSensibility = m_tempSY;
			// JoystickXSensibility
				optCtrl.JoystickXSensibility = m_tempJX;
			// JoystickYSensibility
				optCtrl.JoystickYSensibility = m_tempJY;
			//nInvertJoystickXAxis
				optCtrl.InvertJoystickXAxis = m_tempIX;
			//InvertJoystickYAxis
				optCtrl.InvertJoystickYAxis = m_tempIY;
			
		}

		private void Start()
		{
			//Set Cursor to not be visible
       		Cursor.visible = true;

			// Check references
			if ( m_graveyardText == null )
			{
				GameObject g = new GameObject( "Graveyard Text" );
				g.transform.parent = transform;
				m_graveyardText = g.AddComponent<TMPro.TextMeshProUGUI>();
				g = new GameObject( "Graveyard Slider" );
				g.transform.parent = transform;
				m_graveyardSlider = g.AddComponent<Slider>();
				g = new GameObject( "Graveyard Toggle" );
				g.transform.parent = transform;
				m_graveyardToggle = g.AddComponent<Toggle>();
				g = new GameObject( "Graveyard TextSwitcherGroup" );
				g.transform.parent = transform;
				m_graveyardTextSwitcherGroup = g.AddComponent<TextSwitcherGroup>();
				m_graveyardText.enabled = false;
				m_graveyardSlider.enabled = false;
				m_graveyardToggle.enabled = false;
				m_graveyardTextSwitcherGroup.enabled = false;
				m_graveyardText.gameObject.SetActive( false );
				m_graveyardSlider.gameObject.SetActive( false );
				m_graveyardToggle.gameObject.SetActive( false );
				m_graveyardTextSwitcherGroup.gameObject.SetActive( false );
			}
			if ( f_english == null ) { f_english = m_graveyardToggle; }
			if ( f_french == null ) { f_french = m_graveyardToggle; }
			if ( f_resolution == null ) { f_resolution = m_graveyardSlider; }
			if ( f_fullscreen == null ) { f_fullscreen = m_graveyardToggle; }
			if ( f_anisotropicFiltering == null ) { f_anisotropicFiltering = m_graveyardSlider; }
			if ( f_antiAliasing == null ) { f_antiAliasing = m_graveyardSlider; }
			if ( f_levelOfDetails == null ) { f_levelOfDetails = m_graveyardSlider; }
			if ( f_textureQuality == null ) { f_textureQuality = m_graveyardSlider; }
			if ( f_shadersQuality == null ) { f_shadersQuality = m_graveyardSlider; }
			if ( f_physicParticlesQuality == null ) { f_physicParticlesQuality = m_graveyardSlider; }
			if ( f_realtimeReflections == null ) { f_realtimeReflections = m_graveyardToggle; }
			if ( f_shadowDistance == null ) { f_shadowDistance = m_graveyardSlider; }
			if ( f_shadowResolution == null ) { f_shadowResolution = m_graveyardSlider; }
			if ( f_shadowQuality == null ) { f_shadowQuality = m_graveyardSlider; }
			if ( f_particlesQuality == null ) { f_particlesQuality = m_graveyardSlider; }
			if ( f_vsync == null ) { f_vsync = m_graveyardToggle; }
			if ( f_generalVolume == null ) { f_generalVolume = m_graveyardSlider; }
			if ( f_musicVolume == null ) { f_musicVolume = m_graveyardSlider; }
			if ( f_effectsVolume == null ) { f_effectsVolume = m_graveyardSlider; }
			if ( f_voicesVolume == null ) { f_voicesVolume = m_graveyardSlider; }
			if ( f_mute == null ) { f_mute = m_graveyardToggle; }
			if ( f_mouseX == null ) { f_mouseX = m_graveyardSlider; }
			if ( f_mouseY == null ) { f_mouseY = m_graveyardSlider; }
			if ( f_joystickX == null ) { f_joystickX = m_graveyardSlider; }
			if ( f_joystickY == null ) { f_joystickY = m_graveyardSlider; }
			if ( f_invertX == null ) { f_invertX = m_graveyardToggle; }
			if ( f_invertY == null ) { f_invertY = m_graveyardToggle; }
			if ( t_resolution == null ) { t_resolution = m_graveyardText; }
			if ( t_anisotropicFiltering == null ) { t_anisotropicFiltering = m_graveyardTextSwitcherGroup; }
			if ( t_antiAliasing == null ) { t_antiAliasing = m_graveyardTextSwitcherGroup; }
			if ( t_levelOfDetails == null ) { t_levelOfDetails = m_graveyardTextSwitcherGroup; }
			if ( t_textureQuality == null ) { t_textureQuality = m_graveyardTextSwitcherGroup; }
			if ( t_shadersQuality == null ) { t_shadersQuality = m_graveyardTextSwitcherGroup; }
			if ( t_physicParticlesQuality == null ) { t_physicParticlesQuality = m_graveyardTextSwitcherGroup; }
			if ( t_shadowDistance == null ) { t_shadowDistance = m_graveyardTextSwitcherGroup; }
			if ( t_shadowResolution == null ) { t_shadowResolution = m_graveyardTextSwitcherGroup; }
			if ( t_shadowQuality == null ) { t_shadowQuality = m_graveyardTextSwitcherGroup; }
			if ( t_particlesQuality == null ) { t_particlesQuality = m_graveyardTextSwitcherGroup; }
			if ( t_generalVolume == null ) { t_generalVolume = m_graveyardText; }
			if ( t_musicVolume == null ) { t_musicVolume = m_graveyardText; }
			if ( t_effectsVolume == null ) { t_effectsVolume = m_graveyardText; }
			if ( t_voicesVolume == null ) { t_voicesVolume = m_graveyardText; }


			Engine.User u = Engine.UserManager.Instance.Current;
			Engine.UserGraphicOptions go = u.Options.Graphic;
			Engine.UserSoundOptions so = u.Options.Sound;
			Engine.UserCommandOptions co = u.Options.Command;

			m_tempLanguage = Engine.UserGeneralOptions.SystemLanguageToLang( u.Options.General.Language );

			m_tempResolution.width = go.Width;
			m_tempResolution.height = go.Height;
			m_tempResolution.refreshRate = go.RefreshRate;
			m_tempFS = go.FullScreen;
			m_tempAF = go.AnisotropicFiltering;
			m_tempAA = go.AntiAliasing;
			m_tempLOD = go.LevelOfDetails;
			m_tempTQ = go.TextureQuality;
			m_tempSQ = go.ShadersQuality;
			m_tempPPQ = go.PhysicParticle;
			m_tempRR = go.RealtimeReflections;
			m_tempDist = go.ShadowsDistance;
			m_tempRes = go.ShadowResolution;
			m_tempQual = go.ShadowQuality;
			m_tempPQ = go.ParticlesQuality;
			m_tempVS = go.VSync;

			m_tempGeneralVolume = so.GeneralVolume;
			m_tempMusicVolume = so.MusicVolume;
			m_tempEffectsVolume = so.EffectsVolume;
			m_tempVoicesVolume = so.VoicesVolume;
			m_tempMute = so.IsMute;

			m_tempSX = co.MouseXSensibility;
			m_tempSY = co.MouseYSensibility;
			m_tempJX = co.JoystickXSensibility;
			m_tempJY = co.JoystickYSensibility;
			m_tempIX = co.InvertJoystickXAxis;
			m_tempIY = co.InvertJoystickYAxis;

			f_english.isOn = m_tempLanguage == Engine.LocalizedString.Lang.ENGLISH;
			f_french.isOn = m_tempLanguage == Engine.LocalizedString.Lang.FRENCH;
			f_resolution.wholeNumbers = true;
			f_resolution.minValue = 0;
			f_resolution.maxValue = Screen.resolutions.Length - 1;
			f_resolution.value = 0;
			for ( int i = 0; i < Screen.resolutions.Length; ++i )
			{
				if ( ( Screen.resolutions[ i ].width == m_tempResolution.width ) &&
					( Screen.resolutions[ i ].height == m_tempResolution.height ) &&
					( Screen.resolutions[ i ].refreshRate == m_tempResolution.refreshRate ) )
				{
					f_resolution.value = i;
				}
			}
			f_fullscreen.isOn = m_tempFS;
			f_anisotropicFiltering.value = ( float )m_tempAF;
			switch ( m_tempAA )
			{
			case Engine.AntiAliasingValue.Disabled: f_antiAliasing.value = 0.0f; break;
			case Engine.AntiAliasingValue.X2: f_antiAliasing.value = 1.0f; break;
			case Engine.AntiAliasingValue.X4: f_antiAliasing.value = 2.0f; break;
			case Engine.AntiAliasingValue.X8: f_antiAliasing.value = 3.0f; break;
			}
			switch ( m_tempLOD )
			{
			case Engine.LevelOfDetailsValue.HighestQuality: f_levelOfDetails.value = 4.0f; break;
			case Engine.LevelOfDetailsValue.HighQuality: f_levelOfDetails.value = 3.0f; break;
			case Engine.LevelOfDetailsValue.MediumQuality: f_levelOfDetails.value = 2.0f; break;
			case Engine.LevelOfDetailsValue.LowQuality: f_levelOfDetails.value = 1.0f; break;
			case Engine.LevelOfDetailsValue.LowestQuality: f_levelOfDetails.value = 0.0f; break;
			}
			switch ( m_tempTQ )
			{
			case Engine.TextureQualityValue.HighQuality: f_textureQuality.value = 3.0f; break;
			case Engine.TextureQualityValue.MediumQuality: f_textureQuality.value = 2.0f; break;
			case Engine.TextureQualityValue.LowQuality: f_textureQuality.value = 1.0f; break;
			case Engine.TextureQualityValue.LowestQuality: f_textureQuality.value = 0.0f; break;
			}
			switch ( m_tempSQ )
			{
			case Engine.ShadersQualityValue.HighestQuality: f_shadersQuality.value = 4.0f; break;
			case Engine.ShadersQualityValue.HighQuality: f_shadersQuality.value = 3.0f; break;
			case Engine.ShadersQualityValue.MediumQuality: f_shadersQuality.value = 2.0f; break;
			case Engine.ShadersQualityValue.LowQuality: f_shadersQuality.value = 1.0f; break;
			case Engine.ShadersQualityValue.LowestQuality: f_shadersQuality.value = 0.0f; break;
			}
			switch ( m_tempPPQ )
			{
			case Engine.PhysicParticleValue.HighestQuality: f_physicParticlesQuality.value = 4.0f; break;
			case Engine.PhysicParticleValue.HighQuality: f_physicParticlesQuality.value = 3.0f; break;
			case Engine.PhysicParticleValue.MediumQuality: f_physicParticlesQuality.value = 2.0f; break;
			case Engine.PhysicParticleValue.LowQuality: f_physicParticlesQuality.value = 1.0f; break;
			case Engine.PhysicParticleValue.LowestQuality: f_physicParticlesQuality.value = 0.0f; break;
			}
			f_realtimeReflections.isOn = m_tempRR;
			switch ( m_tempDist )
			{
			case Engine.ShadowsDistanceValue.HighDistance: f_shadowDistance.value = 2.0f; break;
			case Engine.ShadowsDistanceValue.MediumDistance: f_shadowDistance.value = 1.0f; break;
			case Engine.ShadowsDistanceValue.LowDistance: f_shadowDistance.value = 0.0f; break;
			}
			f_shadowResolution.value = ( float )m_tempRes;
			f_shadowQuality.value = ( float )m_tempQual;
			switch ( m_tempPQ )
			{
			case Engine.ParticlesQualityValue.HighQuality: f_particlesQuality.value = 0.0f; break;
			case Engine.ParticlesQualityValue.LowQuality: f_particlesQuality.value = 1.0f; break;
			}
			f_vsync.isOn = ( m_tempVS != Engine.VSyncValue.Disabled );
			f_generalVolume.value = m_tempGeneralVolume;
			f_musicVolume.value = m_tempMusicVolume;
			f_effectsVolume.value = m_tempEffectsVolume;
			f_voicesVolume.value = m_tempVoicesVolume;
			f_mute.isOn = m_tempMute;
			f_mouseX.value = m_tempSX * 0.9f + 0.1f;
			f_mouseY.value = m_tempSY * 0.9f + 0.1f;
			f_joystickX.value = m_tempJX * 0.9f + 0.1f;
			f_joystickX.value = m_tempJY * 0.9f + 0.1f;
			f_invertX.isOn = m_tempIX;
			f_invertY.isOn = m_tempIY;

			SetResolution();
			SetAntiAliasing();
			SetLevelOfDetails();
			SetTextureQuality();
			SetShadersQuality();
			SetPhysicParticlesQuality();
			SetShadowsDistance();
			SetShadowResolution();
			SetShadowQuality();
			SetParticlesQuality();
			SetVSyncEnabled( m_tempVS == Engine.VSyncValue.Enabled );
			SetGeneralVolume( m_tempGeneralVolume );
			SetMusicVolume( m_tempMusicVolume );
			SetEffectsVolume( m_tempEffectsVolume );
			SetVoicesVolume( m_tempVoicesVolume );
			SetMute( m_tempMute );

			ResetValues();
		}

		public void PlayGame( string _sceneName )
		{
            GameMusicManager.Instance.m_ActualMusic = GameMusicManager.EGameMusicManagerState.eNone;
            GameMusicManager.Instance.m_AudioSource.Stop();
			SceneManager.LoadScene( _sceneName );
		}
		public void QuitGame()
		{
			Application.Quit();
		}
		public void OnNavigation( GameObject _target )
		{
			UnityEngine.EventSystems.EventSystem es = UnityEngine.EventSystems.EventSystem.current;
			es.SetSelectedGameObject( _target );
		}
		public void OnLanguageChanged()
		{
			m_tempLanguage = f_english.isOn ? Engine.LocalizedString.Lang.ENGLISH : Engine.LocalizedString.Lang.FRENCH;
		}
		public void ApplyGeneral()
		{
			Engine.UserGeneralOptions go = Engine.UserManager.Instance.Current.Options.General;
			SystemLanguage lang = Engine.UserGeneralOptions.SupportedLanguages[ ( int )m_tempLanguage ];
			if ( go.Language != lang )
			{
				GameLocalizedStringManager.Instance.LoadLang( m_tempLanguage );
				go.Language = lang;
				Engine.UserManager.Instance.Save();
			}
		}
		public void SetResolution()
		{
			int index = Mathf.RoundToInt( f_resolution.value );
			if ( index >= 0 && index < Screen.resolutions.Length )
			{
				m_tempResolution = Screen.resolutions[ index ];
				Resolution r = Screen.resolutions[ index ];
				t_resolution.text = r.ToString();
			}
		}
		public void SetFullscreen( bool _value )
		{
			m_tempFS = _value;
		}
		public void SetAnisotropicFiltering()
		{
			int index = Mathf.RoundToInt( f_anisotropicFiltering.value );
			t_anisotropicFiltering.SetText( index );
			m_tempAF = ( AnisotropicFiltering )index;
		}
		public void SetAntiAliasing()
		{
			int index = Mathf.RoundToInt( f_antiAliasing.value );
			t_antiAliasing.SetText( index );
			if ( index != 0 )
			{
				index = Mathf.RoundToInt( Mathf.Pow( 2.0f, index ) );
			}
			m_tempAA = ( Engine.AntiAliasingValue )index;
		}
		public void SetLevelOfDetails()
		{
			int index = Mathf.RoundToInt( f_levelOfDetails.value );
			t_levelOfDetails.SetText( index );
			switch ( index )
			{
			case 0: m_tempLOD = Engine.LevelOfDetailsValue.LowestQuality; break;
			case 1: m_tempLOD = Engine.LevelOfDetailsValue.LowQuality; break;
			case 2: m_tempLOD = Engine.LevelOfDetailsValue.MediumQuality; break;
			case 3: m_tempLOD = Engine.LevelOfDetailsValue.HighQuality; break;
			case 4: m_tempLOD = Engine.LevelOfDetailsValue.HighestQuality; break;
			}
		}
		public void SetTextureQuality()
		{
			int index = Mathf.RoundToInt( f_textureQuality.value );
			t_textureQuality.SetText( index );
			switch ( index )
			{
			case 0: m_tempTQ = Engine.TextureQualityValue.LowestQuality; break;
			case 1: m_tempTQ = Engine.TextureQualityValue.LowQuality; break;
			case 2: m_tempTQ = Engine.TextureQualityValue.MediumQuality; break;
			case 3: m_tempTQ = Engine.TextureQualityValue.HighQuality; break;
			}
		}
		public void SetShadersQuality()
		{
			int index = Mathf.RoundToInt( f_shadersQuality.value );
			t_shadersQuality.SetText( index );
			switch ( index )
			{
			case 0: m_tempSQ = Engine.ShadersQualityValue.LowestQuality; break;
			case 1: m_tempSQ = Engine.ShadersQualityValue.LowQuality; break;
			case 2: m_tempSQ = Engine.ShadersQualityValue.MediumQuality; break;
			case 3: m_tempSQ = Engine.ShadersQualityValue.HighQuality; break;
			case 4: m_tempSQ = Engine.ShadersQualityValue.HighestQuality; break;
			}
		}
		public void SetPhysicParticlesQuality()
		{
			int index = Mathf.RoundToInt( f_physicParticlesQuality.value );
			t_physicParticlesQuality.SetText( index );
			switch ( index )
			{
			case 0: m_tempPPQ = Engine.PhysicParticleValue.LowestQuality; break;
			case 1: m_tempPPQ = Engine.PhysicParticleValue.LowQuality; break;
			case 2: m_tempPPQ = Engine.PhysicParticleValue.MediumQuality; break;
			case 3: m_tempPPQ = Engine.PhysicParticleValue.HighQuality; break;
			case 4: m_tempPPQ = Engine.PhysicParticleValue.HighestQuality; break;
			}
		}
		public void SetRealTimeReflections( bool _enabled )
		{
			m_tempRR = _enabled;
		}
		public void SetShadowsDistance()
		{
			int index = Mathf.RoundToInt( f_shadowDistance.value );
			t_shadowDistance.SetText( index );
			switch ( index )
			{
			case 0: m_tempDist = Engine.ShadowsDistanceValue.LowDistance; break;
			case 1: m_tempDist = Engine.ShadowsDistanceValue.MediumDistance; break;
			case 2: m_tempDist = Engine.ShadowsDistanceValue.HighDistance; break;
			}
		}
		public void SetShadowResolution()
		{
			int index = Mathf.RoundToInt( f_shadowResolution.value );
			t_shadowResolution.SetText( index );
			m_tempRes = ( ShadowResolution )index;
		}
		public void SetShadowQuality()
		{
			int index = Mathf.RoundToInt( f_shadowQuality.value );
			t_shadowQuality.SetText( index );
			m_tempQual = ( ShadowQuality )index;
		}
		public void SetParticlesQuality()
		{
			int index = Mathf.RoundToInt( f_particlesQuality.value );
			t_particlesQuality.SetText( index );
			m_tempPQ = ( Engine.ParticlesQualityValue )( 1 - index );
		}
		public void SetVSyncEnabled( bool _enabled )
		{
			m_tempVS = _enabled ? Engine.VSyncValue.Enabled : Engine.VSyncValue.Disabled;
		}
		public void ApplyGraphics()
		{
			bool changes = false;
			Engine.UserGraphicOptions opt = Engine.UserManager.Instance.Current.Options.Graphic;
			if ( ( opt.Width != m_tempResolution.width ) ||
				( opt.Width != m_tempResolution.width ) ||
				( opt.Width != m_tempResolution.width ) ||
				( opt.Width != m_tempResolution.width ) )
			{
				changes = true;
				Screen.SetResolution( m_tempResolution.width, m_tempResolution.height, m_tempFS, m_tempResolution.refreshRate );
				opt.Width = m_tempResolution.width;
				opt.Height = m_tempResolution.height;
				opt.RefreshRate = m_tempResolution.refreshRate;
				opt.FullScreen = m_tempFS;
			}
			if ( opt.AnisotropicFiltering != m_tempAF )
			{
				changes = true;
				QualitySettings.anisotropicFiltering = m_tempAF;
				opt.AnisotropicFiltering = m_tempAF;
			}
			if ( opt.AntiAliasing != m_tempAA )
			{
				changes = true;
				QualitySettings.antiAliasing = ( int )m_tempAA;
				opt.AntiAliasing = m_tempAA;
			}
			if ( opt.LevelOfDetails != m_tempLOD )
			{
				changes = true;
				QualitySettings.lodBias = ( float )m_tempLOD * 0.01f;
				opt.LevelOfDetails = m_tempLOD;
			}
			if ( opt.TextureQuality != m_tempTQ )
			{
				changes = true;
				QualitySettings.masterTextureLimit = ( int )m_tempTQ;
				opt.TextureQuality = m_tempTQ;
			}
			if ( opt.ShadersQuality != m_tempSQ )
			{
				changes = true;
				QualitySettings.pixelLightCount = ( int )m_tempSQ;
				opt.ShadersQuality = m_tempSQ;
			}
			if ( opt.PhysicParticle != m_tempPPQ )
			{
				changes = true;
				QualitySettings.particleRaycastBudget = ( int )m_tempPPQ;
				opt.PhysicParticle = m_tempPPQ;
			}
			if ( opt.RealtimeReflections != m_tempRR )
			{
				changes = true;
				QualitySettings.realtimeReflectionProbes = m_tempRR;
				opt.RealtimeReflections = m_tempRR;
			}
			if ( opt.ShadowsDistance != m_tempDist )
			{
				changes = true;
				QualitySettings.shadowDistance = ( float )m_tempDist;
				opt.ShadowsDistance = m_tempDist;
			}
			if ( opt.ShadowResolution != m_tempRes )
			{
				changes = true;
				QualitySettings.shadowResolution = m_tempRes;
				opt.ShadowResolution = m_tempRes;
			}
			if ( opt.ShadowQuality != m_tempQual )
			{
				changes = true;
				QualitySettings.shadows = m_tempQual;
				opt.ShadowQuality = m_tempQual;
			}
			if ( opt.ParticlesQuality != m_tempPQ )
			{
				changes = true;
				QualitySettings.softParticles = m_tempPQ == Engine.ParticlesQualityValue.HighQuality;
				opt.ParticlesQuality = m_tempPQ;
			}
			if ( opt.VSync != m_tempVS )
			{
				changes = true;
				QualitySettings.vSyncCount = ( int )m_tempVS;
				opt.VSync = m_tempVS;
			}
			if ( changes )
			{
				Engine.UserManager.Instance.Save();
			}
		}
		public void SetGeneralVolume( float _value )
		{
			t_generalVolume.text = ( int )( Mathf.Clamp01( _value ) * 100.0f ) + " %";
			m_tempGeneralVolume = _value;
		}
		public void SetMusicVolume( float _value )
		{
			t_musicVolume.text = ( int )( Mathf.Clamp01( _value ) * 100.0f ) + " %";
			m_tempMusicVolume = _value;
		}
		public void SetEffectsVolume( float _value )
		{
			t_effectsVolume.text = ( int )( Mathf.Clamp01( _value ) * 100.0f ) + " %";
			m_tempEffectsVolume = _value;
		}
		public void SetVoicesVolume( float _value )
		{
			t_voicesVolume.text = ( int )( Mathf.Clamp01( _value ) * 100.0f ) + " %";
			m_tempVoicesVolume = _value;
		}
		public void SetMute( bool _mute )
		{
			m_tempMute = _mute;
		}
		public void ApplySound()
		{
			Engine.UserSoundOptions opt = Engine.UserManager.Instance.Current.Options.Sound;
			bool changes = false;
			if ( opt.GeneralVolume != m_tempGeneralVolume )
			{
				changes = true;
				// todo apply to soundmanager
				opt.GeneralVolume = m_tempGeneralVolume;
			}
			if ( opt.MusicVolume != m_tempMusicVolume )
			{
				changes = true;
				// todo apply to soundmanager
				opt.MusicVolume = m_tempMusicVolume;
			}
			if ( opt.EffectsVolume != m_tempEffectsVolume )
			{
				changes = true;
				// todo apply to soundmanager
				opt.EffectsVolume = m_tempEffectsVolume;
			}
			if ( opt.VoicesVolume != m_tempVoicesVolume )
			{
				changes = true;
				// todo apply to soundmanager
				opt.VoicesVolume = m_tempVoicesVolume;
			}
			if ( opt.IsMute != m_tempMute )
			{
				changes = true;
				// todo apply to soundmanager
				opt.IsMute = m_tempMute;
			}

			if ( changes )
			{
				Engine.UserManager.Instance.Save();
			}
		}
		public void SetMouseX( float _value )
		{
			m_tempSX = _value;
		}
		public void SetMouseY( float _value )
		{
			m_tempSY = _value;
		}
		public void SetJoystickX( float _value )
		{
			m_tempJX = _value;
		}
		public void SetJoystickY( float _value )
		{
			m_tempJY = _value;
		}
		public void SetInvertX( bool _value )
		{
			m_tempIX = _value;
		}
		public void SetInvertY( bool _value )
		{
			m_tempIY = _value;
		}
		public void ApplyControl()
		{
			Engine.UserCommandOptions opt = Engine.UserManager.Instance.Current.Options.Command;
			bool changes = false;
			if ( !Mathf.Approximately( opt.MouseXSensibility, m_tempSX ) )
			{
				changes = true;
				opt.MouseXSensibility = m_tempSX;
			}
			if ( !Mathf.Approximately( opt.MouseYSensibility, m_tempSY ) )
			{
				changes = true;
				opt.MouseYSensibility = m_tempSY;
			}
			if ( !Mathf.Approximately( opt.JoystickXSensibility, m_tempJX ) )
			{
				changes = true;
				opt.JoystickXSensibility = m_tempJX;
			}
			if ( !Mathf.Approximately( opt.JoystickYSensibility, m_tempJY ) )
			{
				changes = true;
				opt.JoystickYSensibility = m_tempJY;
			}
			if ( opt.InvertJoystickXAxis != m_tempIX )
			{
				changes = true;
				opt.InvertJoystickXAxis = m_tempIX;
			}
			if ( opt.InvertJoystickYAxis != m_tempIY )
			{
				changes = true;
				opt.InvertJoystickYAxis = m_tempIY;
			}

			if ( changes )
			{
				Engine.UserManager.Instance.Save();
			}
		}
	}
}
