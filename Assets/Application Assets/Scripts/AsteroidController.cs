using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
	private GameEngine gameEngine;
	private GameObject asteroidPrefab;
	public GameObject sparksPrefab;

	void Awake () {
		GameObject mainEngine = GameObject.FindGameObjectWithTag ("GameController");
		gameEngine = mainEngine.GetComponent<GameEngine> ();
		asteroidPrefab = gameEngine.asteroidPrefab;
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "_bullet") {
			Instantiate(sparksPrefab, transform.position, Quaternion.identity);
			gameObject.rigidbody.AddForce(100f * col.gameObject.transform.forward);
			gameObject.rigidbody.AddExplosionForce(1000f, col.gameObject.transform.position, 0);
			Destroy (col.gameObject);

			// delete this asteroid
			//Destroy (gameObject);

			// and randomly create a new one somewhere else
			//GameObject asteroid = (GameObject) Instantiate(asteroidPrefab, new Vector3(Random.Range (-80f, 80f), 0, Random.Range (-80f, 80f)), Quaternion.identity);
			//asteroid.transform.localScale = Vector3.one * Random.Range (0.5f, 2.5f);
		}
	}

	void LateUpdate () {
		GameObject[] sparks = GameObject.FindGameObjectsWithTag("_asteroid_sparks");

		foreach (GameObject spark in sparks) {
			if (!spark.particleSystem.IsAlive()) {
				Destroy (spark);
			}
		}
	}
}