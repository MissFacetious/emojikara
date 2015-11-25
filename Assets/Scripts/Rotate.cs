using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	public float speed;
	public bool clockwise;
	
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 motion;
		if (clockwise) motion = Vector3.back;
		else motion =  Vector3.forward;
		transform.RotateAround (transform.position, motion, speed);
	}
}