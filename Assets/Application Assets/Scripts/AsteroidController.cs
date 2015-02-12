using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
	public GameObject sparksPrefab;

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "_bullet") {
			Instantiate(sparksPrefab, transform.position, Quaternion.identity);
			gameObject.rigidbody.AddForce(100f * col.gameObject.transform.forward);
			gameObject.rigidbody.AddExplosionForce(1000f, col.gameObject.transform.position, 0);
			Destroy (col.gameObject);
			Destroy (gameObject);

			GameObject game = GameObject.FindWithTag("_main_engine");
			GameObject asteroidPrefab = game.GetComponent<GameEngine>().asteroidPrefab;

			GameObject asteroid = (GameObject) Instantiate(asteroidPrefab, new Vector3(Random.Range (-80f, 80f), 0, Random.Range (-80f, 80f)), Quaternion.identity);
			asteroid.transform.localScale = Vector3.one * Random.Range (0.5f, 2.5f);
		}
	}

	void LateUpdate ()
	{
		GameObject[] sparks = GameObject.FindGameObjectsWithTag("_asteroid_sparks");

		foreach (GameObject spark in sparks) {
			if (!spark.particleSystem.IsAlive()) {
				Destroy (spark);
			}
		}
	}
}