using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceGameObject : MonoBehaviour {

	public bool requiresLineOfSight = false;
	public float maxTargetAquisitionRange = 1000.0f;
	public GameObject targetGameObject;
	public float damping;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (targetGameObject) {
			
			if (requiresLineOfSight) {
				Vector3 direction = targetGameObject.transform.position - transform.position;
				RaycastHit hit;	
				if (Physics.Raycast (transform.position, direction, out hit, maxTargetAquisitionRange)) {
					if (hit.transform == targetGameObject.transform) {
						Turn ();
					}
				}
			} else {
				Turn ();
			}
		}
	}

	void Turn () {
		
		var lookPos = targetGameObject.transform.position - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping); 
	}


}
