/*
 * LICENCE
 */
public interface ICutSceneSupervisorBase
{
	void OnCutSceneTransitionOk( string _cutSceneName, int _previousSnapshot, int _newSnapshot );
	void OnCutSceneTransitionTime( string _cutSceneName, int _previousSnapshot, int _newSnapshot );
	void OnCutSceneTransitionChoice( string _cutSceneName, int _previousSnapshot, int _newSnapshot, int _choiceIndex );

	// Called just after one of the functions above, allows overriding of default cutscene flow
	bool OnOverrideSceneTransition( out int _nextScene );
}
