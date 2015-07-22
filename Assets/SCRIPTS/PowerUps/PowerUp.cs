using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public GameObject pickUpBullet;

	public void DestroySelf() {
		Destroy (this.gameObject);
	}
	public void Used() {
		DestroySelf ();
	}

}
