using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	//Configurable adjustments for smooth movement
	public float stabilityCoeficient 	= 2.0f;
	public float stabilitySpeed 		= 5.0f;
	public float followSensitivity 		= 15f;
	public float rotationSensibility 	= 1f;

	//Position in space to follow and force applied to reach it
	public Transform target;
	private Vector3 force;

	// Update is called once per frame
	void FixedUpdate () {
		
		//Move and rotate object to target place
	    force = target.position - transform.position;
		GetComponent<Rigidbody>().AddForceAtPosition (force * followSensitivity, transform.position);
		GetComponent<Rigidbody>().AddRelativeTorque(-Vector3.forward*force.x);
		//Debug.Log(force)

		//Always try to keep it vertical by adjusting torque around Z
		Vector3 predictedUp  = Quaternion.AngleAxis(GetComponent<Rigidbody>().angularVelocity.magnitude * Mathf.Rad2Deg * stabilityCoeficient / stabilitySpeed, GetComponent<Rigidbody>().angularVelocity) * transform.up;
		Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
		GetComponent<Rigidbody>().AddTorque(torqueVector * stabilitySpeed * stabilitySpeed);
	}

	void OnTriggerEnter(Collider other){
		Destroy(gameObject);
	}
}