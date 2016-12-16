using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuTexts : MonoBehaviour {

	public Text ScoreText;
	public Text levelText;

	void Start() {

		ScoreText.GetComponent<Text> ().text = "" + PlayerPrefs.GetInt ("High Score");
		levelText.GetComponent<Text> ().text = "" + PlayerPrefs.GetInt ("LEVEL");
	}

	void Update() {
		ScoreText.GetComponent<Text> ().text = "" +  PlayerPrefs.GetInt ("High Score");
		levelText.GetComponent<Text> ().text = "" + PlayerPrefs.GetInt ("LEVEL");
	}
}
