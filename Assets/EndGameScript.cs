using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EndGameScript : MonoBehaviour {
	
	public Text ScoreText;
	public Text levelText;

	void Start() {

		ScoreText.GetComponent<Text> ().text = "Score: " + GameObject.FindGameObjectWithTag ("Manager").GetComponent<EnemyManager> ().Score;
		levelText.GetComponent<Text> ().text = "Level: " + PlayerPrefs.GetInt ("LEVEL");
	}

	void Update() {
		ScoreText.GetComponent<Text> ().text = "Score: " + GameObject.FindGameObjectWithTag ("Manager").GetComponent<EnemyManager> ().Score;
		levelText.GetComponent<Text> ().text = "Level: " + PlayerPrefs.GetInt ("LEVEL");
	}
}