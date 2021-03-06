﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
[RequireComponent (typeof (CharacterController))]
public class playerController : MonoBehaviour {

	public Transform handHold;

	private Vector3 prevDir;
	private float rotationSpeed = 950;
	private float walkSpeed = 10;
	private float runSpeed = 12;
	public float maxRun;
	private bool depleted = false;
	private float currentRun;
	private float acceleration = 5;

	//BUFFS
	public bool damageOn = false;
	private int damageDuration;
	private float damageStarts;
	private bool speedOn = false;
	private int speedDuration;
	private float speedStarts;
	public bool shieldOn = false;
	private int shieldDuration;
	private float shieldStarts;

	private Vector3 velocityMod;

	//System Variables
	private Quaternion targetRotation;

	//Components
    private CharacterController controller;
	public Camera cam;
	private bool shootable;

	public Gun[] guns;
	private Gun currentGun;

	private GameGUI gui;

	public Canvas PauseMenu;



	// Use this for initialization
	void Start () {

		walkSpeed += (PlayerPrefs.GetInt("speed") * 0.05f);
		runSpeed += (PlayerPrefs.GetInt("speed") * 0.05f);


		PauseMenu = PauseMenu.GetComponent<Canvas> ();
		PauseMenu.enabled = false;


		currentRun = maxRun;
        controller = GetComponent<CharacterController>();
		gui = GameObject.FindGameObjectWithTag ("GUI").GetComponent<GameGUI> ();
		cam = Camera.main;
		gui.setHighScoreText (PlayerPrefs.GetInt("High Score"));
		EquipGun (0);
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - speedStarts >= speedDuration) {
			speedOn = false;
		}
		if (Time.time - shieldStarts >= shieldDuration) {
			shieldOn = false;
		}
		if (Time.time - damageStarts >= damageDuration) {
			damageOn = false;
		}


		if (speedOn) {
			walkSpeed = 18;
			runSpeed = 20;
		} else if (!speedOn) {
			walkSpeed = 10 +(PlayerPrefs.GetInt("speed") * 0.05f);
			runSpeed = 12 + (PlayerPrefs.GetInt("speed") * 0.05f);
		}

		shootable = false;

		controlMouse ();


		if (currentGun) {
			if (Input.GetButtonDown ("Shoot") && shootable) {
				currentGun.Shoot ();	

			} else if (Input.GetButton ("Shoot")  && shootable) {
				currentGun.shootAuto ();
			
			}

		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			PauseMenu.enabled = true;
			Time.timeScale = 0.0f;
		} else if (Time.timeScale == 1.0f) {

			PauseMenu.enabled = false;
		}
			
	}

	void EquipGun(int i) {
		if (currentGun) {
			Destroy (currentGun.gameObject);
		}
		currentGun = Instantiate (guns [i], handHold.position, handHold.rotation) as Gun;
		currentGun.transform.parent = handHold;

	}

	void controlMouse() {
		shootable = true;

		Vector3 mousePosition = Input.mousePosition;
		mousePosition = cam.ScreenToWorldPoint (new Vector3 (mousePosition.x, mousePosition.y, cam.transform.position.y - transform.position.y));
		targetRotation = Quaternion.LookRotation (mousePosition - new Vector3(transform.position.x, 0, transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);


		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		velocityMod = Vector3.MoveTowards (velocityMod, input, acceleration * Time.deltaTime);
		Vector3 motion = velocityMod;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)? .7f:1;
		if (Input.GetButton("run") && currentRun > 0 && depleted == false) {
			motion *= runSpeed;
			currentRun--;
			gui.setSprint (currentRun / maxRun);
			if (currentRun <= 0) {
				depleted = true;
			}
		} else { 
			motion *= walkSpeed;
			if (currentRun < maxRun) {
				currentRun+=0.25f;
			} if (currentRun == maxRun) {
				depleted = false;
			}
			gui.setSprint (currentRun / maxRun);
		}

		motion += Vector3.up * -8;

		controller.Move (motion * Time.deltaTime);

	}

	void controlMobile() {
		

		Vector3 shootDirection = Vector3.right * CrossPlatformInputManager.GetAxis ("Mouse X") + Vector3.forward * CrossPlatformInputManager.GetAxis ("Mouse Y");

		if (CrossPlatformInputManager.GetAxis ("Mouse X") != 0) {
			shootable = true;
		} else if (CrossPlatformInputManager.GetAxis ("Mouse Y") != 0) {
			shootable = true;
		}

		if (shootDirection.x == 0 && shootDirection.y == 0 && (prevDir.x != 0 || prevDir.y != 0)) {
			
		} else {
			transform.rotation = Quaternion.LookRotation (shootDirection, Vector3.up);
			prevDir = shootDirection;
		}

		Vector3 input = new Vector3 (CrossPlatformInputManager.GetAxisRaw ("Horizontal"), 0, CrossPlatformInputManager.GetAxisRaw ("Vertical"));
		velocityMod = Vector3.MoveTowards (velocityMod, input, acceleration * Time.deltaTime);
		Vector3 motion = velocityMod;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)? .7f:1;
		motion *= (Input.GetButton ("run")) ? runSpeed : walkSpeed;
		motion += Vector3.up * -8;

		controller.Move (motion * Time.deltaTime);



	}
		
	void OnControllerColliderHit (ControllerColliderHit hit){
		if (hit.gameObject.tag == "HEALTH" && (this.gameObject.GetComponent<Entity> ().health < this.gameObject.GetComponent<Entity> ().maxHealth)) {
			DestroyObject (hit.gameObject);
			this.gameObject.GetComponent<Entity> ().health += 20;
			if (this.gameObject.GetComponent<Entity> ().health > this.gameObject.GetComponent<Entity> ().maxHealth) {
				this.gameObject.GetComponent<Entity> ().health = this.gameObject.GetComponent<Entity> ().maxHealth;
			}
			gui.setHealthText (this.gameObject.GetComponent<Entity> ().health/ this.gameObject.GetComponent<Entity> ().maxHealth, this.gameObject.GetComponent<Entity> ().health);
		}

		if (hit.gameObject.tag == "DamageBuff") {
			DestroyObject (hit.gameObject);
			damageOn = true;
			damageStarts = Time.time;
			damageDuration = 10;
		}

		if (hit.gameObject.tag == "ShieldBuff") {
			DestroyObject (hit.gameObject);
			shieldOn = true;
			shieldStarts = Time.time;
			shieldDuration = 10;
		}

		if (hit.gameObject.tag == "SpeedBuff") {
			Debug.Log ("Here");
			DestroyObject (hit.gameObject);
			speedOn = true;
			speedStarts = Time.time;
			speedDuration = 10;

		}

	
	}
		
}
