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
namespace Engine
{
	[System.Serializable]
	public class Scheduler
	{
		#region Members
		private int m_performanceCounter = 0;
		#endregion

		#region Methods
		public void Update( PerformanceModifier _performances, int _arraySize, out int _min, out int _max )
		{
			// Disable performance optimizations for very small amount of objects
			if ( _arraySize < 10 )
			{
				_min = 0;
				_max = _arraySize;
			}
			else
			{
				int perf = ( int )_performances + 1;
				int performanceState = m_performanceCounter % perf;
				int emitterCountToProcess = _arraySize / perf;

				_min = performanceState * emitterCountToProcess;
				_max = ( performanceState + 1 ) * emitterCountToProcess;

				if ( ( int )_performances == performanceState )
				{
					_max += performanceState;
				}
			}
			++m_performanceCounter;
		}

		public void Reset()
		{
			m_performanceCounter = 0;
		}
		#endregion
	}
}
