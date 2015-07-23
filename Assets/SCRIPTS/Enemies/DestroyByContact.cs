using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bullet") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
