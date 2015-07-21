using UnityEngine;
using System.Collections;

public class StandardBullet : MonoBehaviour {

	public int speed;
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed *10);
	}
}
