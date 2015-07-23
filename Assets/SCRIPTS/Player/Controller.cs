using UnityEngine;
using System.Collections;
using InControl;

public class Controller : MonoBehaviour {

	Rigidbody rb;
	float x;
	float y;
	float counter;
	public GameObject equippedBullet;
	public GameObject fireParticles;
	GameObject instanciatedParticles;
	GameObject instanciatedBullet;

	void Start () {
		rb = GetComponent<Rigidbody>();
		counter = 0f;
	}

	void FixedUpdate() {
		BulletManager bulletValues = equippedBullet.GetComponent<BulletManager> ();
		rb.angularVelocity = Vector3.zero;
		var inputDevice = InputManager.ActiveDevice;
		x = -inputDevice.LeftStickX;
		y = inputDevice.LeftStickY;
		
		if ((x != 0.0f || y != 0.0f) && (x > 0.3f || x < -0.3f || y > 0.3f || y < -0.3f)) {
			float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(90.0f - angle, Vector3.forward);
		}

		if (inputDevice.Action1.IsPressed && (counter <= 0f)) {
			counter = bulletValues.rateOfFire * 100;
			rb.AddForce(transform.up * -bulletValues.force);
			instanciatedBullet = Instantiate (equippedBullet, transform.position, transform.rotation) as GameObject;
			instanciatedParticles = Instantiate(fireParticles, transform.position, transform.rotation) as GameObject;
		}
		counter--;
	}

	void CancelCamEffect() {
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AnalogGlitch>().colorDrift = 0;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AnalogGlitch> ().horizontalShake = 0;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PowerUp") {
			equippedBullet = other.GetComponent<PowerUp>().pickUpBullet;
			other.GetComponent<PowerUp>().Used();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AnalogGlitch>().colorDrift = 0.4f;
			Invoke("CancelCamEffect",0.35f);
		}
	}
}
