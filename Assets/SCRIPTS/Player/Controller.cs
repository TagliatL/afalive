using UnityEngine;
using System.Collections;
using InControl;

public class Controller : MonoBehaviour {

	Rigidbody rb;
	float x;
	float y;
	public int force;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		x = -inputDevice.LeftStickX;
		y = inputDevice.LeftStickY;

		if ((x != 0.0f || y != 0.0f) && (x > 0.3f || x < -0.3f || y > 0.3f || y < -0.3f)) {
			float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(90.0f - angle, Vector3.forward);
		}

		if (inputDevice.Action1.WasPressed) {
			rb.AddForce(transform.up * -force);
		}
	}
}
