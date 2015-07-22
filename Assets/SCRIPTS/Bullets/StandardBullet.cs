using UnityEngine;
using System.Collections;

public class StandardBullet : BulletManager {

	void OnEnable() {
		Object.Destroy(this.gameObject, this.lifeTime);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed *10);
	}
}
