using UnityEngine;
using System.Collections;

public class RotatingEnemyController : MonoBehaviour {

	public float rotationSpeed;
	public int hitDamage;
	public GameObject explosion;
	public int life;

	// Update is called once per frame
	void FixedUpdate () {
		this.transform.Rotate(this.transform.forward * rotationSpeed * Time.deltaTime);

	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Bullet")) {
			life -= other.GetComponent<BulletManager>().damages;
			Instantiate (other.GetComponent<BulletManager>().impactParticles, other.transform.position, transform.rotation);
			Destroy (other.gameObject);
			if (life <= 0) {
				Instantiate (explosion, transform.position, transform.rotation);
				Destroy (gameObject);
			}
		} else if (other.CompareTag("Mine")) {
			other.GetComponent<StandardBullet>().ExplodeMine();
		}
	}
}
