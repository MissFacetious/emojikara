using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CreateBoard : MonoBehaviour {

	protected static GameObject PausePanel;
	protected static List<GameObject> BOARD;
	public static List<GameObject> currentlySelected;
	public static List<GameObject> currentlyDesired;

	public static int score;
	public static bool reset;
	public static int numberChoose;
	
	static Word randomWord;
	static int index;
	float ZCHANGE = 0f;

	static List<Emoji> someValues;
	static List<Emoji> wordValues;

	// Use this for initialization
	void Start () {
		if (LoadSave.words == null || LoadSave.words.Count <= 0) {
			LoadSave.loadFile ();
		}

		PausePanel = GameObject.Find ("PausePanel");
		PausePanel.SetActive (false);

		StartAgain ();
	}

	
	public void StartAgain() {
		reset = false;

		currentlySelected = new List<GameObject>();
		currentlyDesired = new List<GameObject>();
		wordValues = new List<Emoji> ();
		someValues = new List<Emoji> ();
		numberChoose = 0;
		score = 0;
		index = 0;

		if (BOARD == null)
			BOARD = new List<GameObject>();

		// destroy all cards
		for (int i=0; i < BOARD.Count; i++) {
			Destroy (BOARD[i]);
		}

		BOARD = new List<GameObject>();

		randomWord = new Word();

		// change word
		currentlySelected = new List<GameObject>();
		currentlyDesired = new List<GameObject>();

		createBoardWords ();
		createCards (Constants.ROWS, Constants.COLUMNS);

		createWord ();
		colorBackground ();
	}

	public static void setPausePanel(bool set) {
		PausePanel.SetActive(set);
	}

	public static List<GameObject> getBoard() {
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
				Emoji emoji = pickRandomEmoji ();
				//someValues
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
		int rand = UnityEngine.Random.Range (0, LoadSave.words.Count - 1);
		string myRandomWord = LoadSave.words[rand].getComplete();

		randomWord = LoadSave.words[rand];

		List<Emoji> emojiList = LoadSave.words [rand].getEmojis();
		for (int i=0; i < emojiList.Count; i++) {
			emojiList[i].getWord();
			emojiList[i].getFilename();
		}
		return emojiList;
	}

	Emoji pickRandomEmoji() {
		// pick a word
		int rand = UnityEngine.Random.Range (0, LoadSave.emojis.Count - 1);
		string myRandomEmoji = LoadSave.emojis[rand].getWord();
		string emojiList = LoadSave.emojis [rand].getFilename();

		// make sure it is not a duplicate of something already picked or random word

		Emoji e = new Emoji ();
		e.setWord (myRandomEmoji);
		e.setFilename (emojiList);

		return e;
	}


	GameObject createCard(GameObject box, int row, int column, float xoffset, float yoffset) {
		GameObject card = null;
		try {
			card = (GameObject)Instantiate (Resources.Load<GameObject> ("Card"));
			card.transform.position = new Vector3 (xoffset, yoffset, ZCHANGE);
			card.GetComponent<Card>().myCard = row+"_"+column+"_card";
			card.GetComponent<Card>().row = row;
			card.GetComponent<Card>().column = column;
			card.GetComponent<Card>().value = someValues[index].getFilename();
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
			StartAgain();
			reset = false;
			Application.LoadLevel("title");
		}
	}
}
