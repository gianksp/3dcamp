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
	private Rigidbody rididbody;

	/**
	 * Inisialise
	 */
	void Start () {
		rididbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		//Move and rotate object to target place
	    force = target.position - transform.position;
		rididbody.AddForceAtPosition (force * followSensitivity, transform.position);
		rididbody.AddRelativeTorque(-Vector3.forward*force.x);

		//Always try to keep it vertical by adjusting torque around Z
		Vector3 predictedUp  = Quaternion.AngleAxis(rididbody.angularVelocity.magnitude * Mathf.Rad2Deg * stabilityCoeficient / stabilitySpeed, rididbody.angularVelocity) * transform.up;
		Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
		rididbody.AddTorque(torqueVector * stabilitySpeed * stabilitySpeed);
	}

	void OnTriggerEnter(Collider other){
		//Destroy(gameObject);
	}
}