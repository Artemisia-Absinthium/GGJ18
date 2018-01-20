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

namespace Engine
{
	[System.Serializable]
	public class User
	{
		#region Members
		private UserID m_ID;
		private string m_name;
		private UserOptions m_options;

		private static int s_instance = -1;
		private static List<User> s_users = null;

		public const string kGlobalName = "__internal_global";
		#endregion

		#region Properties
		public UserID ID
		{
			get { return m_ID; }
		}

		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		public UserOptions Options
		{
			get { return m_options; }
		}

		public static User Instance
		{
			get
			{
				if ( s_instance >= 0 && s_instance < s_users.Count )
				{
					return s_users[ s_instance ];
				}
				return null;
			}
		}
		#endregion

		#region Methods
		public static void Initialize()
		{
			if ( s_users == null )
			{
				s_users = new List<User>();
			}
		}

		public static void New( string _userName, bool _overrideCurrent )
		{
			User u = new User();
			u.m_ID = UserID.Generate( _userName );
			u.m_name = _userName;
			u.m_options = new UserOptions();
			u.m_options.New();
			if ( _overrideCurrent )
			{
				s_instance = s_users.Count;
			}
			s_users.Add( u );
		}

		public static void SerializeR( System.IO.BinaryReader _reader )
		{
			if ( _reader == null )
			{
				return;
			}
			s_instance = _reader.ReadInt32();
			int userCount = _reader.ReadInt32();
			for ( int i = 0; i < userCount; ++i )
			{
				User u = new User();
				u.m_ID = new UserID();
				u.m_ID.SerializeR( _reader );
				u.m_name = _reader.ReadString();
				u.m_options = new UserOptions();
				u.m_options.SerializeR( _reader );
				s_users.Add( u );
			}
		}

		public static void SerializeW( System.IO.BinaryWriter _writer )
		{
			_writer.Write( s_instance );
			_writer.Write( s_users.Count );
			for ( int i = 0; i < s_users.Count; ++i )
			{
				s_users[ i ].m_ID.SerializeW( _writer );
				_writer.Write( s_users[ i ].m_name );
				s_users[ i ].m_options.SerializeW( _writer );
			}
		}

		public static string[] GetNames()
		{
			string[] result = new string[ s_users.Count ];
			int less = 0;
			for( int i = 0; i < s_users.Count; ++i )
			{
				if ( s_users[ i ].m_name == kGlobalName )
				{
					++less;
				}
				else
				{
					result[ i - less ] = s_users[ i ].m_name;
				}
			}
			System.Array.Resize( ref result, result.Length - less );
			return result;
		}

		public static bool SetUserByName( string _name )
		{
			for ( int i = 0; i < s_users.Count; ++i )
			{
				if ( s_users[ i ].m_name == _name )
				{
					s_instance = i;
					return true;
				}
			}
			return false;
		}
		#endregion
	}
}
