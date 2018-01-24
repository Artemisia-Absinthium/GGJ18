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
using UnityEngine;
using UnityEngine.UI;

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
	private Text m_text = null;
	[SerializeField]
	private Text[] m_choiceTexts = null;
	[SerializeField]
	private Button[] m_choiceButtons = null;
	[SerializeField]
	private string m_okActionName = null;
#pragma warning restore 649

	private bool m_okPressed = false;
	private int m_buttonPressed = -1;
	private Engine.InputAction m_okAction;

	private Color m_transparent;

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
		foreach ( Text t in m_choiceTexts )
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
		m_transparent = new Color( 0.0f, 0.0f, 0.0f, 0.0f );
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
			m_text.alignment = TextAnchor.UpperCenter;
		}
		else
		{
			m_text.alignment = TextAnchor.MiddleCenter;
		}
	}

	public void SetSprite( Sprite _sprite, int _position )
	{
		switch ( _position )
		{
		case 0:
			if ( _sprite == null )
			{
				m_leftSprite.color = m_transparent;
			}
			else
			{
				m_leftSprite.color = Color.white;
				m_leftSprite.sprite = _sprite;
				m_leftSprite.SetNativeSize();
				m_leftSprite.rectTransform.anchoredPosition = new Vector2( 300, 540 );
			}
			break;
		case 1:
			if ( _sprite == null )
			{
				m_rightSprite.color = m_transparent;
			}
			else
			{
				m_rightSprite.color = Color.white;
				m_rightSprite.sprite = _sprite;
				m_rightSprite.SetNativeSize();
				m_rightSprite.rectTransform.anchoredPosition = new Vector2( -300, 540 );
			}
			break;
		case 2:
			if ( _sprite == null )
			{
				m_centerSprite.color = m_transparent;
			}
			else
			{
				m_centerSprite.color = Color.white;
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
			foreach ( Text t in m_choiceTexts )
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
