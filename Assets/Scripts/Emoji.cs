using UnityEngine;
using System.Collections;

public class Emoji : MonoBehaviour {

	private string word;
	private string filename;
	Color color;
	public bool used = false;

	public void setWord(string word) {
		this.word = word;
	}

	public string getWord() {
		return word;
	}

	public void setFilename(string filename) {
		this.filename = filename;
	}

	public string getFilename() {
		return filename;
	}

	public void setColor(string myColor) {
		Color c = Color.black;
		if (myColor == "HOTPINK") {
			c = Constants.HOTPINK;
		}
		if (myColor == "GREEN") {
			c = Constants.GREEN;
		}
		if (myColor == "BLUE") {
			c = Constants.BLUE;
		}
		if (myColor == "WHITE") {
			c = Constants.WHITE;
		}
		if (myColor == "CYAN") {
			c = Constants.CYAN;
		}
		if (myColor == "RED") {
			c = Constants.RED;
		}
		if (myColor == "ORANGE") {
			c = Constants.ORANGE;
		}
		if (myColor == "YELLOW") {
			c = Constants.YELLOW;
		}
		if (myColor == "BROWN") {
			c = Constants.BROWN;
		}
		if (myColor == "GRAY") {
			c = Constants.GRAY;
		}
		if (myColor == "BLACK") {
			c = Constants.BLACK;
		}
		if (myColor == "LIGHTGRAY") {
			c = Constants.LIGHTGRAY;
		}
		if (myColor == "GOLD") {
			c = Constants.GOLD;
		}
		if (myColor == "PURPLE") {
			c = Constants.PURPLE;
		}
		if (myColor == "MAROON") {
			c = Constants.MAROON;
		}
		if (myColor == "PINK") {
			c = Constants.PINK;
		}
		if (myColor == "SKYBLUE") {
			c = Constants.SKYBLUE;
		}
		if (myColor == "LIGHTBROWN") {
			c = Constants.LIGHTBROWN;
		}
		color = c;
	}

	public Color getColor() {
		return color;
	}
}
