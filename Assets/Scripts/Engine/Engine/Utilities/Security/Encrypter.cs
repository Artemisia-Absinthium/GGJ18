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
using System.Security.Cryptography;

namespace Engine
{
	[System.Serializable]
	public sealed class Encrypter : ICryptoTransform
	{
		#region Members
		private System.Random m_rand;
		private static byte[] s_masks =
		{
			0x0F, 0x17, 0x1B, 0x1D, 0x1E, 0x27, 0x2B, 0x2D,
			0x2E, 0x33, 0x35, 0x36, 0x39, 0x3A, 0x3C, 0x47,
			0x4B, 0x4D, 0x4E, 0x53, 0x55, 0x56, 0x59, 0x5A,
			0x5C, 0x63, 0x65, 0x66, 0x69, 0x6A, 0x6C, 0x71,
			0x72, 0x74, 0x78, 0x87, 0x8B, 0x8D, 0x8E, 0x93,
			0x95, 0x96, 0x99, 0x9A, 0x9C, 0xA3, 0xA5, 0xA6,
			0xA9, 0xAA, 0xAC, 0xB1, 0xB2, 0xB4, 0xB8, 0xC3,
			0xC5, 0xC6, 0xC9, 0xCA, 0xCC, 0xD1, 0xD2, 0xD4,
			0xD8, 0xE1, 0xE2, 0xE4, 0xE8, 0xF0, 0x00, 0xFF
		};
		public static int s_maskCount = 72;
		#endregion

		#region Properties
		public bool CanReuseTransform
		{
			get { return true; }
		}
		public bool CanTransformMultipleBlocks
		{
			get { return true; }
		}
		public int InputBlockSize
		{ 
			get { return 1024; }
		}
		public int OutputBlockSize
		{
			get { return 1024; }
		}
		#endregion

		#region Methods
		public Encrypter( int _seed )
		{
			m_rand = new System.Random( _seed );
		}

		public void Dispose()
		{

		}

		public int TransformBlock( byte[] _inputBuffer, int _inputOffset, int _inputCount, byte[] _outputBuffer, int _outputOffset )
		{
			for ( int i = 0; i < _inputCount; ++i )
			{
				_outputBuffer[ i + _outputOffset ] = ( byte )( _inputBuffer[ i + _inputOffset ] ^ s_masks[ m_rand.Next( 0, s_maskCount - 1 ) ] );
			}
			return _inputCount;
		}

		public byte[] TransformFinalBlock( byte[] _inputBuffer, int _inputOffset, int _inputCount )
		{
			byte[] outputBuffer = new byte[ _inputCount ];
			TransformBlock( _inputBuffer, _inputOffset, _inputCount, outputBuffer, 0 );
			return outputBuffer;
		}
		#endregion
	}
}
