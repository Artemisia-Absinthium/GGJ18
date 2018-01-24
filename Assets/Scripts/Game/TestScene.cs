/*
 * LICENCE
 */
using UnityEngine;
using Game;

public class TestScene : MonoBehaviour, ICutSceneSupervisorBase
{
	public string speakerName = "John Micheal";
	public bool playTestCutScene;
	private int cutSceneInstance = -1;
	
	void Update()
	{
		if ( playTestCutScene )
		{
			if ( CutScenePlayer.Instance.IsPlaying() )
			{
				// Wait
			}
			else if ( CutScenePlayer.Instance.HasFinished( cutSceneInstance ) )
			{
				playTestCutScene = false;
				cutSceneInstance = -1;
			}
			else
			{
				cutSceneInstance = CutScenePlayer.Instance.Play( "Test Scene", this );
				CutScenePlayer.Instance.SetArgument( cutSceneInstance, 1, speakerName );
			}
		}
	}
	
	// Methods from ICutSceneSupervisorBase
	public void OnCutSceneTransitionOk( string _cutSceneName, int _previousSnapshot, int _newSnapshot )
	{
		
	}
	public void OnCutSceneTransitionTime( string _cutSceneName, int _previousSnapshot, int _newSnapshot )
	{
		
	}
	public void OnCutSceneTransitionChoice( string _cutSceneName, int _previousSnapshot, int _newSnapshot, int _choiceIndex )
	{
		if ( _newSnapshot == 2 )
		{
			CutScenePlayer.Instance.SetArgument( cutSceneInstance, 2, System.DateTime.Now.Hour );
		}
	}

	public bool OnOverrideSceneTransition( out int _nextScene )
	{
		_nextScene = -1;
		return false;
	}
}