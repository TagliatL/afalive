using UnityEngine;
using System.Collections;

public class StandardBullet : BulletManager {

	public float mineTimeout;
	public float areaOfEffect;
	public GameObject explosionMine;

	void OnEnable() {
		if (this.tag == "Bullet") {
			Object.Destroy(this.gameObject, this.lifeTime);
		} else if (this.tag == "Mine") {
			StartCoroutine("MineTimer");
		}
	}

	public void ExplodeMine() {
		Object.Destroy(this.gameObject);
		if (explosionMine != null) {
			Instantiate (explosionMine, transform.position, Quaternion.identity);
		}
		Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, areaOfEffect);
		foreach (Collider hitCollider in hitColliders) {
			if (hitCollider.CompareTag("Enemy") || hitCollider.CompareTag("Core")) {
				hitCollider.gameObject.GetComponent<DestroyByContact>().life -= this.damages;
			}
		}
	}

	IEnumerator MineTimer() {
		yield return new WaitForSeconds(mineTimeout);
		ExplodeMine();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed *10);
	}
}
