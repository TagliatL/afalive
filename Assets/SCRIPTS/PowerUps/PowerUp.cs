using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public GameObject pickUpBullet;
	public GameObject effectWhenCollected;
	public void DestroySelf() {
		Destroy (this.gameObject);
	}
	public void Used() {
		Instantiate (effectWhenCollected, transform.position, Quaternion.identity);
		DestroySelf ();
	}

}
