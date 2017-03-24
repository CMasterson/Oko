using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	public float radius = 10.0F;
	public float power = 200.0F;
	public float damage = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {

		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {

			Damagable damageScript = hit.GetComponentInParent<Damagable> ();
			if (damageScript) {
				damageScript.TakeDamage (damage);
			}

			Rigidbody rb = hit.GetComponentInParent<Rigidbody>();
			if (rb != null && hit != gameObject) {
				rb.AddExplosionForce (power, explosionPos, radius, 3.0F);
			}

		}

		Destroy (gameObject);
	}
}
