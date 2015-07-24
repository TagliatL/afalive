using System;
using UnityEngine;
using UnityEngine.UI;

public class Chrono : MonoBehaviour {
	
	public Text chrono;
	public Text yourTime;
	private bool _isRunning;
	private bool _wasRunningLastUpdate;
	private float _elapsedSeconds;
	private float _timeLastUpdate;
	
	
	void Start() {
		StartTimer ();
		InvokeRepeating("updateTextMesh", 0, 0.2f);
	}
	
	public void StartTimer() {
		_isRunning = true;
	}
	
	public void ResetTimer() {
		_elapsedSeconds = 0;
	}
	
	public void StopTimer() {
		_isRunning = false;
	}
	
	private void updateTextMesh() {
		if (!_isRunning) {
			_wasRunningLastUpdate = false;
			return;
		}
		if (_wasRunningLastUpdate) {
			var deltaTime = Time.time - _timeLastUpdate;
			_elapsedSeconds += deltaTime;
		}
		
		var timeSpan = TimeSpan.FromSeconds(_elapsedSeconds);
		chrono.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

		_timeLastUpdate = Time.time;
		_wasRunningLastUpdate = true;
	}
}