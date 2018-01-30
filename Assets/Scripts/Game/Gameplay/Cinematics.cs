/*
 * LICENCE
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
	[System.Serializable]
	public class Cinematics : MonoBehaviour
	{
		[SerializeField]
		private Image m_background;
		[SerializeField]
		private TMPro.TextMeshProUGUI m_caravanText;
		[SerializeField]
		private TMPro.TextMeshProUGUI m_characterText;
		[SerializeField]
		private float m_textFadeSpeed = 1.0f;

		private static Cinematics s_instance = null;

		private bool m_startCinematicFinished = false;
		private bool m_chapter1to2TransitionFinished = false;
		private bool m_chapter2to3TransitionFinished = false;
		private bool m_endCinematicFinished = false;
		private bool m_startCinematicPlaying = false;
		private bool m_chapter1to2TransitionPlaying = false;
		private bool m_chapter2to3TransitionPlaying = false;
		private bool m_endCinematicPlaying = false;
		private bool m_playing = false;

		private Engine.InputAction m_interaction = null;

		public static Cinematics Instance { get { return s_instance; } }
		public bool StartCinematicFinished { get { return m_startCinematicFinished; } }
		public bool Chapter1to2TransitionFinished { get { return m_chapter1to2TransitionFinished; } }
		public bool Chapter2to3TransitionFinished { get { return m_chapter2to3TransitionFinished; } }
		public bool EndCinematicFinished { get { return m_endCinematicFinished; } }
		public bool StartCinematicPlaying { get { return m_startCinematicPlaying; } }
		public bool Chapter1to2TransitionPlaying { get { return m_chapter1to2TransitionPlaying; } }
		public bool Chapter2to3TransitionPlaying { get { return m_chapter2to3TransitionPlaying; } }
		public bool EndCinematicPlaying { get { return m_endCinematicPlaying; } }
		public bool Playing { get { return m_playing; } }

		void Start()
		{
			s_instance = this;
			Debug.Assert( m_background );
			Debug.Assert( m_caravanText );
			Debug.Assert( m_characterText );

			m_interaction = Engine.InputManager.Instance.GetAction( "Interaction" );
		}

		private IEnumerator SetText( TMPro.TextMeshProUGUI _target, string _text )
		{
			_target.text = _text;
			float time = Time.time;
			float targetTime = Time.time + m_textFadeSpeed;
			while ( Time.time < targetTime )
			{
				_target.color = Color32.Lerp( Color.black, Color.white, ( Time.time - time ) / m_textFadeSpeed );
				yield return null;
			}
			_target.color = Color.white;
			while ( !m_interaction.Down ) { yield return null; }
			time = Time.time;
			targetTime = Time.time + m_textFadeSpeed;
			while ( Time.time < targetTime )
			{
				_target.color = Color32.Lerp( Color.white, Color.black, ( Time.time - time ) / m_textFadeSpeed );
				yield return null;
			}
		}

		public IEnumerator BeginStartCinematic()
		{
			if ( m_startCinematicFinished )
			{
				yield return null;
			}
			m_playing = true;
			m_startCinematicPlaying = true;
			m_background.gameObject.SetActive( true );
			m_caravanText.gameObject.SetActive( true );
			m_characterText.gameObject.SetActive( true );

			m_background.color = Color.black;
			m_caravanText.color = Color.black;
			m_characterText.color = Color.black;

			yield return new WaitForSeconds( 2.0f );
			yield return SetText( m_caravanText, GameLocalizedStringManager.Instance.Get( Strings.CINEMATIC_INTRO_1 ) );
			yield return new WaitForSeconds( 0.25f );
			yield return SetText( m_caravanText, GameLocalizedStringManager.Instance.Get( Strings.CINEMATIC_INTRO_2 ) );
			yield return new WaitForSeconds( 0.25f );
			yield return SetText( m_caravanText, GameLocalizedStringManager.Instance.Get( Strings.CINEMATIC_INTRO_3 ) );
			yield return new WaitForSeconds( 1.0f );
			yield return SetText( m_characterText, GameLocalizedStringManager.Instance.Get( Strings.CINEMATIC_INTRO_4 ) );
			yield return new WaitForSeconds( 0.25f );
			yield return SetText( m_characterText, GameLocalizedStringManager.Instance.Get( Strings.CINEMATIC_INTRO_5 ) );
			yield return new WaitForSeconds( 2.0f );
			m_caravanText.gameObject.SetActive( false );
			m_characterText.gameObject.SetActive( false );

			float time = Time.time;
			float targetTime = Time.time + 2.0f;
			Color endColor = new Color( 0.0f, 0.0f, 0.0f, 0.0f );
			while ( Time.time < targetTime )
			{
				m_background.color = Color32.Lerp( Color.black, endColor, ( Time.time - time ) / m_textFadeSpeed );
				yield return null;
			}
			m_background.gameObject.SetActive( false );

			m_playing = false;
			m_startCinematicPlaying = false;
			m_startCinematicFinished = true;
		}

		public IEnumerator BeginChapter1To2Transition()
		{
			if ( m_chapter1to2TransitionFinished )
			{
				yield return null;
			}
		}

		public IEnumerator BeginChapter2To3Transition()
		{
			if ( m_chapter2to3TransitionFinished )
			{
				yield return null;
			}
		}

		public IEnumerator BeginEndCinematic()
		{
			if ( m_endCinematicFinished )
			{
				yield return null;
			}
		}
	}
}
