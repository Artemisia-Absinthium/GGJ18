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
	public class UserCommandOptions : ISerializeRW
	{
		#region Members
		private float m_mouseXSensibility = 0.5f;
		private float m_mouseYSensibility = 0.5f;
		private float m_joystickXSensibility = 0.5f;
		private float m_joystickYSensibility = 0.5f;
		private bool m_invertJoystickXAxis = false;
		private bool m_invertJoystickYAxis = false;
		#endregion

		#region Properties
		public float MouseXSensibility
		{
			get { return m_mouseXSensibility; }
			set { m_mouseXSensibility = 0.1f + 0.9f * Mathf.Clamp01( value ); }
		}
		public float MouseYSensibility
		{
			get { return m_mouseYSensibility; }
			set { m_mouseYSensibility = 0.1f + 0.9f * Mathf.Clamp01( value ); }
		}
		public float JoystickXSensibility
		{
			get { return m_joystickXSensibility; }
			set { m_joystickXSensibility = 0.1f + 0.9f * Mathf.Clamp01( value ); }
		}
		public float JoystickYSensibility
		{
			get { return m_joystickYSensibility; }
			set { m_joystickYSensibility = 0.1f + 0.9f * Mathf.Clamp01( value ); }
		}
		public bool InvertJoystickXAxis
		{
			get { return m_invertJoystickXAxis; }
			set { m_invertJoystickXAxis = value; }
		}
		public bool InvertJoystickYAxis
		{
			get { return m_invertJoystickYAxis; }
			set { m_invertJoystickYAxis = value; }
		}
		#endregion

		#region Methods
		public void New()
		{
			m_mouseXSensibility = 0.5f;
			m_mouseYSensibility = 0.5f;
			m_joystickXSensibility = 0.5f;
			m_joystickYSensibility = 0.5f;
			m_invertJoystickXAxis = false;
			m_invertJoystickYAxis = false;
		}

		public void SerializeR( System.IO.BinaryReader reader )
		{
			m_mouseXSensibility = reader.ReadSingle();
			m_mouseYSensibility = reader.ReadSingle();
			m_joystickXSensibility = reader.ReadSingle();
			m_joystickYSensibility = reader.ReadSingle();
			m_invertJoystickXAxis = reader.ReadBoolean();
			m_invertJoystickYAxis = reader.ReadBoolean();
		}

		public void SerializeW( System.IO.BinaryWriter writer )
		{
			writer.Write( m_mouseXSensibility );
			writer.Write( m_mouseYSensibility );
			writer.Write( m_joystickXSensibility );
			writer.Write( m_joystickYSensibility );
			writer.Write( m_invertJoystickXAxis );
			writer.Write( m_invertJoystickYAxis );
		}
		#endregion
	}
}