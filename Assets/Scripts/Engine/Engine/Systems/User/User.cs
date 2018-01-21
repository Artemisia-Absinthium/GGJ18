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
		#endregion

		#region Methods
		public static User New( string _userName )
		{
			User u = new User();
			u.m_ID = UserID.Generate( _userName );
			u.m_name = _userName;
			u.m_options = new UserOptions();
			u.m_options.New();
			return u;
		}

		public void SerializeR( System.IO.BinaryReader _reader )
		{
			m_ID = new UserID();
			m_ID.SerializeR( _reader );
			m_name = _reader.ReadString();
			m_options = new UserOptions();
			m_options.SerializeR( _reader );
		}

		public void SerializeW( System.IO.BinaryWriter _writer )
		{
			m_ID.SerializeW( _writer );
			_writer.Write( m_name );
			m_options.SerializeW( _writer );
		}
		#endregion
	}
}
