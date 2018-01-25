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
using UnityEditor;
using System.IO;
using System.Security.Cryptography;

/*
 * How to use:
 * 
 * // Put this in an editor script, replacing MyStringEnum
 * // by the enumeration of all of your strings
 * 
 * public class MyLocalizedStringBuilder : Engine.LocalizedStringBuilder<MyStringEnum>
 * {
 *     [MenuItem( "Engine/Localized strings" )]
 *     public static void Open()
 *     {
 *         OpenWindow<MyLocalizedStringBuilder>();
 *     }
 * }
 */

namespace Engine
{
	[System.Serializable]
	public abstract class LocalizedStringBuilder<T> : EditorWindow
	{
		// Change me to save as binary or as text
		private const bool kSaveBinary = false;
		private const bool kLoadBinary = false;

		private const string kTempPath = "Assets/Temp/LocalizedStrings/";
		private const int kStringsPerWindow = 20;

		private Vector2 m_scrollPosition = new Vector2();
		private int m_currentWindow = 0;
		private int m_windowCount = 0;

		private string[][] m_array = null;
		private bool[] m_folders = null;
		
		private static int s_count = 0;

		//[MenuItem( "Engine/Localized strings" )]
		public static void OpenWindow<U>() where U : LocalizedStringBuilder<T>
		{
			Directory.CreateDirectory( kTempPath );
			Directory.CreateDirectory( LocalizedStringManager<T>.kResourcesPath );

			s_count = ( int )System.Enum.Parse( typeof( T ), "COUNT" );
			LocalizedStringBuilder<T> instance = GetWindow<U>();
			instance.titleContent = new GUIContent( "Localized strings" );
			instance.Refresh();
		}

