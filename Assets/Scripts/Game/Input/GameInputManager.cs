/*
 * LICENCE
 */

namespace Game
{
	[System.Serializable]
	public enum GameActions
	{
		CONFIRM,
		BACK,

		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	[System.Serializable]
	public class GameInputActions : Engine.InputAction<GameActions>
	{
	}

	[System.Serializable]
	public class GameInputManager : Engine.InputManager<GameInputActions, GameActions>
	{

	}
}
