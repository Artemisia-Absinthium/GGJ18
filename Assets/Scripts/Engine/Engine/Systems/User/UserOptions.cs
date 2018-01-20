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
	public class UserOptions : ISerializeRW
	{
		#region Members
		private UserGeneralOptions m_generalOptions;
		private UserCommandOptions m_commandOptions;
		private UserGraphicOptions m_graphicOptions;
		private UserSoundOptions m_soundOptions;
		#endregion

		#region Properties
		public UserGeneralOptions General
		{
			get { return m_generalOptions; }
		}
		public UserCommandOptions Command
		{
			get { return m_commandOptions; }
		}
		public UserGraphicOptions Graphic
		{
			get { return m_graphicOptions; }
		}
		public UserSoundOptions Sound
		{
			get { return m_soundOptions; }
		}
		#endregion

		#region Methods
		public void New()
		{
			m_generalOptions = new UserGeneralOptions();
			m_commandOptions = new UserCommandOptions();
			m_graphicOptions = new UserGraphicOptions();
			m_soundOptions = new UserSoundOptions();
			m_generalOptions.New();
			m_commandOptions.New();
			m_graphicOptions.New();
			m_soundOptions.New();
		}

		public void SerializeR( System.IO.BinaryReader _reader )
		{
			m_generalOptions = new UserGeneralOptions();
			m_generalOptions.SerializeR( _reader );
			m_commandOptions = new UserCommandOptions();
			m_commandOptions.SerializeR( _reader );
			m_graphicOptions = new UserGraphicOptions();
			m_graphicOptions.SerializeR( _reader );
			m_soundOptions = new UserSoundOptions();
			m_soundOptions.SerializeR( _reader );
		}

		public void SerializeW( System.IO.BinaryWriter _writer )
		{
			m_generalOptions.SerializeW( _writer );
			m_commandOptions.SerializeW( _writer );
			m_graphicOptions.SerializeW( _writer );
			m_soundOptions.SerializeW( _writer );
		}
		#endregion
	}
}
