using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffINTheWater : MonoBehaviour {

	public float delta = 0.0f;

	float minY = -0.3f;
	float maxY = 0f;

	float currentY = 0.0f;

	float totalTime = 0.0f;
	// Use this for initialization
	void Start () {
		currentY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;

		float sinRes = (Mathf.Sin(totalTime + delta) * 0.15f);
		Vector3 pos = transform.localPosition;
		pos.y = sinRes;
		transform.localPosition = pos;
	}
}
