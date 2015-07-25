using UnityEngine;
using System.Collections;
using InControl;

public class Controller : MonoBehaviour {

	Rigidbody rb;
	float x;
	float y;
	float counter;
	bool isDead;
	bool uppgrading;
	public int life;
	public GameObject explosion;
	public GameObject equippedBullet;
	public GameObject oldEquippedBullet;
	public GameObject usedBullet;
	public GameObject[] uppgradeWeapons;
	public SpriteRenderer ship;
	GameObject tempoBullet;
	public GameObject fireParticles;
	GameObject instanciatedParticles;
	GameObject instanciatedBullet;
	BulletManager bulletValues;


	void Start () {
		bulletValues= usedBullet.GetComponent<BulletManager> ();
		rb = GetComponent<Rigidbody>();
		counter = 0f;
	}

	void Update() {
		float colorMultiplier = (float)life/(float)6.0f;
		ship.color = Color.Lerp(ship.color, new Color(colorMultiplier, colorMultiplier, colorMultiplier), 2 * Time.deltaTime);
	}

	void FixedUpdate() {
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
			instanciatedBullet = Instantiate (usedBullet, transform.position, transform.rotation) as GameObject;
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

	void Equip(Collider other) {

		if (equippedBullet != null && oldEquippedBullet == null) {
			oldEquippedBullet = equippedBullet;
			equippedBullet = other.GetComponent<PowerUp> ().pickUpBullet;
			usedBullet = equippedBullet;
			uppgrading = false;
		} else {
			if(other.GetComponent<PowerUp> ().pickUpBullet.name == equippedBullet.name 
			 || other.GetComponent<PowerUp> ().pickUpBullet.name == oldEquippedBullet.name) {
				uppgrading = false;
			}
			else {
				oldEquippedBullet = equippedBullet;
				equippedBullet = other.GetComponent<PowerUp> ().pickUpBullet;
				uppgrading = false;
			}

			if(equippedBullet.name == oldEquippedBullet.name) {
				usedBullet = equippedBullet;
				uppgrading = false;
			}
			if ((equippedBullet.name == "Shotgun" && oldEquippedBullet.name == "Mines") 
			    || (equippedBullet.name == "Mines" && oldEquippedBullet.name == "Shotgun")) {
				usedBullet = uppgradeWeapons[1];
				uppgrading = false;
			} else if ((equippedBullet.name == "Shotgun" && oldEquippedBullet.name == "Gatling") 
			           || (equippedBullet.name == "Gatling" && oldEquippedBullet.name == "Shotgun")) {
				usedBullet = uppgradeWeapons[2];
				uppgrading = false;
			} else if ((equippedBullet.name == "Mines" && oldEquippedBullet.name == "Gatling") 
			           || (equippedBullet.name == "Gatling" && oldEquippedBullet.name == "Mines")) {
				usedBullet = uppgradeWeapons[0];
				uppgrading = false;
			}
		}
		bulletValues= usedBullet.GetComponent<BulletManager> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PowerUp" && !uppgrading) {
			uppgrading = true;
			other.GetComponent<PowerUp>().Used();
			Equip(other);
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
