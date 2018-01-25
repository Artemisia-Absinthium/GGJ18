/*
 * LICENCE
 */
namespace Game
{
	public interface ICutSceneSupervisorBase
	{
		bool OnCutSceneTransitionOk( string _cutSceneName, int _previousSnapshot, int _newSnapshot, ref int _nextScene );
		bool OnCutSceneTransitionTime( string _cutSceneName, int _previousSnapshot, int _newSnapshot, ref int _nextScene );
		bool OnCutSceneTransitionChoice( string _cutSceneName, int _previousSnapshot, int _newSnapshot, int _choiceIndex, ref int _nextScene );
	}
}
