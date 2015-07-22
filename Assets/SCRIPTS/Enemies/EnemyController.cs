using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion rotation = Quaternion.LookRotation
			(target.transform.position - transform.position, transform.TransformDirection(-Vector3.forward));
		transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
	}
}
