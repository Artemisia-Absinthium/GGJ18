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
using System.IO;
using System.Security.Cryptography;

namespace Engine
{
	[System.Serializable]
	public abstract class LocalizedStringManager<T> : MonoBehaviour
	{
		private string[] m_strings;
		private string[][] m_dlcs;
		public const string kResourcesPath = "Assets/Resources/";
		private const string kBinName = "localizedStrings";
		private static LocalizedStringManager<T> s_instance = null;
		private static int s_count = 0;
		public const string kBinPath = kResourcesPath + kBinName + ".bytes";

		public static LocalizedStringManager<T> Instance
		{
			get { return s_instance; }
		}

		public string Get<U>( U _index ) where U : struct, System.IConvertible
		{
			int index = System.Convert.ToInt32( _index );
			if ( index < 0 || m_strings == null )
			{
				return "Missing LocalizedString";
			}
			if ( index < m_strings.Length )
			{
				return m_strings[ index ];
			}
			return GetDLCString( index );
		}

		void Awake()
		{
			if ( s_instance != null )
			{
				Destroy( this );
				return;
			}
			DontDestroyOnLoad( this );
			s_instance = this;
			s_count = ( int )System.Enum.Parse( typeof( T ), "COUNT" );
			m_strings = new string[ s_count ];

			User currentUser = UserManager.Instance.Current;
			LocalizedString.Lang currentLang = LocalizedString.Lang.ENGLISH;
			if ( currentUser != null )
			{
				currentLang = UserGeneralOptions.SystemLanguageToLang( currentUser.Options.General.Language );
			}
			LoadLang( currentLang );
		}

		public void LoadLang( LocalizedString.Lang _lang )
		{
			if ( ( _lang < 0 ) || ( _lang >= LocalizedString.Lang.COUNT ) )
			{
				_lang = LocalizedString.Lang.ENGLISH;
			}
			TextAsset content = Resources.Load<TextAsset>( kBinName );
			if ( content == null )
			{
				Debug.Log( "Unable to load " + kBinName + ".bytes" );
				return;
			}
			MemoryStream memoryStream = new MemoryStream( content.bytes, false );
			CryptoStream cryptoStream = new CryptoStream( memoryStream, new Encrypter( LocalizedString.EncryptionKey ), CryptoStreamMode.Read );
			BinaryReader reader = new BinaryReader( cryptoStream );

			reader.ReadInt64();
			long langCount = reader.ReadInt64();
			long offset = 0;
			long baseOffset = 0;

			// Find offset
			for ( int i = 0; i < langCount; ++i )
			{
				long temp = reader.ReadInt64();
				reader.ReadInt64();
				if ( i == 0 )
				{
					baseOffset = temp;
				}
				if ( i == ( int )_lang )
				{
					offset = temp;
				}
			}

			// Move to offset by 1024 byte steps
			offset -= baseOffset;
			byte[] buffer = new byte[ 1024 ];
			while ( offset > 1024 )
			{
				offset -= reader.Read( buffer, 0, 1024 );
			}
			if ( offset != 0 )
			{
				reader.Read( buffer, 0, ( int )offset );
			}

			//Extract strings
			m_strings = new string[ s_count ];
			for ( int i = 0; i < s_count; ++i )
			{
				m_strings[ i ] = reader.ReadString();
			}

			reader.Close();
		}

		private string GetDLCString( int _index )
		{
			if ( m_dlcs != null )
			{
				int dlcIndex = ( int )( _index & 0xFFC00000 ) >> 22;
				if ( ( dlcIndex >= 0 ) &&
					( m_dlcs.Length > dlcIndex ) &&
					( m_dlcs[ dlcIndex ] != null ) )
				{
					int strIndex = _index & 0x003FFFFF;
					if ( ( strIndex >= 0 ) && ( strIndex < m_dlcs[ dlcIndex ].Length ) )
					{
						return m_dlcs[ dlcIndex ][ strIndex ];
					}
				}
			}
			return "Missing DLC LocalizedString";
		}

		public void AddDLCStrings( int _index, string[] _strings )
		{
			if ( m_dlcs == null )
			{
				m_dlcs = new string[ 1000 ][];
				if ( _index >= 0 && _index < 1000 )
				{
					m_dlcs[ _index ] = new string[ _strings.Length ];
					System.Array.Copy( _strings, m_dlcs[ _index ], _strings.Length );
				}
			}
		}
	}
}
