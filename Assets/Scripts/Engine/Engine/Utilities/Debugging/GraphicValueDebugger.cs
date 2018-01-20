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

#pragma warning disable 414

namespace Engine
{
#pragma warning disable 649
	[System.Serializable]
	[AddComponentMenu( "Engine/Utilities/Debugging/Graphic Value Debugger" )]
	public class GraphicValueDebugger : MonoBehaviour
	{
		#region Fields
		[Header( "This is a debugging tool removed in build" )]
		[SerializeField]
		[Tooltip( "Name of the debugged value" )]
		private string m_valueName;
		[SerializeField]
		[Tooltip( "Max value of the input" )]
		private float m_max = 1.0f;
		[SerializeField]
		[Tooltip( "Min value of the input" )]
		private float m_min = 0.0f;
		[SerializeField]
		[Tooltip( "Position of the debugger" )]
		private Vector2 m_position = new Vector2( 10.0f, 10.0f );
		[SerializeField]
		[Tooltip( "Size of the debugger" )]
		private Vector2 m_size = new Vector2( 50.0f, 150.0f );
		[SerializeField]
		[Tooltip( "Color of the gauge" )]
		private Color m_color = Color.red;
		[SerializeField]
		[Tooltip( "Color of the gauge background" )]
		private Color m_backgroundColor = Color.white;
		[SerializeField]
		[Tooltip( "Color of the texte" )]
		private Color m_textColor = Color.black;
		#endregion
#if DEBUGGING
		#region Members
		private float m_value = 0.0f;
		private float m_percent = 0.0f;

		private static Texture2D s_rectTexture;
		private static GUIStyle s_rectStyle;
		#endregion

		#region Methods
		void Start()
		{
			if ( s_rectTexture == null )
			{
				s_rectTexture = new Texture2D( 1, 1 );
			}

			if ( s_rectStyle == null )
			{
				s_rectStyle = new GUIStyle();
			}
		}

		public static void Rectangle( Rect position, Color color )
		{
			s_rectTexture.SetPixel( 0, 0, color );
			s_rectTexture.Apply();

			s_rectStyle.normal.background = s_rectTexture;

			GUI.Box( position, GUIContent.none, s_rectStyle );
		}

		void OnGUI()
		{
			Rectangle( new Rect( m_position, m_size ), m_backgroundColor );
			Rectangle( new Rect( m_position + new Vector2( 0.0f, m_size.y * ( 1.0f - m_percent ) ), new Vector2( m_size.x, m_size.y * m_percent ) ), m_color );
			GUI.color = m_textColor;
			GUI.Label( new Rect( m_position + new Vector2( 5.0f, 5.0f ), m_size ), m_valueName );
			GUI.Label( new Rect( m_position + new Vector2( 5.0f, m_size.y - 20.0f ), m_size ), m_value.ToString() );
		}
		public void SetValue( float _value )
		{
			m_value = _value;
			float deltaSize = m_max - m_min;
			if ( deltaSize != 0.0f )
			{
				m_percent = Mathf.Clamp01( ( m_value - m_min ) / deltaSize );
			}
		}
		#endregion
#else
		#region Methods
		void Start()
		{
			Destroy(this);
		}
		public void SetValue(float _value)
		{

		}
		#endregion
#endif
	}
#pragma warning restore 649
}
