using UnityEngine;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour {

	public Transform healthBar;

	public void setHealthText(float percentHealth) {

		healthBar.localScale = new Vector3 (percentHealth, 1, 1);

	}
}
