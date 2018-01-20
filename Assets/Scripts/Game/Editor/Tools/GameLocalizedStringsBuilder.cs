/*
 * LICENCE
 */
using UnityEditor;

namespace Game
{
	[System.Serializable]
	public class GameLocalizedStringsBuilder : Engine.LocalizedStringBuilder<Strings>
	{
		[MenuItem( "Engine/Localized strings" )]
		public static void Open()
		{
			OpenWindow<GameLocalizedStringsBuilder>();
		}
	}
}
