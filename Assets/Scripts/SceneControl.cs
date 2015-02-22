using UnityEngine;
using System.Collections;

public class SceneControl : MonoBehaviour {
  public GameObject asteroidPrefab;
  public GameObject twinklePrefab;

  void Start() {
    GenerateSceneObjects();
  }

  void GenerateSceneObjects() {
    CreateAsteroids();
    CreateTwinkles();
  }

  void CreateAsteroids() {
    GameObject asteroid;

    for (int i = 0; i < 25; i++) {
      asteroid = (GameObject) Instantiate(asteroidPrefab, new Vector3(Random.Range (-80f, 80f), 0, Random.Range (-80f, 80f)), Quaternion.identity);
      asteroid.transform.localScale = Vector3.one * Random.Range (1.0f, 2.5f);
    }
  }

  void CreateTwinkles() {
    for (int i = 0; i < 1000; i++) {
      Instantiate(twinklePrefab, new Vector3(Random.Range (-400f, 650f), -100, Random.Range (-400f, 650f)), Quaternion.identity);
    }
  }
}
