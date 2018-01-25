/*
 * LICENCE
 */
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	[System.Serializable]
	public class TextSwitcherGroup : MonoBehaviour
	{
		[SerializeField]
		private Text[] m_texts;

		public void SetText( int _index )
		{
			if ( m_texts == null )
			{
				return;
			}
			for( int i = 0; i < m_texts.Length; ++i )
			{
				m_texts[ i ].gameObject.SetActive( ( _index == i ) );
			}
		}
	}
}
