/*
 * LICENCE
 */

namespace Game
{
	[System.Serializable]
	public class GameLocalizedStringManager : Engine.LocalizedStringManager<Strings>
	{
	}

	public class LocStr
	{
		private static LocStr s_instance = new LocStr();
		public string this[ Strings _str ]
		{
			get { return GameLocalizedStringManager.Instance.Get( _str ); }
		}
		public static string GetStr( Strings _str )
		{
			return GameLocalizedStringManager.Instance.Get( _str );
		}
		public static LocStr Get
		{
			get { return s_instance; }
		}
	}
}
