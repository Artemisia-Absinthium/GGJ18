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
		private Text[] m_gameTitle;
		[SerializeField]
		private Text[] m_play;
		[SerializeField]
		private Text[] m_settings;
		[SerializeField]
		private Text[] m_credits;
		[SerializeField]
		private Text[] m_exit;
		[SerializeField]
		private Text[] m_back;
		[SerializeField]
		private Text[] m_previous;
		[SerializeField]
		private Text[] m_next;

		// Options
		[ Header( "Options" )]
		[SerializeField]
		private Text[] m_generalOptions;
		[SerializeField]
		private Text[] m_graphicOptions;
		[SerializeField]
		private Text[] m_soundOptions;
		[SerializeField]
		private Text[] m_controlOptions;
		[SerializeField]
		private Text[] m_apply;
		[SerializeField]
		private Text[] m_cancel;

		// General
		[ Header( "General" )]
		[SerializeField]
		private Text[] m_language;
		[SerializeField]
		private Text[] m_english;
		[SerializeField]
		private Text[] m_french;

		// Graphics
		[Header( "Graphics" )]
		[SerializeField]
		private Text[] m_resolution;
		[SerializeField]
		private Text[] m_fullscreen;
		[SerializeField]
		private Text[] m_anisotropicFiltering;
		[SerializeField]
		private Text[] m_antiAliasing;
		[SerializeField]
		private Text[] m_levelOfDetails;
		[SerializeField]
		private Text[] m_textureQuality;
		[SerializeField]
		private Text[] m_shadersQuality;
		[SerializeField]
		private Text[] m_physicParticles;
		[SerializeField]
		private Text[] m_realTimeReflections;
		[SerializeField]
		private Text[] m_shadowDistance;
		[SerializeField]
		private Text[] m_shadowResolution;
		[SerializeField]
		private Text[] m_shadowQuality;
		[SerializeField]
		private Text[] m_particleQuality;
		[SerializeField]
		private Text[] m_vsync;
		[SerializeField]

		private Text[] m_enabled;
		[SerializeField]
		private Text[] m_disabled;
		[SerializeField]
		private Text[] m_forceEnabled;
		[SerializeField]
		private Text[] m_highest;
		[SerializeField]
		private Text[] m_high;
		[SerializeField]
		private Text[] m_medium;
		[SerializeField]
		private Text[] m_low;
		[SerializeField]
		private Text[] m_lowest;
		[SerializeField]
		private Text[] m_hardShadowsOnly;
		[SerializeField]
		private Text[] m_hardAndSoft;

		// Sound
		[Header( "Sound" )]
		[SerializeField]
		private Text[] m_generalVolume;
		[SerializeField]
		private Text[] m_musicVolume;
		[SerializeField]
		private Text[] m_effectsVolume;
		[SerializeField]
		private Text[] m_voicesVolume;
		[SerializeField]
		private Text[] m_muteSound;

		// Controls
		[Header( "Controls" )]
		[SerializeField]
		private Text[] m_mouseSensibilityX;
		[SerializeField]
		private Text[] m_mouseSensibilityY;
		[SerializeField]
		private Text[] m_joystickSensibilityX;
		[SerializeField]
		private Text[] m_joystickSensibilityY;
		[SerializeField]
		private Text[] m_invertX;
		[SerializeField]
		private Text[] m_invertY;

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

			foreach ( Text t in m_gameTitle ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GAME_TITLE ); }
			foreach ( Text t in m_play ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PLAY ); }
			foreach ( Text t in m_settings ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SETTINGS ); }
			foreach ( Text t in m_credits ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CREDITS ); }
			foreach ( Text t in m_exit ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.EXIT ); }
			foreach ( Text t in m_back ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.BACK ); }
			foreach ( Text t in m_previous ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PREVIOUS ); }
			foreach ( Text t in m_next ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.NEXT ); }

			foreach ( Text t in m_generalOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GENERAL_OPTIONS ); }
			foreach ( Text t in m_graphicOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GRAPHICS_OPTIONS ); }
			foreach ( Text t in m_soundOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SOUND_OPTIONS ); }
			foreach ( Text t in m_controlOptions ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CONTROL_OPTIONS ); }
			foreach ( Text t in m_apply ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.APPLY ); }
			foreach ( Text t in m_cancel ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.CANCEL ); }

			foreach ( Text t in m_language ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LANGUAGE ); }
			foreach ( Text t in m_english ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ENGLISH ); }
			foreach ( Text t in m_french ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FRENCH ); }

			foreach ( Text t in m_resolution ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.RESOLUTION ); }
			foreach ( Text t in m_fullscreen ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FULLSCREEN ); }
			foreach ( Text t in m_anisotropicFiltering ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ANISOTROPIC_FILTERING ); }
			foreach ( Text t in m_antiAliasing ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ANTI_ALIASING ); }
			foreach ( Text t in m_levelOfDetails ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LEVEL_OF_DETAIL ); }
			foreach ( Text t in m_textureQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.TEXTURE_QUALITY ); }
			foreach ( Text t in m_shadersQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADERS_QUALITY ); }
			foreach ( Text t in m_physicParticles ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PHYSIC_PARTICLES ); }
			foreach ( Text t in m_realTimeReflections ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.REALTIME_REFLECTIONS ); }
			foreach ( Text t in m_shadowDistance ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_DISTANCE ); }
			foreach ( Text t in m_shadowResolution ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_RESOLUTION ); }
			foreach ( Text t in m_shadowQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.SHADOW_QUALITY ); }
			foreach ( Text t in m_particleQuality ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.PARTICLES_QUALITY ); }
			foreach ( Text t in m_vsync ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.VSYNC ); }

			foreach ( Text t in m_enabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.ENABLED ); }
			foreach ( Text t in m_disabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.DISABLED ); }
			foreach ( Text t in m_forceEnabled ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.FORCE_ENABLED ); }
			foreach ( Text t in m_highest ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HIGHEST ); }
			foreach ( Text t in m_high ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HIGH ); }
			foreach ( Text t in m_medium ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MEDIUM ); }
			foreach ( Text t in m_low ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LOW ); }
			foreach ( Text t in m_lowest ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.LOWEST ); }
			foreach ( Text t in m_hardShadowsOnly ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HARD_SHADOWS_ONLY ); }
			foreach ( Text t in m_hardAndSoft ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.HARD_AND_SOFT ); }

			foreach ( Text t in m_generalVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.GENERAL_VOLUME ); }
			foreach ( Text t in m_musicVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MUSIC_VOLUME ); }
			foreach ( Text t in m_effectsVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.EFFECTS_VOLUME ); }
			foreach ( Text t in m_voicesVolume ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.VOICES_VOLUME ); }
			foreach ( Text t in m_muteSound ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MUTE ); }

			foreach ( Text t in m_mouseSensibilityX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MOUSE_SENSIBILITY_X ); }
			foreach ( Text t in m_mouseSensibilityY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.MOUSE_SENSIBILITY_Y ); }
			foreach ( Text t in m_joystickSensibilityX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.JOYSTICK_SENSIBILITY_X ); }
			foreach ( Text t in m_joystickSensibilityY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.JOYSTICK_SENSIBILITY_Y ); }
			foreach ( Text t in m_invertX ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.INVERT_X ); }
			foreach ( Text t in m_invertY ) { t.text = GameLocalizedStringManager.Instance.Get( Strings.INVERT_Y ); }


		}
	}
}
