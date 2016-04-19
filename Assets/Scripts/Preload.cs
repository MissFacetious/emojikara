using UnityEngine;
using System.Collections;

public class Preload : MonoBehaviour {

	public static int firstLoad;
	public static GameObject Music;
	public static GameObject OptionsPanel;
	public static bool inOptions;
	public static bool okayToProceed;
	// Use this for initialization
	void Start () {
		//OptionsPanel = GameObject.Find ("OptionsPanel");
		Music = GameObject.Find ("MusicPlay");
		//Preload.OptionsPanel.transform.position = new Vector2 (-100f, 0f);
		DontDestroyOnLoad(this.gameObject);
		inOptions = false;
		okayToProceed = false;
		StartCoroutine (loading ());
		if (Music != null) {
		//	Debug.Log ("music is ok");
		} else {
			Debug.Log ("problem with music");
		}
		if (!Music.GetComponent<AudioSource>().isPlaying) {
			if (firstLoad == 0) {
			Music.GetComponent<AudioSource>().Play();
			}
		}
		firstLoad = 1;
	}

	public static void toggleMusic() {
		if (Music.GetComponent<AudioSource> ().isPlaying) {
			Debug.Log ("pausing music");
			Music.GetComponent<AudioSource> ().Pause ();
		} else {
			Debug.Log ("music wasn't playing, so play");
			Music.GetComponent<AudioSource> ().Play ();
		}
	}
	
	IEnumerator loading() {
		float loadProgress = 0f;
		yield return new WaitForSeconds(0.1f);
		GameObject play = GameObject.Find ("play");
		GameObject timed = GameObject.Find ("timed");
		GameObject options = GameObject.Find ("options");
		GameObject exit = GameObject.Find ("exit");
		if (play != null) play.GetComponent<Card> ().picked = true;
		yield return new WaitForSeconds(0.1f);
		if (timed != null) timed.GetComponent<Card> ().picked = true;
		yield return new WaitForSeconds(0.1f);
		if (options != null) options.GetComponent<Card> ().picked = true;
		yield return new WaitForSeconds(0.1f);
		if (exit != null) exit.GetComponent<Card> ().picked = true;
		okayToProceed = true;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
