/*
 * LICENCE
 */

namespace Game
{
	[System.Serializable]
	public enum GameCacheObjects
	{
		Player,
		PlayerCamera,

		Count
	}

	[System.Serializable]
	public class GameCachedObject : Engine.CacheObject<GameCacheObjects>
	{

	}
}
