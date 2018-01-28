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
		private bool m_customSpeakMusic = false;

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
		private bool D1_B1_d1 = false;
		private bool D1_P3_b1 = false;
		private bool D1_B1_c1 = false;
		private bool D1_R1_c2 = false;
		private bool D1_B1_c2 = false;
		private bool D1_B3_a2 = false;
		private bool D1_B2_a0 = false;
		private bool D1_B3_a1 = false;

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
					Camera c = GameCache.Instance.GetObject( GameCacheObjects.PlayerCamera ).GetComponent<Camera>();
					c.cullingMask = -1;
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
				StartCutscene( "D1_P1", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void P2()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_P2", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void P3()
		{
			if ( m_currentChapter == 1 )
			{
				if ( D1_B1_d1 && !D1_P3_b1 )
				{
					StartCutscene( "D1_P345", GameMusicManager.EGameMusicManagerState.eNone );
					D1_P3_b1 = true;
				}
				else
				{
					StartCutscene( "D1_P3", GameMusicManager.EGameMusicManagerState.eNone );
				}
			}
		}

		private void P4()
		{
			if ( m_currentChapter == 1 )
			{
				if ( D1_B1_d1 && !D1_P3_b1 )
				{
					StartCutscene( "D1_P345", GameMusicManager.EGameMusicManagerState.eNone );
					D1_P3_b1 = true;
				}
				else
				{
					StartCutscene( "D1_P4", GameMusicManager.EGameMusicManagerState.eNone );
				}
			}
		}

		private void P5()
		{
			if ( m_currentChapter == 1 )
			{
				if ( D1_B1_d1 && !D1_P3_b1 )
				{
					StartCutscene( "D1_P345", GameMusicManager.EGameMusicManagerState.eNone );
					D1_P3_b1 = true;
				}
				else
				{
					StartCutscene( "D1_P5", GameMusicManager.EGameMusicManagerState.eNone );
				}
			}
		}

		private void P6()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_P6", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void R0()
		{
			if ( m_currentChapter == 1 )
			{
				if ( !D1_N5_c8 || D1_B1_c1 )
				{
					StartCutscene( "D1_R0", GameMusicManager.EGameMusicManagerState.eNone );
				}
			}
		}

		private void R1()
		{
			if ( m_currentChapter == 1 )
			{
				if ( D1_N5_c8 )
				{
					StartCutscene( "D1_R1", GameMusicManager.EGameMusicManagerState.eNone );
				}
			}
		}

		private void R2()
		{
			if ( m_currentChapter == 1 )
			{
				if ( D1_N5_c8 )
				{
					StartCutscene( "D1_R2", GameMusicManager.EGameMusicManagerState.eNone );
				}
			}
		}

		private void B1()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_B1", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void B2()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_B3", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void B3()
		{
			if ( m_currentChapter == 1 )
			{
				D1_B2_a0 = true;
				StartCutscene( "D1_B3", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void B4()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_B4", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M1()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_M1", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M2()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_M2", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M3()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_M3", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M4()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_M4", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M5()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_M5", GameMusicManager.EGameMusicManagerState.eNone );
			}
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

		private void StartCutscene( string _name, GameMusicManager.EGameMusicManagerState _music )
		{
			m_cutSceneInstance = m_csp.Play( _name, this );
			if ( _music != GameMusicManager.EGameMusicManagerState.eNone )
			{
				GameMusicManager.Instance.ChangeMusic( _music );
				m_customSpeakMusic = true;
			}
			Camera c = GameCache.Instance.GetObject( GameCacheObjects.PlayerCamera ).GetComponent<Camera>();
			c.cullingMask = ~LayerMask.GetMask( "Characters" );
		}

		public bool OnCutSceneTransitionOk( string _cutSceneName, int _previousSnapshot, int _newSnapshot, ref int _nextScene )
		{
			if ( m_currentChapter == 1 )
			{
				switch ( _cutSceneName )
				{
				case "D1_P1":
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
					break;
				case "D1_P2":
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
					break;
				case "D1_P345":
					{
						if ( _previousSnapshot == -1 )
						{
							D1_P3_b1 = true;
						}
					}
					break;
				case "D1_P3":
				case "D1_P4":
				case "D1_P5":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_P3_b1 )
							{
								_nextScene = 1;
								return true;
							}
						}
					}
					break;
				case "D1_R0":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_B1_c1 )
							{
								_nextScene = 1;
								return true;
							}
						}
					}
					break;
				case "D1_R1":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_B1_c4 )
							{
								_nextScene = 1;
								if ( D1_R1_c2 )
								{
									_nextScene = 10;
									if ( D1_B1_c2 )
									{
										_nextScene = 11;
									}
								}
								return true;
							}
						}
					}
					break;
				case "D1_R2":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_B3_a2 )
							{
								_nextScene = 2;
								return true;
							}
						}
					}
					break;
				case "D1_B1":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_P1_b1 )
							{
								_nextScene = 2;
								if ( D1_B3_a2 )
								{
									_nextScene = 5;
								}
								return true;
							}
						}
					}
					break;
				case "D1_B3":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_B2_a0 )
							{
								_nextScene = 1;
								return true;
							}
							else
							{
								D1_B2_a0 = true;
							}
						}
					}
					break;
				case "D1_M1":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_M1_a2 )
							{
								_nextScene = 5;
								return true;
							}
						}
					}
					break;
				case "D1_M2":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_M2_a1 )
							{
								_nextScene = 3;
								if ( D1_M5_a1 || D1_M2_c1 || D1_M2_c2 )
								{
									_nextScene = 4;
								}
								return true;
							}
						}
					}
					break;
				case "D1_M4":
					{
						if ( _previousSnapshot == -1 )
						{
							_nextScene = ( ( int )( Time.time * 100 ) ) % 3;
							if ( _nextScene != 0 )
							{
								return true;
							}
						}
					}
					break;
				case "D1_M5":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_M5_a1 )
							{
								_nextScene = 6;
								if ( D1_M5_c1 )
								{
									_nextScene = 6;
								}
								else
								{
									_nextScene = 7;
									D1_M5_c1 = true;
								}
								return true;
							}
							else
							{
								D1_M5_a1 = true;
							}
						}
					}
					break;
				}
			}

			if ( _newSnapshot == -1 && m_customSpeakMusic )
			{
				GameMusicManager.Instance.ChangeMusic( GameMusicManager.EGameMusicManagerState.eBaseVillage );
				m_customSpeakMusic = false;
			}
			return false;
		}

		public bool OnCutSceneTransitionTime( string _cutSceneName, int _previousSnapshot, int _newSnapshot, ref int _nextScene )
		{
			if ( _newSnapshot == -1 && m_customSpeakMusic )
			{
				GameMusicManager.Instance.ChangeMusic( GameMusicManager.EGameMusicManagerState.eBaseVillage );
				m_customSpeakMusic = false;
			}
			return false;
		}

		public bool OnCutSceneTransitionChoice( string _cutSceneName, int _previousSnapshot, int _newSnapshot, int _choiceIndex, ref int _nextScene )
		{
			if ( _cutSceneName == "D1_R1" )
			{
				if ( _previousSnapshot == 1 )
				{
					if ( _choiceIndex == 1 )
					{
						D1_R1_c2 = true;
					}
				}
			}
			else if ( _cutSceneName == "D1_B3" )
			{
				if ( _previousSnapshot == 2 && _newSnapshot == 3 )
				{
					D1_B3_a2 = true;
				}
			}
			else if ( _cutSceneName == "D1_M1" )
			{
				if ( _previousSnapshot == 0 && _newSnapshot == 1 && _choiceIndex == 1 )
				{
					D1_M1_a2 = true;
				}
			}

			if ( _newSnapshot == -1 && m_customSpeakMusic )
			{
				GameMusicManager.Instance.ChangeMusic( GameMusicManager.EGameMusicManagerState.eBaseVillage );
				m_customSpeakMusic = false;
			}
			return false;
		}
	}
}
