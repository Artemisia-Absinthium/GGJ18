﻿/*
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
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;

namespace Engine
{
#pragma warning disable 649
	[System.Serializable]
	[ComVisible( true )]
	public abstract class InputManager<T, U> : MonoBehaviour where T : InputAction<U>, new()
	{
		#region Fields
		[SerializeField]
		[Tooltip( "The list of actions of this manager" )]
		private T[] m_actions;
		[SerializeField]
		[Tooltip( "Horizontal axis for joystick camera movement" )]
		private string m_joystickHorizontalName = "";
		[SerializeField]
		[Tooltip( "Dead zone of the horizontal camera axis" )]
		private float m_joystickHorizontalDeadZone = 0.2f;
		[SerializeField]
		[Tooltip( "Is the horizontal axs inverted" )]
		private bool m_joystickHorizontalIsInverted = false;
		[SerializeField]
		[Tooltip( "Vertical axis for joystick camera movement" )]
		private string m_joystickVerticalName = "";
		[SerializeField]
		[Tooltip( "Dead zone of the vertical camera axis" )]
		private float m_joystickVerticalDeadZone = 0.2f;
		[SerializeField]
		[Tooltip( "Is the vertical axs inverted" )]
		private bool m_joystickVerticalIsInverted = false;
		[SerializeField]
		[TextArea( 5, 15 )]
		[Tooltip( "An xml representation of the configuration that override previously defined configurations" )]
		private string m_XMLConfiguration = null;
		#endregion

		#region Members
		private Vector2 m_mouseDelta;
		private Vector2 m_mousePosition;
		private Vector2 m_joystickDelta;

		private static InputManager<T, U> s_instance = null;
		#endregion

		#region Properties
		public static InputManager<T, U> Instance
		{
			get { return s_instance; }
		}
		#endregion

		#region Methods
		string GetStringValue( string _str )
		{
			if ( _str != null )
			{
				return _str;
			}
			return "";
		}
		float GetFloatValue( string _str )
		{
			float output = 0.0f;
			if ( _str != null )
			{
				try
				{
					output = Single.Parse( _str, System.Globalization.CultureInfo.InvariantCulture );
				}
				catch ( Exception _e )
				{
					Debug.LogError( _e );
				}
			}
			return output;
		}
		int GetIntValue( string _str )
		{
			int output = 0;
			if ( _str != null )
			{
				try
				{
					output = Int32.Parse( _str );
				}
				catch ( Exception _e )
				{
					Debug.LogError( _e );
				}
			}
			return output;
		}
		bool GetBoolValue( string _str )
		{
			return ( _str != null ) && ( _str == "true" );
		}

		void Awake()
		{
			if ( s_instance != null )
			{
				Destroy( this );
				return;
			}
			s_instance = this;
			DontDestroyOnLoad( this );
			if ( m_XMLConfiguration != null && m_XMLConfiguration.Length != 0 )
			{
				try
				{
					InputXMLParser.Inputs xmlInputs = null;
					XmlSerializer serializer = new XmlSerializer( typeof( InputXMLParser.Inputs ) );
					TextReader reader = new StringReader( m_XMLConfiguration );
					xmlInputs = ( InputXMLParser.Inputs )serializer.Deserialize( reader );
					if ( xmlInputs != null )
					{
						if ( xmlInputs.Actions != null )
						{
							int size = GetIntValue( xmlInputs.Actions.Size );
							if ( size > 0 && xmlInputs.Actions.Action.Count == size )
							{
								m_actions = new T[ size ];
								for ( int i = 0; i < size; ++i )
								{
									InputXMLParser.Action action = xmlInputs.Actions.Action[ i ];
									try
									{
										m_actions[ i ] = new T();
										m_actions[ i ].ID = ( U )Enum.Parse( typeof( U ), GetStringValue( action.Id ) );
										if ( !Enum.IsDefined( typeof( U ), m_actions[ i ].ID ) )
										{
											Debug.LogError( "Invalid ID \"" + action.Id + "\" in xml" );
										}
										m_actions[ i ].KeyboardCode = ( KeyCode )Enum.Parse( typeof( KeyCode ), GetStringValue( action.Keyboardkey ) );
										if ( !Enum.IsDefined( typeof( KeyCode ), m_actions[ i ].KeyboardCode ) )
										{
											Debug.LogError( "Invalid Keyboard key \"" + action.Keyboardkey + "\" in xml" );
										}
										m_actions[ i ].JoystickIsAxis = GetBoolValue( action.Joystick.IsAxis );
										if ( m_actions[ i ].JoystickIsAxis )
										{
											m_actions[ i ].JoystickAxis = GetStringValue( action.Joystick.Axis.Name );
											m_actions[ i ].JoystickAxisIsPositive = GetBoolValue( action.Joystick.Axis.Positive );
											m_actions[ i ].JoystickAxisDeadZone = GetFloatValue( action.Joystick.Axis.Dead );
										}
										else
										{
											m_actions[ i ].JoystickButton = ( KeyCode )Enum.Parse( typeof( KeyCode ), GetStringValue( action.Joystick.Button ) );
											if ( !Enum.IsDefined( typeof( KeyCode ), m_actions[ i ].JoystickButton ) )
											{
												Debug.LogError( "Invalid Joystick button \"" + action.Joystick.Button + "\" in xml" );
											}
										}
									}
									catch ( Exception _e )
									{
										Debug.LogError( _e );
									}
								}
							}
							else
							{
								Debug.LogError( "Actions informations size is not valide in xml" );
							}
						}
						else
						{
							Debug.LogError( "No actions informations provided in xml" );
						}
						if ( xmlInputs.Joysticks != null )
						{
							if ( xmlInputs.Joysticks.Horizontal != null )
							{
								m_joystickHorizontalName = GetStringValue( xmlInputs.Joysticks.Horizontal.Name );
								m_joystickHorizontalDeadZone = GetFloatValue( xmlInputs.Joysticks.Horizontal.Dead );
								m_joystickHorizontalIsInverted = GetBoolValue( xmlInputs.Joysticks.Horizontal.IsInverted );
							}
							else
							{
								Debug.LogError( "No horizontal joysticks informations provided in xml" );
							}
							if ( xmlInputs.Joysticks.Vertical != null )
							{
								m_joystickVerticalName = GetStringValue( xmlInputs.Joysticks.Vertical.Name );
								m_joystickVerticalDeadZone = GetFloatValue( xmlInputs.Joysticks.Vertical.Dead );
								m_joystickVerticalIsInverted = GetBoolValue( xmlInputs.Joysticks.Vertical.IsInverted );
							}
							else
							{
								Debug.LogError( "No vertical joysticks informations provided in xml" );
							}
						}
						else
						{
							Debug.LogError( "No joysticks informations provided in xml" );
						}
					}
				}
				catch ( Exception _e )
				{
					Debug.LogError( _e );
				}
			}
		}

		void Update()
		{
			for ( int i = 0; i < m_actions.Length; ++i )
			{
				if ( m_actions[ i ] != null )
				{
					m_actions[ i ].Update();
				}
			}
			m_mousePosition = Input.mousePosition;
			m_mouseDelta.x = Input.GetAxis( "Mouse X" );
			m_mouseDelta.y = Input.GetAxis( "Mouse Y" );
			m_joystickDelta.x = Input.GetAxis( m_joystickHorizontalName );
			m_joystickDelta.y = Input.GetAxis( m_joystickVerticalName );
			if ( Mathf.Abs( m_joystickDelta.x ) < m_joystickHorizontalDeadZone )
			{
				m_joystickDelta.x = 0.0f;
			}
			else if ( m_joystickHorizontalIsInverted )
			{
				m_joystickDelta.x = -m_joystickDelta.x;
			}
			if ( Mathf.Abs( m_joystickDelta.y ) < m_joystickVerticalDeadZone )
			{
				m_joystickDelta.y = 0.0f;
			}
			else if ( m_joystickVerticalIsInverted )
			{
				m_joystickDelta.y = -m_joystickDelta.y;
			}
		}

		public T GetAction( U _action )
		{
			int index = ( int )( object )_action;
			for ( int i = 0; i < m_actions.Length; ++i )
			{
				if ( ( m_actions[ i ] != null ) && ( ( int )( object )( m_actions[ i ].ID ) == index ) )
				{
					return m_actions[ i ];
				}
			}
			return null;
		}

		public Vector2 GetMouseDelta()
		{
			return m_mouseDelta;
		}

		public Vector2 GetJoystickDelta()
		{
			return m_joystickDelta;
		}

		public Vector2 GetMousePosition()
		{
			return m_mousePosition;
		}
		#endregion
	}
#pragma warning restore 649
}
