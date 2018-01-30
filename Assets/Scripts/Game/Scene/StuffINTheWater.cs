/*
 * LICENCE
 */
using UnityEngine;

public class StuffINTheWater : MonoBehaviour {

	public float delta = 0.0f;
	
	void Update () {
		float sinRes = (Mathf.Sin(Time.time + delta) * 0.15f);
		Vector3 pos = transform.localPosition;
		pos.y = sinRes;
		transform.localPosition = pos;
	}
}
