using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UnlocksScript : MonoBehaviour {

	private int damageLevel;
	public Text damageText;

	private int accuracyLevel;
	public Text accuracyText;

	private int speedLevel;
	public Text speedText;

	public Text pointsToSpendText;
	private int pointsToSpend;

	public Transform DamageBar;

	public Transform AccuracyBar;

	public Transform SpeedBar;

	// Use this for initialization
	void Start () {

		/*Reset
		PlayerPrefs.SetInt ("LEVEL", 1);
		PlayerPrefs.SetInt ("pointsToSpend", 0);
		float experienceToLevel = 1 * 50 + Mathf.Pow (1 * 2, 2);
		PlayerPrefs.SetFloat ("EXP TO LEVEL", experienceToLevel);
		*/

		damageLevel = PlayerPrefs.GetInt ("damage");
		accuracyLevel = PlayerPrefs.GetInt ("accuracy");
		speedLevel = PlayerPrefs.GetInt ("speed");
		pointsToSpend = PlayerPrefs.GetInt ("pointsToSpend");

	}
	
	// Update is called once per frame
	void Update () {

		float damageProgress = damageLevel / 100f;
		float accuracyProgress = accuracyLevel / 100f;
		float speedProgress = speedLevel / 100f;

		DamageBar.localScale = new Vector3 (damageProgress, 0.124f, 1);
		AccuracyBar.localScale = new Vector3 (accuracyProgress, 0.124f, 1);
		SpeedBar.localScale = new Vector3 (speedProgress, 0.124f, 1);

		damageLevel = PlayerPrefs.GetInt ("damage");
		accuracyLevel = PlayerPrefs.GetInt ("accuracy");
		speedLevel = PlayerPrefs.GetInt ("speed");
		pointsToSpend = PlayerPrefs.GetInt ("pointsToSpend");

		pointsToSpendText.text = pointsToSpend + "";
		damageText.text = damageLevel + "";
		accuracyText.text = accuracyLevel + "";
		speedText.text = speedLevel + "";
	
	}

	public void UpDamage() {
		if (pointsToSpend >= 1 && damageLevel <100) {
			damageLevel++;
			PlayerPrefs.SetInt ("damage", damageLevel);
			pointsToSpend--;
			PlayerPrefs.SetInt ("pointsToSpend", pointsToSpend);
		}
	}

	public void UpAccuracy() {
		if (pointsToSpend >= 1 && accuracyLevel <100) {
			accuracyLevel++;
			PlayerPrefs.SetInt ("accuracy", accuracyLevel);
			pointsToSpend--;
			PlayerPrefs.SetInt ("pointsToSpend", pointsToSpend);
		}
	}

	public void UpSpeed() {
		if (pointsToSpend >= 1 && speedLevel <100) {
			speedLevel++;
			PlayerPrefs.SetInt ("speed", speedLevel);
			pointsToSpend--;
			PlayerPrefs.SetInt ("pointsToSpend", pointsToSpend);
		}
	}
}
