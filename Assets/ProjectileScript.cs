using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	public float radius = 10.0F;
	public float power = 200.0F;

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
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null && hit != gameObject) {
				Debug.Log ("Pop");
				rb.AddExplosionForce (power, explosionPos, radius, 3.0F);
			}

		}

		Destroy (gameObject);
	}
}
