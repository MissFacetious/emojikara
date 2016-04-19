using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour {

	Camera camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		// out default is 1280 x 720
		
		float widthRatio = Screen.width;
		float heightRatio = Screen.height;

		//if (camera.aspect != 1) {
			//Debug.Log("fix aspect");
			Screen.SetResolution(Screen.width, (Screen.width*9/16), false);


	}
}
