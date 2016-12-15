using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour {

	private Vector3 spawnPos;

	private int spawnRadius = 5;


	public GameObject Splitter;
	public GameObject littleEnemy;
	public GameObject BigEnemy;
	public GameObject BossEnemy;

	public GameObject enemyPrefab;
	public GameObject barrel;

	private int startWave = 1;

	private int enemiesToSpawn = 1;

	private int currentWave;

	public Spawner[] spawners;

	public HealthKitManager healthKitManager;


	private bool waveCompleted = false;
	[HideInInspector]
	public int remainingEnemies = 0;

	private GameGUI gui;
	[HideInInspector]
	public float Score;
	[HideInInspector]


	void Start() {
		gui = GameObject.FindGameObjectWithTag ("GUI").GetComponent<GameGUI> ();
		currentWave = startWave;
		gui.SetWave (currentWave);
		gui.setScore (Score);
		spawnWave ();
	}


	// Update is called once per frame
	void Update () {
		gui.setScore (Score);
		gui.SetWave (currentWave);
		gui.setRemaining (remainingEnemies);
		checkComplete ();
		if (waveCompleted) {
			currentWave++;
			gui.SetWave (currentWave);
			enemiesToSpawn = currentWave;
			spawnWave ();
		}
	
	}

	void checkComplete() {

		if (remainingEnemies == 0) {
			waveCompleted = true;
		}

		//IF THERE ARE NO ZOMBIES start a new wave

	}
	public void spawnWave() {
		GameObject[] barrels = GameObject.FindGameObjectsWithTag ("Barrel");
		foreach (GameObject barrel in barrels) {
			DestroyObject (barrel);
		}
		for (int i = 0; i < spawners.Length; i++) {
			for (int j = 0; j < enemiesToSpawn; j++) {

				spawnPos = spawners[i].gameObject.transform.position + Random.insideUnitSphere * spawnRadius;
				Vector3 adjustedPos = new Vector3(spawnPos.x, 1.75f, spawnPos.z);

				int zombieGen = Random.Range (0, 100);

				if (currentWave >= 6) {

					if (zombieGen < 12) {
						Instantiate (littleEnemy, adjustedPos, Quaternion.identity);
					} else if (zombieGen >= 12 && zombieGen < 20) {
						Instantiate (BigEnemy, adjustedPos, Quaternion.identity);
					} else if (zombieGen >= 20 && zombieGen < 28) {
						Instantiate (BossEnemy, adjustedPos, Quaternion.identity);
					}  else if (zombieGen >= 28 && zombieGen < 35) {
						Instantiate (Splitter, adjustedPos, Quaternion.identity);
					} else {
						Instantiate (enemyPrefab, adjustedPos, Quaternion.identity);
					}

				} else if (currentWave >= 5) {

					if (zombieGen < 12) {
						Instantiate (littleEnemy, adjustedPos, Quaternion.identity);
					} else if (zombieGen >= 12 && zombieGen < 20) {
						Instantiate (BigEnemy, adjustedPos, Quaternion.identity);
					} else if (zombieGen >= 20 && zombieGen < 28) {
						Instantiate (BossEnemy, adjustedPos, Quaternion.identity);
					} else {
						Instantiate (enemyPrefab, adjustedPos, Quaternion.identity);
					}

				} else if (currentWave >= 4) {
					if (zombieGen < 12) {
						Instantiate (littleEnemy, adjustedPos, Quaternion.identity);
					} else if (zombieGen >= 12 && zombieGen < 20) {
						Instantiate (BigEnemy, adjustedPos, Quaternion.identity);
					}
					else {
						Instantiate (enemyPrefab, adjustedPos, Quaternion.identity);
					}
				} else if (currentWave >= 3) {
					if (zombieGen < 12) {
						Instantiate (littleEnemy, adjustedPos, Quaternion.identity);
					} else {
						Instantiate (enemyPrefab, adjustedPos, Quaternion.identity);
					}
				} else {
					Instantiate (enemyPrefab, adjustedPos, Quaternion.identity);
				}


				if (Random.Range(0, 100) < 7) {
					Vector3 barrelpos = spawners[i].gameObject.transform.position + Random.insideUnitSphere * 30;
					Vector3 barrelpos2 = new Vector3(barrelpos.x, 1f, barrelpos.z);
					Instantiate(barrel, barrelpos2, Quaternion.identity);
				}
					

			}
			remainingEnemies += enemiesToSpawn;
		}
		waveCompleted = false;
		healthKitManager.newWave ();

	}
		
}
