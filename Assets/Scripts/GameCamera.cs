using UnityEngine;

public class GameCamera : MonoBehaviour {
	public Transform target;

	private Vector3 cameraTarget;

	// Use this for initialization
	void Start() {
		cameraTarget = new Vector3 (target.position.x, transform.position.y, target.position.z - 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		cameraTarget = new Vector3 (target.position.x, transform.position.y, target.position.z - 1.5f);
		transform.position = Vector3.Lerp (transform.position, cameraTarget, Time.deltaTime * 8);
	}
}
