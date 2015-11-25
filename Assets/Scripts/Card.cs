using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Card : MonoBehaviour {
	public string name;
	public bool menu;
	public string myCard;
	public int row;
	public int column;
	public string value;
	public bool picked;
	public bool correct;
	public bool controller;
	bool go = true;

	SpriteRenderer renderer;
	Animator animator;
	Animation animation;
	AudioSource[] audio;

	CreateBoard board = new CreateBoard();
	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		animation = GetComponent<Animation>();
		controller = false;
		GameObject Music = GameObject.Find ("Music");
		if (Music != null) {
			audio = Music.GetComponents<AudioSource> ();
		}
	}


	void OnMouseDown() {
		if (menu) {
			if (audio != null)  
				audio[1].Play();
			if (Preload.okayToProceed) {
				if (!Preload.inOptions) {
					if (name == "options") {
						Preload.OptionsPanel.SetActive(true);
						Preload.inOptions = true;
						ControllerInput.once = true;
					}
					if (name == "exit") {
						Application.Quit ();
					}
					if (name == "play") {
						Application.LoadLevel("match");
					}
					if (name == "timed") {
						Application.LoadLevel("match");
					}
					if (name == "rewards") {
						Debug.Log ("game center");
					}
				}
				else if (Preload.inOptions) {
					Debug.Log ("in options");
					if (name == "music") {
						if (Preload.Music.GetComponent<AudioSource>().isPlaying) {
							Preload.Music.GetComponent<AudioSource>().Pause();
						}
						else {
							Preload.Music.GetComponent<AudioSource>().Play ();
						}
					}
					if (name == "reset") {
						Debug.Log ("rest");
					}
					if (name == "back") {
						//Debug.Log ("back");
						Preload.OptionsPanel.SetActive(false);
						Preload.inOptions = false;
						ControllerInput.once = true;
					}
				}
			}
			if (ControllerInput.pause) {
				Debug.Log ("in pause");
				if (name == "music") {
					if (Preload.Music.GetComponent<AudioSource>().isPlaying) {
						Preload.Music.GetComponent<AudioSource>().Pause();
					}
					else {
						Preload.Music.GetComponent<AudioSource>().Play ();
					}
				}
				if (name == "main") {
					ControllerInput.pause = false;
					ControllerInput.once =true;
					CreateBoard.setPausePanel(false);
					CreateBoard.reset = true;
				}
				if (name == "back") {
					CreateBoard.setPausePanel(false);
					ControllerInput.pause = false;
					ControllerInput.once = true;
				}
			}
			if (name == "pause") {
				CreateBoard.setPausePanel(true);
				ControllerInput.pause = true;
				ControllerInput.once = true;
			}
		} 
		else {

			if (ControllerInput.canGo) {
				if (menu && name == "") {
					return;
				}
				// flip over and show what's under
				CreateBoard.numberChoose++;
				CreateBoard.currentlySelected.Add (gameObject);
				picked = true;

				if (CreateBoard.numberChoose >= 2) {
					CreateBoard.score++;
					GameObject scoreObject = GameObject.Find ("Score");
					scoreObject.GetComponent<TextMesh> ().text = CreateBoard.score.ToString ();

					// check if they are the emojis we want
					if (isCorrect ()) {
						StartCoroutine (showSuccess ());
					} else {
						StartCoroutine (showFailure ());
					}
				}
			}
		}
	}

	bool isCorrect() {
		int index = 0;
		for (int i=0; i < CreateBoard.currentlyDesired.Count; i++) {
			for (int j=0; j < CreateBoard.currentlySelected.Count; j++) {
				if (CreateBoard.currentlyDesired[i].GetComponent<Card>().value == CreateBoard.currentlySelected[j].GetComponent<Card>().value) {
					index++;
				}
			}
		}
		if (index == CreateBoard.currentlyDesired.Count && CreateBoard.currentlyDesired.Count > 0) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator	showFailure() {
		if (audio != null)  
			audio[3].Play();
		ControllerInput.canGo = false;
		yield return new WaitForSeconds(1.5f);
		Debug.Log ("keep trying");

		try {
			for (int i=0; i < CreateBoard.currentlySelected.Count; i++) {
				CreateBoard.currentlySelected[i].GetComponent<Card>().picked = false;
			}
		}
		catch (Exception e) {
			Debug.Log ("Not able to turn cards back around! " + e);
		}
		CreateBoard.numberChoose = 0;
		CreateBoard.currentlySelected = new List<GameObject>();
		ControllerInput.canGo = true;
	}

	IEnumerator	showSuccess() {
		if (audio != null)  
			audio[4].Play();
		try {
			GameObject wordObject = GameObject.Find ("Word");
			wordObject.GetComponent<Animator> ().SetBool ("win", true);
		}
		catch (Exception e) {
			Debug.Log ("Not able to animate the word! " + e);
		}
		ControllerInput.canGo = false;
		Debug.Log ("we found both!");
		yield return new WaitForSeconds(4f);

		try {
			GameObject wordObject = GameObject.Find ("Word");
			wordObject.GetComponent<Animator> ().SetBool ("win", false);
		}
		catch (Exception e) {
			Debug.Log ("Not able to stop the word animation! " + e);
		}
		ControllerInput.canGo = true;
		board.StartAgain();
	}


	// Update is called once per frame
	void Update () {

		if (picked) {
			// play animation turn around
			//animator.SetInteger("state", 1);
			//Debug.Log (animator.GetCurrentAnimatorStateInfo(0).length);
			//Debug.Log (animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
			//if (animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime) {
				//animator.SetInteger("state", 0);
				// animation finished...
			renderer.sprite = (Resources.Load<Sprite> ("Emoji/"+value));
			//}

		} else {
			renderer.sprite = (Resources.Load<Sprite> ("card"));
			// play animation turn back
			//animator.SetInteger("state", 2);
		}
		if (correct) { // and done with picked animation
			// play correct animation
			//animator.SetInteger("state", 3);
		}

		if (controller) {
			OnMouseDown();
			controller = false;
		}
	}
}
