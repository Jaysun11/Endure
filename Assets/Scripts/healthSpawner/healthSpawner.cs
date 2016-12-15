using UnityEngine;
using System.Collections;

public class healthSpawner : MonoBehaviour {

	public int numKits;

	private float spawnRadius =  70f;

	private Vector3 spawnPos;

	public GameObject healthPack;


	public void SpawnObjects() {

		for (int i = 0; i < numKits; i++) {
			spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
			Vector3 adjustedPos = new Vector3(spawnPos.x, 1f, spawnPos.z);

			Instantiate(healthPack, adjustedPos, Quaternion.identity);


		}

	}




}
