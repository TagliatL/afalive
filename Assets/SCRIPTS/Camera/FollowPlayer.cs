using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	
	public GameObject player;
	public int distance;
	public Vector3 velocity = Vector3.zero;
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 0.10f);
		transform.position = new Vector3 (transform.position.x, transform.position.y, distance);
	}
}
