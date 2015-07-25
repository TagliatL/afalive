using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	public Vector3 rotation;
	public int speed;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotation * Time.deltaTime*speed);
	}
}
