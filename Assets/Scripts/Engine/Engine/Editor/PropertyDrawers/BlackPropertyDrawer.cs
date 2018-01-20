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
using UnityEditor;

namespace Engine
{
	[CustomPropertyDrawer( typeof( Black.Short ) )]
	public class BlackShortPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );

			int value = EditorGUI.IntField( position, label, Black.Short.FromStamp( propertyValue.longValue ) );

			propertyValue.longValue = Black.Short.ToStamp( ( short )value );

			EditorGUI.EndProperty();
		}
	}
	[CustomPropertyDrawer( typeof( Black.Ushort ) )]
	public class BlackUShortPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );

			int value = EditorGUI.IntField( position, label, Black.Ushort.FromStamp( propertyValue.longValue ) );

			propertyValue.longValue = Black.Ushort.ToStamp( ( ushort )( value & 0xffff ) );

			EditorGUI.EndProperty();
		}
	}
	[CustomPropertyDrawer( typeof( Black.Int ) )]
	public class BlackIntPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );
			
			int value = EditorGUI.IntField( position, label, Black.Int.FromStamp( propertyValue.longValue ) );

			propertyValue.longValue = Black.Int.ToStamp( value );

			EditorGUI.EndProperty();
		}
	}
	[CustomPropertyDrawer( typeof( Black.Uint ) )]
	public class BlackUintPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );

			long value = EditorGUI.LongField( position, label, Black.Uint.FromStamp( propertyValue.longValue ) );

			propertyValue.longValue = Black.Uint.ToStamp( ( uint )( value & 0xffffffff ) );

			EditorGUI.EndProperty();
		}
	}
	[CustomPropertyDrawer( typeof( Black.Long ) )]
	public class BlackLongPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );

			long value = EditorGUI.LongField( position, label, Black.Long.FromStamp( propertyValue.longValue ) );

			propertyValue.longValue = Black.Long.ToStamp( value );

			EditorGUI.EndProperty();
		}
	}
	[CustomPropertyDrawer( typeof( Black.Ulong ) )]
	public class BlackUlongPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );

			ulong oldValue = Black.Ulong.FromStamp( propertyValue.longValue );
			string newValue = EditorGUI.TextField( position, label, oldValue.ToString() );

			float result = ExpressionEvaluator.Evaluate<float>( newValue );

			propertyValue.longValue = Black.Ulong.ToStamp( result > 0.0f ? ( ulong )result : 0 );

			EditorGUI.EndProperty();
		}
	}
	[CustomPropertyDrawer( typeof( Black.Bool ) )]
	public class BlackBoolPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );

			bool value = EditorGUI.Toggle( position, label, Black.Bool.FromStamp( propertyValue.longValue ) );

			propertyValue.longValue = Black.Bool.ToStamp( value );

			EditorGUI.EndProperty();
		}
	}
	[CustomPropertyDrawer( typeof( Black.Float ) )]
	public class BlackFloatPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );

			float value = EditorGUI.FloatField( position, label, Black.Float.FromStamp( propertyValue.longValue ) );

			propertyValue.longValue = Black.Float.ToStamp( value );

			EditorGUI.EndProperty();
		}
	}
	[CustomPropertyDrawer( typeof( Black.Double) )]
	public class BlackDoublePropertyDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{

			EditorGUI.BeginProperty( position, label, property );

			SerializedProperty propertyValue = property.FindPropertyRelative( "m_value" );

			double value = EditorGUI.DoubleField( position, label, Black.Double.FromStamp( propertyValue.longValue ) );

			propertyValue.longValue = Black.Double.ToStamp( value );

			EditorGUI.EndProperty();
		}
	}
}
