using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

 	public Transform target;
	public float speed;
	public float rotationSpeed;
	public float maneuverDistance;
	bool avoiding;
	int counter;
	RaycastHit lastHit;

	void Start() {
		target = null;
		avoiding = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (target != null) {
			RaycastHit hitInfo;
			bool hit = Physics.Raycast(transform.position, transform.up, out hitInfo, maneuverDistance);
			Quaternion rotation;
			if (avoiding) {
				rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lastHit.transform.position + transform.right, transform.TransformDirection(-Vector3.forward)), rotationSpeed * Time.deltaTime);
				transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
				transform.position = transform.position + (transform.up * speed * Time.deltaTime);
				counter--;
				if (counter <= 0) {
					avoiding = false;
				}
			} else if (hit) {
				if (hitInfo.collider.tag == "Enemy") {
					rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hitInfo.transform.position + transform.right, transform.TransformDirection(-Vector3.forward)), rotationSpeed * Time.deltaTime);
					transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
					transform.position = transform.position + (transform.up * speed * Time.deltaTime);
					avoiding = true;
					counter = 10;
					lastHit = hitInfo;
				} else { 
					rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position, transform.TransformDirection(-Vector3.forward)), rotationSpeed * Time.deltaTime);
					transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
					transform.position = transform.position + (transform.up * speed * Time.deltaTime);
				}
			} else { 
				rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position, transform.TransformDirection(-Vector3.forward)), rotationSpeed * Time.deltaTime);
				transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
				transform.position = transform.position + (transform.up * speed * Time.deltaTime);
			}
		}
	}


}
