using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour {

	public GameObject target;
	public bool requiresLineOfSight = false;
	public float maxTargetAquisitionRange = 1000.0f;
	public float attackDelay;
	public GameObject projectilePrefab;
	public float rangedAttackDamage;
	public float projectileSpeed;

	float attackTime;

	// Use this for initialization
	void Start () {
		attackTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time >= attackTime) {
			Attack ();
		}
	}

	void Attack () {
		if (requiresLineOfSight && target) {
			Vector3 fwd = transform.TransformDirection (Vector3.forward);
			RaycastHit hit;	
			if (Physics.Raycast (transform.position, fwd, out hit, maxTargetAquisitionRange)) {
				if (hit.transform == target.transform) {
					LaunchProjectile ();
				}
			}
		} else {
			if (Time.time >= attackTime) {
				LaunchProjectile ();
			}
		}
	}

	void LaunchProjectile () {

		GameObject projectile = Instantiate (projectilePrefab);

		projectile.transform.position = transform.Find("ProjectileEmitter").transform.position;
		projectile.GetComponent<Rigidbody> ().velocity = transform.forward * projectileSpeed;
		projectile.GetComponent<ProjectileScript> ().damage = rangedAttackDamage;

		attackTime = Time.time + attackDelay;

	}
}
