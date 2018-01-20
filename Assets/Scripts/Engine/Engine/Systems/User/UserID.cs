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
	public class UserID : System.IComparable, ISerializeRW
	{
		#region Members
		private int m_time;
		private int m_pid;
		private int m_seed;
		private int m_mac;
		#endregion

		#region Methods
		public static UserID Generate( string _seed )
		{
			UserID id = new UserID();
			id.m_time = System.DateTime.Now.GetHashCode();
			id.m_pid = System.Diagnostics.Process.GetCurrentProcess().Id;
			id.m_seed = _seed.GetHashCode();
			System.Net.NetworkInformation.NetworkInterface[] n = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
			if ( n.Length == 0 )
			{
				id.m_mac = 0;
			}
			else
			{
				System.Net.NetworkInformation.PhysicalAddress pa = n[ 0 ].GetPhysicalAddress();
				if ( pa == null )
				{
					id.m_mac = 0;
				}
				else
				{
					id.m_mac = pa.GetHashCode();
				}
			}
			return id;
		}

		public int CompareTo( object _obj )
		{
			UserID other = ( UserID )_obj;
			int dtTime = m_time - other.m_time;
			if ( dtTime == 0 )
			{
				int dtPid = m_pid - other.m_pid;
				if ( dtPid == 0 )
				{
					int dtSeed = m_seed - other.m_seed;
					if ( dtSeed == 0 )
					{
						return m_mac - other.m_mac;
					}
					return dtSeed;
				}
				return dtPid;
			}
			return dtTime;
		}

		public override string ToString()
		{
			return string.Format( "{0:X8}-{1:X4}-{2:X8}-{3:X8}", m_time, m_pid, m_seed, m_mac );
		}

		public void SerializeR( System.IO.BinaryReader _reader )
		{
			m_time = _reader.ReadInt32();
			m_pid = _reader.ReadInt32();
			m_seed = _reader.ReadInt32();
			m_mac = _reader.ReadInt32();
		}

		public void SerializeW( System.IO.BinaryWriter _writer )
		{
			_writer.Write( m_time );
			_writer.Write( m_pid );
			_writer.Write( m_seed );
			_writer.Write( m_mac );
		}
		#endregion
	}
}
