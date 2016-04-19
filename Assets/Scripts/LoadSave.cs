using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LoadSave : MonoBehaviour {

	static bool unlockAll = false;
	static JSONObject json;
	static public List<Emoji> emojis = new List<Emoji>();
	static public List<Word> words = new List<Word>();
	static bool INIT;
	static string UNLOCKED_CARDS;
	public static List<string> unlockedCards;

	public static int HIGH_SCORE;
	public static int TIME_SCORE;

	public LoadSave () {
		//PlayerPrefs.DeleteAll();
	}

	public static void loadFile() {
		string text = "";
		try {
			TextAsset textAsset = Resources.Load("Dictionary") as TextAsset;
			text = textAsset.text;
		}
		catch (IOException e) { Debug.Log(e); }
		json = new JSONObject (text);
		loadEmojis();
		loadWords();
	}

	public static void loadEmojis() {
		JSONObject mappingsText = null;
		if (json != null) {
			mappingsText = json["mappings"];
		}
		else {
			loadFile ();
			loadEmojis();
		}

		for (int i=0; i < mappingsText.Count; i++) {
			Emoji emoji = new Emoji();
			JSONObject wordText = mappingsText[i];

			string word = wordText["word"].ToString().Trim('"');
			string filename = wordText["emoji"].ToString().Trim('"');

			emoji.setWord(word);
			emoji.setFilename(filename);
			emojis.Add (emoji);
		}
	}

	public static void loadWords() {
		JSONObject wordsText = null;
		if (json != null) {
			wordsText = json["words"];
		}
		else {
			loadFile ();
			loadWords();
		}

		for (int i=0; i < wordsText.Count; i++) {
			Word word = new Word ();

			JSONObject wordText = wordsText[i];
			string complete = wordText["complete"].ToString().Trim('"');
			word.setComplete(complete);

			JSONObject emojisText = wordText["emojis"];

			List<Emoji> emojis = new List<Emoji>();

			for (int e=0; e < emojisText.Count; e++) {
				Emoji emoji = new Emoji();

				JSONObject etext = emojisText[e];
				string wordAText = etext["word"].ToString().Trim('"');
				if (wordAText == "") {
					break;
				}
				string color = etext["color"].ToString().Trim('"');
				string filename = findEmojiForWord(wordAText);

				emoji.setColor(color);
				emoji.setWord(wordAText);
				emoji.setFilename(filename);

				if (unlockAll) {
					addToUnlocked(filename);
				}

				emojis.Add(emoji);
			}
			word.setEmojis(emojis);

			words.Add (word);
		}

		//int count = wordsText.Count;
		//for (int l=0; l < count; l++) {
		//	if (unlockAll) {
		//		emojis.Add (emojis[l]);
		//	}
		//}
		//printOutUnused();
	}

	public static string findEmojiForWord(string word) {
		for (int i=0; i < emojis.Count; i++) {
			if (word == emojis[i].getWord()) {
				return emojis[i].getFilename();
			}
		}
		return null;
	}

	public static void addToUnlocked(string value) {
		if (unlockedCards == null) {
			unlockedCards = new List<string>();
		}
	
		for (int i=0; i < unlockedCards.Count; i++) {
			if (unlockedCards[i] == value) {
				return;
			}
		}

		unlockedCards.Add(value);
		//Debug.Log ("just added " + value);
	}

	public static void load() {
		Debug.Log ("LOAD PERSISTENCE");
		//PlayerPrefs.DeleteAll();
		
		if (PlayerPrefs.GetInt ("INIT") == 0) {
			// we haven't save ever
			save();
		}
		else {
			try {
				INIT = PlayerPrefs.GetInt("INIT")==1?true:false;
				UNLOCKED_CARDS = PlayerPrefs.GetString("UNLOCKED_CARDS");
				string[] unlockedCardsStr = UNLOCKED_CARDS.Split ('|');

				unlockedCards = new List<string>();

				for (int i=0; i < unlockedCardsStr.Length; i++) {
					if (unlockedCardsStr[i] != "") {
						unlockedCards.Add(unlockedCardsStr[i]);
						//Debug.Log (unlockedCardsStr[i]);
					}
				}

				//Debug.Log ("omg, how big is unlocked in LOAD " + unlockedCards.Count);

				if (PlayerPrefs.HasKey("HIGH_SCORE")) {
					HIGH_SCORE = PlayerPrefs.GetInt("HIGH_SCORE");
				}
				else {
					HIGH_SCORE = 500;
				}
				if (PlayerPrefs.HasKey("TIME_SCORE")) {
					TIME_SCORE = PlayerPrefs.GetInt("TIME_SCORE");
				}
				else {
					TIME_SCORE = 0;
				}

			}
			// handle the error
			catch (IOException err) {
				Debug.Log("Got: " + err);
				//save();
			}
			//}
		}
	}
	
	public static void save() {
		Debug.Log ("SAVE PERSISTENCE");
		try {
			PlayerPrefs.SetInt("INIT",1);

			UNLOCKED_CARDS = "";
			//Debug.Log ("omg, how big is unlocked in SAVE " + unlockedCards.Count);
			if (unlockedCards != null) {
				for (int i=0; i < unlockedCards.Count; i++) {
					UNLOCKED_CARDS += unlockedCards[i] + "|";

					//Debug.Log (unlockedCards[i]);
				}
			}

			PlayerPrefs.SetString("UNLOCKED_CARDS",UNLOCKED_CARDS);
			//Debug.Log (UNLOCKED_CARDS);
			PlayerPrefs.SetInt("HIGH_SCORE",HIGH_SCORE);
			PlayerPrefs.SetInt("TIME_SCORE",TIME_SCORE);
		}
		// handle the error
		catch (IOException err) {
			Debug.Log("Got: " + err);
		}
	}

	public static void reset() {
		Debug.Log ("JUST RESET YOUR EVERYTHING");
		//SetInt(name, value?1:0);
		try {
			PlayerPrefs.DeleteAll();
			INIT = false;
			UNLOCKED_CARDS = "";
			unlockedCards = new List<string>();
			HIGH_SCORE = 500;
			TIME_SCORE = 0;
			save();

		}
		// handle the error
		catch (IOException err) {
			//Debug.Log("Got: " + err);
		}

	}

	static void printOutUnused() {

		List<string> these = new List<string> ();
		for (int i=0; i < emojis.Count; i++) {
			string word = emojis[i].getWord();
			bool found = false;
			for (int w=0; w < words.Count; w++) {
				List<Emoji> es = words[w].getEmojis();
				if (word == es[0].getWord()) {
					found = true;
				}
				if (word == es[1].getWord()) {
					found = true;
				}
			}
			if (!found) {
				these.Add (word);
			}
		}


		string str = "";
		for (int i=0; i < these.Count; i++) {
			str += these[i] + ", ";

		}
		Debug.Log ("unused emojis " + these.Count + " of " + emojis.Count + " . total used " + (emojis.Count - these.Count));
		Debug.Log (str);
	}
}

