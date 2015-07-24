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

	IEnumerator SwitchLevel() {
		while (isSwitching) {
			cam.transform.position = Vector3.Lerp(new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z),
			                                      new Vector3(-levels[currentLevel].transform.localPosition.x,cam.transform.position.y,cam.transform.position.z), 10 * Time.deltaTime);
				
			if (Mathf.Abs(cam.transform.position.x) >= Mathf.Abs(levels[currentLevel].transform.localPosition.x) - 0.1 && currentLevel > previousLevel) {
				print("lol");
				isSwitching = false;
			} else if (Mathf.Abs(cam.transform.position.x) <= Mathf.Abs(levels[currentLevel].transform.localPosition.x) + 0.1 && currentLevel < previousLevel) {
				print("lol2");
				isSwitching = false;
			}
			yield return null;
		
		}
	}

	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		if (!isSwitching && inputDevice.LeftStickX > 0.6f) {
			if (currentLevel < levels.Length - 1) {
				previousLevel = currentLevel;
				currentLevel++;
				isSwitching = true;
				StartCoroutine("SwitchLevel");
			}
		} else if (!isSwitching && inputDevice.LeftStickX < -0.6f) {
			if (currentLevel > 0) {
				previousLevel = currentLevel;
				currentLevel--;
				isSwitching = true;
				StartCoroutine("SwitchLevel");
			}
		}
	}
}
