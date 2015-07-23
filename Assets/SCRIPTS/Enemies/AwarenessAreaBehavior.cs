using UnityEngine;
using System.Collections;

public class AwarenessAreaBehavior : MonoBehaviour {

	public GameObject ship;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			ship.GetComponent<EnemyController>().AssignTarget(other.transform);
		}
	}
}
