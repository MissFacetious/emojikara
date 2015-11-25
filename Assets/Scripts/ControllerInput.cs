using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

	public static int currentX;
	public static int currentY;
	public static bool canGo;
	public static bool pause;

	AudioSource[] audio;

	CreateBoard board;
	public bool menu;
	public static bool once = false;
	// Use this for initialization
	void Start () {
		canGo = true;
		pause = false;
		board = new CreateBoard();
		// starting place not shown or 0, 0?
		currentY = 2;
		if (menu) {
			currentX = 2;
			currentY= 2;

		}
		changeWhereInGrid (0, 0);
		GameObject Music = GameObject.Find ("Music");
		if (Music != null) {
			audio = Music.GetComponents<AudioSource> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Preload.inOptions && once) {
			currentX = 0;
			currentY = 2;
			changeWhereInGrid(0,0);
			once = false;
		}
		else if (!Preload.inOptions && !pause && once) {
			currentX = 2;
			currentY = 2;
			changeWhereInGrid (0, 0);
			once = false;
		}
		else if (pause && once) {
			currentX = 0;
			currentY = 2;
			changeWhereInGrid (0, 0);
			once = false;
		}
		if (Input.GetKeyDown ("left")) {
			if (audio != null)  
				audio[1].Play();
			changeWhereInGrid (-1, 0);
			//Debug.Log ("pressed left");
		} else if (Input.GetKeyDown ("right")) {
			if (audio != null)  
				audio[1].Play();
			changeWhereInGrid (1, 0);
			//Debug.Log ("pressed right");
		} else if (Input.GetKeyDown ("up")) {
			if (audio != null)  
				audio[1].Play();
			changeWhereInGrid (0, 1);
			//Debug.Log ("pressed up");
		} else if (Input.GetKeyDown ("down")) {
			if (audio != null)  
				audio[1].Play();
			changeWhereInGrid (0, -1);
			//Debug.Log ("pressed down");
		} else if (Input.GetKeyDown ("space")) {
			if (menu) {
				if (audio != null)  
					audio[2].Play();
				if (Preload.inOptions) {
					if (currentX == 0 && currentY == 2) {
						if (Preload.Music.GetComponent<AudioSource>().isPlaying) {
							Preload.Music.GetComponent<AudioSource>().Pause();
						}
						else {
							Preload.Music.GetComponent<AudioSource>().Play ();
						}
					}
					if (currentX == 0 && currentY == 1) {
						Debug.Log ("reset");
					}
					if (currentX == 0 && currentY == 0) {
						Preload.OptionsPanel.SetActive(false);
						Preload.inOptions = false;
						currentX = 2;
						currentY = 2;
						changeWhereInGrid (0, 0);
					}
				}
				else {
					if (Preload.okayToProceed) {
						if (currentX == 2 && currentY == 2) {
							Application.LoadLevel("match");
						}
						if (currentX == 0 && currentY == 1) {
							Application.LoadLevel("match");
						}
						if (currentX == 3 && currentY == 1) {
							Preload.OptionsPanel.SetActive(true);
							Preload.inOptions = true;
							currentX = 0;
							currentY = 2;
							changeWhereInGrid(0,0);
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
							once =true;
							CreateBoard.setPausePanel(false);
						}
						if (currentX == 0 && currentY == 1) {
							if (Preload.Music.GetComponent<AudioSource>().isPlaying) {
								Preload.Music.GetComponent<AudioSource>().Pause();
							}
							else {
								Preload.Music.GetComponent<AudioSource>().Play ();
							}
						}
						if (currentX == 0 && currentY == 0) {
							pause = false;
							once =true;
							CreateBoard.setPausePanel(false);
							CreateBoard.reset = true;
						}
					}
					else {
						if (currentX == 5 && currentY == 1) {
							Debug.Log ("pause for menu");
							pause = true;
							once =true;
							CreateBoard.setPausePanel(true);
						}
						else {
							for (int i=0; i < CreateBoard.getBoardSize(); i++) {
								if (CreateBoard.getBoard(i).GetComponent<Card> ().column == currentX &&
								    CreateBoard.getBoard(i).GetComponent<Card> ().row == currentY) {
									string value = CreateBoard.getBoard(i).GetComponent<Card> ().value;
									CreateBoard.getBoard(i).GetComponent<Card> ().controller = true;
								}
							}
							changeWhereInGrid (1, 0);
						}
					}
				}
			}
		}
	}

	public void changeWhereInGrid(int hor, int vert) {
		if (menu) {
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
			}
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

		transform.position = new Vector3(xoffset, yoffset, 0f);
	}
}
