using UnityEngine;
using System.Collections;

public class inFront : MonoBehaviour {

	public int layer;
	// Use this for initialization
	void Start () {
		if (layer <= 0) {
			gameObject.GetComponent<Renderer> ().sortingOrder = 30;
		} else {
			gameObject.GetComponent<Renderer> ().sortingOrder = layer;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
