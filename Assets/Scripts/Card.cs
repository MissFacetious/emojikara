using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Card : MonoBehaviour {


	public bool idle;
	public bool fall;
	public string thisName;
	public bool menu;
	public string myCard;
	public int row;
	public int column;
	public string value;
	public bool picked;
	public bool correct;
	public bool controller;
	public bool arrow;
	public bool custom;
	bool go = true;

	public bool touched = false;
	bool pressed = false;

	SpriteRenderer renderer;
	Animator animator;
	Animation animation;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		animation = GetComponent<Animation>();
		controller = false;
		fall = false;
		idle = true;
	}


	void OnMouseDown() {
		if (!ControllerInput.win) {
			if (!pressed)
				touched = true;
            PlayIt.currentCard = gameObject;
			PlayIt.play = true;
		}
	}
	

	// Update is called once per frame
	void Update () {

		if (fall) {
			GetComponent<Rigidbody2D>().isKinematic = false;
		}

		if (picked) {
			renderer.sprite = (Resources.Load<Sprite> ("Emoji/" + value));
		} else if (arrow) {

		}
		else {
			renderer.sprite = (Resources.Load<Sprite> ("card"));
		}
		if (correct) { // and done with picked animation

		}
		if (idle) {
		//	animator.SetBool("idle", true);
		} else {
		//	animator.SetBool("idle", false);
		}

		if (controller) {
			pressed = true;
			OnMouseDown ();
			controller = false;
		}
	}
}
