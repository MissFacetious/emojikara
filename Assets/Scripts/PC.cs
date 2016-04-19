using UnityEngine;
using System.Collections;

public class PC : MonoBehaviour {

	public bool custom;

	// Use this for initialization
	void Start () {
		if (custom) {
			// remove the background from camera
			GetComponent<Camera>().cullingMask &=  ~(1 << LayerMask.NameToLayer("Background"));
			GetComponent<Camera>().orthographicSize = 7;
			// move camera over -5f, 0f, 0f
			transform.position = new Vector3(-5f, 0f, -10f);
			// move other camera over 5f, 0f, 0f
			GameObject.Find ("phone").transform.position = new Vector3(-5f, 0f, 0f);

			GameObject postit = GameObject.Find ("postit");
			TextMesh[] tm = postit.GetComponentsInChildren<TextMesh>();
			tm[0].text = "";
			tm[1].text = "";
		}
		else {
			GetComponent<Camera>().depth = -1;
			transform.position = new Vector3(-100f, 0f, 0f);
			gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
