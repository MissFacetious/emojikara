using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (splash ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	IEnumerator splash() {

		GameObject Splash = GameObject.Find ("splash-background");
		GameObject logo = GameObject.Find ("ig-logo");
		GameObject black = GameObject.Find ("black");
		//start.transform.position =new Vector2(100f, 0f);
		//start_gamepad.transform.position =new Vector2(100f, 0f);
		
		Splash.transform.position = new Vector2 (0f, 0f);
		logo.transform.position = new Vector2 (0f, 0f);
		yield return new WaitForSeconds(3f);
		
		SpriteRenderer myRender1 = Splash.GetComponent<SpriteRenderer> ();
		SpriteRenderer myRender2 = logo.GetComponent<SpriteRenderer> ();
		//SpriteRenderer myRender3 = logo2.GetComponent<SpriteRenderer> ();
		float value = 100;



		while(value > 0) {
			value--;
			yield return new WaitForSeconds(0.05f);
			myRender1.color = new Color(1f, 1f, 1f, (value/100f));
			myRender2.color = new Color(1f, 1f, 1f, (value/100f));
			//myRender3.color = new Color(1f, 1f, 1f, (value/100f));
		}

		Application.LoadLevelAsync ("title");
	}
}
