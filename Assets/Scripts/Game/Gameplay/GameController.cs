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

namespace Game
{
	[System.Serializable]
	public class GameController : MonoBehaviour, ICutSceneSupervisorBase
	{
		private static GameController s_instance = null;
		private CutScenePlayer m_csp = null;

		private int m_currentChapter = 1;
		private int m_cutSceneInstance = -1;

		private bool D1_P1_a1 = false;
		private bool haveWateringCan = false;
		private bool D1_P1_a3 = false;
		private bool D1_N5_c8 = false;
		private bool D1_B1_c4 = false;
		private bool D1_P1_b1 = false;
		private bool D1_M1_a2 = false;
		private bool D1_M2_a1 = false;
		private bool D1_M5_a1 = false;
		private bool D1_M5_c1 = false;
		private bool D1_N1_a1 = false;
		private bool D1_N4_a1 = false;
		private bool D1_N5_c2 = false;
		private bool D1_N5_c3 = false;
		private bool D1_N5_c5 = false;
		private bool D1_N5_c6 = false;
		private bool D1_M2_c1 = false;
		private bool D1_M2_c2 = false;
		private bool D1_M2_c9 = false;

		void Awake()
		{
			if ( s_instance != null )
			{
				Destroy( this );
				return;
			}
			DontDestroyOnLoad( this );
			s_instance = this;
		}

		public bool IsSpeaking { get { return m_cutSceneInstance >= 0; } }
		public static GameController Instance
		{
			get { return s_instance; }
		}

		void Start()
		{
			m_csp = CutScenePlayer.Instance;
			Debug.Assert( m_csp );
		}

		void Update()
		{
			if ( IsSpeaking )
			{
				if ( !m_csp.IsPlaying() )
				{
					m_cutSceneInstance = -1;
				}
			}
		}

		public void OnCharacterInteraction( Character.Name _name )
		{
			if ( IsSpeaking )
			{
				return;
			}
			switch ( _name )
			{
			case Character.Name.NOBODY: break;
			case Character.Name.PEASANT1_GRANDMOTHER: P1(); break;
			case Character.Name.PEASANT2_FATHER: P2(); break;
			case Character.Name.PEASANT3_COUSIN: P3(); break;
			case Character.Name.PEASANT4_NIECE: P4(); break;
			case Character.Name.PEASANT5_WORKER: P5(); break;
			case Character.Name.PEASANT6_CHILD: P6(); break;
			case Character.Name.RELIGIOUS0_DOOR: R0(); break;
			case Character.Name.RELIGIOUS1_COMMANDER: R1(); break;
			case Character.Name.RELIGIOUS2_TEACHER: R2(); break;
			case Character.Name.BARTENDER1_MOTHER: B1(); break;
			case Character.Name.BARTENDER2_CHILD1: B2(); break;
			case Character.Name.BARTENDER3_CHILD2: B3(); break;
			case Character.Name.BARTENDER4_DOG: B4(); break;
			case Character.Name.MEDICIN1_FATHER: M1(); break;
			case Character.Name.MEDECIN2_MOTHER: M2(); break;
			case Character.Name.MEDECIN3_DAUGHTER: M3(); break;
			case Character.Name.MEDECIN4_SON: M4(); break;
			case Character.Name.MEDECIN5_CHILD: M5(); break;
			case Character.Name.NOBLE1_GRANDMOTHER: N1(); break;
			case Character.Name.NOBLE2_GRANDFATHER: N2(); break;
			case Character.Name.NOBLE3_FATHER: N3(); break;
			case Character.Name.NOBLE4_SON: N4(); break;
			case Character.Name.NOBLE5_CHILD: N5(); break;
			}
		}

		private void P1()
		{
			if ( m_currentChapter == 1 )
			{
				m_cutSceneInstance = m_csp.Play( "D1_P1", this );
			}
		}

		private void P2()
		{
			if ( m_currentChapter == 1 )
			{
				m_cutSceneInstance = m_csp.Play( "D1_P2" );
			}
		}

		private void P3()
		{
			if ( m_currentChapter == 1 )
			{
				m_cutSceneInstance = m_csp.Play( "D1_P3" );
			}
		}

		private void P4()
		{
			if ( m_currentChapter == 1 )
			{
				m_cutSceneInstance = m_csp.Play( "D1_P4" );
			}
		}

		private void P5()
		{
			if ( m_currentChapter == 1 )
			{
				m_cutSceneInstance = m_csp.Play( "D1_P5" );
			}
		}

		private void P6()
		{
			if ( m_currentChapter == 1 )
			{
				m_cutSceneInstance = m_csp.Play( "D1_P6" );
			}
		}

		private void R0()
		{

		}

		private void R1()
		{

		}

		private void R2()
		{

		}

		private void B1()
		{

		}

		private void B2()
		{

		}

		private void B3()
		{

		}

		private void B4()
		{

		}

		private void M1()
		{

		}

		private void M2()
		{

		}

		private void M3()
		{

		}

		private void M4()
		{

		}

		private void M5()
		{

		}

		private void N1()
		{

		}

		private void N2()
		{

		}

		private void N3()
		{

		}

		private void N4()
		{

		}

		private void N5()
		{

		}

		public bool OnCutSceneTransitionOk( string _cutSceneName, int _previousSnapshot, int _newSnapshot, ref int _nextScene )
		{
			if ( _cutSceneName == "D1_P1" )
			{
				if ( _previousSnapshot == -1 )
				{
					if ( D1_P1_a1 )
					{
						_nextScene = 1;
						if ( haveWateringCan )
						{
							_nextScene = 2;
							haveWateringCan = false;
						}
						else if ( D1_N5_c8 )
						{
							_nextScene = 4;
							if ( D1_P1_b1 )
							{
								_nextScene = 5;
							}
						}
						else if ( D1_P1_a3 )
						{
							_nextScene = 3;
						}
						return true;
					}
				}
				else if ( _newSnapshot == -1 )
				{
					if ( _previousSnapshot == 0 )
					{
						D1_P1_a1 = true;
					}
					else if ( _previousSnapshot == 2 )
					{
						D1_P1_a3 = true;
					}
					else if ( _previousSnapshot == 4 )
					{
						D1_P1_b1 = true;
					}
				}
			}
			else if ( _cutSceneName == "D1_P2" )
			{
				if ( _previousSnapshot == -1 )
				{
					if ( D1_M2_c9 )
					{
						_nextScene = 1;
						return true;
					}
				}
			}
			return false;
		}

		public bool OnCutSceneTransitionTime( string _cutSceneName, int _previousSnapshot, int _newSnapshot, ref int _nextScene )
		{

			return false;
		}

		public bool OnCutSceneTransitionChoice( string _cutSceneName, int _previousSnapshot, int _newSnapshot, int _choiceIndex, ref int _nextScene )
		{

			return false;
		}
	}
}
