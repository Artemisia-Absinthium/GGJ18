/*
 * LICENCE
 */
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	[System.Serializable]
	public class CutSceneUIAdapter : MonoBehaviour, ICutSceneAdapterBase
	{
#pragma warning disable 649
		[SerializeField]
		private GameObject m_cutSceneMaster = null;
		[SerializeField]
		private UnityEngine.EventSystems.EventSystem m_eventSystem = null;

		[SerializeField]
		private Image m_leftSprite = null;
		[SerializeField]
		private Image m_rightSprite = null;
		[SerializeField]
		private Image m_centerSprite = null;

		[SerializeField]
		private TMPro.TextMeshProUGUI m_text = null;
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_choiceTexts = null;
		[SerializeField]
		private Button[] m_choiceButtons = null;
		[SerializeField]
		private string m_okActionName = null;
#pragma warning restore 649

		private bool m_okPressed = false;
		private int m_buttonPressed = -1;
		private Engine.InputAction m_okAction;

		void Start()
		{
			Debug.Assert( m_cutSceneMaster );
			Debug.Assert( m_eventSystem != null );
			Debug.Assert( m_leftSprite );
			Debug.Assert( m_rightSprite );
			Debug.Assert( m_centerSprite );
			Debug.Assert( m_text );
			Debug.Assert( m_choiceTexts != null );
			Debug.Assert( m_choiceTexts.Length >= 4 );
			foreach ( TMPro.TextMeshProUGUI t in m_choiceTexts )
			{
				Debug.Assert( t );
			}
			Debug.Assert( m_choiceButtons != null );
			Debug.Assert( m_choiceButtons.Length >= 4 );
			foreach ( Button b in m_choiceButtons )
			{
				Debug.Assert( b );
			}
			m_okAction = Engine.InputManager.Instance.GetAction( m_okActionName );
			Debug.Assert( m_okAction != null );
			SetChoices( null );
			m_cutSceneMaster.SetActive( false );
		}

		void Update()
		{
			m_okPressed = m_okPressed || m_okAction.Down;
		}

		public void OnClicButton( int _index )
		{
			m_buttonPressed = _index;
		}

		public void SetText( string _text, bool _forChoice )
		{
			m_text.text = _text;
			if ( _forChoice )
			{
				m_text.alignment = TMPro.TextAlignmentOptions.Top;
			}
			else
			{
				m_text.alignment = TMPro.TextAlignmentOptions.Center;
			}
		}

		public void SetSprite( Sprite _sprite, int _position )
		{
			switch ( _position )
			{
			case 0:
				if ( _sprite == null )
				{
					m_leftSprite.enabled = false;
				}
				else
				{
					m_leftSprite.enabled = true;
					m_leftSprite.sprite = _sprite;
					m_leftSprite.SetNativeSize();
					m_leftSprite.rectTransform.anchoredPosition = new Vector2( 300, 540 );
				}
				break;
			case 1:
				if ( _sprite == null )
				{
					m_rightSprite.enabled = false;
				}
				else
				{
					m_rightSprite.enabled = true;
					m_rightSprite.sprite = _sprite;
					m_rightSprite.SetNativeSize();
					m_rightSprite.rectTransform.anchoredPosition = new Vector2( -300, 540 );
				}
				break;
			case 2:
				if ( _sprite == null )
				{
					m_centerSprite.enabled = false;
				}
				else
				{
					m_centerSprite.enabled = true;
					m_centerSprite.sprite = _sprite;
					m_centerSprite.SetNativeSize();
					m_centerSprite.rectTransform.anchoredPosition = new Vector2( 0, 540 );
				}
				break;
			}
		}

		public void SetChoices( string[] _choices )
		{
			if ( _choices == null )
			{
				foreach ( TMPro.TextMeshProUGUI t in m_choiceTexts )
				{
					t.gameObject.SetActive( false );
				}
				foreach ( Button b in m_choiceButtons )
				{
					b.gameObject.SetActive( false );
				}
				return;
			}
			for ( int i = 0; i < _choices.Length; ++i )
			{
				m_choiceTexts[ i ].gameObject.SetActive( true );
				m_choiceTexts[ i ].text = _choices[ i ];
				m_choiceButtons[ i ].gameObject.SetActive( true );
			}
			if ( _choices.Length > 0 )
			{
				m_eventSystem.SetSelectedGameObject( m_choiceButtons[ 0 ].gameObject );
			}
		}

		public bool ReceiveChoice( out int _choice )
		{
			_choice = m_buttonPressed;
			return _choice >= 0;
		}

		public bool ReceiveOk()
		{
			return m_okPressed;
		}

		public void StartCutScene( string _name )
		{
			m_cutSceneMaster.SetActive( true );
			SetChoices( null );
			SetText( "", false );
			SetSprite( null, 0 );
			SetSprite( null, 1 );
			SetSprite( null, 2 );
		}

		public void EndCutScene()
		{
			m_cutSceneMaster.SetActive( false );
		}

		public void StartSnapshot( int _index )
		{
			m_okPressed = false;
			m_buttonPressed = -1;
			SetChoices( null );
		}

		public void EndSnapshot()
		{
			// Nothing to do
		}
	}
}
