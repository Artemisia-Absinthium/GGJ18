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
	public class UserGraphicOptions : ISerializeRW
	{
		#region Members
		private AnisotropicFiltering m_anisotropicFiltering = AnisotropicFiltering.Enable;
		private AntiAliasingValue m_antiAliasing = AntiAliasingValue.X4;
		private LevelOfDetailsValue m_levelOfDetails = LevelOfDetailsValue.HighQuality;
		private TextureQualityValue m_textureQuality = TextureQualityValue.HighQuality;
		private ShadersQualityValue m_shadersQuality = ShadersQualityValue.HighQuality;
		private PhysicParticleValue m_physicParticleQuality = PhysicParticleValue.HighQuality;
		private bool m_realtimeReflections = true;
		private ShadowsDistanceValue m_shadowDistance = ShadowsDistanceValue.MediumDistance;
		private ShadowResolution m_shadowResolution = ShadowResolution.High;
		private ShadowQuality m_shadowQuality = ShadowQuality.All;
		private ParticlesQualityValue m_particlesQuality = ParticlesQualityValue.HighQuality;
		private VSyncValue m_vSyncEnabled = VSyncValue.Enabled;
		private GraphicAPI m_graphicApi = GraphicAPI.DirectX11;
		#endregion

		#region Methods
		public void New()
		{
			m_anisotropicFiltering = AnisotropicFiltering.Enable;
			m_antiAliasing = AntiAliasingValue.X4;
			m_levelOfDetails = LevelOfDetailsValue.HighQuality;
			m_textureQuality = TextureQualityValue.HighQuality;
			m_shadersQuality = ShadersQualityValue.HighQuality;
			m_physicParticleQuality = PhysicParticleValue.HighQuality;
			m_realtimeReflections = true;
			m_shadowDistance = ShadowsDistanceValue.MediumDistance;
			m_shadowResolution = ShadowResolution.High;
			m_shadowQuality = ShadowQuality.All;
			m_particlesQuality = ParticlesQualityValue.HighQuality;
			m_vSyncEnabled = VSyncValue.Enabled;
			m_graphicApi = GraphicAPI.DirectX11;
	}

		public void SerializeR( System.IO.BinaryReader _reader )
		{
			m_anisotropicFiltering = (AnisotropicFiltering)_reader.ReadInt32();
			m_antiAliasing = (AntiAliasingValue)_reader.ReadByte();
			m_levelOfDetails = (LevelOfDetailsValue)_reader.ReadInt16();
			m_textureQuality = (TextureQualityValue)_reader.ReadByte();
			m_shadersQuality = (ShadersQualityValue)_reader.ReadByte();
			m_physicParticleQuality = (PhysicParticleValue)_reader.ReadInt16();
			m_realtimeReflections = _reader.ReadBoolean();
			m_shadowDistance = (ShadowsDistanceValue)_reader.ReadInt16();
			m_shadowResolution = (ShadowResolution)_reader.ReadInt32();
			m_shadowQuality = (ShadowQuality)_reader.ReadInt32();
			m_particlesQuality = (ParticlesQualityValue)_reader.ReadByte();
			m_vSyncEnabled = (VSyncValue)_reader.ReadByte();
			m_graphicApi = (GraphicAPI)_reader.ReadByte();
		}

		public void SerializeW( System.IO.BinaryWriter _writer )
		{
			_writer.Write( (int)m_anisotropicFiltering );
			_writer.Write( (byte)m_antiAliasing );
			_writer.Write( (short)m_levelOfDetails );
			_writer.Write( (byte)m_textureQuality );
			_writer.Write( (byte)m_shadersQuality );
			_writer.Write( (short)m_physicParticleQuality );
			_writer.Write( m_realtimeReflections );
			_writer.Write( (short)m_shadowDistance );
			_writer.Write( (int)m_shadowResolution );
			_writer.Write( (int)m_shadowQuality );
			_writer.Write( (byte)m_particlesQuality );
			_writer.Write( (byte)m_vSyncEnabled );
			_writer.Write( (byte)m_graphicApi );
		}
		#endregion
	}
}