		private void Refresh()
		{
			if ( m_array == null )
			{
				Reload();
			}
		}
		private void InitArray()
		{
			m_array = new string[ s_count ][];
			m_folders = new bool[ kStringsPerWindow ];
			for ( int lang = 0; lang < s_count; ++lang )
			{
				m_array[ lang ] = new string[ ( int )LocalizedString.Lang.COUNT ];
				for ( int str = 0; str < ( int )LocalizedString.Lang.COUNT; ++str )
				{
					m_array[ lang ][ str ] = "";
				}
			}
		}
		private void Reload()
		{
			InitArray();
			m_windowCount = s_count / kStringsPerWindow + 1;
			for ( int lang = 0; lang < ( int )LocalizedString.Lang.COUNT; ++lang )
			{
				string path = kTempPath + System.Enum.GetName( typeof( LocalizedString.Lang ), ( ( LocalizedString.Lang )lang ) ) + ".str";
				if ( kLoadBinary )
				{
					BinLoad( lang, path );
				}
				else
				{
					TxtLoad( lang, path );
				}
			}
		}
		private void Save()
		{
			for ( int lang = 0; lang < ( int )LocalizedString.Lang.COUNT; ++lang )
			{
				string path = kTempPath + System.Enum.GetName( typeof( LocalizedString.Lang ), ( ( LocalizedString.Lang )lang ) ) + ".str";
				if ( kSaveBinary )
				{
					BinSave( lang, path );
				}
				else
				{
					TxtSave( lang, path );
				}
			}
			AssetDatabase.Refresh( ImportAssetOptions.ForceUpdate );
		}
		private void BinSave( int _lang, string _path)
		{
			FileStream fileStream = new FileStream( _path, FileMode.Create );
			BinaryWriter writer = new BinaryWriter( fileStream );
			writer.Write( 'B' );
			writer.Write( 'I' );
			writer.Write( 'N' );
			writer.Write( ( long )m_array.Length );
			for ( int str = 0; str < m_array.Length; ++str )
			{
				writer.Write( System.Enum.GetName( typeof( T ), str ) );
				if ( m_array[ str ][ _lang ] == null )
				{
					writer.Write( "" );
				}
				else
				{
					writer.Write( m_array[ str ][ _lang ] );
				}
			}
			writer.Close();
		}
		private void BinLoad( int _lang, string _path )
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream( _path, FileMode.Open );
			}
			catch ( System.Exception e )
			{
				Debug.Log( "Unable to open file " + _path + ":\n" + e.Message );
				return;
			}
			BinaryReader reader = new BinaryReader( fileStream );
			byte[] type = reader.ReadBytes( 3 );
			if ( type[ 0 ] != 'B' || type[ 1 ] != 'I' || type[ 2 ] != 'N')
			{
				Debug.Log( "The localized string file is not in binary format!" );
				reader.Close();
				return;
			}
			long length = reader.ReadInt64();
			for ( int i = 0; i < length; ++i )
			{
				string key = reader.ReadString();
				string value = reader.ReadString();
				try
				{
					int index = ( int )System.Enum.Parse( typeof( T ), key );
					m_array[ index ][ _lang ] = value;
				}
				catch
				{
					Debug.Log( "Warning! The string " + key + " is no more a part of the enum..." );
					Debug.Log( "If you want to recover it, read this value to the " + typeof( T ).Name + " enum" );
				}
			}
			reader.Close();
		}
		private void TxtSave( int _lang, string _path )
		{
			FileStream fileStream = new FileStream( _path, FileMode.Create );
			StreamWriter writer = new StreamWriter( fileStream );
			writer.WriteLine( "TXT" );
			for ( int str = 0; str < m_array.Length; ++str )
			{
				writer.Write( System.Enum.GetName( typeof( T ), str ) + ":" );
				if ( m_array[ str ][ _lang ] == null )
				{
					writer.Write( "\"\"" );
				}
				else
				{
					string correctString = "\"" + m_array[ str ][ _lang ].Replace( "\"", "\\\"" ) + "\"";
					writer.WriteLine( correctString );
				}
			}
			writer.Close();
		}
		private void TxtLoad( int _lang, string _path )
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream( _path, FileMode.Open );
			}
			catch ( System.Exception e )
			{
				Debug.Log( "Unable to open file " + _path + ":\n" + e.Message );
				return;
			}
			StreamReader reader = new StreamReader( fileStream );
			string type = reader.ReadLine();
			if ( type != "TXT" )
			{
				Debug.Log( "The localized string file is not in text format!" );
				reader.Close();
				return;
			}
			int lineNumber = 0;
			while ( !reader.EndOfStream )
			{
				++lineNumber;
				string line = reader.ReadLine().Trim();
				if ( line.Length == 0 )
				{
					continue;
				}
				int indexSeparator = line.IndexOf( ':' );
				if ( indexSeparator < 0 )
				{
					Debug.Log( "Error: Syntax error, missing ':' separator line " + lineNumber );
					reader.Close();
					return;
				}
				string key = line.Substring( 0, indexSeparator );
				string value = line.Substring( indexSeparator + 1 );
				value = value.Substring( 1, value.Length - 2 );
				try
				{
					int index = ( int )System.Enum.Parse( typeof( T ), key );
					m_array[ index ][ _lang ] = value;
				}
				catch
				{
					Debug.Log( "Warning! The string " + key + " is no more a part of the enum..." );
					Debug.Log( "If you want to recover it, read this value to the " + typeof( T ).Name + " enum" );
				}
			}
			reader.Close();
		}
		private void Export()
		{
			byte[][] bytes = new byte[ ( int )LocalizedString.Lang.COUNT ][];
			for ( int lang = 0; lang < ( int )LocalizedString.Lang.COUNT; ++lang )
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter writer = new BinaryWriter( memoryStream );
				for ( int str = 0; str < m_array.Length; ++str )
				{
					writer.Write( m_array[ str ][ lang ] );
				}
				memoryStream.Flush();
				bytes[ lang ] = new byte[ memoryStream.Length ];
				System.Array.Copy( memoryStream.GetBuffer(), bytes[ lang ], memoryStream.Length );
				writer.Close();
			}

			FileStream outputFile = new FileStream( LocalizedStringManager<T>.kBinPath, FileMode.OpenOrCreate );
			CryptoStream cryptoStream = new CryptoStream( outputFile, new Encrypter( LocalizedString.EncryptionKey ), CryptoStreamMode.Write );
			BinaryWriter outputWriter = new BinaryWriter( cryptoStream );
			outputWriter.Write( ( long )s_count );
			outputWriter.Write( ( long )LocalizedString.Lang.COUNT );
			long offset = 2 * sizeof( long ) * ( 1 + ( long )LocalizedString.Lang.COUNT );
			for ( int i = 0; i < bytes.Length; ++i )
			{
				outputWriter.Write( offset );
				outputWriter.Write( ( long )bytes[ i ].Length );
				offset += bytes[ i ].Length;
			}
			for ( int i = 0; i < bytes.Length; ++i )
			{
				outputWriter.Write( bytes[ i ] );
			}
			outputWriter.Close();
			AssetDatabase.Refresh( ImportAssetOptions.ForceUpdate );
		}

		void ResetFolders( bool _state = false )
		{
			if ( m_folders == null )
			{
				m_folders = new bool[ kStringsPerWindow ];
			}
			for ( int i = 0; i < kStringsPerWindow; ++i )
			{
				m_folders[ i ] = _state;
			}
		}

		void OnGUI()
		{
			Refresh();
			EditorGUILayout.BeginHorizontal();
			bool save = GUILayout.Button( "Save" );
			bool saveReload = GUILayout.Button( "Save & reload" );
			bool reload = GUILayout.Button( "Reload" );
			EditorGUILayout.EndHorizontal();

			if ( save || saveReload )
			{
				Save();
			}
			if ( saveReload || reload )
			{
				Reload();
			}

			if ( GUILayout.Button( "Export" ) )
			{
				Export();
			}

			GUILayoutOption prevNextWidth = GUILayout.Width( EditorGUIUtility.currentViewWidth / 4.0f - 4.75f );

			EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginDisabledGroup( m_currentWindow == 0 );
			if ( GUILayout.Button( "Previous 10", prevNextWidth ) )
			{
				if ( m_currentWindow != 0 )
				{
					ResetFolders();
				}
				m_currentWindow = Mathf.Max( 0, m_currentWindow - 10 );
			}
			if ( GUILayout.Button( "Previous", prevNextWidth ) )
			{
				if ( m_currentWindow != 0 )
				{
					ResetFolders();
				}
				m_currentWindow = Mathf.Max( 0, m_currentWindow - 1 );
			}
			EditorGUI.EndDisabledGroup();
			EditorGUI.BeginDisabledGroup( m_currentWindow == m_windowCount - 1 );
			if ( GUILayout.Button( "Next", prevNextWidth ) )
			{
				if ( m_currentWindow != m_currentWindow - 1 )
				{
					ResetFolders();
				}
				m_currentWindow = Mathf.Min( m_windowCount - 1, m_currentWindow + 1 );
			}
			if ( GUILayout.Button( "Next 10", prevNextWidth ) )
			{
				if ( m_currentWindow != m_currentWindow - 1 )
				{
					ResetFolders();
				}
				m_currentWindow = Mathf.Min( m_windowCount - 1, m_currentWindow + 10 );
			}
			EditorGUI.EndDisabledGroup();
			EditorGUILayout.EndHorizontal();

			if ( GUILayout.Button( "Fold / Unfold all " ) )
			{
				ResetFolders( !m_folders[ 0 ] );
			}

			m_scrollPosition = EditorGUILayout.BeginScrollView( m_scrollPosition );

			for ( int str = kStringsPerWindow * m_currentWindow; str < Mathf.Min( m_array.Length, kStringsPerWindow * ( m_currentWindow + 1 ) ); str++ )
			{
				m_folders[ str % kStringsPerWindow ] = EditorGUILayout.Foldout( m_folders[ str % kStringsPerWindow ], System.Enum.GetName( typeof( T ), str ) );
				if ( m_folders[ str % kStringsPerWindow ] )
				{
					for ( int lang = 0; lang < ( int )LocalizedString.Lang.COUNT; ++lang )
					{
						m_array[ str ][ lang ] = EditorGUILayout.TextField( System.Enum.GetName( typeof( LocalizedString.Lang ), lang ), m_array[ str ][ lang ] );
					}
				}
			}
			EditorGUILayout.Space();
			EditorGUILayout.EndScrollView();
		}
	}
}
