using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public int life;
	public GameObject explosion;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bullet") {
			life -= other.GetComponent<BulletManager>().damages;
			Instantiate (other.GetComponent<BulletManager>().impactParticles, other.transform.position, transform.rotation);
			Destroy (other.gameObject);
			if(life <= 0) {
				Instantiate (explosion, transform.position, transform.rotation);
				Destroy (gameObject);
			}
		}
	}
}
