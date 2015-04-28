using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody>().velocity = Vector3.forward*120f;
	}
}
