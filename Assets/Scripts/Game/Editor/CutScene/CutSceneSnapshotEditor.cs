/*
 * LICENCE
 */
using System.Collections.Generic;

namespace Game
{
	[System.Serializable]
	public class CutSceneSnapshotEditor
	{
		public string Left = "none", Right = "none", Center = "none";
		public int Speaker = -1;
		public string Text = "NONE";
		public List<string> Choices = new List<string>();
		public bool Ok = true;
		public int OkTarget = -1;
		public bool Timed = false;
		public float Time = 0.0f;
		public int TimeTarget = -1;
		public List<int> ChoiceTargets = new List<int>();

		public CutSceneSnapshotEditor()
		{

		}

		public CutSceneSnapshotEditor( CutSceneSnapshotEditor _toCopy )
		{
			Left = _toCopy.Left;
			Right = _toCopy.Right;
			Center = _toCopy.Center;
		}
	}
}
