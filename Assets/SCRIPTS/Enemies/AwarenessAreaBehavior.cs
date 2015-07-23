using UnityEngine;
using System.Collections;

public class AwarenessAreaBehavior : MonoBehaviour {

	public EnemyController ship;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			ship.target = other.transform;
		}
	}
}
