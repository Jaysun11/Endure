using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour {

	private Vector3 spawnPos;

	private int spawnRadius = 5;

	public GameObject enemyPrefab;

	private int startWave = 1;

	private int enemiesToSpawn = 1;

	private int currentWave;

	public Spawner[] spawners;

	public HealthKitManager healthKitManager;
	public KitManager ammoKitManager;

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
			enemiesToSpawn =  currentWave/2 + (int) (Mathf.Pow (currentWave/2, 1.01f));
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
		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < enemiesToSpawn; j++) {

				spawnPos = spawners[i].gameObject.transform.position + Random.insideUnitSphere * spawnRadius;
				Vector3 adjustedPos = new Vector3(spawnPos.x, 2.75f, spawnPos.z);

				Instantiate(enemyPrefab, adjustedPos, Quaternion.identity);

			}
			remainingEnemies += enemiesToSpawn;
		}
		waveCompleted = false;
		ammoKitManager.newWave ();
		healthKitManager.newWave ();

	}
		
}
