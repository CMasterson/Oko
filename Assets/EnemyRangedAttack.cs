using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour {


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
	void Update () {
		if (Time.time >= attackTime) {
			Attack ();
		}
	}

	void Attack () {

		GameObject projectile = Instantiate (projectilePrefab);

		projectile.transform.position = transform.Find("ProjectileEmitter").transform.position;
		projectile.GetComponent<Rigidbody> ().velocity = transform.forward * projectileSpeed;
		projectile.GetComponent<ProjectileScript> ().damage = rangedAttackDamage;

		attackTime = Time.time + attackDelay;

	}
}
