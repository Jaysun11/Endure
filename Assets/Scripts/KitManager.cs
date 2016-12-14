using UnityEngine;
using System.Collections;

public class KitManager : MonoBehaviour {

	public  GameObject[] spawners;

	private GameObject[] kits;

	public void newWave() {


		kits = GameObject.FindGameObjectsWithTag ("AMMO");
		foreach (GameObject pack in kits) {
			DestroyObject (pack);
		}


		for (int i = 0; i < spawners.Length; i++) {
				spawners [i].GetComponent<AmmoSpawn> ().SpawnObjects (); 
		}

	}
		
}
