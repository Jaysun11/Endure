using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MENUSCRIPT : MonoBehaviour {

	public Canvas Quitmenu;
	public Canvas optionsMenu;
	public Canvas unlocksMenu;

	public Button startText;
	public Button options;
	public Button unlockText;

	public Button musicOn;
	public Button gunOn;


	public Button exitText;
	// Use this for initialization
	void Start () {
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		options = options.GetComponent<Button> ();
		unlockText = unlockText.GetComponent<Button> ();

		optionsMenu = optionsMenu.GetComponent<Canvas> ();
		Quitmenu = Quitmenu.GetComponent<Canvas> ();
		unlocksMenu = unlocksMenu.GetComponent<Canvas> ();
		unlocksMenu.enabled = false;
		Quitmenu.enabled = false;
		optionsMenu.enabled = false;
	}
		
	
	public void ExitPressed() {
		Quitmenu.enabled = true;
		startText.enabled = false;
		options.enabled = false;
		unlockText.enabled = false;
		exitText.enabled = false;
	}
	public void NoPress() {
		Quitmenu.enabled = false;
		startText.enabled = true;
		options.enabled = true;
		unlockText.enabled = true;
		exitText.enabled = true;
	}
	public void PlayPress() {
		SceneManager.LoadScene ("Endure");
		Time.timeScale = 1.0f;
	}

	public void OptionsPress() {
		optionsMenu.enabled = true;
		startText.enabled = false;
		options.enabled = false;
		unlockText.enabled = false;
		exitText.enabled = false;
		if (PlayerPrefs.GetInt ("GunOn") == 1) {
			gunOn.GetComponent<Text> ().text = "ON";
		} else if (PlayerPrefs.GetInt ("GunOn") == 0) {
			gunOn.GetComponent<Text> ().text = "OFF";
		}
		if (PlayerPrefs.GetInt ("MusicOn") == 1) {
			musicOn.GetComponent<Text> ().text = "ON";
		} else if (PlayerPrefs.GetInt ("MusicOn") == 0) {
			musicOn.GetComponent<Text> ().text = "OFF";
		}
	}
	public void exitOptionsPress() {
		optionsMenu.enabled = false;
		startText.enabled = true;
		options.enabled = true;
		unlockText.enabled = true;
		exitText.enabled = true;
	}
	public void GunSoundPress() {
		if (gunOn.GetComponent<Text> ().text == "ON") {
			gunOn.GetComponent<Text> ().text = "OFF";
			PlayerPrefs.SetInt ("GunOn", 0);
		} else {
			gunOn.GetComponent<Text> ().text = "ON";
			PlayerPrefs.SetInt ("GunOn", 1);
		}
	}
	public void MusicSoundPress() {

		if (musicOn.GetComponent<Text> ().text == "ON") {
			musicOn.GetComponent<Text> ().text = "OFF";
			PlayerPrefs.SetInt ("MusicOn", 0);
		} else {
			musicOn.GetComponent<Text> ().text = "ON";
			PlayerPrefs.SetInt ("MusicOn", 1);
		}

	}


	public void UnlocksPress() {
		unlocksMenu.enabled = true;
		startText.enabled = false;
		options.enabled = false;
		unlockText.enabled = false;
		exitText.enabled = false;
	}

	public void exitUnlocksPress() {
		unlocksMenu.enabled = false;
		startText.enabled = true;
		options.enabled = true;
		unlockText.enabled = true;
		exitText.enabled = true;
	}
	public void Exit() {
		Application.Quit();
	}






}
