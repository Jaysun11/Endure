using UnityEngine;
using System.Collections;


[RequireComponent (typeof (AudioSource))]

public class Gun : MonoBehaviour {

	public int gunOn;
	public float spread = 6f;

	public LayerMask collisionMask;
	public LayerMask collisionMask2;
	public LayerMask collisionMask3;

	public Transform spawn;
	private LineRenderer tracer;
	public Transform shellEjectionPoint;

	public float gunID;
	public enum GunType {Semi, Burst, Auto};
	public GunType gunType;
	public Rigidbody shell;

	private float damage = 5;

	public int damageMultipler;


	public float fireRate;

	private float nextPossibleShot;
	private float secondsBetween;


	void Start() {
		damageMultipler = PlayerPrefs.GetInt("damage");
		damage += (damageMultipler * 0.3f);
		spread -= (0.05f * PlayerPrefs.GetInt("accuracy"));

		secondsBetween = 60 / fireRate;

	

		if (GetComponent<LineRenderer> ()) {
			tracer = GetComponent<LineRenderer> ();
		}

	}

	public void Shoot() {

		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<playerController> ().damageOn) {
			secondsBetween = (60 / fireRate)/2;
			gameObject.GetComponent<AudioSource> ().volume = 0.25f;
		} else {
			gameObject.GetComponent<AudioSource> ().volume = 0.05f;
			secondsBetween = 60 / fireRate;
		}


		if (PlayerPrefs.GetInt ("GunOn") == 1) {
			gameObject.GetComponent<AudioSource> ().mute = false;
		} else if (PlayerPrefs.GetInt ("GunOn") == 0) {
			gameObject.GetComponent<AudioSource> ().mute = true;
		}

		if (canShoot ()) {

			//SPREAD 
			float neg = 1;
			if (Random.Range(0,10) <= 5) {
				neg = -1;
			}

			Ray ray = new Ray (spawn.position, Quaternion.Euler (0, Random.Range(0.1f,1) * spread * neg, 0) * spawn.forward);

			//END SPREAD
			RaycastHit hit;

			float shotDistance = 40;

			if (Physics.Raycast (ray, out hit, shotDistance, collisionMask)) {
				shotDistance = hit.distance;

				if (hit.collider.GetComponent<Entity> ()) {
					hit.collider.GetComponent<Entity> ().takeDamage (damage);
				}
			} else if (Physics.Raycast (ray, out hit, shotDistance, collisionMask2)) {
				shotDistance = hit.distance;
			} else if (Physics.Raycast (ray, out hit, shotDistance, collisionMask3)) {
				shotDistance = hit.distance;
				if (hit.collider.GetComponent<Entity> ()) {

					hit.collider.GetComponent<Entity> ().takeDamage (damage);
				}
			}
				
			nextPossibleShot = Time.time + secondsBetween;

			GetComponent<AudioSource>().Play ();

			if (tracer) {
				StartCoroutine ("RenderTracer", ray.direction*shotDistance);
			}


			Rigidbody newShell = Instantiate (shell, shellEjectionPoint.position, Quaternion.identity) as Rigidbody;
		
			newShell.AddForce (shellEjectionPoint.forward * Random.Range (150f, 200f) + spawn.forward * Random.Range (-10f, 10f));

		}
	}


	public bool canShoot() {
		bool canShoot = true;

		if (Time.time < nextPossibleShot) {
			canShoot = false;
		}
			
		return canShoot;
	
	}
		
	public void shootAuto() {

		if (gunType == GunType.Auto) {
			Shoot ();

		}

	}

	public void shootBurst() {

	}

	public void shootSingle() {

	}

	IEnumerator RenderTracer(Vector3 hitPoint) {
		tracer.enabled = true;
		tracer.SetPosition (0, spawn.position);
		tracer.SetPosition (1, spawn.position + hitPoint);
		yield return null;
		tracer.enabled = false;
	}


}
