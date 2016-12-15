using UnityEngine;
using System.Collections;

public class Barrel : Entity {

	public float damageDealt;

	public Detonator det;

	public override void takeDamage (float dmg) {

		base.takeDamage (dmg);

	}

	void AreaDamage(Vector3 location, float radius, float damage) {
		Collider[] objectsInRange = Physics.OverlapSphere (location, radius);

		foreach (Collider col in objectsInRange) {

			Enemy enemy = col.GetComponent<Enemy> ();

			if (enemy != null) {

				float proximity = (location - enemy.transform.position).magnitude;
				float effect = 1 - (proximity / radius);

				enemy.takeDamage (damage * effect);
			}
				

		}
	}


	public override void Die () {
		Vector3 spawnPos = gameObject.transform.position;
		Instantiate(det, spawnPos, Quaternion.identity);
		AreaDamage (spawnPos, 20, damageDealt);
		base.Die ();

	}

}
