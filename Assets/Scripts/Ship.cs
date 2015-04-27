using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	//Configurable adjustments for smooth movement
	public float stabilityCoeficient 	= 0.3f;
	public float stabilitySpeed 		= 5.0f;
	public float followSensitivity 		= 5f;
	public float rotationSensibility 	= 3f;

	//Position in space to follow and force applied to reach it
	public Transform target;
	private Vector3 force;

	// Update is called once per frame
	void FixedUpdate () {
		
		//Move and rotate object to target place
	    force = target.position - transform.position;
		GetComponent<Rigidbody>().AddForceAtPosition (force * followSensitivity, transform.position);

		//Always try to keep it vertical by adjusting torque around Z
		Vector3 predictedUp  = Quaternion.AngleAxis(GetComponent<Rigidbody>().angularVelocity.magnitude * Mathf.Rad2Deg * stabilityCoeficient / stabilitySpeed, GetComponent<Rigidbody>().angularVelocity) * transform.up;
		Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
		GetComponent<Rigidbody>().AddTorque(torqueVector * stabilitySpeed * stabilitySpeed);
	}
}