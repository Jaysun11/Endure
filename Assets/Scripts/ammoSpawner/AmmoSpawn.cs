using UnityEngine;
using System.Collections;

public class AmmoSpawn : MonoBehaviour {

	public int numKits;

	private float spawnRadius = 10;

	private Vector3 spawnPos;

	public GameObject ammoKit;


	public void SpawnObjects() {

		for (int i = 0; i < numKits; i++) {
			spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
			Vector3 adjustedPos = new Vector3(spawnPos.x, 0.3f, spawnPos.z);

			Instantiate(ammoKit, adjustedPos, Quaternion.identity);


		}

	}




}
