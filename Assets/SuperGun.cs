using UnityEngine;
using System.Collections;

public class SuperGun : MonoBehaviour {

	void Update() {
		transform.Rotate(0, 50*Time.deltaTime, 0 );

	}
}
