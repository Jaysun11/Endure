using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public Transform expBar;
	public Transform healthBar;

	public Transform SprintBar;

	public TextMesh levelText;

	public TextMesh healthText;

	public TextMesh scoreText;

	public TextMesh waveText;

	public TextMesh remainingText;

	public TextMesh highText;

	public void setHighScoreText(int text) {

		highText.text = "" + text;

	}

	public void setExpText(float percentTolevel, int playerLevel) {

		levelText.text = "Level: " + playerLevel;
		expBar.localScale = new Vector3 (percentTolevel, 1, 1);

	}
	public void setHealthText(float percentHealth, float playerHealth) {

		healthText.text = "Health: " + playerHealth;
		healthBar.localScale = new Vector3 (percentHealth, 1, 1);

	}
	public void setSprint(float percentSprint) {


		SprintBar.localScale = new Vector3 (percentSprint, 1, 1);

	}
		

	public void SetWave(int wave) {

		waveText.text = "WAVE:  " + wave;

	}

	public void setScore(float score) {

		scoreText.text = score + "";

	}

	public string getScore() {

		return scoreText.text;

	}

	public void setRemaining(int Remaining) {

		remainingText.text  = "Remaining:  " + Remaining;

	}



}
