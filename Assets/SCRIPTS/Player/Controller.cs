using UnityEngine;
using System.Collections;
using InControl;

public class Controller : MonoBehaviour {

	Rigidbody rb;
	float x;
	float y;
	float counter;
	bool isDead;
	public int life;
	public GameObject explosion;
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

		if (inputDevice.Action1.IsPressed && (counter <= 0f) && !isDead) {
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
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AnalogGlitch>().horizontalShake = 0;
	}

	void BackToMenu() {
		Application.LoadLevel ("LevelSelection");
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PowerUp") {
			equippedBullet = other.GetComponent<PowerUp>().pickUpBullet;
			other.GetComponent<PowerUp>().Used();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AnalogGlitch>().colorDrift = 0.4f;
			Invoke("CancelCamEffect",0.35f);
		} else if (other.tag == "Enemy") {
			Destroy (other.gameObject);
			Instantiate (explosion, other.transform.position, other.transform.rotation);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AnalogGlitch>().horizontalShake = 0.1f;
			Invoke("CancelCamEffect",0.3f);

			life -= 1;
			if (life <= 0) {
				CancelCamEffect();
				Instantiate (explosion, transform.position, transform.rotation);
				GetComponentInChildren<SpriteRenderer>().enabled = false;
				isDead = true;
				Invoke("BackToMenu",1.5f);
			}
		}
	}
}
