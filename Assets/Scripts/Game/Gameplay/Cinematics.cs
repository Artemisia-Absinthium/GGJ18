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

		private Engine.InputAction m_interaction = null;

		public static Cinematics Instance { get { return s_instance; } }
		public bool StartCinematicFinished { get { return m_startCinematicFinished; } }
		public bool Chapter1to2TransitionFinished { get { return m_chapter1to2TransitionFinished; } }
		public bool Chapter2to3TransitionFinished { get { return m_chapter2to3TransitionFinished; } }
		public bool EndCinematicFinished { get { return m_endCinematicFinished; } }

		void Start()
		{
			s_instance = this;
			Debug.Assert( m_background );
			Debug.Assert( m_caravanText );
			Debug.Assert( m_characterText );

			m_interaction = Engine.InputManager.Instance.GetAction( "Interaction" );
		}

		void Update()
		{

		}

		public IEnumerator BeginCinematic()
		{
			if ( m_startCinematicFinished )
			{
				yield return null;
			}
			m_background.gameObject.SetActive( true );
			m_caravanText.gameObject.SetActive( true );
			m_characterText.gameObject.SetActive( true );

			m_background.color = Color.black;
			m_caravanText.color = Color.black;
			m_characterText.color = Color.black;

			yield return new WaitForSeconds( 2.0f );

			m_caravanText.text = GameLocalizedStringManager.Instance.Get( Strings.CINEMATIC_INTRO_1 );
			float time = Time.time;
			float targetTime = Time.time + m_textFadeSpeed;
			while ( Time.time < targetTime )
			{
				m_caravanText.color = Color32.Lerp( Color.black, Color.white, ( Time.time - time ) / m_textFadeSpeed );
				yield return null;
			}
			m_caravanText.color = Color.white;
			while ( !m_interaction.Down ) { yield return null; }


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
