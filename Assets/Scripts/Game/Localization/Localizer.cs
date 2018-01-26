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

			foreach ( TMPro.TextMeshProUGUI t in m_gameTitle ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GAME_TITLE ); }
			foreach ( TMPro.TextMeshProUGUI t in m_play ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PLAY ); }
			foreach ( TMPro.TextMeshProUGUI t in m_settings ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SETTINGS ); }
			foreach ( TMPro.TextMeshProUGUI t in m_credits ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CREDITS ); }
			foreach ( TMPro.TextMeshProUGUI t in m_exit ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.EXIT ); }
			foreach ( TMPro.TextMeshProUGUI t in m_back ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.BACK ); }
			foreach ( TMPro.TextMeshProUGUI t in m_previous ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PREVIOUS ); }
			foreach ( TMPro.TextMeshProUGUI t in m_next ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.NEXT ); }

			foreach ( TMPro.TextMeshProUGUI t in m_generalOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GENERAL_OPTIONS ); }
			foreach ( TMPro.TextMeshProUGUI t in m_graphicOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GRAPHICS_OPTIONS ); }
			foreach ( TMPro.TextMeshProUGUI t in m_soundOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SOUND_OPTIONS ); }
			foreach ( TMPro.TextMeshProUGUI t in m_controlOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CONTROL_OPTIONS ); }
			foreach ( TMPro.TextMeshProUGUI t in m_apply ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.APPLY ); }
			foreach ( TMPro.TextMeshProUGUI t in m_cancel ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CANCEL ); }

			foreach ( TMPro.TextMeshProUGUI t in m_language ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LANGUAGE ); }
			foreach ( TMPro.TextMeshProUGUI t in m_english ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ENGLISH ); }
			foreach ( TMPro.TextMeshProUGUI t in m_french ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FRENCH ); }

			foreach ( TMPro.TextMeshProUGUI t in m_resolution ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.RESOLUTION ); }
			foreach ( TMPro.TextMeshProUGUI t in m_fullscreen ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FULLSCREEN ); }
			foreach ( TMPro.TextMeshProUGUI t in m_anisotropicFiltering ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ANISOTROPIC_FILTERING ); }
			foreach ( TMPro.TextMeshProUGUI t in m_antiAliasing ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ANTI_ALIASING ); }
			foreach ( TMPro.TextMeshProUGUI t in m_levelOfDetails ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LEVEL_OF_DETAIL ); }
			foreach ( TMPro.TextMeshProUGUI t in m_textureQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.TEXTURE_QUALITY ); }
			foreach ( TMPro.TextMeshProUGUI t in m_shadersQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADERS_QUALITY ); }
			foreach ( TMPro.TextMeshProUGUI t in m_physicParticles ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PHYSIC_PARTICLES ); }
			foreach ( TMPro.TextMeshProUGUI t in m_realTimeReflections ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.REALTIME_REFLECTIONS ); }
			foreach ( TMPro.TextMeshProUGUI t in m_shadowDistance ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_DISTANCE ); }
			foreach ( TMPro.TextMeshProUGUI t in m_shadowResolution ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_RESOLUTION ); }
			foreach ( TMPro.TextMeshProUGUI t in m_shadowQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_QUALITY ); }
			foreach ( TMPro.TextMeshProUGUI t in m_particleQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PARTICLES_QUALITY ); }
			foreach ( TMPro.TextMeshProUGUI t in m_vsync ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.VSYNC ); }

			foreach ( TMPro.TextMeshProUGUI t in m_enabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ENABLED ); }
			foreach ( TMPro.TextMeshProUGUI t in m_disabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.DISABLED ); }
			foreach ( TMPro.TextMeshProUGUI t in m_forceEnabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FORCE_ENABLED ); }
			foreach ( TMPro.TextMeshProUGUI t in m_highest ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HIGHEST ); }
			foreach ( TMPro.TextMeshProUGUI t in m_high ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HIGH ); }
			foreach ( TMPro.TextMeshProUGUI t in m_medium ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MEDIUM ); }
			foreach ( TMPro.TextMeshProUGUI t in m_low ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LOW ); }
			foreach ( TMPro.TextMeshProUGUI t in m_lowest ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LOWEST ); }
			foreach ( TMPro.TextMeshProUGUI t in m_hardShadowsOnly ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HARD_SHADOWS_ONLY ); }
			foreach ( TMPro.TextMeshProUGUI t in m_hardAndSoft ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HARD_AND_SOFT ); }

			foreach ( TMPro.TextMeshProUGUI t in m_generalVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GENERAL_VOLUME ); }
			foreach ( TMPro.TextMeshProUGUI t in m_musicVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MUSIC_VOLUME ); }
			foreach ( TMPro.TextMeshProUGUI t in m_effectsVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.EFFECTS_VOLUME ); }
			foreach ( TMPro.TextMeshProUGUI t in m_voicesVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.VOICES_VOLUME ); }
			foreach ( TMPro.TextMeshProUGUI t in m_muteSound ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MUTE ); }

			foreach ( TMPro.TextMeshProUGUI t in m_mouseSensibilityX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MOUSE_SENSIBILITY_X ); }
			foreach ( TMPro.TextMeshProUGUI t in m_mouseSensibilityY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MOUSE_SENSIBILITY_Y ); }
			foreach ( TMPro.TextMeshProUGUI t in m_joystickSensibilityX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.JOYSTICK_SENSIBILITY_X ); }
			foreach ( TMPro.TextMeshProUGUI t in m_joystickSensibilityY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.JOYSTICK_SENSIBILITY_Y ); }
			foreach ( TMPro.TextMeshProUGUI t in m_invertX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.INVERT_X ); }
			foreach ( TMPro.TextMeshProUGUI t in m_invertY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.INVERT_Y ); }


		}
	}
}
