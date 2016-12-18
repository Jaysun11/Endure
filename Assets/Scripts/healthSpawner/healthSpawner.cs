using UnityEngine;
using System.Collections;

public class healthSpawner : MonoBehaviour {

	public int numKits;

	private float spawnRadius =  70f;

	private Vector3 spawnPos;

	public GameObject healthPack;

	public GameObject damBuff;

	public GameObject speedBuff;

	public void SpawnObjects() {

		for (int i = 0; i < numKits; i++) {
			spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
			Vector3 adjustedPos = new Vector3(spawnPos.x, 1f, spawnPos.z);

			Instantiate(healthPack, adjustedPos, Quaternion.identity);

			if (Random.Range (0, 10) < 1) {
				spawnPos = transform.position + Random.insideUnitSphere * spawnRadius*3;
				Vector3 adjustedPos2 = new Vector3(spawnPos.x, 1f, spawnPos.z);
				if (Random.Range (0, 10) < 5) {
					Instantiate (damBuff, adjustedPos2, Quaternion.identity);
				} else {
					Instantiate (speedBuff, adjustedPos2, Quaternion.identity);
				}
			}


		}

	}




}
