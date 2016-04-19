using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CreateBoard : MonoBehaviour {

	public GameObject smiley;
	public GameObject outline;
//	protected static GameObject PausePanel;
	//protected static GameObject ResultsPanel;
	protected static List<GameObject> BOARD;
	public static List<GameObject> currentlySelected;
	public static List<GameObject> currentlyDesired;
	public static List<string> guesses;
	public static List<int> alreadyPicked;

	public static int score;
	public static int matches;
	public static int timer;
	public static bool reset;
	public static int numberChoose;
	
	static Word randomWord;
	static int index;
	float ZCHANGE = 0f;

	static List<Emoji> someValues;
	static List<Emoji> wordValues;

	// Use this for initialization
	void Start () {
		outline = GameObject.Find ("outline");
		//PausePanel = GameObject.Find ("PausePanel");
		//ResultsPanel = GameObject.Find ("ResultsPanel");
		smiley = GameObject.Find ("smiley");
		if (LoadSave.words == null || LoadSave.words.Count <= 0) {
			LoadSave.loadFile ();
		}


		//if (PausePanel != null)
//			PausePanel.transform.position = new Vector2 (-100f, 0f);
		//if (ResultsPanel != null)
			//ResultsPanel.transform.position = new Vector2 (-100f, 0f);
		EndMatch ();
		StartAgain ();
	}

	public static void EndMatch() {
		alreadyPicked = new List<int> ();
	}

	public void StartAgain() {
		Panels.setGridPanel (true);
		reset = false;

		currentlySelected = new List<GameObject>();
		currentlyDesired = new List<GameObject>();
		wordValues = new List<Emoji> ();
		someValues = new List<Emoji> ();
		numberChoose = 0;
		//score = 0;
		index = 0;

		if (BOARD == null)
			BOARD = new List<GameObject>();

		// destroy all cards
		for (int i=0; i < BOARD.Count; i++) {
			GameObject tile = BOARD[i];
			if (tile != null) {
				for (int c=0; c < tile.transform.childCount; c++) {
					Destroy(tile.transform.GetChild(c).gameObject);
				}
				Destroy (tile);
			}
		}

		BOARD = new List<GameObject>();

		randomWord = new Word();

		// change word
		currentlySelected = new List<GameObject>();
		currentlyDesired = new List<GameObject>();

		guesses = new List<string>();

		createBoardWords ();
		createCards (Constants.ROWS, Constants.COLUMNS);

		createWord ();
		colorBackground ();
		ControllerInput.win = false;
	}


	
	public List<GameObject> getBoard() {
		return BOARD;
	}
	
	public static GameObject getBoard(int index) {
		if (index < BOARD.Count) {
			return BOARD [index];
		}
		return null;
	}
	
	public static int getBoardSize() {
		return BOARD.Count;
	}

	void colorBackground() {
		try {
			List<Emoji> emojiColor = randomWord.getEmojis();
			GameObject.Find ("Background").GetComponent<SpriteRenderer>().color = emojiColor[0].getColor();
			GameObject.Find ("Circle").GetComponent<SpriteRenderer>().color = emojiColor[1].getColor();
		}
		catch (Exception e) {
			Debug.Log ("Not able to change background colors! " + e);
		}
	}

	void createWord() {
		try {
			string word = "";
			GameObject wordObject = GameObject.Find ("Word");
			wordObject.GetComponent<TextMesh>().text = randomWord.getComplete();
		}
		catch (Exception e) {
			Debug.Log ("Not able to create word display! " + e);
		}
	}

	void createCards(int rows, int columns) {
		try {
			GameObject box = GameObject.Find ("Box");
			//int columns = Convert.ToInt32(Mathf.Floor(boardSize/rows));
			for (int c=0; c < columns; c++) {
				for (int r=0; r < rows; r++) {
					float xoffset = Constants.TILE_WIDTH*c+0.1f;
					float yoffset = Constants.TILE_HEIGHT*r+0.1f;

					xoffset -= Constants.XOFFSET;
					yoffset -= Constants.YOFFSET;

					createCard(box, r, c, xoffset, yoffset);
				}
			}
		}
		catch (Exception e) {
			Debug.Log ("Not able to create cards! " + e);
		}
	}

	void createBoardWords() {
		try {
			// the main word
			List<Emoji> elist = pickRandomWord();
			for (int e=0; e < elist.Count; e++) {
				wordValues.Add (elist[e]);
				someValues.Add (elist[e]);
			}
		}
		catch (Exception e) {
			Debug.Log ("Not able to grab a random word! " + e);
		}

		try {
			// create a list of random tiles
			for (int i=0; i < (Constants.ROWS*Constants.COLUMNS)-2; i++) {
				Emoji emoji = pickRandomEmoji();
				someValues.Add(emoji);
			}
		}
		catch (Exception e) {
			Debug.Log ("Not able to create random cards! " + e);
		}
		// now randomize a bit the someValues so it's not so predictable
		mixUpValues();
	}

	void mixUpValues() {
		try {
			for (int i = someValues.Count - 1; i > 0; i--) {
				int j = UnityEngine.Random.Range (0, someValues.Count - 1);
				Emoji temp = someValues[i]; // Notice the change on this line
				someValues[i] = someValues[j];
				someValues[j] = temp;
			}
		}
		catch (Exception e) {
			Debug.Log ("Not able to randomly sort cards! " + e);
		}
	}

	List<Emoji> pickRandomWord() {
		// pick a word
		bool okay = false;
		int rand = UnityEngine.Random.Range (0, LoadSave.words.Count - 1);

		while (!okay) {
			okay = true;
			rand = UnityEngine.Random.Range (0, LoadSave.words.Count - 1);

			for (int i=0; i < alreadyPicked.Count; i++) {
				if (rand == alreadyPicked[i]) {
				    okay = false;
				    break;
				}
			}
	    }

		alreadyPicked.Add (rand);
		string myRandomWord = LoadSave.words[rand].getComplete();

		randomWord = LoadSave.words[rand];

		List<Emoji> emojiList = LoadSave.words[rand].getEmojis();
		for (int i=0; i < emojiList.Count; i++) {
			emojiList[i].getWord();
			emojiList[i].getFilename();
		}
		return emojiList;
	}

	Emoji pickRandomEmoji() {
		Emoji e = new Emoji();
		string myRandomEmoji = "";
		string emojiList = "";
		// pick a word

		bool okay = false;

		while (!okay) {
			okay = true;
			int rand = UnityEngine.Random.Range (0, LoadSave.emojis.Count - 1);
			myRandomEmoji = LoadSave.emojis [rand].getWord ();
			emojiList = LoadSave.emojis [rand].getFilename ();

			// make sure it is not a duplicate of something already picked or random word

			// make sure it's not the two cards we need
			if (wordValues.Count > 1 && (wordValues [0].getFilename () == emojiList || wordValues [1].getFilename () == emojiList)) {
				//Debug.Log ("try again, it's a card you need");
				okay = false;
				//e = pickRandomEmoji ();
			}
			// make sure it's not already on the board (found this doesn't work)
			for (int i=0; i < someValues.Count; i++) {
				//Debug.Log (someValues[i].getFilename() + " " + emojiList);
				if (someValues[i].getFilename() == emojiList) {
					// repeat, try again
					//Debug.Log ("try again, already on board");
					//e = pickRandomEmoji ();
					okay = false;
				}
			}

		}
		e.setWord (myRandomEmoji);
		e.setFilename (emojiList);

		return e;
	}


	GameObject createCard(GameObject box, int row, int column, float xoffset, float yoffset) {
		GameObject card = null;
		try {
			card = (GameObject)Instantiate (Resources.Load<GameObject> ("Card"));
			card.transform.localPosition = new Vector3 (xoffset, yoffset, ZCHANGE);
			card.GetComponent<Card>().myCard = row+"_"+column+"_card";
			card.GetComponent<Card>().row = row;
			card.GetComponent<Card>().column = column;
			card.GetComponent<Card>().value = someValues[index].getFilename();
			card.GetComponent<Card>().thisName = someValues[index].getWord();
			card.GetComponent<Card>().name = someValues[index].getWord();
			BOARD.Add (card);

			for (int i=0; i < wordValues.Count; i++) {
				if (wordValues[i].getWord() == someValues[index].getWord()) {
					currentlyDesired.Add (card);
				}
			}
			index++;
			card.transform.parent = box.transform;
		}
		catch (Exception e) {
			Debug.Log ("Not able to create card! " + e);
		}
		return card;
	}

	// Update is called once per frame
	void Update () {
		if (reset) {
			//Debug.Log ("restart");
			//StartAgain();

			LoadSave.save();
			PlayIt.TYPE = PlayIt.MENU;
			//Application.LoadLevel("title");
			Panels.setGridPanel (false);
			StartCoroutine(loadScene("title"));
			reset = false;
		}
	}

	public IEnumerator loadScene(string scene) {
		int temp = PlayIt.TYPE;
		PlayIt.TYPE = PlayIt.LOADING;
		smiley.transform.position = new Vector3(0f, 0f, 0f);
		outline.transform.position = new Vector3(100f, 0f, 0f);
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
}
