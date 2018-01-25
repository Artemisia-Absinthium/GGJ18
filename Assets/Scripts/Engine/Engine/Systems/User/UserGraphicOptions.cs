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
		private Resolution m_resolution = new Resolution();
		private bool m_fullscreen = true;
		private AnisotropicFiltering m_anisotropicFiltering = UnityEngine.AnisotropicFiltering.Enable;
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

		#region Properties
		public int Width
		{
			get { return m_resolution.width; }
			set { m_resolution.width = value; }
		}
		public int Height
		{
			get { return m_resolution.height; }
			set { m_resolution.height = value; }
		}
		public int RefreshRate
		{
			get { return m_resolution.refreshRate; }
			set { m_resolution.refreshRate = value; }
		}
		public bool FullScreen
		{
			get { return m_fullscreen; }
			set { m_fullscreen = value; }
		}
		public AnisotropicFiltering AnisotropicFiltering
		{
			get { return m_anisotropicFiltering; }
			set { m_anisotropicFiltering = value; }
		}
		public AntiAliasingValue AntiAliasing
		{
			get { return m_antiAliasing; }
			set { m_antiAliasing = value; }
		}
		public LevelOfDetailsValue LevelOfDetails
		{
			get { return m_levelOfDetails; }
			set { m_levelOfDetails = value; }
		}
		public TextureQualityValue TextureQuality
		{
			get { return m_textureQuality; }
			set { m_textureQuality = value; }
		}
		public ShadersQualityValue ShadersQuality
		{
			get { return m_shadersQuality; }
			set { m_shadersQuality = value; }
		}
		public PhysicParticleValue PhysicParticle
		{
			get { return m_physicParticleQuality; }
			set { m_physicParticleQuality = value; }
		}
		public bool RealtimeReflections
		{
			get { return m_realtimeReflections; }
			set { m_realtimeReflections = value; }
		}
		public ShadowsDistanceValue ShadowsDistance
		{
			get { return m_shadowDistance; }
			set { m_shadowDistance = value; }
		}
		public ShadowResolution ShadowResolution
		{
			get { return m_shadowResolution; }
			set { m_shadowResolution = value; }
		}
		public ShadowQuality ShadowQuality
		{
			get { return m_shadowQuality; }
			set { m_shadowQuality = value; }
		}
		public ParticlesQualityValue ParticlesQuality
		{
			get { return m_particlesQuality; }
			set { m_particlesQuality = value; }
		}
		public VSyncValue VSync
		{
			get { return m_vSyncEnabled; }
			set { m_vSyncEnabled = value; }
		}
		public GraphicAPI GraphicAPI
		{
			get { return m_graphicApi; }
			set { m_graphicApi = value; }
		}
		#endregion

		#region Methods
		public void New()
		{
			m_resolution = Screen.resolutions[ 0 ];
			m_fullscreen = true;
			m_anisotropicFiltering = UnityEngine.AnisotropicFiltering.Enable;
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
			m_resolution.width = _reader.ReadInt16();
			m_resolution.height = _reader.ReadInt16();
			m_resolution.refreshRate = _reader.ReadInt16();
			m_fullscreen = _reader.ReadBoolean();
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
			_writer.Write( ( short )m_resolution.width );
			_writer.Write( ( short )m_resolution.height );
			_writer.Write( ( short )m_resolution.refreshRate );
			_writer.Write( m_fullscreen );
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