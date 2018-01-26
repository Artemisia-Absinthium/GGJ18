/*
 * LICENCE
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Game
{
	[System.Serializable]
	public class ReplaceTextByTextMeshPro : MonoBehaviour
	{
		[MenuItem("CONTEXT/Text/Replace Text by TextMeshPro")]
		public static void Replace( MenuCommand command )
		{
			Text t = ( Text )command.context;
			if ( t == null )
			{
				return;
			}
			string text = t.text;
			int fontSize = t.fontSize;
			FontStyle style = t.fontStyle;
			Color color = t.color;
			GameObject go = t.gameObject;
			RectTransform rt = go.GetComponent<RectTransform>();
			Quaternion locR = rt.localRotation;
			Vector3 locP = rt.localPosition;
			Vector3 locS = rt.localScale;
			Vector2 amin = rt.anchorMin;
			Vector2 amax = rt.anchorMax;
			Vector2 ap = rt.anchoredPosition;
			Vector2 sd = rt.sizeDelta;
			Vector2 piv = rt.pivot;
			DestroyImmediate( t );
			TMPro.TextMeshProUGUI mesh = go.AddComponent<TMPro.TextMeshProUGUI>();
			mesh.text = text;
			mesh.fontSize = fontSize;
			mesh.fontStyle = TMPro.FontStyles.Normal;
			if ( ( style & FontStyle.Bold ) != 0 )
			{
				mesh.fontStyle |= TMPro.FontStyles.Bold;
			}
			if ( ( style & FontStyle.Italic ) != 0 )
			{
				mesh.fontStyle |= TMPro.FontStyles.Italic;
			}
			mesh.color = color;
			rt.localRotation = locR;
			rt.localPosition = locP;
			rt.localScale = locS;
			rt.anchorMin = amin;
			rt.anchorMax = amax;
			rt.anchoredPosition = ap;
			rt.sizeDelta = sd;
			rt.pivot = piv;
		}
	}
}
