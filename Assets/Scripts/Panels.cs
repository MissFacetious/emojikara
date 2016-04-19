using UnityEngine;
using System.Collections;

public class Panels : MonoBehaviour {

	protected static GameObject PausePanel;
	protected static GameObject ResultsPanel;
	protected static GameObject OptionsPanel;
	protected static GameObject PhonePanel;
	protected static GameObject GridPanel;
	protected static GameObject PostitPanel;
	// Use this for initialization
	void Start () {
		PausePanel = GameObject.Find ("PausePanel");
		ResultsPanel = GameObject.Find ("ResultsPanel");
		OptionsPanel = GameObject.Find ("OptionsPanel");
		PhonePanel = GameObject.Find ("phonePanel");
		GridPanel = GameObject.Find ("Box");
		PostitPanel = GameObject.Find ("postit");

		if (PausePanel != null)
			setPausePanel (false);
		if (ResultsPanel != null)
			setResultsPanel (false);
		if (OptionsPanel != null)
			setOptionsPanel (false);
		if (PhonePanel != null)
			setPhonePanel (false);
		if (PostitPanel != null)
			setPostItPanel (false);
		//if (GridPanel != null) 
		//	setGridPanel (false);
	}

	public static void setPostItPanel(bool set) {
		if (PostitPanel != null) {
			if (set) {
				PostitPanel.transform.position = new Vector2 (0f, 0f);
			}
			else {
				PostitPanel.transform.position = new Vector2 (-100f, 0f);
			}
		}
	}

	public static void setPhonePanel(bool set) {
		PhonePanel = GameObject.Find ("phonePanel");
		if (PhonePanel != null) {
			if (set) {
				PhonePanel.transform.localPosition = new Vector2 (-7.126932f, -0.9512743f);
			}
			else {
				PhonePanel.transform.localPosition = new Vector2 (-100f, 0f);
			}
		}
	}

	public static void setGridPanel(bool set) {
		if (GridPanel != null) {
			if (set) {
				GridPanel.transform.position = new Vector2 (0f, -0.8f);
			}
			else {
				GridPanel.transform.position = new Vector2 (-100f, -0.8f);
			}
		}
	}

	public static void setPausePanel(bool set) {
		if (PausePanel != null) {
			if (set) {
				PausePanel.transform.position = new Vector2 (0f, 0f);
			}
			else {
				PausePanel.transform.position = new Vector2 (-100f, 0f);
			}
		}
	}
	
	public static void setResultsPanel(bool set) {
		if (ResultsPanel != null) {
			if (set) {
				ResultsPanel.transform.position = new Vector2 (0f, 0f);
			}
			else {
				ResultsPanel.transform.position = new Vector2 (-100f, 0f);
			}
		}
	}

	public static void setOptionsPanel(bool set) {
		if (OptionsPanel != null) {
			if (set) {
				OptionsPanel.transform.position = new Vector2 (0f, 0f);
			}
			else {
				OptionsPanel.transform.position = new Vector2 (-100f, 0f);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
