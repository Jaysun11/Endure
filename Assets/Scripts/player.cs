using UnityEngine;
using System.Collections;

public class player : Entity {

	private int level;
	private float currentLevelExperience;
	private float experienceToLevel;

	void Start() {
		levelUP ();
		gui.setHealthText (this.health / this.maxHealth, this.health);
	}

	public void AddExperience(float exp) {
		currentLevelExperience += exp;
		if (currentLevelExperience >= experienceToLevel) {
			currentLevelExperience -= experienceToLevel;
			levelUP ();
		}

		gui.setExpText (currentLevelExperience / experienceToLevel, level);
	}

	private void levelUP() {
		level++;
		experienceToLevel = level * 50 + Mathf.Pow (level * 2, 2);

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

	}

	public override void Die () {
		
		base.Die ();
		GameOver ();
	}

}
