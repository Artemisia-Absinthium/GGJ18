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
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace Engine
{
	[System.Serializable]
	public class UserManager : MonoBehaviour
	{
		#region Methods
		void Awake()
		{
			User.Initialize();

			string userFile = Application.persistentDataPath + "/users.sav";
			if ( File.Exists( userFile ) )
			{
				FileStream file = File.Open( userFile, FileMode.Open );
				CryptoStream stream = new CryptoStream( file, new Encrypter( 8081 ), CryptoStreamMode.Read );
				BinaryReader reader = new BinaryReader( stream );
				User.SerializeR( reader );
				reader.Close();
			}
			else
			{
				User.New( User.kGlobalName, true );
				Directory.CreateDirectory( Application.persistentDataPath );
				FileStream file = File.Create( userFile );
				CryptoStream stream = new CryptoStream( file, new Encrypter( 8081 ), CryptoStreamMode.Write );
				BinaryWriter writer = new BinaryWriter( stream );
				User.SerializeW( writer );
				writer.Close();
			}
		}
		#endregion
	}
}
