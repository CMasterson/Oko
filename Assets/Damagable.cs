using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public float maxHealth;
	public float currentHealth;


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

		if (currentHealth <= 0) {
			Destroy (gameObject);
		}
		
	}

	public void TakeDamage (float damageToTake) {
		currentHealth -= damageToTake;
	}
}
