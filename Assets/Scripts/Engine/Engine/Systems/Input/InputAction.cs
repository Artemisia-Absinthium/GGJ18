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
using System.Runtime.InteropServices;
using UnityEngine;

namespace Engine
{
	[System.Serializable]
	[ComVisible( true )]
	public class InputAction<T>
	{
		#region Fields
		[SerializeField]
		[Tooltip("The id of the input action")]
		private T m_id = default( T );
		[SerializeField]
		[Tooltip("The keyboard code associated with this action")]
		private KeyCode m_keyboardKey = KeyCode.None;
		[SerializeField]
		[Tooltip("The joystick button or axis associated with this action")]
		private InputJoystick m_joystickInput = new InputJoystick();
		[SerializeField]
		[Tooltip("The mode of update of this input action")]
		private InputUpdatePolicy m_policy = InputUpdatePolicy.KEYBOARD_AND_JOYSTICK;
		#endregion

		#region Members
		private bool m_state = false;
		private bool m_down = false;
		private bool m_up = false;
		private bool m_fromKeyboard = false;
		private bool m_fromJoystick = false;
		#endregion

		#region Properties
		public T ID
		{
			get { return m_id; }
			set { m_id = value; }
		}
		public KeyCode KeyboardCode
		{
			get { return m_keyboardKey; }
			set { m_keyboardKey = value; }
		}
		public bool State
		{
			get { return m_state; }
		}
		public bool Down
		{
			get { return m_down; }
		}
		public bool Up
		{
			get { return m_up; }
		}
		public bool FromKeyboard
		{
			get { return m_fromKeyboard; }
		}
		public bool FromJoystick
		{
			get { return m_fromJoystick; }
		}
		public InputUpdatePolicy UpdatePolicy
		{
			get { return m_policy; }
			set { m_policy = value; }
		}
		public bool JoystickIsAxis
		{
			get { return m_joystickInput.m_isAxis; }
			set { m_joystickInput.m_isAxis = value; }
		}
		public KeyCode JoystickButton
		{
			get { return m_joystickInput.m_button; }
			set { m_joystickInput.m_button = value; }
		}
		public string JoystickAxis
		{
			get { return m_joystickInput.m_axis; }
			set { m_joystickInput.m_axis = value; }
		}
		public bool JoystickAxisIsPositive
		{
			get { return m_joystickInput.m_isAxisPositive; }
			set { m_joystickInput.m_isAxisPositive = value; }
		}
		public float JoystickAxisDeadZone
		{
			get { return m_joystickInput.m_axisDeadZone; }
			set { m_joystickInput.m_axisDeadZone = value; }
		}
		public float Raw
		{
			get { return m_joystickInput.m_rawAxis; }
		}
		#endregion

		#region Methods
		public void Update()
		{
			Debug.Assert( typeof( T ).IsEnum );
			switch ( m_policy )
			{
			case InputUpdatePolicy.ONLY_KEYBOARD:
				{
					m_state = Input.GetKey( m_keyboardKey );
					m_down = Input.GetKeyDown( m_keyboardKey );
					m_up = Input.GetKeyUp( m_keyboardKey );
					m_joystickInput.m_rawAxis = 0.0f;
					m_fromKeyboard = m_state;
					m_fromJoystick = false;
				}
				break;
			case InputUpdatePolicy.ONLY_JOYSTICK:
				{
					if ( m_joystickInput.m_isAxis )
					{
						m_joystickInput.m_rawAxis = Input.GetAxis( m_joystickInput.m_axis );
						bool newState = m_joystickInput.m_isAxisPositive
							? m_joystickInput.m_rawAxis >= m_joystickInput.m_axisDeadZone
							: m_joystickInput.m_rawAxis <= -m_joystickInput.m_axisDeadZone;
						m_down = newState && !m_state;
						m_up = m_state && !newState;
						m_state = newState;
					}
					else
					{
						m_state = Input.GetKey( m_joystickInput.m_button );
						m_down = Input.GetKeyDown( m_joystickInput.m_button );
						m_up = Input.GetKeyUp( m_joystickInput.m_button );
						m_joystickInput.m_rawAxis = 0.0f;
					}
					m_fromJoystick = m_state;
					m_fromKeyboard = false;
				}
				break;
			case InputUpdatePolicy.KEYBOARD_AND_JOYSTICK:
				{
					bool stateK = Input.GetKey( m_keyboardKey );
					bool stateJ;
					if ( m_joystickInput.m_isAxis )
					{
						m_joystickInput.m_rawAxis = Input.GetAxis( m_joystickInput.m_axis );
						stateJ = m_joystickInput.m_isAxisPositive
							? m_joystickInput.m_rawAxis >= m_joystickInput.m_axisDeadZone
							: m_joystickInput.m_rawAxis <= -m_joystickInput.m_axisDeadZone;
					}
					else
					{
						stateJ = Input.GetKey( m_joystickInput.m_button );
						m_joystickInput.m_rawAxis = 0.0f;
					}
					bool newState = stateK || stateJ;
					m_down = newState && !m_state;
					m_up = !newState && m_state;
					m_state = newState;
					m_fromKeyboard = stateK;
					m_fromJoystick = stateJ;
				}
				break;
			}
		}
		#endregion
	}
}
