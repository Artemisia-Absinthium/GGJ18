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
	public static class TweenFunctions
	{
		#region Delegates
		public delegate float EaseDelegate( float _t, float _b, float _e, float _d );
		#endregion

		#region Members
		private static EaseDelegate[] s_functionTable = null;
		#endregion

		#region Methods
		// Linear
		public static float EaseInLinear( float _t, float _b, float _e, float _d )
		{
			return _e * _t / _d + _b;
		}
		public static float EaseOutLinear( float _t, float _b, float _e, float _d )
		{
			return _e * _t / _d + _b;
		}
		public static float EaseInOutLinear( float _t, float _b, float _e, float _d )
		{
			return _e * _t / _d + _b;
		}
		// Quad
		public static float EaseInQuad( float _t, float _b, float _e, float _d )
		{
			return _e * ( _t /= _d ) * _t + _b;
		}
		public static float EaseOutQuad( float _t, float _b, float _e, float _d )
		{
			return -_e * ( _t /= _d ) * ( _t - 2.0f ) + _b;
		}
		public static float EaseInOutQuad( float _t, float _b, float _e, float _d )
		{
			if ( ( _t /= _d / 2.0f ) < 1.0f )
			{
				return _e / 2.0f * _t * _t + _b;
			}
			else
			{
				return -_e / 2.0f * ( ( --_t ) * ( _t - 2.0f ) - 1.0f ) + _b;
			}
		}
		// Cubic
		public static float EaseInCubic( float _t, float _b, float _e, float _d )
		{
			return _e * ( _t /= _d ) * _t * _t + _b;
		}
		public static float EaseOutCubic( float _t, float _b, float _e, float _d )
		{
			return _e * ( ( _t = _t / _d - 1.0f ) * _t * _t + 1.0f ) + _b;
		}
		public static float EaseInOutCubic( float _t, float _b, float _e, float _d )
		{
			if ( ( _t /= _d / 2.0f ) < 1.0f )
			{
				return _e / 2.0f * _t * _t * _t + _b;
			}
			else
			{
				return _e / 2.0f * ( ( _t -= 2.0f ) * _t * _t + 2.0f ) + _b;
			}
		}
		// Quart
		public static float EaseInQuart( float _t, float _b, float _e, float _d )
		{
			return _e * ( _t /= _d ) * _t * _t * _t + _b;
		}
		public static float EaseOutQuart( float _t, float _b, float _e, float _d )
		{
			return -_e * ( ( _t = _t / _d - 1.0f ) * _t * _t * _t - 1.0f ) + _b;
		}
		public static float EaseInOutQuart( float _t, float _b, float _e, float _d )
		{
			if ( ( _t /= _d / 2.0f ) < 1.0f )
			{
				return _e / 2.0f * _t * _t * _t * _t + _b;
			}
			else
			{
				return -_e / 2.0f * ( ( _t -= 2.0f ) * _t * _t * _t - 2.0f ) + _b;
			}
		}
		// Quint
		public static float EaseInQuint( float _t, float _b, float _e, float _d )
		{
			return _e * ( _t /= _d ) * _t * _t * _t * _t + _b;
		}
		public static float EaseOutQuint( float _t, float _b, float _e, float _d )
		{
			return _e * ( ( _t = _t / _d - 1.0f ) * _t * _t * _t * _t + 1 ) + _b;
		}
		public static float EaseInOutQuint( float _t, float _b, float _e, float _d )
		{
			if ( ( _t /= _d / 2.0f ) < 1.0f )
			{
				return _e / 2.0f * _t * _t * _t * _t * _t + _b;
			}
			else
			{
				return _e / 2.0f * ( ( _t -= 2.0f ) * _t * _t * _t * _t + 2.0f ) + _b;
			}
		}
		// Sine
		public static float EaseInSine( float _t, float _b, float _e, float _d )
		{
			return -_e * Mathf.Cos( _t / _d * ( Mathf.PI / 2.0f ) ) + _e + _b;
		}
		public static float EaseOutSine( float _t, float _b, float _e, float _d )
		{
			return _e * Mathf.Sin( _t / _d * ( Mathf.PI / 2.0f ) ) + _b;
		}
		public static float EaseInOutSine( float _t, float _b, float _e, float _d )
		{
			return -_e / 2.0f * ( Mathf.Cos( Mathf.PI * _t / _d ) - 1 ) + _b;
		}
		// Expo
		public static float EaseInExpo( float _t, float _b, float _e, float _d )
		{
			return ( _t == 0.0f ) ? _b : _e * Mathf.Pow( 2.0f, 10 * ( _t / _d - 1.0f ) ) + _b;
		}
		public static float EaseOutExpo( float _t, float _b, float _e, float _d )
		{
			return ( _t == _d ) ? _b + _e : _e * ( -Mathf.Pow( 2.0f, -10.0f * _t / _d ) + 1.0f ) + _b;
		}
		public static float EaseInOutExpo( float _t, float _b, float _e, float _d )
		{
			if ( _t == 0.0f )
			{
				return _b;
			}
			else if ( _t == _d )
			{
				return _b + _e;
			}
			else if ( ( _t /= _d / 2.0f ) < 1.0f )
			{
				return _e / 2.0f * Mathf.Pow( 2.0f, 10.0f * ( _t - 1.0f ) ) + _b;
			}
			else
			{
				return _e / 2.0f * ( -Mathf.Pow( 2.0f, -10.0f * --_t ) + 2.0f ) + _b;
			}
		}
		// Circ
		public static float EaseInCirc( float _t, float _b, float _e, float _d )
		{
			return -_e * ( Mathf.Sqrt( 1.0f - ( _t /= _d ) * _t ) - 1.0f ) + _b;
		}
		public static float EaseOutCirc( float _t, float _b, float _e, float _d )
		{
			return _e * Mathf.Sqrt( 1.0f - ( _t = _t / _d - 1.0f ) * _t ) + _b;
		}
		public static float EaseInOutCirc( float _t, float _b, float _e, float _d )
		{
			if ( ( _t /= _d / 2.0f ) < 1.0f )
			{
				return -_e / 2.0f * ( Mathf.Sqrt( 1.0f - _t * _t ) - 1.0f ) + _b;
			}
			else
			{
				return _e / 2.0f * ( Mathf.Sqrt( 1.0f - ( _t -= 2.0f ) * _t ) + 1.0f ) + _b;
			}
		}
		// Elastic
		public static float EaseInElastic( float _t, float _b, float _e, float _d )
		{
			if ( _t == 0 )
			{
				return _b;
			}
			else if ( ( _t /= _d ) == 1 )
			{
				return _b + _e;
			}
			float p = _d * 0.3f;
			float a = _e;
			float s = p / 4.0f;
			float postFix = a * Mathf.Pow( 2.0f, 10.0f * ( _t -= 1.0f ) );
			return -( postFix * Mathf.Sin( ( _t * _d - s ) * ( 2.0f * Mathf.PI ) / p ) ) + _b;
		}
		public static float EaseOutElastic( float _t, float _b, float _e, float _d )
		{
			if ( _t == 0 )
			{
				return _b;
			}
			else if ( ( _t /= _d ) == 1 )
			{
				return _b + _e;
			}
			float p = _d * 0.3f;
			float a = _e;
			float s = p / 4.0f;
			return ( a * Mathf.Pow( 2.0f, -10.0f * _t ) * Mathf.Sin( ( _t * _d - s ) * ( 2.0f * Mathf.PI ) / p ) + _e + _b );
		}
		public static float EaseInOutElastic( float _t, float _b, float _e, float _d )
		{
			if ( _t == 0.0f )
			{
				return _b;
			}
			else if ( ( _t /= _d / 2.0f ) == 2.0f )
			{
				return _b + _e;
			}
			float p = _d * ( 0.3f * 1.5f );
			float a = _e;
			float s = p / 4.0f;
			if ( _t < 1.0f )
			{
				float postFix1 = a * Mathf.Pow( 2.0f, 10.0f * ( _t -= 1.0f ) );
				return -0.5f * ( postFix1 * Mathf.Sin( ( _t * _d - s ) * ( 2.0f * Mathf.PI ) / p ) ) + _b;
			}
			float postFix2 = a * Mathf.Pow( 2.0f, -10.0f * ( _t -= 1.0f ) );
			return postFix2 * Mathf.Sin( ( _t * _d - s ) * ( 2.0f * Mathf.PI ) / p ) * .5f + _e + _b;
		}
		// Back
		public static float EaseInBack( float _t, float _b, float _e, float _d )
		{
			float s = 1.70158f;
			float postFix = _t /= _d;
			return _e * ( postFix ) * _t * ( ( s + 1.0f ) * _t - s ) + _b;
		}
		public static float EaseOutBack( float _t, float _b, float _e, float _d )
		{
			float s = 1.70158f;
			return _e * ( ( _t = _t / _d - 1.0f ) * _t * ( ( s + 1.0f ) * _t + s ) + 1.0f ) + _b;
		}
		public static float EaseInOutBack( float _t, float _b, float _e, float _d )
		{
			float s = 1.70158f;
			if ( ( _t /= _d / 2.0f ) < 1.0f )
			{
				return _e / 2.0f * ( _t * _t * ( ( ( s *= 1.525f ) + 1.0f ) * _t - s ) ) + _b;
			}
			else
			{
				float postFix = _t -= 2.0f;
				return _e / 2.0f * ( postFix * _t * ( ( ( s *= 1.525f ) + 1.0f ) * _t + s ) + 2.0f ) + _b;
			}
		}
		// Bounce
		public static float EaseInBounce( float _t, float _b, float _e, float _d )
		{
			return _e - EaseOutBounce( _d - _t, 0, _e, _d ) + _b;
		}
		public static float EaseOutBounce( float _t, float _b, float _e, float _d )
		{
			if ( ( _t /= _d ) < 1.0f / 2.75f )
			{
				return _e * ( 7.5625f * _t * _t ) + _b;
			}
			else if ( _t < 2.0f / 2.75f )
			{
				return _e * ( 7.5625f * ( _t -= 1.5f / 2.75f ) * _t + 0.75f ) + _b;
			}
			else if ( _t < 2.5f / 2.75f )
			{
				return _e * ( 7.5625f * ( _t -= 2.25f / 2.75f ) * _t + 0.9375f ) + _b;
			}
			else
			{
				return _e * ( 7.5625f * ( _t -= 2.625f / 2.75f ) * _t + 0.984375f ) + _b;
			}
		}
		public static float EaseInOutBounce( float _t, float _b, float _e, float _d )
		{
			if ( _t < _d / 2 )
			{
				return EaseInBounce( _t * 2.0f, 0.0f, _e, _d ) * 0.5f + _b;
			}
			else
			{
				return EaseOutBounce( _t * 2.0f - _d, 0.0f, _e, _d ) * 0.5f + _e * 0.5f + _b;
			}
		}

		public static float Ease( TweenEase _ease, float _t, float _b, float _e, float _d )
		{
			if ( s_functionTable == null )
			{
				s_functionTable = new EaseDelegate[ ( int )TweenEase.NONE + 1 ];
				s_functionTable[ ( int )TweenEase.IN_LINEAR ] = new EaseDelegate( EaseInLinear );
				s_functionTable[ ( int )TweenEase.OUT_LINEAR ] = new EaseDelegate( EaseOutLinear );
				s_functionTable[ ( int )TweenEase.INOUT_LINEAR ] = new EaseDelegate( EaseInOutLinear );
				s_functionTable[ ( int )TweenEase.IN_QUAD ] = new EaseDelegate( EaseInQuad );
				s_functionTable[ ( int )TweenEase.OUT_QUAD ] = new EaseDelegate( EaseOutQuad );
				s_functionTable[ ( int )TweenEase.INOUT_QUAD ] = new EaseDelegate( EaseInOutQuad );
				s_functionTable[ ( int )TweenEase.IN_CUBIC ] = new EaseDelegate( EaseInCubic );
				s_functionTable[ ( int )TweenEase.OUT_CUBIC ] = new EaseDelegate( EaseOutCubic );
				s_functionTable[ ( int )TweenEase.INOUT_CUBIC ] = new EaseDelegate( EaseInOutCubic );
				s_functionTable[ ( int )TweenEase.IN_QUART ] = new EaseDelegate( EaseInQuart );
				s_functionTable[ ( int )TweenEase.OUT_QUART ] = new EaseDelegate( EaseOutQuart );
				s_functionTable[ ( int )TweenEase.INOUT_QUART ] = new EaseDelegate( EaseInOutQuart );
				s_functionTable[ ( int )TweenEase.IN_QUINT ] = new EaseDelegate( EaseInQuint );
				s_functionTable[ ( int )TweenEase.OUT_QUINT ] = new EaseDelegate( EaseOutQuint );
				s_functionTable[ ( int )TweenEase.INOUT_QUINT ] = new EaseDelegate( EaseInOutQuint );
				s_functionTable[ ( int )TweenEase.IN_SINE ] = new EaseDelegate( EaseInSine );
				s_functionTable[ ( int )TweenEase.OUT_SINE ] = new EaseDelegate( EaseOutSine );
				s_functionTable[ ( int )TweenEase.INOUT_SINE ] = new EaseDelegate( EaseInOutSine );
				s_functionTable[ ( int )TweenEase.IN_EXPO ] = new EaseDelegate( EaseInExpo );
				s_functionTable[ ( int )TweenEase.OUT_EXPO ] = new EaseDelegate( EaseOutExpo );
				s_functionTable[ ( int )TweenEase.INOUT_EXPO ] = new EaseDelegate( EaseInOutExpo );
				s_functionTable[ ( int )TweenEase.IN_CIRC ] = new EaseDelegate( EaseInCirc );
				s_functionTable[ ( int )TweenEase.OUT_CIRC ] = new EaseDelegate( EaseOutCirc );
				s_functionTable[ ( int )TweenEase.INOUT_CIRC ] = new EaseDelegate( EaseInOutCirc );
				s_functionTable[ ( int )TweenEase.IN_ELASTIC ] = new EaseDelegate( EaseInElastic );
				s_functionTable[ ( int )TweenEase.OUT_ELASTIC ] = new EaseDelegate( EaseOutElastic );
				s_functionTable[ ( int )TweenEase.INOUT_ELASTIC ] = new EaseDelegate( EaseInOutElastic );
				s_functionTable[ ( int )TweenEase.IN_BACK ] = new EaseDelegate( EaseInBack );
				s_functionTable[ ( int )TweenEase.OUT_BACK ] = new EaseDelegate( EaseOutBack );
				s_functionTable[ ( int )TweenEase.INOUT_BACK ] = new EaseDelegate( EaseInOutBack );
				s_functionTable[ ( int )TweenEase.IN_BOUNCE ] = new EaseDelegate( EaseInBounce );
				s_functionTable[ ( int )TweenEase.OUT_BOUNCE ] = new EaseDelegate( EaseOutBounce );
				s_functionTable[ ( int )TweenEase.INOUT_BOUNCE ] = new EaseDelegate( EaseInOutBounce );
				s_functionTable[ ( int )TweenEase.NONE ] = new EaseDelegate( EaseInLinear );
			}
			return s_functionTable[ ( int )_ease ]( _t, _b, _e, _d );
		}
		#endregion
	}
}
