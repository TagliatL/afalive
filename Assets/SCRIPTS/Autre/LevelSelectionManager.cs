using UnityEngine;
using System.Collections;
using InControl;

public class LevelSelectionManager : MonoBehaviour {

	public Camera cam;
	public int currentLevel;
	int previousLevel;
	GameObject[] levels;
	bool isSwitching;
	private float startTime;


	void Start () {
		isSwitching = false;
		levels = GameObject.FindGameObjectsWithTag("Core");
	}



	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		previousLevel = currentLevel;
		if (!isSwitching && inputDevice.LeftStickX > 0.6f) {
			isSwitching = true;
			currentLevel++;

			if(currentLevel != previousLevel) {
				startTime+=Time.deltaTime;
				cam.transform.position = Vector3.Lerp(new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z),
				new Vector3(levels[currentLevel].transform.position.x,cam.transform.position.y,cam.transform.position.z), 10*startTime);

				if(cam.transform.position.x == levels[currentLevel].transform.position.x){
					isSwitching = false;
				}
			}
		}
	}
}
