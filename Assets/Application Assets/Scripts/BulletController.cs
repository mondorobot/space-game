using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	private GameObject player;

	void Awake() {
		player = GameObject.FindWithTag ("Player");
	}

	void FixedUpdate () {
		float dist = Vector3.Distance (player.transform.position, transform.position);
		if (dist > 50) {
			Destroy (gameObject);
		}
	}
}
