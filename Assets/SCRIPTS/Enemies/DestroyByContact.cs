using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public int life;
	public float dropChance;
	public GameObject[] powerUps;
	public GameObject explosion;
	SpriteRenderer renderer;
	int fullLife;
	float speed;

	void Start() {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		fullLife = life;
		speed = 2;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bullet") {
			life -= other.GetComponent<BulletManager>().damages;
			Instantiate (other.GetComponent<BulletManager>().impactParticles, other.transform.position, transform.rotation);
			Destroy (other.gameObject);
			if(life <= 0) {
				Instantiate (explosion, transform.position, transform.rotation);

				if (Random.Range(0, 100) < dropChance) {
					Instantiate(powerUps[Random.Range(0, powerUps.Length)], transform.position, transform.rotation);
				}
				Destroy (gameObject);
			}
		} else if (other.tag == "Mine") {
			other.GetComponent<StandardBullet>().ExplodeMine();
			if(life <= 0) {
				Instantiate (explosion, transform.position, transform.rotation);
				
				if (Random.Range(0, 100) < dropChance) {
					Instantiate(powerUps[Random.Range(0, powerUps.Length)], transform.position, transform.rotation);
				}
				Destroy (gameObject);
			}
		}
	}

	void Update() {
		float colorMultiplier = (float)life/(float)fullLife;
		renderer.color = Color.Lerp(renderer.color, new Color(colorMultiplier, colorMultiplier, colorMultiplier), speed * Time.deltaTime);
	}

	public void Explode() {
		Instantiate (explosion, transform.position, transform.rotation);
				Destroy (gameObject);
	}
}
