using UnityEngine;
using System.Collections;
using InControl;

public class GameManager : MonoBehaviour {

	GameObject theCore;
	public GameObject thePlayer;
	public GameObject spawner;
	public GameObject startText;
	public GameObject chrono;
	public GameObject yourTime;
	GameObject timer;
	bool levelStarted;
	DestroyByContact coreValues;
	// Use this for initialization
	void Start () {
		levelStarted = false;
		theCore = GameObject.FindGameObjectWithTag ("Core");
		coreValues = theCore.GetComponent<DestroyByContact> ();
	}

	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;

		if (inputDevice.Action1.WasPressed) {
			startText.SetActive(false);
			spawner.GetComponent<EnemyGenerator>().enabled = true;
			thePlayer.GetComponent<Controller>().enabled = true;
			chrono.SetActive(true);

			levelStarted = true;
		}
		if (coreValues.life <= 0) {
			spawner.GetComponent<EnemyGenerator>().enabled = false;
			thePlayer.GetComponent<Controller>().enabled = false;
			chrono.GetComponent<Chrono>().StopTimer();
		}
	}
}
