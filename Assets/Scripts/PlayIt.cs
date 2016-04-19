using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayIt : MonoBehaviour {

	public static bool startAgain;
	GameObject prevCardTouched;
	bool winning;
	public GameObject smiley;
	public GameObject outline;
	public static int MENU = -1;
	public static int PLAY = 0;
	public static int TIMED = 1;
	public static int CUSTOM = 2;
	public static int LOADING = 3;
	public static int TYPE = MENU;
	//public static string card;
	public static bool play;
	public static GameObject currentCard;
	CreateBoard board = new CreateBoard();
	public static int matchIndex;

	AudioSource[] audio;

	// Use this for initialization
	void Start () {
		startAgain = false;
		ControllerInput.win = false;
		winning = false;
		smiley = GameObject.Find ("smiley");
		outline = GameObject.Find ("outline");
		play = false;
		//card = "";
		GameObject Music = GameObject.Find ("MusicPlay");
		
		if (Music != null) {
			audio = Music.GetComponents<AudioSource> ();
		}
		startAgain = true;
	}

	public void StartAgain() {
		CreateBoard.score = 0;
		if (TYPE == PLAY) {
			matchIndex = 1;
			GameObject t = GameObject.Find ("Time");
			if (t != null)
				t.GetComponent<TextMesh>().text = matchIndex.ToString ();
		}
		if (TYPE == TIMED) {
			CreateBoard.timer = Constants.TIMER;
			CreateBoard.matches = 0;
			StartCoroutine(showTimer());
		}
		if (TYPE == LOADING) {
			TYPE = MENU;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//try {
			if (startAgain) {
				StartAgain();
				startAgain = false;
			}
			if (play) {
				if (currentCard != null) {
					string card = currentCard.GetComponent<Card>().thisName;
					//Debug.Log ("inside of play it " + card + " or " + currentCard.GetComponent<Card>().value);

					if (currentCard.GetComponent<Card> ().menu) {
						if (card.ToLower () == "up") {
							// the sound for clicking anything?
							if (audio != null)  {
								audio[1].Play ();
							}
							ControllerInput.custom_index--;
							ControllerInput.displayCustom();
						}
						if (card.ToLower () == "down") {
							// the sound for clicking anything?
							if (audio != null)  {
								audio[1].Play ();
							}
							ControllerInput.custom_index++;
							ControllerInput.displayCustom ();
						}
						if (card.ToLower() == "main") {
							// the sound for moving to a new scene
							if (audio != null)  {
								audio[2].Play ();
							}
							TYPE = MENU;
							ControllerInput.pause = false;
							ControllerInput.results = false;
							ControllerInput.once = true;
							Panels.setResultsPanel (false);
							Panels.setPausePanel (false);
							CreateBoard.reset = true;
						}
						if (Preload.okayToProceed) {
							if (prevCardTouched != currentCard) {
								StartCoroutine(prevCard(currentCard));
								if (ControllerInput.pause || ControllerInput.results) {
									//Debug.Log ("in pause " +card);
									if (card.ToLower() == "music" || card.ToLower() == "musictoggle") {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("music").transform.position;
										Debug.Log ("now change state");
										Preload.toggleMusic();
									}
									if (card.ToLower() == "main") {
										// the sound for moving to a new scene
										if (audio != null)  {
											audio[2].Play ();
										}
										outline.transform.position = GameObject.Find ("main").transform.position;
										TYPE = MENU;
										ControllerInput.pause = false;
										ControllerInput.results = false;
										ControllerInput.once = true;
										Panels.setResultsPanel (false);
										Panels.setPausePanel (false);
										CreateBoard.reset = true;
									}
									if ((card.ToLower() == "pause" || card.ToLower() == "back" || card.ToLower () == "return") && !ControllerInput.results) {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("back").transform.position;
										//if (ControllerInput.results) {
										if (TYPE == CUSTOM) {
											if (!ControllerInput.pause) {
												Debug.Log ("move panels");
												Panels.setGridPanel (false);
												Panels.setPausePanel (true);
												ControllerInput.pause = true;
												ControllerInput.once = true;
											}	
											else {
												Debug.Log ("move panels");
												Panels.setGridPanel (true);
												Panels.setPausePanel (false);
												ControllerInput.pause = false;
												ControllerInput.once = true;
											}
										}
										else {
											Debug.Log ("back");
											Panels.setResultsPanel (false);
											ControllerInput.results = false;
											ControllerInput.once = true;
											Panels.setPausePanel (false);
											ControllerInput.pause = false;
											Panels.setGridPanel(true);
										}
									}
		
									if (card.ToLower() == "again") {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("again").transform.position;
										//if (ControllerInput.results) {
									PlayIt.startAgain = true;
										board.StartAgain();
										Panels.setResultsPanel (false);
										Panels.setGridPanel(true);
										ControllerInput.results = false;
										ControllerInput.once = true;
									}
								}
								else if (!Preload.inOptions) {
									if ((card.ToLower () == "pause" || card.ToLower () == "menu" || card.ToLower () == "return") && !ControllerInput.results) {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("pause").transform.position;
										Panels.setGridPanel(false);
										Panels.setPausePanel (true);
										ControllerInput.pause = true;
										ControllerInput.once = true;
									}
									if (card.ToLower () == "options") {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("options").transform.position;
										//Preload.OptionsPanel.transform.position = new Vector2 (0f, 0f);
										Panels.setOptionsPanel (true);
										Panels.setGridPanel (false);
										Preload.inOptions = true;
										ControllerInput.once = true;
									}
									if (card.ToLower () == "exit") {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("exit").transform.position;
										Application.Quit ();
									}
									if (card.ToLower () == "play") {
										// the sound for moving to a new scene
										if (audio != null)  {
											audio[2].Play ();
										}
										outline.transform.position = GameObject.Find ("play").transform.position;
										LoadSave.load ();
										TYPE = PLAY;
										StartAgain();
										//Application.LoadLevel ("match");
										Panels.setGridPanel (false);
										StartCoroutine(loadScene("match"));
									}
									if (card.ToLower () == "timed") {
										// the sound for moving to a new scene
										if (audio != null)  {
											audio[2].Play ();
										}
										outline.transform.position = GameObject.Find ("timed").transform.position;
										LoadSave.load ();
										TYPE = TIMED;
										StartAgain();
										Panels.setGridPanel (false);
										StartCoroutine(loadScene("match"));
									}
								} 
								else if (Preload.inOptions) {
									Debug.Log ("in options");
									if (card.ToLower () == "music" || card.ToLower () == "musictoggle") {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("music").transform.position;
										Debug.Log ("in options where we hit the music toggle");
										Preload.toggleMusic();
									}
									if (card.ToLower () == "reset") {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("reset").transform.position;
										Debug.Log ("reset");
										LoadSave.reset ();
									}
									if (card.ToLower () == "back") {
										// the sound for clicking anything?
										if (audio != null)  {
											audio[1].Play ();
										}
										outline.transform.position = GameObject.Find ("back").transform.position;
										Debug.Log ("back");
										//Preload.OptionsPanel.transform.position = new Vector2 (-100f, 0f);
										Panels.setOptionsPanel (false);
										Panels.setGridPanel (true);
										Preload.inOptions = false;
										ControllerInput.once = true;
									}
								}
							}
						}
					}
					else {
						if (ControllerInput.canGo) {

							StartCoroutine (clickIt ());
							//Debug.Log ("hitting card " + currentCard.GetComponent<Card>().value);
							if (currentCard.GetComponent<Card>().menu && card == "") {
								return;
							}

							if (currentCard.GetComponent<Card>().touched) {
								outline.transform.position = GameObject.Find (currentCard.GetComponent<Card>().thisName).transform.position;
								currentCard.GetComponent<Card>().touched = false;
							}

							// don't allow user to pick the same card
							if (CreateBoard.numberChoose > 0 &&
								currentCard.GetComponent<Card>().value == CreateBoard.currentlySelected[0].GetComponent<Card> ().value) {
								currentCard = null;
								Debug.Log ("you selected the same card, duh");
								return;
							}
							// flip over and show what's under
							CreateBoard.numberChoose++;
							CreateBoard.currentlySelected.Add (currentCard);
							currentCard.GetComponent<Card> ().picked = true;
					
							string valueStr1 = CreateBoard.currentlyDesired[0].GetComponent<Card>().thisName;
							string valueStr2 = CreateBoard.currentlyDesired[1].GetComponent<Card>().thisName;
						
							bool add = true;
							// add to something you guessed
							if (currentCard.GetComponent<Card> ().thisName == valueStr1 || currentCard.GetComponent<Card> ().thisName == valueStr2) {
								add = false;
							}
							for (int i=0; i < CreateBoard.guesses.Count; i++) {
								if (currentCard.GetComponent<Card> ().thisName == CreateBoard.guesses[i]) {
									add = false;
									break;
								}
							}

							if (add) {
								CreateBoard.guesses.Add(currentCard.GetComponent<Card> ().thisName);
							}

							if (CreateBoard.numberChoose >= 2) {
								if (TYPE == PLAY) {
									CreateBoard.score++;
									GameObject scoreObject = GameObject.Find ("Score");
									scoreObject.GetComponent<TextMesh> ().text = CreateBoard.score.ToString ();
								}
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
				currentCard = null;
				play = false;
			}
		//}
		//catch (Exception e) {
		//	Debug.Log (e);
		//}
	}


	IEnumerator clickIt() {
		if (audio != null) {
			audio [1].Play ();
		}
		yield return new WaitForSeconds (0.15f);
	}
	
	bool isCorrect() {
		int index = 0;
		string value1 = "";
		string value2 = "";
		for (int i=0; i < CreateBoard.currentlyDesired.Count; i++) {
			for (int j=0; j < CreateBoard.currentlySelected.Count; j++) {
				if (CreateBoard.currentlyDesired[i].GetComponent<Card>().value == CreateBoard.currentlySelected[j].GetComponent<Card>().value) {
					if (index == 0) {
						value1 = CreateBoard.currentlyDesired[i].GetComponent<Card>().value;
						index++;
					}
					if (index == 1) {
						value2 = CreateBoard.currentlyDesired[i].GetComponent<Card>().value;
						if (value1 != value2) {
							index++;
						}
					}
				}
			}
		}

		if (index == CreateBoard.currentlyDesired.Count && CreateBoard.currentlyDesired.Count > 0) {
			return true;
		} else {
			return false;
		}
	}

	IEnumerator showAnimation(GameObject card) {
		card.GetComponent<Animator> ().SetBool ("select", true);
		yield return new WaitForSeconds (0.15f);
		card.GetComponent<Animator> ().SetBool ("select", false);
	}

	IEnumerator showTimer() {
		bool once = false;
		if (TYPE == TIMED) {
			while (CreateBoard.timer > 0) {
				GameObject timerStr = GameObject.Find ("Time");
				if (timerStr != null) {
				timerStr.GetComponent<TextMesh> ().text = CreateBoard.timer.ToString ();
				}
				while (ControllerInput.pause || winning) {
					yield return new WaitForSeconds (0f);
				}
				yield return new WaitForSeconds (Constants.WAITTIME);
				CreateBoard.timer--;
				once= true;
				if (timerStr != null) {
					timerStr.GetComponent<TextMesh> ().text = CreateBoard.timer.ToString ();
				}
				if (CreateBoard.timer < 6 && CreateBoard.timer >= 0 && once) {
					if (audio != null) {
						audio[14].Play ();
					}
					once= false;
				}
			}
			while (ControllerInput.pause || winning) {
				yield return new WaitForSeconds (0f);
			}
			ControllerInput.results = true;
			ControllerInput.once = true;
			
			showResults();
			Panels.setResultsPanel(true);
			Panels.setGridPanel(false);
		}
		yield return new WaitForSeconds(0);
	}

	IEnumerator	showFailure() {
		if (audio != null) {
			audio [3].Play ();
		}
		ControllerInput.canGo = false;
		yield return new WaitForSeconds(Constants.WAITTIME);
		//Debug.Log ("keep trying");
		
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
		ControllerInput.win = true;
		winning = true;
		//Debug.Log ("show success");
		if (audio != null) {
			audio [4].Play ();
		}

		try {
			if (CreateBoard.currentlyDesired.Count > 0) LoadSave.addToUnlocked(CreateBoard.currentlyDesired[0].GetComponent<Card>().value);
			if (CreateBoard.currentlyDesired.Count > 1) LoadSave.addToUnlocked(CreateBoard.currentlyDesired[1].GetComponent<Card>().value);
			GameObject wordObject = GameObject.Find ("Word");
			wordObject.GetComponent<Animator> ().SetBool ("win", true);
		}
		catch (Exception e) {
			Debug.Log ("Not able to animate the word! " + e);
		}
		// move two cards into the front

		CreateBoard.EndMatch ();
		outline.transform.position = new Vector3 (100f, 0, 0);

		
		int count = 0;
		GameObject card1 = CreateBoard.currentlyDesired[1];
		GameObject card2 = CreateBoard.currentlyDesired[0];
		card1.transform.localPosition = new Vector3(card1.transform.localPosition.x, card1.transform.localPosition.y, -9f);
		card2.transform.localPosition = new Vector3(card2.transform.localPosition.x, card2.transform.localPosition.y, -9f);

		Destroy (card1.GetComponent<Rigidbody2D> ());
		Destroy (card1.GetComponent<BoxCollider2D> ());
		Destroy (card2.GetComponent<Rigidbody2D> ());
		Destroy (card2.GetComponent<BoxCollider2D> ());

		List<GameObject> myBoard = board.getBoard();
		for (int i=0; i < myBoard.Count; i++) {
			string obj = myBoard[i].GetComponent<Card>().value;
			if (!(card1.GetComponent<Card>().value == obj || card2.GetComponent<Card>().value == obj)) {
				myBoard[i].GetComponent<Card>().fall = true;
				yield return new WaitForSeconds(0.05f);
			}
		}

		// randomly play one of these
		if (audio != null) {
			int random = UnityEngine.Random.Range (7, 11);
			audio[random].Play ();
		}

		card1.GetComponent<Renderer> ().sortingOrder = 30;
		card2.GetComponent<Renderer> ().sortingOrder = 30;

		GameObject particles = Resources.Load("Fireworks") as GameObject;
		GameObject p1 = (GameObject)Instantiate (particles);
		GameObject p2 = (GameObject)Instantiate (particles);
		p1.transform.parent = card1.transform;
		p2.transform.parent = card2.transform;


		while (count < 50) {
			// card1 goes to -2, -0.8
			Vector3 card1Placement = new Vector3(-2, 0.8f, card1.transform.localPosition.z);
			Vector3 card2Placement = new Vector3(2, 0.8f, card2.transform.localPosition.z);
			// card2 goes to 2, -0.8
			card1.transform.localPosition = Vector3.MoveTowards(card1.transform.localPosition, card1Placement, 0.5f);
			card2.transform.localPosition = Vector3.MoveTowards(card2.transform.localPosition, card2Placement, 0.5f);
			card1.transform.localScale = 1.01f*card1.transform.localScale;
			card2.transform.localScale = 1.01f*card2.transform.localScale;
			p1.transform.position =  card1.transform.position;
			p2.transform.position = card2.transform.position;
			yield return new WaitForSeconds(0.05f);
			count++;
		}

		ControllerInput.canGo = false;
		//Debug.Log ("we found both!");
		yield return new WaitForSeconds(Constants.CHANGETIME/2);
		
		try {
			GameObject wordObject = GameObject.Find ("Word");
			wordObject.GetComponent<Animator> ().SetBool ("win", false);
		}
		catch (Exception e) {
			Debug.Log ("Not able to stop the word animation! " + e);
		}
		ControllerInput.canGo = true;

		if (TYPE == TIMED) {
			if (CreateBoard.timer > 0) {
				CreateBoard.matches++;
				GameObject scoreObject = GameObject.Find ("Score");
				scoreObject.GetComponent<TextMesh> ().text = CreateBoard.matches.ToString ();

				board.StartAgain ();
			}
			//showResults();
		} else if (TYPE == PLAY) {
			if (matchIndex >= Constants.ROUND) {
				ControllerInput.results = true;
				ControllerInput.once = true;

				showResults();
				Panels.setResultsPanel(true);
				Panels.setGridPanel(false);
			}
			else {
				matchIndex++;
				GameObject.Find ("Time").GetComponent<TextMesh>().text = matchIndex.ToString ();
				board.StartAgain ();
			}
		}
		winning = false;
		ControllerInput.win = false;
	}

	void showResults() {
		if (audio != null) {
			audio [5].Play ();
		}
		
		TextMesh results = GameObject.Find ("results").GetComponent<TextMesh> ();
		if (TYPE == PLAY) {
			results.text = "You played " + Constants.ROUND + " rounds and took " + CreateBoard.score + " turns.";
			if (LoadSave.HIGH_SCORE > CreateBoard.score) {
				results.text += "\n\nYou achieved a better score! " + LoadSave.HIGH_SCORE + " -> " + CreateBoard.score + " turns";
				LoadSave.HIGH_SCORE = CreateBoard.score;
			}
		}
		//results.text = "You found x matches in 60 seconds.";
		if (TYPE == TIMED) {
			results.text = "You found " + CreateBoard.matches + " matches in " + Constants.TIMER + " seconds.";
			if (LoadSave.TIME_SCORE < CreateBoard.matches) {
				results.text += "\n\nYou achieved a better score! " + LoadSave.TIME_SCORE + " -> " + CreateBoard.matches + " matches";
				LoadSave.TIME_SCORE = CreateBoard.matches;
			}
		}

		// include other guesses they had if any
		// randomly pick guessed words 
		if (CreateBoard.guesses.Count > 1) {
			Panels.setPhonePanel (true);

			int j = UnityEngine.Random.Range (0, CreateBoard.guesses.Count - 1);
			int k = UnityEngine.Random.Range (0, CreateBoard.guesses.Count - 1);
			while (j == k) {
				k = UnityEngine.Random.Range (0, CreateBoard.guesses.Count - 1);
			}
			
			string word1 = CreateBoard.guesses [j];
			string word2 = CreateBoard.guesses [k];

			List<string> messageStr = new List<string>();

			messageStr.Add ("In "+ word1 + ", do as the \n" + word2 + " do");
			messageStr.Add ("When in " + word1 + " is \nhow we do " + word2);
			messageStr.Add ("The squeaky " + word1 + " \ngets the " + word2);
			messageStr.Add ("When the going gets " + word1 + ", \nthe " + word1 + " get going");
			messageStr.Add ("No " + word1 + " is a \n" + word2);
			messageStr.Add (word1 + " favors the \n" + word2);
			messageStr.Add ("Hope for the " + word1 + ", \nbut prepare for the \n" + word2);
			messageStr.Add ("Better " + word1 + " \nthan " + word2);
			messageStr.Add ("Keep your " + word1 + " close \nand your " + word2 + " closer");
			messageStr.Add ("A " + word1 + " is worth \na thousand " + word2);
			messageStr.Add (word1 + " is the greater \npart of " + word2);

			int mIndex = UnityEngine.Random.Range (0, messageStr.Count - 1);
			//Debug.Log (messageStr.Count + " " + mIndex);
			string sample = messageStr[mIndex];

			//Debug.Log (sample);
			TextMesh message = GameObject.Find ("message").GetComponent<TextMesh> ();
			message.text = sample;

			string filename1 = LoadSave.findEmojiForWord (word1);
			string filename2 = LoadSave.findEmojiForWord (word2);

			Card card1 = GameObject.Find ("card1").GetComponent<Card> ();
			Card card2 = GameObject.Find ("card2").GetComponent<Card> ();
			card1.picked = true;
			card1.thisName = word1;
			card1.value = filename1;
			card2.picked = true;
			card2.thisName = word2;
			card2.value = filename2;

		} else {
			Panels.setPhonePanel (false);
		}
		if (audio != null) {
			audio [6].Play ();
		}
	}

	public IEnumerator loadScene(string scene) {
		int temp = PlayIt.TYPE;
		PlayIt.TYPE = PlayIt.LOADING;
		smiley.transform.position = new Vector3(0f, 0f, 0f);
		outline.transform.position = new Vector3(100f, 0f, 0f);
		GameObject selected = GameObject.Find ("selected");
		if (selected != null) selected.transform.position = new Vector3 (100f, 0f, 0f);
		GameObject wordObject = GameObject.Find ("Word");
		if (wordObject != null) wordObject.GetComponent<TextMesh>().text = "";
		GameObject pause = GameObject.Find ("pause");
		if (pause != null) pause.transform.position = new Vector3(100f, 0f, 0f);
		//yield return new WaitForSeconds(5f);
		PlayIt.TYPE = temp;
		AsyncOperation async = Application.LoadLevelAsync (scene);
		yield return async;
		
		smiley.transform.position = new Vector3(100, 0f, 0f);
		outline.transform.position = new Vector3(0f, 0f, 0f);
		yield return new WaitForSeconds(0);
	}

	public IEnumerator prevCard(GameObject card) {
		prevCardTouched = card;
		yield return new WaitForSeconds(0.3f);
		prevCardTouched = null;
	}
}
