using UnityEngine;

public class DiveHead : MonoBehaviour {

	public Transform parent;				//Parent object rotation
	private float zAxisAngleOffset = 270;	//Adjustment angle
	private Quaternion parentRotation;		//Parent rotation after adjustment angle fix

	float rotationY = 0;
	float rotationX = 180f;
	Quaternion rot;
	private Gyroscope gyro;

	/// <summary>
	/// Validate support for sensors
	/// </summary>
	void Start () {

		if (SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;
		} else {
			Debug.Log ("Gyroscope is not supported");
		}

		#if UNITY_IPHONE
			rotationX = 90f;
		#endif
		#if UNITY_ANDROID
			rotationX = 270f;
		#endif

		rot = Quaternion.AngleAxis (rotationX, Vector3.up) * Quaternion.AngleAxis (zAxisAngleOffset, Vector3.right);
	}

	/// <summary>
	/// Execute rotation of player head based on gyroscope input
	/// </summary>
	protected void Update() {

		//Move according to gyro if enabled
		if (gyro != null && gyro.enabled) {

			transform.rotation = ConvertRotation (rot * gyro.attitude);

		} else {
				
		    rotationX +=  Input.GetAxis("Mouse X")*4f;
			rotationY += Input.GetAxis("Mouse Y")*6f;
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
	}

	/// <summary>
	/// Convert the gyro rotation to something we can use
	/// </summary>
	/// <returns>The rotation.</returns>
	/// <param name="q">Q.</param>
	private static Quaternion ConvertRotation(Quaternion q) {

		return new Quaternion(q.x, q.y, -q.z, -q.w);	
	}

	void OnGUI () {
		string active;
		if (gyro != null && gyro.enabled) {
						active = "true";
				} else {
						active = "false";
				}
		GUI.Label (new Rect (10, 10, 400, 50), "Supported ? " + SystemInfo.supportsGyroscope.ToString ()+" Active ? "+active);
	}
}