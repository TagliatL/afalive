using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour {

	public Camera cam;
	public int currentLevel;
	public Text name;
	int previousLevel;
	GameObject[] levels;
	bool isSwitching;
	private float startTime;


	void Start () {
		isSwitching = false;
		levels = GameObject.FindGameObjectsWithTag("Core");
		print (levels.Length);
		cam.transform.position = new Vector3(-levels[currentLevel].transform.TransformPoint(Vector3.zero).x,cam.transform.position.y,cam.transform.position.z);
	}

	IEnumerator SwitchLevel() {
		name.text = levels [previousLevel].name;
		while (isSwitching) {
			cam.transform.position = Vector3.Lerp(new Vector3(cam.transform.position.x,cam.transform.position.y,cam.transform.position.z),
			                                      new Vector3(-levels[currentLevel].transform.TransformPoint(Vector3.zero).x,cam.transform.position.y,cam.transform.position.z), 10 * Time.deltaTime);
				
			if (cam.transform.position.x >= -levels[currentLevel].transform.TransformPoint(Vector3.zero).x - 0.1 && currentLevel > previousLevel) {
				cam.transform.position = new Vector3(-levels[currentLevel].transform.TransformPoint(Vector3.zero).x,cam.transform.position.y,cam.transform.position.z);
				print (currentLevel);
				print("lol");
				isSwitching = false;
			} else if (cam.transform.position.x <= -levels[currentLevel].transform.TransformPoint(Vector3.zero).x + 0.1 && currentLevel < previousLevel) {
				cam.transform.position = new Vector3(-levels[currentLevel].transform.TransformPoint(Vector3.zero).x,cam.transform.position.y,cam.transform.position.z);
				print (currentLevel);
				print("lol2");
				isSwitching = false;
			}
			yield return new WaitForEndOfFrame();
			
		}
	}

	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		if (!isSwitching && inputDevice.LeftStickX > 0.6f) {
			if (currentLevel < levels.Length - 1) {
				print (currentLevel);
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
		if (inputDevice.Action1.WasPressed) {
			Application.LoadLevel("viruses"+currentLevel);
		}
	}
}
