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
	public class RigidbodySerializer : ISerializeRW
	{
		#region Members
		private Rigidbody m_rigidbdy = null;
		#endregion

		#region Contructors
		public RigidbodySerializer()
		{

		}

		public RigidbodySerializer( Rigidbody _rigidbody )
		{
			m_rigidbdy = _rigidbody;
		}
		#endregion

		#region Methods
		public void Set( Rigidbody _rigidbody )
		{
			m_rigidbdy = _rigidbody;
		}

		public void SerializeR( System.IO.BinaryReader _reader )
		{
			m_rigidbdy.angularDrag = _reader.ReadSingle();
			m_rigidbdy.angularVelocity = new Vector3( _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle() );
			m_rigidbdy.centerOfMass = new Vector3( _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle() );
			m_rigidbdy.collisionDetectionMode = ( CollisionDetectionMode )_reader.ReadInt32();
			m_rigidbdy.constraints = ( RigidbodyConstraints )_reader.ReadInt32();
			m_rigidbdy.detectCollisions = _reader.ReadBoolean();
			m_rigidbdy.drag = _reader.ReadSingle();
			m_rigidbdy.freezeRotation = _reader.ReadBoolean();
			Vector3 inertiaTensor = new Vector3( _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle() );
			if ( inertiaTensor.x != 0 && inertiaTensor.y != 0 & inertiaTensor.z != 0 )
			{
				m_rigidbdy.inertiaTensor = inertiaTensor;
			}
			m_rigidbdy.inertiaTensorRotation = new Quaternion( _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle() );
			m_rigidbdy.interpolation = ( RigidbodyInterpolation )_reader.ReadInt32();
			m_rigidbdy.isKinematic = _reader.ReadBoolean();
			m_rigidbdy.mass = _reader.ReadSingle();
			m_rigidbdy.maxAngularVelocity = _reader.ReadSingle();
			m_rigidbdy.maxDepenetrationVelocity = _reader.ReadSingle();
			m_rigidbdy.position = new Vector3( _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle() );
			m_rigidbdy.rotation = new Quaternion( _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle() );
			m_rigidbdy.sleepThreshold = _reader.ReadSingle();
			m_rigidbdy.solverIterations = _reader.ReadInt32();
			m_rigidbdy.solverVelocityIterations = _reader.ReadInt32();
			m_rigidbdy.useGravity = _reader.ReadBoolean();
			m_rigidbdy.velocity = new Vector3( _reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle() );
		}

		public void SerializeW( System.IO.BinaryWriter _writer )
		{
			_writer.Write( m_rigidbdy.angularDrag );
			_writer.Write( m_rigidbdy.angularVelocity.x );
			_writer.Write( m_rigidbdy.angularVelocity.y );
			_writer.Write( m_rigidbdy.angularVelocity.z );
			_writer.Write( m_rigidbdy.centerOfMass.x );
			_writer.Write( m_rigidbdy.centerOfMass.y );
			_writer.Write( m_rigidbdy.centerOfMass.z );
			_writer.Write( ( int )m_rigidbdy.collisionDetectionMode );
			_writer.Write( ( int )m_rigidbdy.constraints );
			_writer.Write( m_rigidbdy.detectCollisions );
			_writer.Write( m_rigidbdy.drag );
			_writer.Write( m_rigidbdy.freezeRotation );
			_writer.Write( m_rigidbdy.inertiaTensor.x );
			_writer.Write( m_rigidbdy.inertiaTensor.y );
			_writer.Write( m_rigidbdy.inertiaTensor.z );
			_writer.Write( m_rigidbdy.inertiaTensorRotation.x );
			_writer.Write( m_rigidbdy.inertiaTensorRotation.y );
			_writer.Write( m_rigidbdy.inertiaTensorRotation.z );
			_writer.Write( m_rigidbdy.inertiaTensorRotation.w );
			_writer.Write( ( int )m_rigidbdy.interpolation );
			_writer.Write( m_rigidbdy.isKinematic );
			_writer.Write( m_rigidbdy.mass );
			_writer.Write( m_rigidbdy.maxAngularVelocity );
			_writer.Write( m_rigidbdy.maxDepenetrationVelocity );
			_writer.Write( m_rigidbdy.position.x );
			_writer.Write( m_rigidbdy.position.y );
			_writer.Write( m_rigidbdy.position.z );
			_writer.Write( m_rigidbdy.rotation.x );
			_writer.Write( m_rigidbdy.rotation.y );
			_writer.Write( m_rigidbdy.rotation.z );
			_writer.Write( m_rigidbdy.rotation.w );
			_writer.Write( m_rigidbdy.sleepThreshold );
			_writer.Write( m_rigidbdy.solverIterations );
			_writer.Write( m_rigidbdy.solverVelocityIterations );
			_writer.Write( m_rigidbdy.useGravity );
			_writer.Write( m_rigidbdy.velocity.x );
			_writer.Write( m_rigidbdy.velocity.y );
			_writer.Write( m_rigidbdy.velocity.z );
		}
		#endregion
	}
}
