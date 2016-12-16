using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

	public Canvas Quitmenu;

	public Button Resume;
	public Button options;

	public Button exitText;
	// Use this for initialization
	void Start () {

		Quitmenu = Quitmenu.GetComponent<Canvas> ();
		Resume = Resume.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		options = options.GetComponent<Button> ();
		Quitmenu.enabled = false;

	}

	public void ExitPressed() {
		Quitmenu.enabled = true;
		Resume.enabled = false;
		options.enabled = false;
		exitText.enabled = false;
	}
	public void NoPress() {
		Quitmenu.enabled = false;
		Resume.enabled = true;
		options.enabled = true;
		exitText.enabled = true;
	}
	public void PlayPress() {
		Time.timeScale = 1.0f;
		Quitmenu.enabled = false;
	}
	public void OptionsPress() {

	}
		
	public void Exit() {
		SceneManager.LoadScene (0);
	}
}
