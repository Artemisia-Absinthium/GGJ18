/*
 * LICENCE
 */
using UnityEngine;

[System.Serializable]
public class Cimetary : Engine.BehaviourBase
{
	public override void Trigger( Engine.TriggerBase _trigger, Object _data )
	{
		if ( GameMusicManager.Instance.m_ActualMusic != GameMusicManager.EGameMusicManagerState.eFaucheuse )
		{
			GameMusicManager.Instance.ChangeMusic( GameMusicManager.EGameMusicManagerState.eFaucheuse );
		}
	}
}
