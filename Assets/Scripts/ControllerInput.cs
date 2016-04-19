using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class ControllerInput : MonoBehaviour {

	public GameObject smiley;
	public GameObject outline;
	public static int currentX;
	public static int currentY;
	public static bool canGo;
	public static bool pause;
	public static bool results;
	public static bool win;

	public static int custom_index;

	AudioSource[] audio;


	PlayIt playIt;
	CreateBoard board;
	public bool menu;
	public bool custom;
	public static bool once = false;
	// Use this for initialization
	void Start () {
		smiley = GameObject.Find ("smiley");
		outline = GameObject.Find ("outline");
		canGo = true;
		pause = false;
		custom_index = 0;
		board = new CreateBoard();
		playIt = GameObject.Find ("Background").GetComponent<PlayIt> ();
		// starting place not shown or 0, 0?
		currentY = 2;
		if (menu) {
			currentX = 2;
			currentY= 2;
		}
		if (custom) {
			currentX = 0;
			currentY= 1;
			// load all the unlocked cards from playprefs
		}
		changeWhereInGrid (0, 0);
		GameObject Music = GameObject.Find ("MusicPlay");
		if (Music != null) {
			audio = Music.GetComponents<AudioSource> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		try {
		if (Preload.inOptions && once && !custom) {
			currentX = 0;
			currentY = 2;
			changeWhereInGrid(0,0);
			once = false;
		}
			else if (!Preload.inOptions && !pause && !results && once && !custom) {
			currentX = 2;
			currentY = 2;
			changeWhereInGrid (0, 0);
			once = false;
		}
		else if (pause && once) {
			//	Debug.Log ("in pause");
			currentX = 0;
			currentY = 2;
			changeWhereInGrid (0, 0);
			once = false;
		}
		else if (results && once && !custom) {
			//Debug.Log ("in results");
			currentX = 0;
			currentY = 0;
			changeWhereInGrid (0, 0);
			once = false;
		}
		else if (custom && once) {
			currentX = 0;
			currentY = 1;
			changeWhereInGrid (0, 0);
			once = false;
		}
			/*
		InputManager.OnDeviceAttached += inputDevice => Debug.Log ("Attached: " + inputDevice.Name);
		InputManager.OnDeviceDetached += inputDevice => Debug.Log ("Detached: " + inputDevice.Name);
		InputManager.OnActiveDeviceChanged += inputDevice => Debug.Log ("Active device changed to: " + inputDevice.Name);
		*/
			if (!win && (PCKeyMapperBool ("left"))) {
			if (audio != null)  {			
				audio [13].Play ();
			}
			changeWhereInGrid (-1, 0);
			//Debug.Log ("pressed left");
			} else if (!win && (PCKeyMapperBool ("right"))) {
			if (audio != null) {		
				audio [13].Play ();
				}
			changeWhereInGrid (1, 0);
			//Debug.Log ("pressed right");
			} else if (!win && (PCKeyMapperBool("up"))) {
			if (audio != null)  {
				audio [13].Play ();
				}
			changeWhereInGrid (0, 1);
			//Debug.Log ("pressed up");
			} else if (!win && (PCKeyMapperBool("down"))) {
			if (audio != null)  {
				audio [13].Play ();
				}
			changeWhereInGrid (0, -1);
			//Debug.Log ("pressed down");
			} else if (!win && (PCKeyMapperBool("space"))) {
			if (menu) {
				if (audio != null)  {
					audio [2].Play ();
					}
				if (Preload.inOptions) {
					if (currentX == 0 && currentY == 2) {
						Preload.toggleMusic();
					}
					if (currentX == 0 && currentY == 1) {
						Debug.Log ("reset");
						LoadSave.reset ();
					}
					if (currentX == 0 && currentY == 0) {
						Panels.setOptionsPanel (false);
						Preload.inOptions = false;
						currentX = 2;
						currentY = 2;
						changeWhereInGrid (0, 0);
							Panels.setGridPanel(true);
						}
				} else {
					if (Preload.okayToProceed) {
						if (currentX == 2 && currentY == 2) {
							LoadSave.load ();
							//PlayIt.TYPE = PlayIt.LOADING;
							Panels.setGridPanel (false);
							StartCoroutine(loadScene("match"));
							//Application.LoadLevel ("match");
							PlayIt.TYPE = PlayIt.PLAY;
						}
						if (currentX == 0 && currentY == 1) {
							LoadSave.load ();
							PlayIt.TYPE = PlayIt.TIMED;
							//Application.LoadLevel ("match");
							Panels.setGridPanel (false);
							StartCoroutine(loadScene("match"));
						}
						if (currentX == 1 && currentY == 0) {
							LoadSave.load ();
							PlayIt.TYPE = PlayIt.CUSTOM;
							//Application.LoadLevel ("words");
							Panels.setGridPanel (false);
							StartCoroutine(loadScene("words"));
						}
						if (currentX == 3 && currentY == 1) {
							Panels.setOptionsPanel (true);
							Preload.inOptions = true;
							currentX = 0;
							currentY = 2;
							changeWhereInGrid (0, 0);
								Panels.setGridPanel(false);
							}
						if (currentX == 4 && currentY == 0) {
							Application.Quit ();
						}
						if (currentX == 1 && currentY == 0) {
							Debug.Log ("game center");
						}
					}
				}
			} else {
				if (canGo) {
					if (pause) {
						if (currentX == 0 && currentY == 2) {
							pause = false;
							once = true;
							Panels.setPausePanel (false);
							Panels.setGridPanel (true);
						}
						if (currentX == 0 && currentY == 1) {
							Preload.toggleMusic();
						}
					} 
					else if (results) {
						if (currentX == 0 && currentY == 0) {
							results = false;
							once = true;
							Panels.setResultsPanel (false);
							CreateBoard.reset = true;
						}
						if (currentX == 4 && currentY == 0) {
							Debug.Log ("hit again");
							results = false;
							once = true;
							Panels.setResultsPanel (false);
							Panels.setGridPanel (true);
							//if (playIt == null) {
							//	playIt = new PlayIt();
							//}
							playIt.StartAgain ();

							//if (board == null) {
							//		board = new CreateBoard();
							//	}
							board.StartAgain ();
						}
					}
					else if (custom) {
							if (currentX == 5 && currentY == 1) {
								Debug.Log ("pause for menu");
								pause = true;
								once = true;
								Panels.setPausePanel (true);
								Panels.setGridPanel(false);
							} 
						}
					else {
						if (currentX == 5 && currentY == 1) {
							//Debug.Log ("pause for menu");
							pause = true;
							once = true;
							Panels.setPausePanel (true);
							Panels.setGridPanel(false);
						} 
						else {
							for (int i=0; i < CreateBoard.getBoardSize(); i++) {
								if (CreateBoard.getBoard (i).GetComponent<Card> ().column == currentX &&
									CreateBoard.getBoard (i).GetComponent<Card> ().row == currentY) {
									string value = CreateBoard.getBoard (i).GetComponent<Card> ().value;
									CreateBoard.getBoard (i).GetComponent<Card> ().controller = true;
								}
							}
							if (currentX < 4) {
								changeWhereInGrid (1, 0);
							}
							else {
								changeWhereInGrid (-1, 0);
							}
						}
					}
				}
			}
			} else if (!win && (PCKeyMapperBool("pause"))) {
				if (PlayIt.TYPE > PlayIt.MENU) {
				if (audio != null)  {
					audio [2].Play ();
				}
				outline.transform.position = GameObject.Find ("back").transform.position;
				if (!ControllerInput.pause) {
					//Debug.Log ("move panels");
					Panels.setGridPanel (false);
					Panels.setPausePanel (true);
					ControllerInput.pause = true;
					ControllerInput.once = true;
				}	
				else {
					//Debug.Log ("move panels");
					Panels.setGridPanel (true);
					Panels.setPausePanel (false);
					ControllerInput.pause = false;
					ControllerInput.once = true;
				}
				}
			}
		}
		catch (Exception e) {
			Debug.Log (e);
		}
	}
	
	public static void displayCustom() {

		if (LoadSave.unlockedCards.Count > 0) {
			if (custom_index < 0) {
				custom_index = LoadSave.unlockedCards.Count - 1;
			}
			if (custom_index > LoadSave.unlockedCards.Count - 1) {
				custom_index = 0;
			}
		}
	//	Debug.Log (currentY +  " " + currentX);
		//currentY = custom_index;

		if (LoadSave.unlockedCards.Count > 0 && LoadSave.unlockedCards.Count > custom_index) {
			GameObject swap = GameObject.Find ("Card" + currentX);
			if (swap != null) {
				swap.GetComponent<Card> ().picked = true;
				swap.GetComponent<Card> ().value = LoadSave.unlockedCards[custom_index];
			}
		}
	}

	public void changeWhereInGrid(int hor, int vert) {
		if (menu && !custom) {
			if (Preload.inOptions) {
				currentX = 0;
				currentY += vert;
				if (currentY < 0) {
					currentY = Constants.ROWS - 1;
				}
				if (currentY > Constants.ROWS - 1) {
					currentY = 0;
				}
			} 
			else {
				TextMesh desc = GameObject.Find ("Desc").GetComponent<TextMesh>();
				if (currentX == 2 && currentY == 2) {
					if (vert == 1) {
						currentX = 1;
						currentY = 0;
					}
					if (vert == -1) {
						currentX = 0;
						currentY = 1;
					}
					if (hor == -1) {
						currentX = 0;
						currentY = 1;
					}
					if (hor == 1) {
						currentX = 3;
						currentY = 1;
					}
				} else if (currentX == 0 && currentY == 1) {
					if (vert == 1) {
						currentX = 2;
						currentY = 2;
					}
					if (vert == -1) {
						currentX = 1;
						currentY = 0;
					}
					if (hor == -1) {
						currentX = 3;
						currentY = 1;
					}
					if (hor == 1) {
						currentX = 3;
						currentY = 1;
					}
				} else if (currentX == 3 && currentY == 1) {
					if (vert == 1) {
						currentX = 2;
						currentY = 2;
					}
					if (vert == -1) {
						currentX = 4;
						currentY = 0;
					}
					if (hor == -1) {
						currentX = 0;
						currentY = 1;
					}
					if (hor == 1) {
						currentX = 0;
						currentY = 1;
					}
				} else if (currentX == 1 && currentY == 0) {
					if (vert == 1) {
						currentX = 3;
						currentY = 1;
					}
					if (vert == -1) {
						currentX = 2;
						currentY = 2;
					}
					if (hor == -1) {
						currentX = 4;
						currentY = 0;
					}
					if (hor == 1) {
						currentX = 4;
						currentY = 0;
					}
				} else if (currentX == 4 && currentY == 0) {
					if (vert == 1) {
						currentX = 3;
						currentY = 1;
					}
					if (vert == -1) {
						currentX = 2;
						currentY = 2;
					}
					if (hor == -1) {
						currentX = 1;
						currentY = 0;
					}
					if (hor == 1) {
						currentX = 1;
						currentY = 0;
					}
				}

				if (currentX == 2 && currentY == 2) {
					desc.text = Constants.ROUND + " Rounds of Emoji!";
				} else if (currentX == 0 && currentY == 1) {
					desc.text = Constants.TIMER + " seconds of Timed Emoji.";
				} else if (currentX == 3 && currentY == 1) {
					desc.text = "Change Game Settings.";
				} else if (currentX == 1 && currentY == 0) {
					desc.text = "Build your own Emoji Sentences.";
				} else if (currentX == 4 && currentY == 0) {
					desc.text = "Exit the Game.";
				}
			}
		}
		else if (custom && !pause) {
			currentX = -100;
			currentY = -100;
		}
		else {
			if (pause) {
				currentX = 0;
				currentY += vert;
				if (currentY < 0) {
					currentY = Constants.ROWS - 1;
				}
				if (currentY > Constants.ROWS - 1) {
					currentY = 0;
				}
			}
			else if (results) {
				currentX += hor*4;
				currentY = 0;
				if (currentX < 0) {
					currentX = 4;
				}
				if (currentX > 4) {
					currentX = 0;
				}
			}
			else {
				currentX += hor;
				currentY += vert;

				if (currentX < 0) {
					currentX = Constants.COLUMNS - 1;
				}
				if (currentY < 0) {
					currentY = Constants.ROWS - 1;
				}
				if (currentX == 5) {
					currentY = 1;
				}
				if (currentX > Constants.COLUMNS) {
					currentX = 0;
				}
				if (currentY > Constants.ROWS - 1) {
					currentY = 0;
				}
			}
		}
		float xoffset = Constants.TILE_WIDTH*currentX+0.1f;
		float yoffset = Constants.TILE_HEIGHT*currentY+0.1f;
		
		xoffset -= Constants.XOFFSET;
		yoffset -= Constants.YOFFSET;
		if (currentX == 5) {
			xoffset = 5f;
		}

		if (PlayIt.TYPE != PlayIt.LOADING) {
			transform.position = new Vector3 (xoffset, yoffset, 0f);
		}

		if (CreateBoard.reset && custom) {
			Debug.Log ("restart");
			CreateBoard.reset = false;
			//LoadSave.save(); // there is no reason to save here...
			PlayIt.TYPE = PlayIt.MENU;
			Panels.setGridPanel (false);
			StartCoroutine(loadScene("title"));
			//Application.LoadLevel("title");
		}
	}

	public bool PCKeyMapperBool(string action) {
		bool pressed = false;
        if (action == "select")
        {
            pressed = Input.GetKeyDown(KeyCode.Space);
        }
        else if (action == "pause")
        {
            pressed = Input.GetKeyDown("p");
        }
        else
        {
            pressed = Input.GetKeyDown(action);
        }
		return pressed;
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
}
