using UnityEngine;
using System.Collections;

public class Preload : MonoBehaviour {

	public static GameObject Music;
	public static GameObject OptionsPanel;
	public static bool inOptions;
	public static bool okayToProceed;
	public bool menu;
	// Use this for initialization
	void Start () {
		if (menu) {
			OptionsPanel = GameObject.Find ("OptionsPanel");
			Music = GameObject.Find ("Music");
			Preload.OptionsPanel.SetActive (false);
			DontDestroyOnLoad (this.gameObject);
			inOptions = false;
			okayToProceed = false;
			StartCoroutine (loading ());
			if (!Music.GetComponent<AudioSource> ().isPlaying) {
				Music.GetComponent<AudioSource> ().Play ();
			}
		}
	}
	
	IEnumerator loading() {
		float loadProgress = 0f;
		yield return new WaitForSeconds(1f);
		GameObject play = GameObject.Find ("Play");
		GameObject timed = GameObject.Find ("Timed");
		GameObject rewards = GameObject.Find ("Rewards");
		GameObject options = GameObject.Find ("Options");
		GameObject exit = GameObject.Find ("Exit");
		play.GetComponent<Card> ().picked = true;
		yield return new WaitForSeconds(0.5f);
		timed.GetComponent<Card> ().picked = true;
		yield return new WaitForSeconds(0.5f);
		rewards.GetComponent<Card> ().picked = true;
		yield return new WaitForSeconds(0.5f);
		options.GetComponent<Card> ().picked = true;
		yield return new WaitForSeconds(0.5f);
		exit.GetComponent<Card> ().picked = true;
		okayToProceed = true;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
