using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceGameObject : MonoBehaviour {


	public GameObject targetGameObject;
	public float damping;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var lookPos = targetGameObject.transform.position - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping); 
	}
}
