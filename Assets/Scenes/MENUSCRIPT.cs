using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MENUSCRIPT : MonoBehaviour {

	public Canvas Quitmenu;


	public Button startText;
	public Button options;
	public Button unlockText;

	public Button exitText;
	// Use this for initialization
	void Start () {
		
		Quitmenu = Quitmenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		options = options.GetComponent<Button> ();
		unlockText = unlockText.GetComponent<Button> ();
		Quitmenu.enabled = false;
	
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

	}
	public void UnlocksPress() {

	}


	public void Exit() {
		Application.Quit();
	}
}
