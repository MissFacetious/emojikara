using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LoadSave : MonoBehaviour {

	static JSONObject json;
	static public List<Emoji> emojis = new List<Emoji>();
	static public List<Word> words = new List<Word>();
	static bool INIT;

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
		loadEmojis ();
		loadWords ();
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
				string color = etext["color"].ToString().Trim('"');
				string filename = findEmojiForWord(wordAText);

				emoji.setColor(color);
				emoji.setWord(wordAText);
				emoji.setFilename(filename);

				emojis.Add(emoji);
			}
			word.setEmojis(emojis);

			words.Add (word);
		}
	}

	static string findEmojiForWord(string word) {
		for (int i=0; i < emojis.Count; i++) {
			if (word == emojis[i].getWord()) {
				return emojis[i].getFilename();
			}
		}
		return null;
	}

	public static void load () {
		Debug.Log ("LOAD PERSISTENCE");
		if (PlayerPrefs.GetInt ("INIT") == 0) {
			// we haven't save ever
			save ();
		}
		else {
			try {
				INIT = PlayerPrefs.GetInt("FIRST_WIN")==1?true:false;
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
		}
		// handle the error
		catch (IOException err) {
			//Debug.Log("Got: " + err);
		}
	}
}

