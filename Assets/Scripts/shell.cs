using UnityEngine;
using System.Collections;

public class shell : MonoBehaviour {

	private float lifeTime = 5;
	private float deathTime = 5;

	private Material mat;
	private Color originalCol;

	private bool fading;

	private float fadePercentage;
	// Use this for initialization
	void Start () {
		deathTime = Time.time + lifeTime;
		mat = gameObject.GetComponent<Renderer> ().material;
		originalCol = mat.color;

		StartCoroutine ("FadeShell");
	
	}

	IEnumerator FadeShell() {
		while (true) {
			yield return new WaitForSeconds (.2f);
			if (fading) {
				fadePercentage += Time.deltaTime;
				mat.color = Color.Lerp (originalCol, Color.clear, fadePercentage);

				if (fadePercentage >= 1) {
					Destroy (gameObject);
				}

			} else {
				if (Time.time > deathTime) {
					fading = true;
				}
			}
		}
			
	}

	void OnTriggerEnter(Collider c) {
		if (c.tag == "Ground") {
			GetComponent<Rigidbody> ().isKinematic = false;
		}
	}

}
