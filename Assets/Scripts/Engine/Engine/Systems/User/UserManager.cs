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
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace Engine
{
	[System.Serializable]
	public class UserManager : MonoBehaviour, ISerializeRW
	{
		#region Members
		private int m_currentUser = -1;
		private List<User> m_users = null;
		private static UserManager s_instance = null;
		#endregion

		#region Properties
		public static UserManager Instance
		{
			get { return s_instance; }
		}
		public User Current
		{
			get
			{
				if ( m_currentUser >= 0 && m_currentUser < m_users.Count )
				{
					return m_users[ m_currentUser ];
				}
				return null;
			}
		}
		#endregion

		#region Methods
		void Awake()
		{
			if ( s_instance != null )
			{
				Destroy( this );
				return;
			}
			DontDestroyOnLoad( this );
			s_instance = this;

			m_users = new List<User>();

			string userFile = Application.persistentDataPath + "/users.sav";
			if ( File.Exists( userFile ) )
			{
				FileStream file = File.Open( userFile, FileMode.Open );
				CryptoStream stream = new CryptoStream( file, new Encrypter( 8081 ), CryptoStreamMode.Read );
				BinaryReader reader = new BinaryReader( stream );
				SerializeR( reader );
				reader.Close();
			}
			else
			{
				Create( User.kGlobalName, true );
				Directory.CreateDirectory( Application.persistentDataPath );
				FileStream file = File.Create( userFile );
				CryptoStream stream = new CryptoStream( file, new Encrypter( 8081 ), CryptoStreamMode.Write );
				BinaryWriter writer = new BinaryWriter( stream );
				SerializeW( writer );
				writer.Close();
			}
		}
		public User Create( string _userName, bool _overrideCurrent )
		{
			User u = User.New( _userName );
			if ( _overrideCurrent )
			{
				m_currentUser = m_users.Count;
			}
			m_users.Add( u );

			return u;
		}
		public void SerializeR( BinaryReader _reader )
		{
			if ( _reader == null )
			{
				return;
			}
			m_users = new List<User>();
			m_currentUser = _reader.ReadInt32();
			int userCount = _reader.ReadInt32();
			for ( int i = 0; i < userCount; ++i )
			{
				User u = new User();
				u.SerializeR( _reader );
				m_users.Add( u );
			}
		}
		public void SerializeW( BinaryWriter _writer )
		{
			_writer.Write( m_currentUser );
			_writer.Write( m_users.Count );
			for ( int i = 0; i < m_users.Count; ++i )
			{
				m_users[ i ].SerializeW( _writer );
			}
		}
		public bool Load()
		{
			string userFile = Application.persistentDataPath + "/users.sav";
			if ( File.Exists( userFile ) )
			{
				FileStream file = File.Open( userFile, FileMode.Open );
				CryptoStream stream = new CryptoStream( file, new Encrypter( 8081 ), CryptoStreamMode.Read );
				BinaryReader reader = new BinaryReader( stream );
				SerializeR( reader );
				reader.Close();
				return true;
			}
			return false;
		}
		public void Save()
		{
			string userFile = Application.persistentDataPath + "/users.sav";
			if ( m_users.Count == 0 )
			{
				Create( User.kGlobalName, true );
			}
			Directory.CreateDirectory( Application.persistentDataPath );
			FileStream file = File.Create( userFile );
			CryptoStream stream = new CryptoStream( file, new Encrypter( 8081 ), CryptoStreamMode.Write );
			BinaryWriter writer = new BinaryWriter( stream );
			SerializeW( writer );
			writer.Close();
		}

		public string[] GetNames()
		{
			string[] result = new string[ m_users.Count ];
			int less = 0;
			for ( int i = 0; i < m_users.Count; ++i )
			{
				if ( m_users[ i ].Name == User.kGlobalName )
				{
					++less;
				}
				else
				{
					result[ i - less ] = m_users[ i ].Name;
				}
			}
			System.Array.Resize( ref result, result.Length - less );
			return result;
		}

		public bool SetUserByName( string _name )
		{
			for ( int i = 0; i < m_users.Count; ++i )
			{
				if ( m_users[ i ].Name == _name )
				{
					m_currentUser = i;
					return true;
				}
			}
			return false;
		}
		#endregion
	}
}
