/*
 * LICENCE
 */
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class CutSceneParser
	{
		private static char[] s_attributeDelimiter = new char[] { ':', ',' };

		public class SpriteInfo
		{
			public string Path;
			public System.WeakReference Sprite;
			public float Timer;
			public SpriteInfo( string _path )
			{
				Path = _path;
				Sprite = null;
				Timer = 0.0f;
			}
		}

		private Dictionary<string, SpriteInfo> m_sprites = null;

		public CutSceneParser()
		{
			m_sprites = new Dictionary<string, SpriteInfo>();
		}

		public class Attribute
		{
			public enum AttributeType
			{
				LEFT, RIGHT, CENTER, TEXT, CHOICE, OK, TIME, EOB
			}
			public AttributeType Type;
			public string ValueString;
			public int ValueInt;
			public float ValueFloat;
		}

		public bool AddPicture( string _name, string _spritePath )
		{
			if ( m_sprites.ContainsKey( _name ) )
			{
				return false;
			}
			m_sprites[ _name ] = new SpriteInfo( _spritePath );
			return true;
		}

		private bool TryGetPicture( string _name, out Sprite _sprite )
		{
			_sprite = null;
			SpriteInfo si;
			if ( !m_sprites.TryGetValue( _name, out si ) )
			{
				return false;
			}
			if ( si.Sprite == null )
			{
				Sprite s = Resources.Load<Sprite>( si.Path );
				if ( s == null )
				{
					return false;
				}
				si.Sprite = new System.WeakReference( s );
				si.Timer = 100.0f;
			}
			_sprite = ( Sprite )si.Sprite.Target;
			return true;
		}

		public void Update()
		{
			float dt = Time.unscaledDeltaTime;

			foreach ( SpriteInfo si in m_sprites.Values )
			{
				if ( si.Sprite != null )
				{
					--si.Timer;
					if ( si.Timer <= 0.0f )
					{
						if ( !si.Sprite.IsAlive )
						{
							si.Sprite = null;
						}
						else
						{
							si.Timer = 100.0f;
						}
					}
				}
			}
		}

		public CutScene Parse( string _cutFile )
		{
			string[] lines = _cutFile.Split( '\n' );

			Dictionary<int, CutSceneSnapshot> snapshots = new Dictionary<int, CutSceneSnapshot>();

			if ( lines[ 0 ][ 0 ] != '#' )
			{
				Debug.Log( "Syntax error line 0: Expected #" );
				return null;
			}
			string cutsceneName = lines[ 0 ].Substring( 2 ).Trim();
			int lineNumber = 1;

			while ( lineNumber < lines.Length )
			{
				if ( lineNumber == ( lines.Length - 1 ) && lines[ lineNumber ] == "" )
				{
					break;
				}
				string[] block = lines[ lineNumber ].Split( ' ' );
				int blockNumber = -1;
				if ( block.Length != 2 || block[ 1 ].Trim() != "{" )
				{
					Debug.Log( "Error when parsing block start at line " + lineNumber + " " + lines.Length );
					return null;
				}
				++lineNumber;
				try
				{
					blockNumber = int.Parse( block[ 0 ].Trim() );
				}
				catch ( System.Exception )
				{
					Debug.Log( "Error when parsing block start at line " + lineNumber );
					return null;
				}
				CutSceneSnapshot newSnapshot;
				if ( !ParseBlock( lines, ref lineNumber, out newSnapshot ) )
				{
					Debug.Log( "At line " + lineNumber );
					return null;
				}
				snapshots[ blockNumber ] = newSnapshot;
			}

			Dictionary<int, int> retargetTable = new Dictionary<int, int>();

			List<CutSceneSnapshot> list = new List<CutSceneSnapshot>();
			foreach ( KeyValuePair<int, CutSceneSnapshot> kvp in snapshots )
			{
				retargetTable[ kvp.Key ] = list.Count;
				list.Add( kvp.Value );
			}
			if ( !retargetTable.ContainsKey( 0 ) )
			{
				Debug.Log( "Error: No input snapshot found! A snapshot with index 0 should exist" );
				return null;
			}

			foreach ( CutSceneSnapshot snapshot in list )
			{
				snapshot.Retarget( retargetTable );
			}

			CutScene cs = new CutScene( cutsceneName, list.ToArray(), retargetTable[ 0 ] );
			foreach ( CutSceneSnapshot snapshot in list )
			{
				snapshot.SetCutScene( cs );
			}
			return cs;
		}

		private bool ParseBlock( string[] _lines, ref int _thisLine, out CutSceneSnapshot _snapshot )
		{
			Sprite[] pictures = new Sprite[ 3 ];
			Strings text = 0;
			List<Strings> choices = new List<Strings>();
			List<int> choicesNext = new List<int>();
			bool ok = false;
			float time = -1.0f;
			int okNext = -1;
			int timeNext = -1;
			_snapshot = null;

			bool reading = true;

			while ( reading )
			{
				string line = _lines[ _thisLine ].Trim();
				Attribute att;
				if ( ParseAttribute( line, out att ) )
				{
					switch ( att.Type )
					{
					case Attribute.AttributeType.LEFT:
					case Attribute.AttributeType.RIGHT:
					case Attribute.AttributeType.CENTER:
						if ( att.ValueString == "none" )
						{
							pictures[ ( int )att.Type ] = null;
						}
						else if ( !TryGetPicture( att.ValueString, out pictures[ ( int )att.Type ] ) )
						{
							Debug.Log( "Unable to find  picture '" + att.ValueString + "' at line " + _thisLine );
							return false;
						}
						break;
					case Attribute.AttributeType.TEXT:
						try
						{
							text = ( Strings )System.Enum.Parse( typeof( Strings ), att.ValueString );
						}
						catch ( System.ArgumentException )
						{
							Debug.Log( att.ValueString + " is not a valid Strings value at line " + _thisLine );
							return false;
						}
						break;
					case Attribute.AttributeType.CHOICE:
						try
						{
							Strings choice = ( Strings )System.Enum.Parse( typeof( Strings ), att.ValueString );
							choices.Add( choice );
							choicesNext.Add( att.ValueInt );
						}
						catch ( System.ArgumentException )
						{
							Debug.Log( att.ValueString + " is not a valid Strings value at line " + _thisLine );
							return false;
						}
						break;
					case Attribute.AttributeType.OK:
						ok = true;
						okNext = att.ValueInt;
						break;
					case Attribute.AttributeType.TIME:
						time = att.ValueFloat;
						timeNext = att.ValueInt;
						break;
					case Attribute.AttributeType.EOB:
						reading = false;
						break;
					}
				}
				else
				{
					Debug.Log( "At line " + _thisLine );
					return false;
				}
				++_thisLine;
				if ( reading && ( _thisLine == _lines.Length ) )
				{
					Debug.Log( "Unexpected end of file" );
					return false;
				}
			}

			_snapshot = new CutSceneSnapshot(
				pictures,
				text,
				( choices.Count > 0 ) ? choices.ToArray() : null,
				ok,
				time,
				okNext,
				timeNext,
				choicesNext.ToArray() );
			return true;
		}

		private bool ParseAttribute( string _line, out Attribute _attribute )
		{
			_attribute = null;
			string[] line = _line.Split( s_attributeDelimiter );
			if ( ( line == null ) || ( line.Length == 0 ) )
			{
				Debug.Log( "Error splitting line" );
				return false;
			}
			for ( int i = 0; i < line.Length; ++i )
			{
				line[ i ] = line[ i ].Trim();
			}

			_attribute = new Attribute();
			switch ( line[ 0 ] )
			{
			case "left":
				if ( line.Length != 2 )
				{
					Debug.Log( "Syntax error: left instruction expects 1 argument" );
					return false;
				}
				_attribute.Type = Attribute.AttributeType.LEFT;
				_attribute.ValueString = line[ 1 ];
				break;
			case "right":
				if ( line.Length != 2 )
				{
					Debug.Log( "Syntax error: right instruction expects 1 argument" );
					return false;
				}
				_attribute.Type = Attribute.AttributeType.RIGHT;
				_attribute.ValueString = line[ 1 ];
				break;
			case "center":
				if ( line.Length != 2 )
				{
					Debug.Log( "Syntax error: center instruction expects 1 argument" );
					return false;
				}
				_attribute.Type = Attribute.AttributeType.CENTER;
				_attribute.ValueString = line[ 1 ];
				break;
			case "text":
				if ( line.Length != 2 )
				{
					Debug.Log( "Syntax error: text instruction expects 1 argument" );
					return false;
				}
				_attribute.Type = Attribute.AttributeType.TEXT;
				_attribute.ValueString = line[ 1 ];
				break;
			case "choice":
				if ( line.Length != 3 )
				{
					Debug.Log( "Syntax error: choice instruction expects 2 arguments" );
					return false;
				}
				_attribute.Type = Attribute.AttributeType.CHOICE;
				_attribute.ValueString = line[ 1 ];
				if ( line[ 2 ] == "end" )
				{
					_attribute.ValueInt = -1;
				}
				else
				{
					try
					{
						_attribute.ValueInt = System.Int32.Parse( line[ 2 ] );
					}
					catch ( System.Exception )
					{
						Debug.Log( "Syntax error: Argument 1 cannot be converted to integer" );
						return false;
					}
				}
				break;
			case "ok":
				if ( line.Length != 2 )
				{
					Debug.Log( "Syntax error: ok instruction expects 1 argument" );
					return false;
				}
				_attribute.Type = Attribute.AttributeType.OK;
				if ( line[ 1 ] == "end" )
				{
					_attribute.ValueInt = -1;
				}
				else
				{
					try
					{
						_attribute.ValueInt = System.Int32.Parse( line[ 1 ] );
					}
					catch ( System.Exception )
					{
						Debug.Log( "Syntax error: Argument 1 cannot be converted to integer" );
						return false;
					}
				}
				break;
			case "time":
				if ( line.Length != 3 )
				{
					Debug.Log( "Syntax error: time instruction expects 2 arguments" );
					return false;
				}
				_attribute.Type = Attribute.AttributeType.TIME;
				try
				{
					_attribute.ValueFloat = System.Single.Parse( line[ 1 ] );
				}
				catch ( System.Exception )
				{
					Debug.Log( "Syntax error: Argument 1 cannot be converted to real" );
					return false;
				}
				if ( line[ 2 ] == "end" )
				{
					_attribute.ValueInt = -1;
				}
				else
				{
					try
					{
						_attribute.ValueInt = System.Int32.Parse( line[ 2 ] );
					}
					catch ( System.Exception )
					{
						Debug.Log( "Syntax error: Argument 2 cannot be converted to integer" );
						return false;
					}
				}
				break;
			case "}":
				_attribute.Type = Attribute.AttributeType.EOB;
				break;
			default:
				Debug.Log( "Syntax error: " );
				return false;
			}
			return true;
		}
	}
}
