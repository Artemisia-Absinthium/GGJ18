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
using UnityEngine.UI;
using System.Collections;

namespace Game
{
	[System.Serializable]
	public class GameController : MonoBehaviour, ICutSceneSupervisorBase
	{
		[SerializeField]
		private GameObject m_m2;
		[SerializeField]
		private GameObject m_m5;
		[SerializeField]
		private GameObject m_r1;
		[SerializeField]
		private GameObject m_r2;
		[SerializeField]
		private GameObject m_p2_1;
		[SerializeField]
		private GameObject m_p2_2;
		[SerializeField]
		private GameObject m_p3_1;
		[SerializeField]
		private GameObject m_p3_2;
		[SerializeField]
		private GameObject m_p4_1;
		[SerializeField]
		private GameObject m_p4_2;
		[SerializeField]
		private GameObject m_p5_1;
		[SerializeField]
		private GameObject m_p5_2;
		[SerializeField]
		private GameObject m_wateringcan;
		[SerializeField]
		private GameObject m_n5_1;
		[SerializeField]
		private GameObject m_n5_2;
		[SerializeField]
		private GameObject m_n4;
		[SerializeField]
		private GameObject m_twins;
		[SerializeField]
		private GameObject m_dog_1;
		[SerializeField]
		private GameObject m_dog_2;
		[SerializeField]
		private GameObject m_b1;
		[SerializeField]
		private GameObject m_characters;
		[SerializeField]
		private Transform m_characterForestBackPosition;
		[SerializeField]
		private Image m_backgroundImage;
		[SerializeField]
		private GameObject m_forestTrigger;

		private static GameController s_instance = null;
		private CutScenePlayer m_csp = null;

		private int m_currentChapter = 1;
		private int m_cutSceneInstance = -1;
		private bool m_customSpeakMusic = false;
		private int m_wasSpeaking = 0;
		private int m_dogCount = 0;
		private bool m_overrideSpeaking = false;

		public bool haveWateringCan = false;

		// Mathilda
		public bool D1_B1_a1 = false;
		public bool D1_B1_c1 = false;
		public bool D1_B1_c2 = false;
		public bool D1_B1_c4 = false;
		public bool D1_B1_d1 = false;
		public bool D1_B1_d2 = false;
		// Zekiel
		public bool D1_B2_a0 = false;
		// Raziel
		public bool D1_B3_a2 = false;
		// Alex
		public bool D1_M1_a2 = false;
		// Lucie
		public bool D1_M2_a4 = false;
		public bool D1_M2_c1 = false;
		public bool D1_M2_c2 = false;
		public bool D1_M2_c9 = false;
		// Neo
		public bool D1_M5_a1 = false;
		public bool D1_M5_c1 = false;
		// Annika
		public bool D1_N1_a1 = false;
		public bool D1_N1_c5 = false;
		// Lara
		public bool D1_N5_a9 = false;
		public bool D1_N5_c1 = false;
		public bool D1_N5_c5 = false;
		public bool D1_N5_c6 = false;
		public bool D1_N5_c8 = false;
		// Kozbee
		public bool D1_P1_a1 = false;
		public bool D1_P1_a3 = false;
		public bool D1_P1_b1 = false;
		// Cousin
		public bool D1_P3_b1 = false;
		// Ada
		public bool D1_R1_c2 = false;

		public bool D1_P = false;
		public bool D1_B = false;
		public bool D1_M = false;
		public bool D1_N = false;
		public bool D1_R = false;

		void Awake()
		{
			s_instance = this;
		}

		public bool WasSpeaking { get { return m_wasSpeaking > 0; } }
		public bool IsSpeaking { get { return ( m_cutSceneInstance >= 0 ) || m_overrideSpeaking; } }
		public static GameController Instance
		{
			get { return s_instance; }
		}

		void Start()
		{
			m_csp = CutScenePlayer.Instance;
			Debug.Assert( m_csp );


			m_m2.SetActive( false );
			m_m5.SetActive( false );
			m_r1.SetActive( false );
			m_r2.SetActive( false );
			m_p2_2.SetActive( false );
			m_p3_2.SetActive( false );
			m_p4_2.SetActive( false );
			m_p5_2.SetActive( false );
			m_n5_2.SetActive( false );
			m_n4.SetActive( false );
			m_dog_2.SetActive( false );
		}

