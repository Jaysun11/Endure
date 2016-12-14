using UnityEngine;
using System.Collections;

public class Enemy : Entity {

	public float expOnDeath;
	private player player;
	private Transform playerTrans;

	private EnemyManager enemyManager;

	public float damageDealt;

	public float speedRotation;

	public float MoveSpeed;


	void Start() {
		
		enemyManager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<EnemyManager> ();

		
	}

	void Update() {

		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<player>();
			playerTrans = player.transform;
		}
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (playerTrans.position - transform.position), speedRotation * Time.deltaTime);
		transform.position += transform.forward * MoveSpeed * Time.deltaTime;

		Quaternion rot = GetComponent<Rigidbody>().rotation;
		rot [0] = 0;
		rot [2] = 0;
		GetComponent<Rigidbody>().rotation = rot;

	}

	public override void takeDamage (float dmg) {

		base.takeDamage (dmg);

	}

	public override void Die () {

		player.AddExperience (expOnDeath);
		enemyManager.remainingEnemies--;
		enemyManager.Score += 100;

		base.Die ();

	}
		



}
