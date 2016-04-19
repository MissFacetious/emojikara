using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	TextMesh tm;
	TextMesh myTm;
	public float distance;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		GameObject parent = transform.parent.gameObject;
		tm = parent.GetComponent<TextMesh> ();
		myTm = GetComponent<TextMesh> ();
		//myTm.text = tm.text;
		transform.position = parent.transform.position;
		transform.localPosition = new Vector3(distance, distance, 0.2f);
		myTm.text = tm.text;
	}
}
