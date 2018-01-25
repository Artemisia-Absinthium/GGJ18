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

namespace Engine
{
	[System.Serializable]
	public class UserGeneralOptions : ISerializeRW
	{
		#region Members
		private LocalizedString.Lang m_language = LocalizedString.Lang.ENGLISH;
		private static SystemLanguage[] s_supportedLanguages =
		{
			SystemLanguage.English,
			SystemLanguage.French
		};
		#endregion

		#region Properties
		public SystemLanguage Language
		{
			get { return s_supportedLanguages[ ( int )m_language ]; }
			set
			{
				int index = System.Array.IndexOf( s_supportedLanguages, value );
				if ( index >= 0 )
				{
					m_language = ( LocalizedString.Lang )index;
				}
			}
		}
		public static SystemLanguage[] SupportedLanguages
		{
			get
			{
				SystemLanguage[] array = new SystemLanguage[ s_supportedLanguages.Length ];
				System.Array.Copy( s_supportedLanguages, array, s_supportedLanguages.Length );
				return array;
			}
		}
		#endregion

		#region Methods
		public void New()
		{
			m_language = LocalizedString.Lang.ENGLISH;
		}

		public void SerializeR( System.IO.BinaryReader _reader )
		{
			m_language = ( LocalizedString.Lang )_reader.ReadByte();
		}

		public void SerializeW( System.IO.BinaryWriter _writer )
		{
			_writer.Write( ( byte )m_language );
		}
		public static LocalizedString.Lang SystemLanguageToLang( SystemLanguage _language )
		{
			for ( int i = 0; i < s_supportedLanguages.Length; ++i )
			{
				if ( s_supportedLanguages[ i ] == _language )
				{
					return ( LocalizedString.Lang )i;
				}
			}
			return ( LocalizedString.Lang )( -1 );
		}
		#endregion
	}
}
