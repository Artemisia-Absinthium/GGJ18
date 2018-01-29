/*
 * LICENCE
 */
using UnityEngine;
using UnityEngine.UI;

namespace Game
{

	[System.Serializable]
	public class Localizer : MonoBehaviour
	{
		// Main
		[Header("Main")]
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_gameTitle;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_play;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_settings;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_credits;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_exit;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_back;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_previous;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_next;

		// Options
		[ Header( "Options" )]
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_generalOptions;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_graphicOptions;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_soundOptions;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_controlOptions;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_apply;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_cancel;

		// General
		[ Header( "General" )]
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_language;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_english;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_french;

		// Graphics
		[Header( "Graphics" )]
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_resolution;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_fullscreen;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_anisotropicFiltering;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_antiAliasing;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_levelOfDetails;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_textureQuality;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_shadersQuality;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_physicParticles;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_realTimeReflections;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_shadowDistance;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_shadowResolution;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_shadowQuality;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_particleQuality;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_vsync;
		[SerializeField]

		private TMPro.TextMeshProUGUI[] m_enabled;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_disabled;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_forceEnabled;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_highest;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_high;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_medium;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_low;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_lowest;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_hardShadowsOnly;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_hardAndSoft;

		// Sound
		[Header( "Sound" )]
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_generalVolume;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_musicVolume;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_effectsVolume;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_voicesVolume;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_muteSound;

		// Controls
		[Header( "Controls" )]
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_mouseSensibilityX;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_mouseSensibilityY;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_joystickSensibilityX;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_joystickSensibilityY;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_invertX;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_invertY;

		// Pause
		[Header( "Pause" )]
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_pause;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_resume;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_mainMenu;

		private SystemLanguage m_currentLanguage = SystemLanguage.English;
		private Engine.LocalizedString.Lang m_engineLanguage = Engine.LocalizedString.Lang.ENGLISH;

		void Start()
		{
			m_currentLanguage = ( SystemLanguage )( -1 );
		}

