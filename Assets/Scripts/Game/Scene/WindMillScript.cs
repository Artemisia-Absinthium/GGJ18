using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMillScript : MonoBehaviour {

	public GameObject wind;
	public float speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		wind.transform.Rotate(speed * Time.deltaTime, 0.0f, 0.0f);
	}
}
