using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {
	public GameObject asteroidPrefab;
	public GameObject playerPrefab;
	public GameObject planetPrefab;
	public GameObject twinklePrefab;

	void Start () {
		Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		Instantiate (planetPrefab, new Vector3(20f, 0, -20f), Quaternion.identity);

		GameObject asteroid;
		GameObject twinkle;

		for (int i = 0; i < 25; i++) {
			asteroid = (GameObject) Instantiate(asteroidPrefab, new Vector3(Random.Range (-80f, 80f), 0, Random.Range (-80f, 80f)), Quaternion.identity);
			asteroid.transform.localScale = Vector3.one * Random.Range (0.5f, 2.5f);
		}

		for (int i = 0; i < 1000; i++) {
			twinkle = (GameObject) Instantiate(twinklePrefab, new Vector3(Random.Range (-400f, 400f), -100, Random.Range (-400f, 400f)), Quaternion.identity);
			//twinkle.transform.localScale = Vector3.one * Random.Range (0.5f, 2.5f);
		}
	}
}
