using UnityEngine;
using System.Collections;

public class player : Entity {

	private int level;
	private float currentLevelExperience;
	private float experienceToLevel;

	public GameObject gameOverScreen;

	void Start() {
		gameOverScreen.GetComponent<Canvas>().enabled = false;
		level = PlayerPrefs.GetInt ("LEVEL");
		currentLevelExperience = PlayerPrefs.GetFloat ("LEVELEXP");
		experienceToLevel = PlayerPrefs.GetFloat ("EXP TO LEVEL");
		gui.setHealthText (this.health / this.maxHealth, this.health);
		AddExperience (0);
	}

	public void AddExperience(float exp) {
		currentLevelExperience += exp;
		PlayerPrefs.SetFloat ("LEVELEXP", currentLevelExperience);

		if (currentLevelExperience >= experienceToLevel) {
			currentLevelExperience -= experienceToLevel;
			PlayerPrefs.SetFloat ("LEVELEXP", currentLevelExperience);
			levelUP ();
		}

		gui.setExpText (currentLevelExperience / experienceToLevel, level);
	}

	private void levelUP() {
		level++;
		PlayerPrefs.SetInt ("LEVEL", level);
		experienceToLevel = level * 50 + Mathf.Pow (level * 2, 2);
		PlayerPrefs.SetFloat ("EXP TO LEVEL", experienceToLevel);
		AddExperience (0);
	}

	public override void takeDamage (float dmg) {
		
		base.takeDamage (dmg);

		if (health >= 0) {
			gui.setHealthText (this.health / this.maxHealth, this.health);
		}



		if (health <= 0) {
			Die ();
		}

	
	}

	public void GameOver() {

		if (PlayerPrefs.GetInt ("High Score") <= int.Parse(gui.getScore ())) {
			PlayerPrefs.SetInt ("High Score", int.Parse(gui.getScore()));
			gui.setHighScoreText (int.Parse(gui.getScore ()));
		}

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");


		foreach (GameObject enemy in enemies) {
			DestroyObject (enemy);
		}


		gameOverScreen.GetComponent<Canvas> ().enabled = true;

	}

	public override void Die () {
		
		base.Die ();
		GameOver ();
	}

}
