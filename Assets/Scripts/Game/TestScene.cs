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
	public bool OnCutSceneTransitionOk( string _cutSceneName, int _previousSnapshot, int _newSnapshot, ref int _nextScene )
	{
		return false;
	}
	public bool OnCutSceneTransitionTime( string _cutSceneName, int _previousSnapshot, int _newSnapshot, ref int _nextScene )
	{
		return false;
	}
	public bool OnCutSceneTransitionChoice( string _cutSceneName, int _previousSnapshot, int _newSnapshot, int _choiceIndex, ref int _nextScene )
	{
		if ( _newSnapshot == 2 )
		{
			CutScenePlayer.Instance.SetArgument( cutSceneInstance, 2, System.DateTime.Now.Hour );
		}
		return false;
	}
}