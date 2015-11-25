using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Word : MonoBehaviour {

	private string complete;
	private List<Emoji> emojis;


	public string getComplete() {
		return complete;
	}
	
	public void setComplete(string complete) {
		this.complete = complete;
	}

	public List<Emoji> getEmojis() {
		return emojis;
	}

	public void setEmojis(List<Emoji> emojis) {
		this.emojis = emojis;
	}

}
