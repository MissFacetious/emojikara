using UnityEngine;
using System.Collections;

public class inFront : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().sortingOrder = 30;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