		void Update()
		{
			if ( !Cinematics.Instance.StartCinematicFinished )
			{
				if ( !Cinematics.Instance.StartCinematicPlaying )
				{
					StartCoroutine( Cinematics.Instance.BeginStartCinematic() );
				}
			}
			if ( IsSpeaking )
			{
				if ( !m_csp.IsPlaying() )
				{
					m_characters.SetActive( true );
					m_cutSceneInstance = -1;
				}
			}
			else if ( m_wasSpeaking > 0 )
			{
				m_wasSpeaking--;
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
			case Character.Name.COUSIN: C1(); break;
			case Character.Name.DOOR1: R0(); break;
			case Character.Name.DOOR2: D2(); break;
			case Character.Name.DOOR3: D3(); break;
			case Character.Name.WATERINGCAN: WC(); break;
			}
		}

		private void D2()
		{
			if ( m_currentChapter == 1 )
			{
				m_n4.SetActive( true );
				StartCutscene( "D1_D1", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void D3()
		{

		}

		private void WC()
		{
			if ( D1_P1_a1 )
			{
				haveWateringCan = true;
				m_wateringcan.SetActive( false );
			}
		}

		private void P1()
		{
			if ( m_currentChapter == 1 )
			{
				D1_P = true;
				StartCutscene( "D1_P1", GameMusicManager.EGameMusicManagerState.eKozbee );
			}
		}

		private void P2()
		{
			if ( m_currentChapter == 1 )
			{
				D1_P = true;
				StartCutscene( "D1_P2", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void P3()
		{
			if ( m_currentChapter == 1 )
			{
				if ( D1_B1_d1 && !D1_P3_b1 )
				{
					D1_P = true;
					StartCutscene( "D1_P345", GameMusicManager.EGameMusicManagerState.eNone );
					D1_P3_b1 = true;
				}
				else
				{
					D1_P = true;
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
					D1_P = true;
					StartCutscene( "D1_P345", GameMusicManager.EGameMusicManagerState.eNone );
					D1_P3_b1 = true;
				}
				else
				{
					D1_P = true;
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
					D1_P = true;
					StartCutscene( "D1_P345", GameMusicManager.EGameMusicManagerState.eNone );
					D1_P3_b1 = true;
				}
				else
				{
					D1_P = true;
					StartCutscene( "D1_P5", GameMusicManager.EGameMusicManagerState.eNone );
				}
			}
		}

		private void P6()
		{
			if ( m_currentChapter == 1 )
			{
				D1_P = true;
				StartCutscene( "D1_P6", GameMusicManager.EGameMusicManagerState.eLapin );
			}
		}

		private void R0()
		{
			if ( m_currentChapter == 1 )
			{
				if ( !D1_N5_c8 || D1_B1_c1 )
				{
					D1_R = true;
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
					D1_R = true;
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
					D1_R = true;
					StartCutscene( "D1_R2", GameMusicManager.EGameMusicManagerState.eNone );
				}
			}
		}

		private void B1()
		{
			if ( m_currentChapter == 1 )
			{
				D1_B = true;
				StartCutscene( "D1_B1", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void B2()
		{
			if ( m_currentChapter == 1 )
			{
				D1_B = true;
				StartCutscene( "D1_B3", GameMusicManager.EGameMusicManagerState.eLara );
			}
		}

		private void B3()
		{
			if ( m_currentChapter == 1 )
			{
				D1_B2_a0 = true;
				D1_B = true;
				StartCutscene( "D1_B3", GameMusicManager.EGameMusicManagerState.eLara );
			}
		}

		private void B4()
		{
			if ( m_currentChapter == 1 )
			{
				D1_B = true;
				StartCutscene( "D1_B4", GameMusicManager.EGameMusicManagerState.eLara );
			}
		}

		private void M1()
		{
			if ( m_currentChapter == 1 )
			{
				D1_M = true;
				StartCutscene( "D1_M1", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M2()
		{
			if ( m_currentChapter == 1 )
			{
				D1_M = true;
				StartCutscene( "D1_M2", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M3()
		{
			if ( m_currentChapter == 1 )
			{
				D1_M = true;
				StartCutscene( "D1_M3", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M4()
		{
			if ( m_currentChapter == 1 )
			{
				D1_M = true;
				StartCutscene( "D1_M4", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void M5()
		{
			if ( m_currentChapter == 1 )
			{
				D1_M = true;
				StartCutscene( "D1_M5", GameMusicManager.EGameMusicManagerState.eLara );
			}
		}

		private void N1()
		{
			if ( m_currentChapter == 1 )
			{
				D1_N = true;
				StartCutscene( "D1_N1", GameMusicManager.EGameMusicManagerState.eKozbee );
			}
		}

		private void N2()
		{
			if ( m_currentChapter == 1 )
			{
				D1_N = true;
				StartCutscene( "D1_N2", GameMusicManager.EGameMusicManagerState.eKozbee );
			}
		}

		private void N3()
		{
			if ( m_currentChapter == 1 )
			{
				D1_N = true;
				StartCutscene( "D1_N3", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void N4()
		{
			if ( m_currentChapter == 1 )
			{
				D1_N = true;
				StartCutscene( "D1_N4", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		private void N5()
		{
			if ( m_currentChapter == 1 )
			{
				D1_N = true;
				StartCutscene( "D1_N5", GameMusicManager.EGameMusicManagerState.eLara );
			}
		}

		private void C1()
		{
			if ( m_currentChapter == 1 )
			{
				StartCutscene( "D1_C1", GameMusicManager.EGameMusicManagerState.eNone );
			}
		}

		public void OnForestTrigger()
		{
			StartCutscene( "D1_F0", GameMusicManager.EGameMusicManagerState.eNone );
		}

		private IEnumerator ForestBack()
		{
			m_backgroundImage.gameObject.SetActive( true );
			Color transparent = new Color( 0.0f, 0.0f, 0.0f, 0.0f );
			m_backgroundImage.color = transparent;
			float time = Time.time;
			float timeTarget = time + 1.0f;
			while ( Time.time < timeTarget )
			{
				m_backgroundImage.color = Color32.Lerp( transparent, Color.black, ( Time.time - time ) );
				yield return null;
			}
			m_backgroundImage.color = Color.black;
			yield return new WaitForSeconds( 1.0f );
			GameObject playerGO = GameCache.Instance.GetObject( GameCacheObjects.Player );
			PlayerController pc = playerGO.GetComponent<PlayerController>();
			GameObject playerCamera = GameCache.Instance.GetObject( GameCacheObjects.PlayerCamera );
			pc.transform.position = m_characterForestBackPosition.position;
			pc.transform.rotation = m_characterForestBackPosition.rotation;
			pc.SetAngle( pc.transform.rotation.eulerAngles.y );
			pc.SetVerticalView( 0.0f );
			playerCamera.transform.localRotation = Quaternion.identity;
			time = Time.time;
			timeTarget = time + 1.0f;
			while ( Time.time < timeTarget )
			{
				m_backgroundImage.color = Color32.Lerp( Color.black, transparent, ( Time.time - time ) );
				yield return null;
			}
			m_backgroundImage.color = transparent;
			m_backgroundImage.gameObject.SetActive( false );

			yield return null;
			m_overrideSpeaking = false;
		}

		private void StartCutscene( string _name, GameMusicManager.EGameMusicManagerState _music )
		{
			Cursor.visible = true;
			m_cutSceneInstance = m_csp.Play( _name, this );
			if ( _music != GameMusicManager.EGameMusicManagerState.eNone )
			{
				GameMusicManager.Instance.ChangeMusic( _music );
				m_customSpeakMusic = true;
			}
			m_characters.SetActive( false );
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
							else if ( _previousSnapshot == 7 )
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
							if ( D1_B1_d2 )
							{
								_nextScene = 1;
								if ( D1_M2_c9 )
								{
									_nextScene = 2;
								}
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
							if ( !D1_B1_a1 )
							{
								D1_B1_a1 = true;
								return false;
							}
							else if ( D1_P1_b1 )
							{
								_nextScene = 2;
								if ( D1_B3_a2 )
								{
									m_dog_2.SetActive( true );
									m_p2_1.SetActive( false );
									m_p2_2.SetActive( true );
									m_p3_1.SetActive( false );
									m_p3_2.SetActive( true );
									m_p4_1.SetActive( false );
									m_p4_2.SetActive( true );
									m_p5_1.SetActive( false );
									m_p5_2.SetActive( true );
									m_b1.SetActive( false );
									D1_B1_d2 = true;
									_nextScene = 5;
								}
								D1_B1_a1 = true;
								return true;
							}
							else
							{
								_nextScene = 1;
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
							if ( D1_M2_a4 )
							{
								_nextScene = 3;
								if ( D1_M5_a1 || D1_M2_c1 || D1_M2_c2 )
								{
									_nextScene = 4;
								}
								return true;
							}
						}
						if ( _previousSnapshot == 3 && _newSnapshot == -1 )
						{
							D1_M2_a4 = true;
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
				case "D1_N1":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_N1_c5 )
							{
								_nextScene = 13;
								return true;
							}
							else if ( D1_N1_a1 )
							{
								_nextScene = 4;
								if ( ( D1_N5_c5 || D1_N5_c6 ) && D1_N5_a9 )
								{
									_nextScene = 5;
								}
								return true;
							}
							else
							{
								D1_N1_a1 = true;
							}
						}
						else if ( _previousSnapshot == 5 && _newSnapshot == 6 )
						{
							if ( D1_N5_c8 && D1_N5_a9 )
							{
								_nextScene = 7;
								return true;
							}
						}
						else if ( _previousSnapshot == 9 )
						{
							D1_N1_c5 = true;
						}
						else if ( _newSnapshot == 8 || _newSnapshot == 6 )
						{
							m_n5_1.SetActive( false );
							m_n5_2.SetActive( true );
							D1_N5_a9 = true;
						}
					}
					break;
				case "D1_N5":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_N5_c8 )
							{
								_nextScene = 17;
								return true;
							}
							else if ( D1_N5_a9 )
							{
								_nextScene = 9;
								return true;
							}
							else if ( D1_N5_c1 )
							{
								_nextScene = 18;
								if ( D1_N1_a1 )
								{
									_nextScene = 2;
								}
								return true;
							}
							else if ( D1_N1_a1 )
							{
								_nextScene = 1;
								return true;
							}
						}
						else if ( _newSnapshot == 8 || _newSnapshot == 6 )
						{
							m_n5_1.SetActive( false );
							m_n5_2.SetActive( true );
							D1_N5_a9 = true;
						}
					}
					break;
				case "D1_C1":
					{
						if ( _previousSnapshot == -1 )
						{
							if ( D1_P && D1_R && D1_B && D1_M && D1_N )
							{
								_nextScene = 1;
								return true;
							}
						}
					}
					break;
				case "D1_F0":
					{
						if ( _newSnapshot == -1 )
						{
							m_overrideSpeaking = true;
							StartCoroutine( ForestBack() );
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
			if ( _newSnapshot == -1 )
			{
				m_wasSpeaking = 2;
				Cursor.visible = false;
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
			if ( _newSnapshot == -1 )
			{
				m_wasSpeaking = 2;
				Cursor.visible = false;
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
						m_forestTrigger.SetActive( false );
						D1_R1_c2 = true;
					}
				}
			}
			else if ( _cutSceneName == "D1_B1" )
			{
				if ( _previousSnapshot == 2 && _newSnapshot == 3 )
				{
					D1_B1_c4 = true;
				}
			}
			else if ( _cutSceneName == "D1_B3" )
			{
				if ( _previousSnapshot == 2 && _newSnapshot == 3 )
				{
					m_dog_1.SetActive( false );
					m_twins.SetActive( false );
					D1_B3_a2 = true;
				}
			}
			else if ( _cutSceneName == "D1_B4" )
			{
				if ( _previousSnapshot == 0 && _newSnapshot == 1 )
				{
					++m_dogCount;
					if ( m_dogCount >= 12 )
					{
						_nextScene = 2;
						if ( m_dogCount == 15 )
						{
							_nextScene = 3;
						}
						return true;
					}
				}
			}
			else if ( _cutSceneName == "D1_M1" )
			{
				if ( _previousSnapshot == 0 && _newSnapshot == 1 && _choiceIndex == 1 )
				{
					m_m2.SetActive( true );
					D1_M1_a2 = true;
				}
			}
			else if ( _cutSceneName == "D1_N5" )
			{
				if ( _previousSnapshot == 9 && _newSnapshot == 10 && _choiceIndex == 1 )
				{
					m_m5.SetActive( true );
					m_r1.SetActive( true );
					m_r2.SetActive( true );
					D1_N5_c8 = true;
				}
			}
			else if ( _cutSceneName == "D1_C1" )
			{
				if ( _previousSnapshot == 3 && _newSnapshot == -1 && _choiceIndex == 1 )
				{
					StartCoroutine( Cinematics.Instance.BeginChapter1To2Transition() );
				}
			}

			if ( _newSnapshot == -1 && m_customSpeakMusic )
			{
				GameMusicManager.Instance.ChangeMusic( GameMusicManager.EGameMusicManagerState.eBaseVillage );
				m_customSpeakMusic = false;
			}
			if ( _newSnapshot == -1 )
			{
				m_wasSpeaking = 2;
				Cursor.visible = false;
			}
			return false;
		}
	}
}
