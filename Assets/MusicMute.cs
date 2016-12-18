using UnityEngine;
using System.Collections;

public class MusicMute : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {

		if (PlayerPrefs.GetInt ("MusicOn") == 1) {
			gameObject.GetComponent<AudioSource> ().mute = false;
		} else if (PlayerPrefs.GetInt ("MusicOn") == 0) {
			gameObject.GetComponent<AudioSource> ().mute = true;
		}

	}
}
