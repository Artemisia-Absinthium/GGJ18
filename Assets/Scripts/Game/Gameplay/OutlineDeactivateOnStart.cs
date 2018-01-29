using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using cakeslice;

public class OutlineDeactivateOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<Outline>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
