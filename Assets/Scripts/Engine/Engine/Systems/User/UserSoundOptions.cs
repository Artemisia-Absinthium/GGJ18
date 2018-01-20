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
	public class UserSoundOptions : ISerializeRW
	{
		#region Members
		private float m_generalVolume = 1.0f;
		private float m_musicVolume = 0.75f;
		private float m_effectsVolume = 0.8f;
		private float m_voicesVolume = 1.0f;
		private bool m_isMute = false;
		#endregion

		#region Properties
		public float GeneralVolume
		{
			get { return m_generalVolume; }
			set { m_generalVolume = Mathf.Clamp01( value ); }
		}
		public float MusicVolume
		{
			get { return m_musicVolume; }
			set { m_musicVolume = Mathf.Clamp01( value ); }
		}
		public float EffectsVolume
		{
			get { return m_effectsVolume; }
			set { m_effectsVolume = Mathf.Clamp01( value ); }
		}
		public float VoicesVolume
		{
			get { return m_voicesVolume; }
			set { m_voicesVolume = Mathf.Clamp01( value ); }
		}
		public bool IsMute
		{
			get { return m_isMute; }
			set { m_isMute = value; }
		}
		#endregion

		#region Methods
		public void New()
		{
			m_generalVolume = 1.0f;
			m_musicVolume = 0.75f;
			m_effectsVolume = 0.8f;
			m_voicesVolume = 1.0f;
			m_isMute = false;
		}

		public void SerializeR( System.IO.BinaryReader _reader )
		{
			m_generalVolume = _reader.ReadSingle();
			m_musicVolume = _reader.ReadSingle();
			m_effectsVolume = _reader.ReadSingle();
			m_voicesVolume = _reader.ReadSingle();
			m_isMute = _reader.ReadBoolean();
		}

		public void SerializeW( System.IO.BinaryWriter _writer )
		{
			_writer.Write( m_generalVolume );
			_writer.Write( m_musicVolume );
			_writer.Write( m_effectsVolume );
			_writer.Write( m_voicesVolume );
			_writer.Write( m_isMute );
		}
		#endregion
	}
}
