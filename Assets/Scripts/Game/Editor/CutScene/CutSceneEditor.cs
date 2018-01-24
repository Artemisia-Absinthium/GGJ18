/*
 * LICENCE
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Game
{
	[System.Serializable]
	public class CutSceneEditor : EditorWindow
	{
		private int m_currentTab = 0;
		private List<CutSceneSnapshotEditor> m_tabs;
		private string[] m_toolbar;
		private string m_cutsceneName = "Unnamed cutscene";
		private string m_cutSceneFile = null;

		private string m_searchLeft = "none";
		private string m_searchRight = "none";
		private string m_searchCenter = "none";
		private string m_searchText = "";
		private List<string> m_searchChoices = new List<string>();

		private bool m_isEditingSprites = false;
		private Vector2 m_scrollViewPosition = Vector2.zero;

		public struct CachedSprite
		{
			public string Name;
			public string Path;
			public Sprite Sprite;
			public CachedSprite( object _o )
			{
				Name = "none";
				Path = "";
				Sprite = null;
			}
		}

		private static List<CachedSprite> s_sprites = null;
		private static List<string> s_spriteNames = null;
		private static string[] s_stringsEnumNames = null;
		private static string[] s_toolbar = new string[] { "Sprite editor", "Snapshot editor" };
		private static GUILayoutOption s_textFinderStyle = GUILayout.Width( 75 );
		private static CachedSprite s_noneSprite = new CachedSprite( null );
		private static GUILayoutOption s_bigButton;

		[MenuItem( "Game/CutScene editor" )]
		public static void OpenWindow()
		{
			s_stringsEnumNames = System.Enum.GetNames( typeof( Strings ) );
			CheckLoadSprites();
			CutSceneEditor instance = GetWindow<CutSceneEditor>();
			instance.titleContent = new GUIContent( "CutScene edit" );
			instance.Show();
		}

		private static void SaveSprites()
		{
			System.IO.Directory.CreateDirectory( "Assets/Temp/CutScene/" );
			System.IO.FileStream stream = new System.IO.FileStream( "Assets/Temp/CutScene/sprites.sdb", System.IO.FileMode.Create, System.IO.FileAccess.Write );
			System.IO.StreamWriter writer = new System.IO.StreamWriter( stream );
			foreach ( CachedSprite cs in s_sprites )
			{
				writer.WriteLine( ( cs.Name == null ? "" : cs.Name ) + "|" + ( cs.Path == null ? "" : cs.Path ) );
			}
			writer.Close();
		}

		private static void CheckLoadSprites()
		{
			if ( s_sprites == null )
			{
				s_sprites = new List<CachedSprite>();
				s_spriteNames = new List<string>();
				LoadSprites();
			}
		}

		private static void LoadSprites()
		{
			System.IO.FileStream stream = null;
			try
			{
				stream = new System.IO.FileStream( "Assets/Temp/CutScene/sprites.sdb", System.IO.FileMode.Open, System.IO.FileAccess.Read );
			}
			catch ( System.Exception )
			{
				return;
			}
			if ( stream != null )
			{
				System.IO.StreamReader reader = new System.IO.StreamReader( stream );

				while ( !reader.EndOfStream )
				{
					CachedSprite cs = new CachedSprite();
					string line = reader.ReadLine();
					string[] nameAndSprite = line.Split( '|' );
					if ( nameAndSprite.Length != 2 )
					{
						Debug.Log( "Error!" );
						return;
					}
					cs.Name = nameAndSprite[ 0 ];
					s_spriteNames.Add( cs.Name );
					cs.Path = nameAndSprite[ 1 ];
					if ( cs.Path != null && cs.Path.Length > 0 )
					{
						cs.Sprite = AssetDatabase.LoadAssetAtPath<Sprite>( cs.Path );
					}
					s_sprites.Add( cs );
				}

				reader.Close();
			}
		}

		private void LoadCutscene()
		{
			System.IO.Directory.CreateDirectory( "Assets/Temp/Cutscene" );
			if ( EditorUtility.DisplayDialog( "Save file?", "Changes will be lost if unsaved. Do you want to save?", "Yes", "No" ) )
			{
				if ( !SaveCutscene() )
				{
					return;
				}
			}
			string scenePath = EditorUtility.OpenFilePanel( "Load Cutscene", "Assets/Temp/Cutscene", "cut" );
			if ( scenePath != null && scenePath.Length != 0 )
			{
				DoLoadCutScene( scenePath );
			}
		}

		private bool SaveCutscene()
		{
			System.IO.Directory.CreateDirectory( "Assets/Temp/Cutscene" );
			if ( m_cutSceneFile != null && m_cutSceneFile.Length != 0 )
			{
				DoSaveCutScene( m_cutSceneFile );
				return true;
			}
			else
			{
				string scenePath = EditorUtility.SaveFilePanel( "Save Cutscene", "Assets/Temp/Cutscene", m_cutsceneName, "cut" );
				if ( scenePath == null || scenePath.Length == 0 )
				{
					return false;
				}
				DoSaveCutScene( scenePath );
				m_cutSceneFile = scenePath;
				return true;
			}
		}

		private void DoLoadCutScene( string _path )
		{
			FileStream stream = null;
			try
			{
				stream = new FileStream( _path, FileMode.Open, FileAccess.Read );
			}
			catch ( System.Exception )
			{
				return;
			}
			if ( stream != null )
			{
				StreamReader reader = new StreamReader( stream );

				m_currentTab = 0;
				m_tabs = new List<CutSceneSnapshotEditor>();
				m_cutsceneName = reader.ReadLine().Substring( 2 );
				m_cutSceneFile = _path;
				Dictionary<int, int> retarget = new Dictionary<int, int>();
				while ( !reader.EndOfStream )
				{
					string blockStart = reader.ReadLine().Trim();
					if ( blockStart.Length != 0 )
					{
						int blockNumber = -1;
						try
						{
							blockNumber = int.Parse( blockStart.Split( ' ' )[ 0 ] );
						}
						catch ( System.Exception )
						{
							Debug.Log( "Error!" );
							return;
						}
						retarget[ blockNumber ] = m_tabs.Count;
						CutSceneSnapshotEditor csse = new CutSceneSnapshotEditor();
						csse.Ok = false;
						m_tabs.Add( csse );
						bool read = true;
						while ( !reader.EndOfStream && read )
						{
							string[] line = reader.ReadLine().Trim().Split( new char[] { ':', ',' } );
							if ( line.Length > 0 )
							{
								switch ( line[ 0 ] )
								{
								case "left":
									if ( line.Length == 2 )
									{
										csse.Left = line[ 1 ];
									}
									else
									{
										Debug.Log( "Error!" );
										return;
									}
									break;
								case "right":
									if ( line.Length == 2 )
									{
										csse.Right = line[ 1 ];
									}
									else
									{
										Debug.Log( "Error!" );
										return;
									}
									break;
								case "center":
									if ( line.Length == 2 )
									{
										csse.Center = line[ 1 ];
									}
									else
									{
										Debug.Log( "Error!" );
										return;
									}
									break;
								case "text":
									if ( line.Length == 2 )
									{
										csse.Text = line[ 1 ];
									}
									else
									{
										Debug.Log( "Error!" );
										return;
									}
									break;
								case "choice":
									if ( line.Length == 3 )
									{
										csse.Choices.Add( line[ 1 ] );
										if ( line[ 2 ] == "end" )
										{
											csse.ChoiceTargets.Add( -1 );
										}
										else
										{
											csse.ChoiceTargets.Add( int.Parse( line[ 2 ] ) );
										}
									}
									else
									{
										Debug.Log( "Error!" );
										return;
									}
									break;
								case "ok":
									if ( line.Length == 2 )
									{
										csse.Ok = true;
										if ( line[ 1 ] == "end" )
										{
											csse.OkTarget = -1;
										}
										else
										{
											csse.OkTarget = int.Parse( line[ 1 ] );
										}
									}
									else
									{
										Debug.Log( "Error!" );
										return;
									}
									break;
								case "time":
									if ( line.Length == 3 )
									{
										csse.Timed = true;
										csse.Time = float.Parse( line[ 1 ] );
										if ( line[ 2 ] == "end" )
										{
											csse.TimeTarget = -1;
										}
										else
										{
											csse.TimeTarget = int.Parse( line[ 2 ] );
										}
									}
									else
									{
										Debug.Log( "Error!" );
										return;
									}
									break;
								case "}":
									read = false;
									break;
								case "":
									break;
								}
							}
						}
					}
				}
				m_toolbar = new string[ m_tabs.Count + 1 ];
				for ( int i = 0; i < m_tabs.Count; ++i )
				{
					m_toolbar[ i ] = i.ToString();
				}
				m_toolbar[ m_tabs.Count ] = "+";
				if ( m_tabs.Count > 0 )
				{
					ResetSearch();
				}
				else
				{
					m_searchLeft = "";
					m_searchRight = "";
					m_searchCenter = "";
					m_searchText = "";
					m_searchChoices = new List<string>();
				}

				reader.Close();
			}
		}

		private void DoSaveCutScene( string _path )
		{
			Directory.CreateDirectory( "Assets/Temp/CutScene/" );
			FileStream stream = new FileStream( _path, FileMode.Create, FileAccess.Write );
			StreamWriter writer = new StreamWriter( stream );
			Debug.Log( "Saving Cutscene : " + _path );

			writer.WriteLine( "# " + m_cutsceneName );
			for ( int i = 0; i < m_tabs.Count; ++i )
			{
				CutSceneSnapshotEditor csse = m_tabs[ i ];
				writer.WriteLine( i + " {" );

				writer.WriteLine( "\tleft:" + csse.Left );
				writer.WriteLine( "\tright:" + csse.Right );
				writer.WriteLine( "\tcenter:" + csse.Center );
				writer.WriteLine( "\ttext:" + csse.Text );
				if ( ( csse.Choices != null ) &&
					( csse.ChoiceTargets != null ) &&
					( csse.Choices.Count == csse.ChoiceTargets.Count ) )
				{
					for ( int j = 0; j < csse.Choices.Count; ++j )
					{
						if ( csse.ChoiceTargets[ j ] < 0 )
						{
							writer.WriteLine( "\tchoice:" + csse.Choices[ j ] + ",end" );
						}
						else
						{
							writer.WriteLine( "\tchoice:" + csse.Choices[ j ] + "," + csse.ChoiceTargets[ j ] );
						}
					}
				}
				if ( csse.Ok )
				{
					if ( csse.OkTarget == -1 )
					{
						writer.WriteLine( "\tok:end" );
					}
					else
					{
						writer.WriteLine( "\tok:" + csse.OkTarget );
					}
				}
				if ( csse.Timed && csse.Time >= 0.0f )
				{
					if ( csse.TimeTarget == -1 )
					{
						writer.WriteLine( "\ttime:" + string.Format( "{0:F3}", csse.Time ) + ",end" );
					}
					else
					{
						writer.WriteLine( "\ttime:" + string.Format( "{0:F3}", csse.Time ) + "," + csse.TimeTarget );
					}
				}

				writer.WriteLine( "}" );
			}

			writer.Close();
			Debug.Log( "Saved" );

			AssetDatabase.Refresh( ImportAssetOptions.ForceUpdate );
		}

		private bool TextFinder( ref string _result, ref string _temp, string _name, IEnumerable<string> _values )
		{
			GUILayout.BeginVertical();
			GUILayout.BeginHorizontal();
			GUILayout.Label( _name, s_textFinderStyle );
			string text = EditorGUILayout.TextField( _temp );
			bool found = false;
			GUILayout.EndHorizontal();

			if ( !IsIdValid( text ) )
			{
				text = _temp;
			}
			if ( text != _result )
			{
				int i = 0;
				foreach ( string v in _values )
				{
					if ( ( v != null ) && ( v.IndexOf( text, System.StringComparison.OrdinalIgnoreCase ) >= 0 ) )
					{
						GUILayout.BeginHorizontal();
						GUILayout.Label( "", s_textFinderStyle );
						if ( GUILayout.Button( v, GUI.skin.label ) )
						{
							GUI.FocusControl( "" );
							_result = v;
							text = _result;
							found = true;
						}
						GUILayout.EndHorizontal();
						++i;
					}
					if ( i == 10 )
					{
						GUILayout.EndVertical();
						return found;
					}
				}
			}
			_temp = text;
			GUILayout.EndVertical();
			return found;
		}

		private void DrawOnGUISprite( Sprite _sprite, float _w, float _h )
		{
			Rect c = _sprite.rect;
			Rect rect = GUILayoutUtility.GetRect( _w, _h );

			float cRatio = c.height / c.width;
			float rRatio = rect.height / rect.width;
			if ( cRatio > rRatio )
			{
				float width = rect.height / cRatio;
				float deltaX = ( rect.width - width ) / 2.0f;
				rect.xMin += deltaX;
				rect.xMax -= deltaX;
			}
			float height = rect.width * cRatio;
			float deltaY = ( rect.height - height ) / 2.0f;
			if ( cRatio < rRatio )
			{
				rect.yMin += deltaY;
				rect.yMax -= deltaY;
			}

			if ( Event.current.type == EventType.Repaint )
			{
				var tex = _sprite.texture;
				c.xMin /= tex.width;
				c.xMax /= tex.width;
				c.yMin /= tex.height;
				c.yMax /= tex.height;
				GUI.DrawTextureWithTexCoords( rect, tex, c );
			}
		}

		private bool IsIdValid( string _id )
		{
			if ( _id == null || _id.Length == 0 )
			{
				return true;
			}
			char z = _id[ 0 ];
			if ( !( ( z >= 'a' ) && ( z <= 'z' ) ) &&
				 !( ( z >= 'A' ) && ( z <= 'Z' ) ) &&
				 z != '_' )
			{
				return false;
			}

			foreach ( char c in _id )
			{
				if ( !( ( c >= 'a' ) && ( c <= 'z' ) ) &&
					 !( ( c >= 'A' ) && ( c <= 'Z' ) ) &&
					 !( ( c >= '0' ) && ( c <= '9' ) ) &&
					 c != '_' )
				{
					return false;
				}
			}

			return true;
		}

		private bool TryFindSprite( string _name, out CachedSprite _sprite )
		{
			CheckLoadSprites();
			if ( _name == "none" )
			{
				_sprite = s_noneSprite;
			}
			for ( int i = 0; i < s_sprites.Count; ++i )
			{
				if ( s_sprites[ i ].Name == _name )
				{
					_sprite = s_sprites[ i ];
					return true;
				}
			}
			_sprite = new CachedSprite();
			return false;
		}

		private void OnDestroy()
		{
			if ( s_sprites != null )
			{
				SaveSprites();
			}
			if ( m_tabs != null )
			{
				int choice = EditorUtility.DisplayDialogComplex( "Close?", "Changes will be lost if unsaved. Do you really want to close or save before?", "Close", "Don't close", "Save" );
				switch ( choice )
				{
				case 1:
					Rescue();
					break;
				case 2:
					if ( !SaveCutscene() )
					{
						Rescue();
					}
					break;
				}
			}
			else
			{
				if ( !EditorUtility.DisplayDialog( "Close?", "Changes will be lost if unsaved. Do you really want to close?", "Close", "Don't close" ) )
				{
					Rescue();
				}
			}
		}

		private void Rescue()
		{
			CutSceneEditor rescued = Instantiate( this );
			rescued.m_currentTab = m_currentTab;
			rescued.m_tabs = m_tabs;
			rescued.m_toolbar = m_toolbar;
			rescued.m_cutsceneName = m_cutsceneName;
			rescued.m_cutSceneFile = m_cutSceneFile;

			rescued.m_searchLeft = m_searchLeft;
			rescued.m_searchRight = m_searchRight;
			rescued.m_searchCenter = m_searchCenter;
			rescued.m_searchText = m_searchText;

			rescued.m_isEditingSprites = m_isEditingSprites;
			rescued.m_scrollViewPosition = m_scrollViewPosition;

			rescued.Show();
		}

		private void DrawSnapshotSprite( ref string _spriteName, ref string _tempName, string _name, float _width, float _height )
		{
			GUILayout.BeginVertical();
			CachedSprite spritePath = new CachedSprite();
			GUILayout.BeginHorizontal();
			TextFinder( ref _spriteName, ref _tempName, _name, s_spriteNames );
			bool remove = false;
			if ( GUILayout.Button( "Remove", s_textFinderStyle ) )
			{
				GUI.FocusControl( "" );
				remove = true;
			}
			else if ( _tempName == "none" )
			{
				remove = true;
			}
			if ( remove )
			{
				_spriteName = "none";
				_tempName = "none";
				GUILayout.EndHorizontal();
				GUILayoutUtility.GetRect( _width, _height );
				GUILayout.EndVertical();
				return;
			}
			GUILayout.EndHorizontal();
			if ( TryFindSprite( _spriteName, out spritePath ) )
			{
				if ( spritePath.Sprite == null )
				{
					spritePath.Sprite = AssetDatabase.LoadAssetAtPath<Sprite>( spritePath.Path );
				}
			}
			if ( spritePath.Sprite != null )
			{
				DrawOnGUISprite( spritePath.Sprite, _width, _height );
			}
			else
			{
				GUILayoutUtility.GetRect( _width, _height );
			}
			GUILayout.EndVertical();
		}

		private void ResetSearch()
		{
			m_searchText = m_tabs[ m_currentTab ].Text;
			m_searchLeft = m_tabs[ m_currentTab ].Left;
			m_searchRight = m_tabs[ m_currentTab ].Right;
			m_searchCenter = m_tabs[ m_currentTab ].Center;
			m_searchChoices = new List<string>( m_tabs[ m_currentTab ].Choices );
		}

		private void MoveSnapshot( int _count )
		{
			int replaceBy = m_currentTab + _count;

			CutSceneSnapshotEditor tmp = m_tabs[ replaceBy ];
			m_tabs[ replaceBy ] = m_tabs[ m_currentTab ];
			m_tabs[ m_currentTab ] = tmp;

			foreach ( CutSceneSnapshotEditor csse in m_tabs )
			{
				if ( csse.OkTarget == m_currentTab )
				{
					csse.OkTarget = replaceBy;
				}
				else if ( csse.OkTarget == replaceBy )
				{
					csse.OkTarget = m_currentTab;
				}
				if ( csse.TimeTarget == m_currentTab )
				{
					csse.TimeTarget = replaceBy;
				}
				else if ( csse.TimeTarget == replaceBy )
				{
					csse.TimeTarget = m_currentTab;
				}
				for ( int i = 0; i < csse.ChoiceTargets.Count; ++i )
				{
					if ( csse.ChoiceTargets[ i ] == m_currentTab )
					{
						csse.ChoiceTargets[ i ] = replaceBy;
					}
					else if ( csse.ChoiceTargets[ i ] == replaceBy )
					{
						csse.ChoiceTargets[ i ] = m_currentTab;
					}
				}
			}
			m_currentTab += _count;
		}

		private void RemoveSnapshot( int _index )
		{
			foreach ( CutSceneSnapshotEditor csse in m_tabs )
			{
				if ( csse.OkTarget == _index )
				{
					csse.OkTarget = -1;
				}
				else if ( csse.OkTarget > _index )
				{
					--csse.OkTarget;
				}
				if ( csse.TimeTarget == _index )
				{
					csse.TimeTarget = -1;
				}
				else if ( csse.TimeTarget > _index )
				{
					--csse.TimeTarget;
				}
				for ( int i = 0; i < csse.ChoiceTargets.Count; ++i )
				{
					if ( csse.ChoiceTargets[ i ] == _index )
					{
						csse.ChoiceTargets[ i ] = -1;
					}
					else if ( csse.ChoiceTargets[ i ] > _index )
					{
						--csse.ChoiceTargets[ i ];
					}
				}
			}
		}

		private void Export()
		{
			string destPath = "Assets/Resources/CutScene/";
			Directory.CreateDirectory( destPath );
			string tempPath = "Assets/Temp/CutScene/";
			string[] files = Directory.GetFiles( tempPath );

			string title = "Exporting CutScenes...";
			EditorUtility.DisplayProgressBar( title, "", 0.0f );

			for ( int i = 0; i < files.Length; ++i )
			{
				string fileName = files[ i ];
				string ext = Path.GetExtension( fileName );
				if ( ext == ".sdb" )
				{
					File.Copy( fileName, destPath + "SpriteDataBase.txt", true );
				}
				else if ( ext == ".cut" )
				{
					FileStream stream = new FileStream( fileName,FileMode.Open );
					if ( stream != null )
					{
						StreamReader reader = new StreamReader( stream );
						if ( reader != null )
						{
							string name = reader.ReadLine().Trim().Substring( 1 ).Trim();
							File.Copy( fileName, destPath + name + ".txt", true );
						}
						stream.Close();
					}
				}
				EditorUtility.DisplayProgressBar( title, "", i / ( float )files.Length );
			}

			EditorUtility.ClearProgressBar();
			AssetDatabase.Refresh( ImportAssetOptions.ForceUpdate );
		}

		private void OnGUI()
		{
			s_bigButton = GUILayout.Height( GUI.skin.button.lineHeight * 2.5f ); ;
			if ( GUILayout.Button( "Export", s_bigButton ) )
			{
				Export();
			}
			if ( GUILayout.Toolbar( m_isEditingSprites ? 0 : 1, s_toolbar, s_bigButton ) == 0 )
			{
				m_isEditingSprites = true;
				GUILayout.Space( 10 );
				OnGUISprites();
			}
			else
			{
				if ( m_isEditingSprites && s_sprites != null )
				{
					SaveSprites();
				}
				m_isEditingSprites = false;
				GUILayout.Space( 10 );
				OnGUISnapshots();
			}
		}

		private void OnGUISprites()
		{
			if ( GUILayout.Button( "Save sprites", s_bigButton ) )
			{
				SaveSprites();
			}
			m_scrollViewPosition = GUILayout.BeginScrollView( m_scrollViewPosition );

			int x = 0;

			CheckLoadSprites();

			float width = ( position.width - 106 ) / 5.0f;
			float height = width * 0.75f;

			GUILayoutOption maxWidth = GUILayout.Width( width );

			GUILayout.BeginHorizontal();
			for ( int i = 0; i < s_sprites.Count; ++i )
			{
				bool removed = false;
				CachedSprite cached = s_sprites[ i ];
				GUILayout.BeginVertical();

				GUILayout.Label( "Name" );
				string spriteName = EditorGUILayout.TextField( cached.Name, maxWidth );
				if ( !IsIdValid( spriteName ) )
				{
					spriteName = cached.Name;
				}
				else
				{
					s_spriteNames[ i ] = spriteName;
					cached.Name = spriteName;
				}

				Sprite sprite = ( Sprite )EditorGUILayout.ObjectField( cached.Sprite, typeof( Sprite ), false, maxWidth );
				if ( sprite != cached.Sprite )
				{
					cached.Sprite = sprite;
					cached.Path = AssetDatabase.GetAssetPath( sprite );
				}
				EditorGUILayout.TextField( cached.Path, GUI.skin.label, maxWidth );

				if ( GUILayout.Button( "Delete" ) )
				{
					if ( EditorUtility.DisplayDialog( "Confirm deletion?", "You really want to delete this Sprite?", "Yes", "No" ) )
					{
						s_sprites.RemoveAt( i );
						removed = true;
						s_spriteNames.RemoveAt( i );
					}
				}

				if ( cached.Sprite != null )
				{
					DrawOnGUISprite( cached.Sprite, width, height );
				}
				else
				{
					GUILayoutUtility.GetRect( width, height );
				}

				GUILayout.EndVertical();
				++x;
				if ( x == 5 )
				{
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
					x = 0;
				}
				if ( !removed )
				{
					s_sprites[ i ] = cached;
				}
			}
			if ( x != 0 )
			{
				for ( ; x < 5; ++x )
				{
					GUILayout.BeginVertical();
					GUILayout.Label( "", maxWidth );
					GUILayout.Label( "", maxWidth );
					GUILayout.Label( "", maxWidth );
					GUILayoutUtility.GetRect( width, height );
					GUILayout.EndVertical();
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.EndScrollView();
			if ( GUILayout.Button( "Add" ) )
			{
				s_sprites.Add( new CachedSprite() );
				s_spriteNames.Add( "" );
			}
		}

		private void OnGUISnapshots()
		{
			GUILayout.BeginHorizontal();
			if ( GUILayout.Button( "Load cutscene" ) )
			{
				LoadCutscene();
			}
			EditorGUI.BeginDisabledGroup( m_tabs == null );
			if ( GUILayout.Button( "Save cutscene" ) )
			{
				SaveCutscene();
			}
			EditorGUI.EndDisabledGroup();
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Label( "Cutscene name" );
			m_cutsceneName = EditorGUILayout.TextField( m_cutsceneName );
			GUILayout.EndHorizontal();
			if ( m_tabs == null )
			{
				if ( GUILayout.Button( "Create first snapshot" ) )
				{
					m_tabs = new List<CutSceneSnapshotEditor>();
					m_tabs.Add( new CutSceneSnapshotEditor() );
					m_currentTab = 0;
					m_toolbar = new string[ 2 ];
					m_toolbar[ 0 ] = "0";
					m_toolbar[ 1 ] = "+";
					m_searchText = m_tabs[ m_currentTab ].Text;
				}
			}
			else
			{
				int select = GUILayout.Toolbar( m_currentTab, m_toolbar, s_bigButton );
				GUILayout.Space( 25 );
				if ( ( select < m_tabs.Count ) && ( select != m_currentTab ) )
				{
					// Change snapshot
					m_currentTab = select;
					ResetSearch();
				}
				else if ( select == m_tabs.Count )
				{
					// Create new snapshot
					CutSceneSnapshotEditor csse = new CutSceneSnapshotEditor( m_tabs[ m_tabs.Count - 1 ] );
					m_tabs.Add( csse );
					System.Array.Resize( ref m_toolbar, m_toolbar.Length + 1 );
					m_toolbar[ m_toolbar.Length - 2 ] = ( m_toolbar.Length - 2 ).ToString();
					m_toolbar[ m_toolbar.Length - 1 ] = "+";
					m_currentTab = m_toolbar.Length - 2;

					ResetSearch();
				}

				// Move & delete
				GUILayout.BeginHorizontal();
				EditorGUI.BeginDisabledGroup( m_currentTab == 0 );
				if ( GUILayout.Button( "<< Move snapshot position", s_bigButton ) )
				{
					MoveSnapshot( -1 );
				}
				EditorGUI.EndDisabledGroup();
				if ( GUILayout.Button( "Delete snapshot", s_bigButton ) )
				{
					if ( EditorUtility.DisplayDialog( "Confirm deletion?", "You really want to delete this Snapshot?", "Yes", "No" ) )
					{
						if ( m_tabs.Count == 1 )
						{
							m_tabs = null;
							m_toolbar = null;
							return;
						}
						System.Array.Resize( ref m_toolbar, m_toolbar.Length - 1 );
						m_toolbar[ m_toolbar.Length - 1 ] = "+";
						m_tabs.RemoveAt( m_currentTab );
						RemoveSnapshot( m_currentTab );
						if ( m_currentTab > 0 )
						{
							--m_currentTab;
						}
						ResetSearch();
					}
				}
				EditorGUI.BeginDisabledGroup( m_currentTab == ( m_tabs.Count - 1 ) );
				if ( GUILayout.Button( "Move snapshot position >>", s_bigButton ) )
				{
					MoveSnapshot( 1 );
				}
				EditorGUI.EndDisabledGroup();
				GUILayout.EndHorizontal();

				// Sprites
				CheckLoadSprites();
				float sizeW = ( position.width - 104 ) / 3.0f;
				float sizeH = sizeW * 0.75f;
				GUILayout.BeginHorizontal();
				DrawSnapshotSprite( ref m_tabs[ m_currentTab ].Left, ref m_searchLeft, "Left", sizeW, sizeH );
				DrawSnapshotSprite( ref m_tabs[ m_currentTab ].Center, ref m_searchCenter, "Center", sizeW, sizeH );
				DrawSnapshotSprite( ref m_tabs[ m_currentTab ].Right, ref m_searchRight, "Right", sizeW, sizeH );
				GUILayout.EndHorizontal();

				// Text
				if ( s_stringsEnumNames == null )
				{
					s_stringsEnumNames = System.Enum.GetNames( typeof( Strings ) );
				}
				TextFinder( ref m_tabs[ m_currentTab ].Text, ref m_searchText, "Text", s_stringsEnumNames );

				// Choices
				if ( m_tabs[ m_currentTab ].Choices.Count < 4 )
				{
					if ( GUILayout.Button( "Add choice" ) )
					{
						GUI.FocusControl( "" );
						m_tabs[ m_currentTab ].Choices.Add( "NONE" );
						m_tabs[ m_currentTab ].ChoiceTargets.Add( -1 );
						m_searchChoices.Add( "NONE" );
					}
				}
				int choiceToDelete = -1;
				for ( int i = 0; i < m_tabs[ m_currentTab ].Choices.Count; ++i )
				{
					GUILayout.BeginHorizontal();

					string choice = m_tabs[ m_currentTab ].Choices[ i ];
					string search = m_searchChoices[ i ];
					if ( TextFinder( ref choice, ref search, "Choice " + ( i + 1 ), s_stringsEnumNames ) )
					{
						m_tabs[ m_currentTab ].Choices[ i ] = choice;
					}
					m_searchChoices[ i ] = search;

					GUILayout.Label( "Target", s_textFinderStyle );
					m_tabs[ m_currentTab ].ChoiceTargets[ i ] = EditorGUILayout.IntField( m_tabs[ m_currentTab ].ChoiceTargets[ i ] );

					if ( GUILayout.Button( "Delete" ) )
					{
						if ( EditorUtility.DisplayDialog( "Confirm deletion?", "You really want to delete this choice?", "Yes", "No" ) )
						{
							choiceToDelete = i;
						}
					}

					GUILayout.EndHorizontal();
				}
				if ( choiceToDelete >= 0 )
				{
					m_tabs[ m_currentTab ].Choices.RemoveAt( choiceToDelete );
					m_tabs[ m_currentTab ].ChoiceTargets.RemoveAt( choiceToDelete );
					m_searchChoices.RemoveAt( choiceToDelete );
				}

				// Ok & Time targets
				GUILayout.BeginHorizontal();

				GUILayout.BeginVertical();
				m_tabs[ m_currentTab ].Ok = GUILayout.Toggle( m_tabs[ m_currentTab ].Ok, "Enable snapshot transition when pressing OK button" );
				m_tabs[ m_currentTab ].Timed = GUILayout.Toggle( m_tabs[ m_currentTab ].Timed, "Enable snapshot transition when time is elapsed" );
				GUILayout.EndVertical();

				GUILayout.BeginVertical();
				GUILayout.BeginHorizontal();
				EditorGUI.BeginDisabledGroup( !m_tabs[ m_currentTab ].Ok );
				GUILayout.Label( "OK Target", s_textFinderStyle );
				m_tabs[ m_currentTab ].OkTarget = EditorGUILayout.IntField( m_tabs[ m_currentTab ].OkTarget );
				EditorGUI.EndDisabledGroup();
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				EditorGUI.BeginDisabledGroup( !m_tabs[ m_currentTab ].Timed );
				GUILayout.Label( "Time Target", s_textFinderStyle );
				m_tabs[ m_currentTab ].TimeTarget = EditorGUILayout.IntField( m_tabs[ m_currentTab ].TimeTarget );
				EditorGUI.EndDisabledGroup();
				GUILayout.EndHorizontal();
				GUILayout.EndVertical();

				GUILayout.BeginVertical();
				GUILayout.Label( "" );
				GUILayout.BeginHorizontal();
				EditorGUI.BeginDisabledGroup( !m_tabs[ m_currentTab ].Timed );
				GUILayout.Label( "Delay (sec)", s_textFinderStyle );
				m_tabs[ m_currentTab ].Time = EditorGUILayout.FloatField( m_tabs[ m_currentTab ].Time );
				if ( m_tabs[ m_currentTab ].Time < 0.0f )
				{
					m_tabs[ m_currentTab ].Time = 0.0f;
				}
				EditorGUI.EndDisabledGroup();
				GUILayout.EndHorizontal();
				GUILayout.EndVertical();

				GUILayout.EndHorizontal();

				if ( !( m_tabs[ m_currentTab ].Ok || m_tabs[ m_currentTab ].Timed ) && ( m_tabs[ m_currentTab ].Choices.Count == 0 ) )
				{
					Color oldColor = GUI.contentColor;
					GUI.contentColor = Color.red;
					GUILayout.Label( "At least one option should be chosen: Choice, OK or Time" );
					GUI.contentColor = oldColor;
				}
				if ( m_tabs[ m_currentTab ].Ok && m_tabs[ m_currentTab ].Choices.Count > 0 )
				{
					Color oldColor = GUI.contentColor;
					GUI.contentColor = Color.red;
					GUILayout.Label( "Ok target should not be enabled when using choices" );
					GUI.contentColor = oldColor;
				}
			}
		}
	}
}
