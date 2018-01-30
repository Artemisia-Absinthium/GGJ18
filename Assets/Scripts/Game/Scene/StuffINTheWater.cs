/*
 * LICENCE
 */
using UnityEngine;

public class StuffINTheWater : MonoBehaviour
{
	public float delta = 0.0f;
	public float amplitude = 0.15f;
	public float speed = 1.0f;

	private float baseY = 0.0f;

	private void Start()
	{
		baseY = transform.localPosition.y;
	}

	void Update()
	{
		float sinRes = ( Mathf.Sin( speed * Time.time + delta ) * amplitude );
		Vector3 pos = transform.localPosition;
		pos.y = baseY + sinRes;
		transform.localPosition = pos;
	}
}
