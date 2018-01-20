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
	public class GameValidations
	{
		#region Fields
		#endregion

		#region Members
		private static byte[] s_engineKey =
		{
			0x02, 0x03, 0x19, 0x94,
			0x06, 0x10, 0x19, 0x95,
			0x04, 0x11, 0x20, 0x17,

			0x01, 0x06, 0x19, 0x50,
			0x01, 0x10, 0x19, 0x69,

			0x26, 0x11, 0x19, 0x85,
			0x20, 0x09, 0x19, 0x92,
			0x19, 0x08, 0x19, 0x95
		};
		#endregion

		#region Properties
		#endregion

		#region Methods
		private bool CheckKey( string _key )
		{
			byte[] keyChars = System.Text.Encoding.ASCII.GetBytes( _key );
			byte[] keyBytes = new byte[ 32 ];

			for ( int i = 0; i < 32; ++i )
			{
				if ( keyChars[ 2 * i ] < 'a' )
				{
					keyBytes[ i ] = ( byte )( ( keyChars[ 2 * i ] - ( byte )'0' ) << 4 );
				}
				else
				{
					keyBytes[ i ] = ( byte )( 0xA0 + ( ( keyChars[ 2 * i ] - ( byte )'a' ) << 4 ) );
				}
				if ( keyChars[ 2 * i + 1 ] < 'a' )
				{
					keyBytes[ i ] |= ( byte )( ( keyChars[ 2 * i + 1 ] - ( byte )'0' ) & 0xf );
				}
				else
				{
					keyBytes[ i ] |= ( byte )( 0xA + ( ( keyChars[ 2 * i + 1 ] - ( byte )'a' ) & 0xf ) );
				}
			}

			for ( int i = 0; i < 16; ++i )
			{
				byte A = ( byte )( keyBytes[ i ] ^ s_engineKey[ i ] );
				byte B = ( byte )( keyBytes[ i + 16 ] ^ s_engineKey[ 31 - i ] );
				if ( A != B )
				{
					return false;
				}
			}

			return true;
		}

		public bool Validate()
		{
#if UNITY_EDITOR // UNITY_STANDALONE_WIN
			string[] args = System.Environment.GetCommandLineArgs();
			foreach ( string arg in args )
			{
				if ( arg.Length == 69 && arg.StartsWith( "+key:" ) )
				{
					if ( CheckKey( arg.Substring( 5 ) ) )
					{
						return true;
					}
				}
			}
			return true; // false; // In real life because program is not started with right arguments
#else
			return true;
#endif
		}
		#endregion
	}
}
