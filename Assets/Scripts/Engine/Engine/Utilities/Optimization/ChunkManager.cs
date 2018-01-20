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
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
#pragma warning disable 649
	[System.Serializable]
	[AddComponentMenu("Engine/Utilities/Optimization/Chunk Manager")]
	public class ChunkManager : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		[Tooltip("Quality of the update rate of the chunks")]
		private PerformanceModifier m_performances = PerformanceModifier.MEDIUM_PERFORMANCES;
		[SerializeField]
		[Tooltip("List of chunks to manage")]
		private List<Chunk> m_chunks;
		#endregion

		#region Members
		private Scheduler m_chunkScheduler;
		#endregion

		#region Properties
		public PerformanceModifier Performances
		{
			get { return m_performances; }
			set { m_performances = value; }
		}
		#endregion

		#region Methods
		void Start()
		{
			m_chunkScheduler = new Scheduler();
		}

		void Update()
		{
			int firstElement = 0;
			int lastElement = 0;
			m_chunkScheduler.Update( m_performances, m_chunks.Count, out firstElement, out lastElement );
			for ( int iChunk = firstElement; iChunk < lastElement; ++iChunk )
			{
				m_chunks[ iChunk ].Adapt( new Vector3() );
			}
		}
		#endregion
	}
#pragma warning restore 649
}
