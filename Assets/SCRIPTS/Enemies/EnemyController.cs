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
			Quaternion rotation = Quaternion.LookRotation
				(target.transform.position - transform.position, transform.TransformDirection(-Vector3.forward));
			transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
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
