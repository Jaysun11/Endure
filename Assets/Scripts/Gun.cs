using UnityEngine;
using System.Collections;


[RequireComponent (typeof (AudioSource))]

public class Gun : MonoBehaviour {

	private bool reloading;

	[HideInInspector]
	public GameGUI gui;

	public int totalAmmo =200;

	public int ammoPerMag= 10;

	public LayerMask collisionMask;
	public LayerMask collisionMask2;


	public Transform spawn;
	private LineRenderer tracer;
	public Transform shellEjectionPoint;

	public float gunID;
	public enum GunType {Semi, Burst, Auto};
	public GunType gunType;
	public Rigidbody shell;

	public float damage = 5;

	public float fireRate;

	private float nextPossibleShot;
	private float secondsBetween;
	private int currentAmmoinMag;

	void Start() {
		secondsBetween = 60 / fireRate;
		if (GetComponent<LineRenderer> ()) {
			tracer = GetComponent<LineRenderer> ();
		}
		currentAmmoinMag = ammoPerMag;
		if (gui) {
			gui.SetAmmoInfo (totalAmmo, currentAmmoinMag);
		}
	}

	public void Shoot() {

		if (canShoot ()) {
			float neg = 1;
			if (Random.Range(0,10) <= 5) {
				neg = -1;
			}

			Ray ray = new Ray (spawn.position, Quaternion.Euler (0, Random.Range(0.1f,1) * 5 * neg, 0) * spawn.forward);
			RaycastHit hit;

			float shotDistance = 40;

			if (Physics.Raycast (ray, out hit, shotDistance, collisionMask)) {
				shotDistance = hit.distance;

				if (hit.collider.GetComponent<Entity> ()) {

					hit.collider.GetComponent<Entity> ().takeDamage (damage);
				}
			} else if (Physics.Raycast (ray, out hit, shotDistance, collisionMask2)) {
				shotDistance = hit.distance;
			}
				
			nextPossibleShot = Time.time + secondsBetween;
			currentAmmoinMag--;

			if (gui) {
				gui.SetAmmoInfo (totalAmmo, currentAmmoinMag);
			}

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

		if (currentAmmoinMag == 0) {
			canShoot = false;
		}

		if (reloading) {
			canShoot = false;
		}

		return canShoot;
	
	}

	public bool Reload() {
		if (totalAmmo != 0 && currentAmmoinMag != ammoPerMag) {
			reloading = true;
			return true;
		}
		return false;
	}

	public void finishReload() {
		reloading = false;

		totalAmmo -= (ammoPerMag-currentAmmoinMag);
		currentAmmoinMag = ammoPerMag;
		if (totalAmmo < 0) {
			currentAmmoinMag += totalAmmo;
			totalAmmo = 0;
		}

		if (gui) {
			gui.SetAmmoInfo (totalAmmo, currentAmmoinMag);
		}
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


	public int getCurrentAmmo() {
		return currentAmmoinMag;
	}
		

}
