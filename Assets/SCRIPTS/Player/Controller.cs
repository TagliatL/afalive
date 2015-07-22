using UnityEngine;
using System.Collections;
using InControl;

public class Controller : MonoBehaviour {

	Rigidbody rb;
	float x;
	float y;
	bool isContinue;
	public GameObject equippedBullet;
	GameObject instanciatedBullet;

	void Start () {
		rb = GetComponent<Rigidbody>();
		isContinue = false;
	}

	void ResetFire() {
		isContinue = false;
	}

	//coroutine de tir
	IEnumerator ContinuedFire(BulletManager bulletValues) {
		var inputDevice = InputManager.ActiveDevice;
		while (true) {
			if(inputDevice.Action1.IsPressed) {
				rb.AddForce(transform.up * -bulletValues.force);
				instanciatedBullet = Instantiate (equippedBullet, transform.position, transform.rotation) as GameObject;
			}
			//ceci permet en théorie de faire ce que je veux mais quand tu testes tu vois que des fois y'a trop de bullets qui partent
			Invoke("ResetFire",bulletValues.rateOfFire);
			yield return new WaitForSeconds(bulletValues.rateOfFire);
		}
	}
	
	// Update is called once per frame
	void Update () {
		BulletManager bulletValues = equippedBullet.GetComponent<BulletManager> ();
		rb.angularVelocity = Vector3.zero;
		var inputDevice = InputManager.ActiveDevice;
		x = -inputDevice.LeftStickX;
		y = inputDevice.LeftStickY;

		if ((x != 0.0f || y != 0.0f) && (x > 0.3f || x < -0.3f || y > 0.3f || y < -0.3f)) {
			float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(90.0f - angle, Vector3.forward);
		}
		//lance la coroutine si elle n'est pas déjà lancé
		if (inputDevice.Action1.WasPressed && !isContinue) {
			StartCoroutine(ContinuedFire(bulletValues));
			isContinue = true;
		}
		else if (inputDevice.Action1.WasReleased && !isContinue) {
			StopAllCoroutines();
		}
	}
}
