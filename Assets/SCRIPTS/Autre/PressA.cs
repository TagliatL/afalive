using UnityEngine;
using System.Collections;
using InControl;
public class PressA : MonoBehaviour {

	public string scene;
	
	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.Action1.WasPressed) {
			Application.LoadLevel(scene);
		}
	}
}
