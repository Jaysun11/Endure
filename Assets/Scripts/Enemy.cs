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

	public GameObject blood;
	public GameObject splitter1;
	public GameObject splitter2;

	public GameObject PointText;


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



	}

	public override void takeDamage (float dmg) {

		base.takeDamage (dmg);

	}

	public override void Die () {


		player.AddExperience (expOnDeath);
		enemyManager.remainingEnemies--;
		string text1 = "";
		switch (gameObject.name) {

		case "Enemy(Clone)":
			{
				enemyManager.Score+=100;
				text1 = "+100";
				break;
			}
		case "BigEnemy(Clone)":
			{
				enemyManager.Score += 250;
				text1 = "+250";
				break;
			}
		case "LittleEnemy(Clone)":
			{
				enemyManager.Score += 120;
				text1 = "+120";
				break;
			}
		case "BossEnemy(Clone)":
			{
				enemyManager.Score += 500;
				text1 = "+500";
				break;
			}
		case "Splitter(Clone)":
			{
				enemyManager.Score += 200;
				text1 = "+200";
				break;
			}
		case "Splitter1(Clone)":
			{
				enemyManager.Score += 100;
				text1 = "+100";
				break;
			}
		case "Splitter2(Clone)":
			{
				enemyManager.Score += 50;
				text1 = "+50";
				break;
			}

		}


		Vector3 spawnPos = gameObject.transform.position + Random.insideUnitSphere * 10;
		Vector3 adjustedPos = new Vector3(spawnPos.x, 1.75f, spawnPos.z);

		if (gameObject.name == "RegSplitter") {
			for (int i = 0;  i < 2; i++) {
				enemyManager.remainingEnemies++;
				Instantiate (splitter1, adjustedPos, Quaternion.identity);
			}

		} 
		if (gameObject.name == "Splitter1") {
			Debug.Log ("HERE");
			for (int i = 0;  i < 3; i++) {
				enemyManager.remainingEnemies++;
				Instantiate (splitter2, adjustedPos, Quaternion.identity);
			}
		}

		for (int i = 0; i < 5; i++) {
			Vector3 spawnPos1 = gameObject.transform.position + Random.insideUnitSphere * 2;
			Vector3 adjustedPos1 = new Vector3(spawnPos1.x, 0.75f, spawnPos1.z);
			Instantiate (blood, adjustedPos1, Quaternion.identity);
		}

		Vector3 spawnPos2 = gameObject.transform.position;
		Vector3 adjustedPos2  = new Vector3(spawnPos2.x, 2.75f, spawnPos2.z);
		Instantiate (PointText, adjustedPos2, Quaternion.identity);
		PointText.GetComponent<TextMesh> ().text = text1;

		base.Die ();

	}
		



}
