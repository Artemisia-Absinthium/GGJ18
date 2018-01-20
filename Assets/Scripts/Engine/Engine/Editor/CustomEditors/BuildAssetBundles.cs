/*
 * MIT License
 * 
 * Copyright (c) 2017 Joseph Kieffer
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
using UnityEditor;

namespace Engine
{
	[System.Serializable]
	public class BuildAssetBundles
	{
		[MenuItem( "Engine/Build AssetBundles/Windows" )]
		public static void BuildAllAssetBundlesWindows()
		{
			BuildAllAssetBundles( BuildTarget.StandaloneWindows64 );
		}
		[MenuItem( "Engine/Build AssetBundles/Linux" )]
		public static void BuildAllAssetBundlesLinux()
		{
			BuildAllAssetBundles( BuildTarget.StandaloneLinux64 );
		}
		[MenuItem( "Engine/Build AssetBundles/OSX" )]
		public static void BuildAllAssetBundlesOSX()
		{
			BuildAllAssetBundles( BuildTarget.StandaloneOSX );
		}
		[MenuItem( "Engine/Build AssetBundles/All" )]
		public static void BuildAllAssetBundlesAll()
		{
			if ( BuildWindowsAvailable() )
			{
				BuildAllAssetBundlesWindows();
			}
			if ( BuildLinuxAvailable() )
			{
				BuildAllAssetBundlesLinux();
			}
			if ( BuildOSXAvailable() )
			{
				BuildAllAssetBundlesOSX();
			}
		}
		private static void BuildAllAssetBundles( BuildTarget _target )
		{
			string path = System.Enum.GetName( typeof( BuildTarget ), _target );
			if ( !AssetDatabase.IsValidFolder( "Assets/AssetBundles" ) )
			{
				AssetDatabase.CreateFolder( "Assets", "AssetBundles" );
			}
			if ( !AssetDatabase.IsValidFolder( "Assets/AssetBundles/" + path ) )
			{
				AssetDatabase.CreateFolder( "Assets/AssetBundles", path );
			}
			BuildPipeline.BuildAssetBundles( "Assets/AssetBundles/" + path, BuildAssetBundleOptions.None, _target );
			AssetDatabase.Refresh( ImportAssetOptions.ForceUpdate );
		}
		[MenuItem( "Engine/Build AssetBundles/Windows", true )]
		public static bool BuildWindowsAvailable()
		{
			return true;
		}
		[MenuItem( "Engine/Build AssetBundles/Linux", true )]
		public static bool BuildLinuxAvailable()
		{
			return true;
		}
		[MenuItem( "Engine/Build AssetBundles/OSX", true )]
		public static bool BuildOSXAvailable()
		{
			return false;
		}
	}
}
