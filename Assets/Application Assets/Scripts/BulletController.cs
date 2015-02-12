using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	void FixedUpdate () {
		GameObject player = GameObject.FindWithTag ("Player");

		float dist = Vector3.Distance (player.transform.position, transform.position);
		if (dist > 50) {
			Destroy (gameObject);
		}
	}
}
