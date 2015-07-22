using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform target;
	public float speed;
	public float rotationSpeed;

	void Start() {
		target = null;
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {
			Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
			transform.position = transform.position + (transform.up * speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			target = other.transform;
		}
	}
}
