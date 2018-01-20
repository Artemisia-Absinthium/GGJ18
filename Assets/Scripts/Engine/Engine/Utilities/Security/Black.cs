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
	public class Black
	{
		private static long s_mask = 0x8a957def4261b3cL;
		public static long Mask { get { return s_mask; } }

		public enum Type
		{
			SHORT, USHORT, INT, UINT, LONG, ULONG, BOOL, FLOAT, DOUBLE
		}

		[System.Serializable]
		public class TypeBase
		{
			[SerializeField]
			protected long m_value;
		}

		[System.Serializable]
		public class Short : TypeBase
		{
			public Short()
			{
				m_value = Mask;
			}
			public Short( short _s )
			{
				m_value = Mask ^ _s;
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator short( Short _s )
			{
				return ( short )( _s.m_value ^ Mask );
			}
			public static implicit operator Short( short _s )
			{
				return new Short( _s );
			}
			public static short FromStamp( long _s )
			{
				return ( short )( _s ^ Mask );
			}
			public static long ToStamp( short _s )
			{
				return Mask ^ _s;
			}
		}
		[System.Serializable]
		public class Ushort : TypeBase
		{
			public Ushort()
			{
				m_value = Mask;
			}
			public Ushort( ushort _s )
			{
				m_value = Mask ^ _s;
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator ushort( Ushort _s )
			{
				return ( ushort )( _s.m_value ^ Mask );
			}
			public static implicit operator Ushort( ushort _s )
			{
				return new Ushort( _s );
			}
			public static ushort FromStamp( long _s )
			{
				return ( ushort )( _s ^ Mask );
			}
			public static long ToStamp( ushort _s )
			{
				return Mask ^ _s;
			}
		}
		[System.Serializable]
		public class Int : TypeBase
		{
			public Int()
			{
				m_value = Mask;
			}
			public Int( int _s )
			{
				m_value = Mask ^ _s;
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator int( Int _s )
			{
				return ( int )( _s.m_value ^ Mask );
			}
			public static implicit operator Int( int _s )
			{
				return new Int( _s );
			}
			public static int FromStamp( long _s )
			{
				return ( int )( _s ^ Mask );
			}
			public static long ToStamp( int _s )
			{
				return Mask ^ _s;
			}
		}
		[System.Serializable]
		public class Uint : TypeBase
		{
			public Uint()
			{
				m_value = Mask;
			}
			public Uint( uint _s )
			{
				m_value = Mask ^ _s;
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator uint( Uint _s )
			{
				return ( uint )( _s.m_value ^ Mask );
			}
			public static implicit operator Uint( uint _s )
			{
				return new Uint( _s );
			}
			public static uint FromStamp( long _s )
			{
				return ( uint )( _s ^ Mask );
			}
			public static long ToStamp( uint _s )
			{
				return Mask ^ _s;
			}
		}
		[System.Serializable]
		public class Long : TypeBase
		{
			public Long()
			{
				m_value = Mask;
			}
			public Long( long _s )
			{
				m_value = Mask ^ _s;
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator long( Long _s )
			{
				return ( _s.m_value ^ Mask );
			}
			public static implicit operator Long( long _s )
			{
				return new Long( _s );
			}
			public static long FromStamp( long _s )
			{
				return ( _s ^ Mask );
			}
			public static long ToStamp( long _s )
			{
				return Mask ^ _s;
			}
		}
		[System.Serializable]
		public class Ulong : TypeBase
		{
			public Ulong()
			{
				m_value = Mask;
			}
			public Ulong( ulong _s )
			{
				unchecked
				{
					m_value = Mask ^ ( long )_s;
				}
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator ulong( Ulong _s )
			{
				unchecked
				{
					return ( ulong )( _s.m_value ^ Mask );
				}
			}
			public static implicit operator Ulong( ulong _s )
			{
				return new Ulong( _s );
			}
			public static ulong FromStamp( long _s )
			{
				unchecked
				{
					return ( ulong )( _s ^ Mask );
				}
			}
			public static long ToStamp( ulong _s )
			{
				unchecked
				{
					return Mask ^ ( long )_s;
				}
			}
		}
		[System.Serializable]
		public class Bool : TypeBase
		{
			public Bool()
			{
				m_value = Mask;
			}
			public Bool( bool _s )
			{
				m_value = _s ? Mask ^ 1 : Mask;
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator bool( Bool _s )
			{
				return ( _s.m_value ^ Mask ) == 1;
			}
			public static implicit operator Bool( bool _s )
			{
				return new Bool( _s );
			}
			public static bool FromStamp( long _s )
			{
				return ( _s ^ Mask ) == 1;
			}
			public static long ToStamp( bool _s )
			{
				return _s ? Mask ^ 1 : Mask;
			}
		}
		[System.Serializable]
		public class Float : TypeBase
		{
			public Float()
			{
				m_value = Mask;
			}
			public Float( float _s )
			{
				unsafe
				{
					m_value = Mask ^ *( ( long* )&_s );
				}
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator float( Float _s )
			{
				unsafe
				{
					long value = _s.m_value;
					return *( ( float* )&value );
				}
			}
			public static implicit operator Float( float _s )
			{
				return new Float( _s );
			}
			public static float FromStamp( long _s )
			{
				unsafe
				{
					return *( ( float* )&_s );
				}
			}
			public static long ToStamp( float _s )
			{
				unsafe
				{
					return Mask ^ *( ( long* )&_s );
				}
			}
		}
		[System.Serializable]
		public class Double : TypeBase
		{
			public Double()
			{
				m_value = Mask;
			}
			public Double( double _s )
			{
				unsafe
				{
					m_value = Mask ^ *( ( long* )&_s );
				}
			}
			public override string ToString()
			{
				return FromStamp( m_value ).ToString();
			}
			public static implicit operator double( Double _s )
			{
				unsafe
				{
					long value = _s.m_value;
					return *( ( double* )&value );
				}
			}
			public static implicit operator Double( double _s )
			{
				return new Double( _s );
			}
			public static double FromStamp( long _s )
			{
				unsafe
				{
					return *( ( double* )&_s );
				}
			}
			public static long ToStamp( double _s )
			{
				unsafe
				{
					return Mask ^ *( ( long* )&_s );
				}
			}
		}
		[System.Serializable]
		public class Detector
		{
			private long m_hash = 0;
			private long m_hide = 0;
			private long m_value;
			private Type m_type;
			private static System.Random s_rand = null;
			System.Action m_callback = null;
			public Detector( Short _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				m_value = _var;
				m_hide = _var ^ m_hash;
			}
			public Detector( Ushort _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				m_value = _var;
				m_hide = _var ^ m_hash;
			}
			public Detector( Int _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				m_value = _var;
				m_hide = _var ^ m_hash;
			}
			public Detector( Uint _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				m_value = _var;
				m_hide = _var ^ m_hash;
			}
			public Detector( Long _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				m_value = _var;
				m_hide = _var ^ m_hash;
			}
			public Detector( Ulong _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				unchecked
				{
					ulong value = _var;
					m_value = ( long )value;
					m_hide = ( long )value ^ m_hash;
				}
			}
			public Detector( Bool _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				m_value = _var ? 1 : 0;
				m_hide = _var ? m_hash ^ 1 : m_hash;
			}
			public Detector( Float _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				unsafe
				{
					float value = _var;
					m_value = *( ( long* )&value );
					m_hide = m_value ^ m_hash;
				}
			}
			public Detector( Double _var, Type _type, System.Action _onDetect )
			{
				Init( _type, _onDetect );
				unsafe
				{
					double value = _var;
					m_value = *( ( long* )&value );
					m_hide = m_value ^ m_hash;
				}
			}
			private void Init( Type _type, System.Action _onDetect )
			{
				if ( s_rand == null )
				{
					s_rand = new System.Random( ( int )System.DateTime.Now.Ticks );
				}
				m_hash = GenNext();
				m_type = _type;
				m_callback = _onDetect;
			}
			private long GenNext()
			{
				return ( ( ( uint )s_rand.Next() ) << 32 ) | (uint)s_rand.Next();
			}
			public bool Check( Short _var )
			{
				if ( m_type == Type.SHORT )
				{
					short check = ( short )( m_hash ^ m_hide );
					short value = ( short )m_value;
					if ( check != value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					m_value = _var;
					m_hide = _var ^ m_hash;
				}
				return true;
			}
			public bool Check( Ushort _var )
			{
				if ( m_type == Type.USHORT )
				{
					ushort check = ( ushort )( m_hash ^ m_hide );
					ushort value = ( ushort )m_value;
					if ( check != value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					m_value = _var;
					m_hide = _var ^ m_hash;
				}
				return true;
			}
			public bool Check( Int _var )
			{
				if ( m_type == Type.INT )
				{
					int check = ( int )( m_hash ^ m_hide );
					int value = ( int )m_value;
					if ( check != value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					m_value = _var;
					m_hide = _var ^ m_hash;
				}
				return true;
			}
			public bool Check( Uint _var )
			{
				if ( m_type == Type.UINT )
				{
					uint check = ( uint )( m_hash ^ m_hide );
					uint value = ( uint )m_value;
					if ( check != value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					m_value = _var;
					m_hide = _var ^ m_hash;
				}
				return true;
			}
			public bool Check( Long _var )
			{
				if ( m_type == Type.LONG )
				{
					long check = m_hash ^ m_hide;
					if ( check != m_value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					m_value = _var;
					m_hide = _var ^ m_hash;
				}
				return true;
			}
			public bool Check( Ulong _var )
			{
				if ( m_type == Type.ULONG )
				{
					ulong check = 0;
					ulong value = 0;
					unchecked
					{
						check = ( ulong )( m_hash ^ m_hide );
						value = ( ulong )m_value;
					}
					if ( check != value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					unchecked
					{
						value = _var;
						m_value = ( long )value;
						m_hide = ( long )value ^ m_hash;
					}
				}
				return true;
			}
			public bool Check( Bool _var )
			{
				if ( m_type == Type.BOOL )
				{
					bool check = ( m_hash ^ m_hide ) == 1;
					bool value = m_value == 1;
					if ( check != value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					m_value = _var ? 1 : 0;
					m_hide = _var ? m_hash ^ 1 : m_hash;
				}
				return true;
			}
			public bool Check( Float _var )
			{
				if ( m_type == Type.FLOAT )
				{
					float check = 0;
					float value = 0;
					long lValue = m_hash ^ m_hide;
					unsafe
					{
						check = *( ( float* )&lValue );
						lValue = m_value;
						value = *( ( float* )&lValue );
					}
					if ( check != value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					unsafe
					{
						value = _var;
						m_value = *( ( long* )&value );
						m_hide = m_value ^ m_hash;
					}
				}
				return true;
			}
			public bool Check( Double _var )
			{
				if ( m_type == Type.DOUBLE )
				{
					double check = 0;
					double value = 0;
					long lValue = m_hash ^ m_hide;
					unsafe
					{
						check = *( ( double* )&lValue );
						lValue = m_value;
						value = *( ( double* )&lValue );
					}
					if ( check != value )
					{
						if ( m_callback != null )
						{
							m_callback.Invoke();
						}
						return false;
					}
					m_hash = GenNext();
					unsafe
					{
						value = _var;
						m_value = *( ( long* )&value );
						m_hide = m_value ^ m_hash;
					}
				}
				return true;
			}
		}
	}
}
