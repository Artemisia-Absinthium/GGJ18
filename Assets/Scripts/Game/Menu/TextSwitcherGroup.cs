/*
 * LICENCE
 */
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	[System.Serializable]
	[RequireComponent( typeof( CanvasRenderer ) )]
	public class TextSwitcherGroup : Graphic
	{
		[SerializeField]
		private TMPro.TextMeshProUGUI[] m_texts;

		private CanvasRenderer[] m_renderers;
		private Color m_lastColor;
		private CanvasRenderer m_renderer;

		protected override void Start()
		{
			m_renderer = GetComponent<CanvasRenderer>();
			rectTransform.localScale = Vector3.zero;
			m_lastColor = m_renderer.GetColor();
			m_renderers = new CanvasRenderer[ m_texts.Length ];
			for ( int i = 0; i < m_texts.Length; ++i )
			{
				if ( m_texts[ i ] != null )
				{
					m_renderers[ i ] = m_texts[ i ].GetComponent<CanvasRenderer>();
					m_renderers[ i ].SetColor( m_lastColor );
				}
			}
		}

		public void SetText( int _index )
		{
			if ( m_texts == null )
			{
				return;
			}
			for ( int i = 0; i < m_texts.Length; ++i )
			{
				m_texts[ i ].gameObject.SetActive( _index == i );
			}
		}

		void Update()
		{
			Color newColor = m_renderer.GetColor();
			m_lastColor = newColor;
			foreach ( CanvasRenderer cv in m_renderers )
			{
				if ( cv != null )
				{
					cv.SetColor( m_lastColor );
				}
			}
		}
	}
}