		void Update()
		{
			SystemLanguage newLang = Engine.UserManager.Instance.Current.Options.General.Language;
			if ( newLang != m_currentLanguage )
			{
				m_currentLanguage = newLang;
				m_engineLanguage = Engine.UserGeneralOptions.SystemLanguageToLang( m_currentLanguage );
				GameLocalizedStringManager.Instance.LoadLang( m_engineLanguage );
			}

			if ( m_gameTitle != null ) { foreach ( TMPro.TextMeshProUGUI t in m_gameTitle ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GAME_TITLE ); } }
			if ( m_play != null ) { foreach ( TMPro.TextMeshProUGUI t in m_play ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PLAY ); } }
			if ( m_settings != null ) { foreach ( TMPro.TextMeshProUGUI t in m_settings ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SETTINGS ); } }
			if ( m_credits != null ) { foreach ( TMPro.TextMeshProUGUI t in m_credits ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CREDITS ); } }
			if ( m_exit != null ) { foreach ( TMPro.TextMeshProUGUI t in m_exit ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.EXIT ); } }
			if ( m_back != null ) { foreach ( TMPro.TextMeshProUGUI t in m_back ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.BACK ); } }
			if ( m_previous != null ) { foreach ( TMPro.TextMeshProUGUI t in m_previous ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PREVIOUS ); } }
			if ( m_next != null ) { foreach ( TMPro.TextMeshProUGUI t in m_next ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.NEXT ); } }

			if ( m_generalOptions != null ) { foreach ( TMPro.TextMeshProUGUI t in m_generalOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GENERAL_OPTIONS ); } }
			if ( m_graphicOptions != null ) { foreach ( TMPro.TextMeshProUGUI t in m_graphicOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GRAPHICS_OPTIONS ); } }
			if ( m_soundOptions != null ) { foreach ( TMPro.TextMeshProUGUI t in m_soundOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SOUND_OPTIONS ); } }
			if ( m_controlOptions != null ) { foreach ( TMPro.TextMeshProUGUI t in m_controlOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CONTROL_OPTIONS ); } }
			if ( m_apply != null ) { foreach ( TMPro.TextMeshProUGUI t in m_apply ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.APPLY ); } }
			if ( m_cancel != null ) { foreach ( TMPro.TextMeshProUGUI t in m_cancel ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CANCEL ); } }

			if ( m_language != null ) { foreach ( TMPro.TextMeshProUGUI t in m_language ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LANGUAGE ); } }
			if ( m_english != null ) { foreach ( TMPro.TextMeshProUGUI t in m_english ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ENGLISH ); } }
			if ( m_french != null ) { foreach ( TMPro.TextMeshProUGUI t in m_french ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FRENCH ); } }

			if ( m_resolution != null ) { foreach ( TMPro.TextMeshProUGUI t in m_resolution ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.RESOLUTION ); } }
			if ( m_fullscreen != null ) { foreach ( TMPro.TextMeshProUGUI t in m_fullscreen ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FULLSCREEN ); } }
			if ( m_anisotropicFiltering != null ) { foreach ( TMPro.TextMeshProUGUI t in m_anisotropicFiltering ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ANISOTROPIC_FILTERING ); } }
			if ( m_antiAliasing != null ) { foreach ( TMPro.TextMeshProUGUI t in m_antiAliasing ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ANTI_ALIASING ); } }
			if ( m_levelOfDetails != null ) { foreach ( TMPro.TextMeshProUGUI t in m_levelOfDetails ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LEVEL_OF_DETAIL ); } }
			if ( m_textureQuality != null ) { foreach ( TMPro.TextMeshProUGUI t in m_textureQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.TEXTURE_QUALITY ); } }
			if ( m_shadersQuality != null ) { foreach ( TMPro.TextMeshProUGUI t in m_shadersQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADERS_QUALITY ); } }
			if ( m_physicParticles != null ) { foreach ( TMPro.TextMeshProUGUI t in m_physicParticles ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PHYSIC_PARTICLES ); } }
			if ( m_realTimeReflections != null ) { foreach ( TMPro.TextMeshProUGUI t in m_realTimeReflections ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.REALTIME_REFLECTIONS ); } }
			if ( m_shadowDistance != null ) { foreach ( TMPro.TextMeshProUGUI t in m_shadowDistance ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_DISTANCE ); } }
			if ( m_shadowResolution != null ) { foreach ( TMPro.TextMeshProUGUI t in m_shadowResolution ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_RESOLUTION ); } }
			if ( m_shadowQuality != null ) { foreach ( TMPro.TextMeshProUGUI t in m_shadowQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_QUALITY ); } }
			if ( m_particleQuality != null ) { foreach ( TMPro.TextMeshProUGUI t in m_particleQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PARTICLES_QUALITY ); } }
			if ( m_vsync != null ) { foreach ( TMPro.TextMeshProUGUI t in m_vsync ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.VSYNC ); } }

			if ( m_enabled != null ) { foreach ( TMPro.TextMeshProUGUI t in m_enabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ENABLED ); } }
			if ( m_disabled != null ) { foreach ( TMPro.TextMeshProUGUI t in m_disabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.DISABLED ); } }
			if ( m_forceEnabled != null ) { foreach ( TMPro.TextMeshProUGUI t in m_forceEnabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FORCE_ENABLED ); } }
			if ( m_highest != null ) { foreach ( TMPro.TextMeshProUGUI t in m_highest ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HIGHEST ); } }
			if ( m_high != null ) { foreach ( TMPro.TextMeshProUGUI t in m_high ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HIGH ); } }
			if ( m_medium != null ) { foreach ( TMPro.TextMeshProUGUI t in m_medium ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MEDIUM ); } }
			if ( m_low != null ) { foreach ( TMPro.TextMeshProUGUI t in m_low ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LOW ); } }
			if ( m_lowest != null ) { foreach ( TMPro.TextMeshProUGUI t in m_lowest ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LOWEST ); } }
			if ( m_hardShadowsOnly != null ) { foreach ( TMPro.TextMeshProUGUI t in m_hardShadowsOnly ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HARD_SHADOWS_ONLY ); } }
			if ( m_hardAndSoft != null ) { foreach ( TMPro.TextMeshProUGUI t in m_hardAndSoft ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HARD_AND_SOFT ); } }

			if ( m_generalVolume != null ) { foreach ( TMPro.TextMeshProUGUI t in m_generalVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GENERAL_VOLUME ); } }
			if ( m_musicVolume != null ) { foreach ( TMPro.TextMeshProUGUI t in m_musicVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MUSIC_VOLUME ); } }
			if ( m_effectsVolume != null ) { foreach ( TMPro.TextMeshProUGUI t in m_effectsVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.EFFECTS_VOLUME ); } }
			if ( m_voicesVolume != null ) { foreach ( TMPro.TextMeshProUGUI t in m_voicesVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.VOICES_VOLUME ); } }
			if ( m_muteSound != null ) { foreach ( TMPro.TextMeshProUGUI t in m_muteSound ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MUTE ); } }

			if ( m_mouseSensibilityX != null ) { foreach ( TMPro.TextMeshProUGUI t in m_mouseSensibilityX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MOUSE_SENSIBILITY_X ); } }
			if ( m_mouseSensibilityY != null ) { foreach ( TMPro.TextMeshProUGUI t in m_mouseSensibilityY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MOUSE_SENSIBILITY_Y ); } }
			if ( m_joystickSensibilityX != null ) { foreach ( TMPro.TextMeshProUGUI t in m_joystickSensibilityX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.JOYSTICK_SENSIBILITY_X ); } }
			if ( m_joystickSensibilityY != null ) { foreach ( TMPro.TextMeshProUGUI t in m_joystickSensibilityY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.JOYSTICK_SENSIBILITY_Y ); } }
			if ( m_invertX != null ) { foreach ( TMPro.TextMeshProUGUI t in m_invertX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.INVERT_X ); } }
			if ( m_invertY != null ) { foreach ( TMPro.TextMeshProUGUI t in m_invertY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.INVERT_Y ); } }

			if ( m_pause != null ) { foreach ( TMPro.TextMeshProUGUI t in m_pause ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PAUSE ); } }
			if ( m_resume != null ) { foreach ( TMPro.TextMeshProUGUI t in m_resume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.RESUME ); } }
			if ( m_mainMenu != null ) { foreach ( TMPro.TextMeshProUGUI t in m_mainMenu ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MAIN_MENU ); } }


		}
	}
}
