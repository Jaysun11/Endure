using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public Transform expBar;
	public Transform healthBar;

	public TextMesh ammoText;

	public TextMesh levelText;

	public TextMesh healthText;

	public TextMesh scoreText;

	public TextMesh waveText;

	public TextMesh remainingText;

	public void setExpText(float percentTolevel, int playerLevel) {

		levelText.text = "Level: " + playerLevel;
		expBar.localScale = new Vector3 (percentTolevel, 1, 1);

	}
	public void setHealthText(float percentHealth, float playerHealth) {

		healthText.text = "Health: " + playerHealth;
		healthBar.localScale = new Vector3 (percentHealth, 1, 1);


	}

	public void SetAmmoInfo(int total, int current) {

		ammoText.text = "Ammo:  " + current + " / " + total;

	}


	public void SetWave(int wave) {

		waveText.text = "WAVE:  " + wave;

	}

	public void setScore(float score) {

		scoreText.text = score + "";

	}

	public void setRemaining(int Remaining) {

		remainingText.text  = "Remaining:  " + Remaining;

	}



}
