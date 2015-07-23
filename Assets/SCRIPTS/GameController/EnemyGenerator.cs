using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {

	public float arenaSize;
	public float spawnRate;
	public float spawnExitRate;
	public int enemiesPerWave;
	public int numberOfWaves;
	public GameObject player;
	public GameObject enemy;
	public float startWait;
	public float waveWait;

	void Start() {
		StartCoroutine("SpawnWaves");
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds (startWait);
		while (true) {
			Vector3 spawnPosition = Random.insideUnitCircle * arenaSize;
			spawnPosition.z = -3;
			Quaternion spawnRotation = Quaternion.identity;
			// Instantiate Black Hole here;
			for (int i = 0; i < enemiesPerWave; i++) {
				GameObject obj = Instantiate(enemy, spawnPosition, spawnRotation) as GameObject;
				obj.GetComponent<EnemyController>().AssignTarget(player.transform);
				yield return new WaitForSeconds (spawnExitRate);
			}
		yield return new WaitForSeconds (waveWait);
		}
	}
}
