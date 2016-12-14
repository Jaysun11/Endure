using UnityEngine;
using System.Collections;

public class HealthKitManager : MonoBehaviour {

	public  GameObject[] spawners;

	private GameObject[] kits;

	public void newWave() {
		kits = GameObject.FindGameObjectsWithTag ("HEALTH");
		foreach (GameObject pack in kits) {
			DestroyObject (pack);
		}

		for (int i = 0; i < spawners.Length; i++) {

			spawners [i].GetComponent<healthSpawner> ().SpawnObjects ();

		}

	}
		
}
