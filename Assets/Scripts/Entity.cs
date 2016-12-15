using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public float health;
	public float maxHealth;

	[HideInInspector]
	public GameGUI gui;

	void Start() {
		gui = GameObject.FindGameObjectWithTag ("GUI").GetComponent<GameGUI> ();
	}

	public virtual void takeDamage(float dmg) {
		health -= dmg;

		if (health <= 0) {
			Die ();
		}
	}

	public virtual void Die() {
		DestroyObject(gameObject);
	}

	void OnCollisionEnter (Collision hit){
		gui = GameObject.FindGameObjectWithTag ("GUI").GetComponent<GameGUI> ();
		if (hit.gameObject.tag == "Player" && gameObject.tag != "Barrel") {
			
			hit.gameObject.GetComponent<player> ().takeDamage(this.gameObject.GetComponent<Enemy> ().damageDealt);

		}

	}
		
}
