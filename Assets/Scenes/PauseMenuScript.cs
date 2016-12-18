using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

	public Canvas Quitmenu;
	public Canvas PauseMenu;

	public Button Resume;
	public Button options;

	public Button exitText;

	public GameObject fps;
	public GameObject normal;

	public Button musicOn;
	public Button gunOn;

	public Button FPSON;

	public Canvas OptionsMenu;
	// Use this for initialization
	void Start () {
		PauseMenu = PauseMenu.GetComponent<Canvas> ();
		Quitmenu = Quitmenu.GetComponent<Canvas> ();
		Resume = Resume.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		options = options.GetComponent<Button> ();
		Quitmenu.enabled = false;
		OptionsMenu.enabled = false;

	}

	public void ExitPressed() {
		PauseMenu.enabled = false;
		Quitmenu.enabled = true;
		Resume.enabled = false;
		options.enabled = false;
		exitText.enabled = false;
	}
	public void NoPress() {
		PauseMenu.enabled = true;
		Quitmenu.enabled = false;
		Resume.enabled = true;
		options.enabled = true;
		exitText.enabled = true;
	}
	public void PlayPress() {
		Time.timeScale = 1.0f;
		PauseMenu.enabled = false;
	}
	public void OptionsPress() {
		OptionsMenu.enabled = true;
		PauseMenu.enabled = false;
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
	public void OptionsExitPress() {
		OptionsMenu.enabled = false;
		PauseMenu.enabled = true;
	}
		
	public void Exit() {
		SceneManager.LoadScene (0);
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

	public void FPSPress() {

		if (FPSON.GetComponent<Text> ().text == "ON") {
			FPSON.GetComponent<Text> ().text = "OFF";
			normal.SetActive (true);
			fps.SetActive (false);
			Time.timeScale = 0f;

		} else {
			FPSON.GetComponent<Text> ().text = "ON";
			normal.SetActive (false);
			fps.SetActive (true);
			Time.timeScale = 0f;
			fps.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
		}

	}

}
