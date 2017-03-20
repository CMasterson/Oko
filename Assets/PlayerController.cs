using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 6f;            // The speed that the player will move at.

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.

	float attackTime;

	List<Rigidbody> meleeTargets;

	public GameObject projectileSpawnPoint;
	public GameObject projectilePrefab;

	public float rangedAttackDelay = 1.0f;
	public float meleeAttackDelay = 1.0f;
	public float meleeKnockback = 500.0f;

	public float projectileSpeed = 30.0f;



	void Awake () {
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");

		playerRigidbody = GetComponent <Rigidbody> ();

		attackTime = Time.time;

		meleeTargets = new List<Rigidbody> ();
	}
	
	void FixedUpdate ()	
	{
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Move the player around the scene.
		Move (h, v);
		Turning ();
		Attack ();
	}



	void Move (float h, float v)
	{

		float moveSpeed = speed;

		if (Input.GetKey (KeyCode.LeftShift)) {
			moveSpeed = speed * 2;
		}
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);

		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * moveSpeed * Time.deltaTime;

		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotation);
		}
	}
	void Attack () {

		Debug.Log (meleeTargets.Count);
		if (Time.time >= attackTime) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				foreach (Rigidbody rb in meleeTargets) {

					Transform tf = rb.GetComponent<Transform> ();

					Vector3 force = tf.position - transform.position;
					force.Normalize ();

					force *= meleeKnockback;

					rb.AddForce (force);
				}

				attackTime = Time.time + meleeAttackDelay;
			}
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				GameObject projectile = Instantiate (projectilePrefab);
				projectile.transform.position = transform.Find("ProjectileEmitter").transform.position;
				projectile.GetComponent<Rigidbody> ().velocity = transform.forward * projectileSpeed;

				attackTime = Time.time + rangedAttackDelay;
			}
		}
	}

	void OnTriggerEnter(Collider other) {

		Rigidbody rb = other.GetComponent<Rigidbody> ();
		if (other.tag == "Enemy") {
			if (!meleeTargets.Contains (rb)) {
				meleeTargets.Add (rb);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		Rigidbody rb = other.GetComponent<Rigidbody> ();

		if (meleeTargets.Contains (rb)) {
			meleeTargets.Remove (rb);
		}
	}
}
