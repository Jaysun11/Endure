using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
public class FPSCONTROL : MonoBehaviour {

	public Transform handHold;

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
	private bool shootable;

	public Gun[] guns;
	private Gun currentGun;

	private GameGUI gui;

	public Canvas PauseMenu;


	private float maxRun = 150;
	// Use this for initialization
	void Start () {

		PauseMenu = PauseMenu.GetComponent<Canvas> ();
		PauseMenu.enabled = false;

		currentRun = maxRun;
        
		gui = GameObject.FindGameObjectWithTag ("GUI").GetComponent<GameGUI> ();
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
			gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			PauseMenu.enabled = true;
			Time.timeScale = 0.0f;
		} else if (Time.timeScale == 1.0f) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
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

		if (Input.GetButton("run") && currentRun > 0 && depleted == false) {
			currentRun--;
			gui.setSprint (currentRun / maxRun);
			if (currentRun <= 0) {
				depleted = true;
			}
		} else { 
			if (currentRun < maxRun) {
				currentRun+=0.25f;
			} if (currentRun == maxRun) {
				depleted = false;
			}
			gui.setSprint (currentRun / maxRun);
		}

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
			DestroyObject (hit.gameObject);
			speedOn = true;
			speedStarts = Time.time;
			speedDuration = 10;

		}
	}
		
}
