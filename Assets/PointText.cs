using UnityEngine;
using System.Collections;

public class PointText : MonoBehaviour {

	private float lifeTime = 0.1f;
	private float deathTime = 0.1f;

	private Material mat;
	private Color originalCol;

	private bool fading;

	private float fadePercentage;
	// Use this for initialization
	void Start () {
		gameObject.transform.LookAt (Camera.main.transform);
		deathTime = Time.time + lifeTime;
		mat = gameObject.GetComponent<Renderer> ().material;
		originalCol = mat.color;

		StartCoroutine ("FadeText");

	}

	IEnumerator FadeText() {
		while (true) {
			yield return new WaitForSeconds (.05f);
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
}
