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
		gui.setHealthText (this.health / this.maxHealth, this.health);
	}

}
