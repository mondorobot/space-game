using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
  //private GameObject asteroidPrefab;
  public GameObject sparksPrefab;
  public int hitPoints;

  void Awake () {
    //asteroidPrefab = GameEngine.self.asteroidPrefab;
    hitPoints = 3;
  }

  void OnTriggerEnter(Collider col) {
    if (col.gameObject.tag == "_bullet") {
      Debug.Log("trigger entered");

      Instantiate(sparksPrefab, transform.position, Quaternion.identity);
      gameObject.GetComponent<Rigidbody>().AddForce(100f * col.gameObject.transform.forward);
      gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, col.gameObject.transform.position, 0);
      Destroy (col.gameObject);

      // delete this asteroid
      hitPoints--;
      if (hitPoints <= 0) {
        Destroy (gameObject);
      }

      // and randomly create a new one somewhere else
      //GameObject asteroid = (GameObject) Instantiate(asteroidPrefab, new Vector3(Random.Range (-80f, 80f), 0, Random.Range (-80f, 80f)), Quaternion.identity);
      //asteroid.transform.localScale = Vector3.one * Random.Range (0.5f, 2.5f);
    }
  }

  void LateUpdate () {
    GameObject[] sparks = GameObject.FindGameObjectsWithTag("_asteroid_sparks");

    foreach (GameObject spark in sparks) {
      if (!spark.GetComponent<ParticleSystem>().IsAlive()) {
        Destroy (spark);
      }
    }
  }
}
