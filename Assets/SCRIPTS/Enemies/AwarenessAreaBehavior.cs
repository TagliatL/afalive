using UnityEngine;
using System.Collections;

public class AwarenessAreaBehavior : MonoBehaviour {

	public EnemyController ship;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			ship.target = other.transform;
		}
	}
}
