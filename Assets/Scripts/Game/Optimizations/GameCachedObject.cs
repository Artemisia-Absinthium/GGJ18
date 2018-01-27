/*
 * LICENCE
 */

namespace Game
{
	[System.Serializable]
	public enum GameCacheObjects
	{
		Player
	}

	[System.Serializable]
	public class GameCachedObject : Engine.CacheObject<GameCacheObjects>
	{

	}
}
