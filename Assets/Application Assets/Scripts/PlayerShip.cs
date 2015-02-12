using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {
	public Rigidbody laserPrefab;

	void Update () {
		if (Input.GetKeyDown((KeyCode.Space))) {
			Debug.Log ("shoot!");
			Rigidbody bullet = (Rigidbody) Instantiate(laserPrefab, transform.position, Quaternion.identity);

			bullet.transform.Rotate(transform.rotation.eulerAngles + 90f * Vector3.up);
			bullet.rigidbody.AddForce(-1000f * transform.forward);
		}

		if (Input.GetKey(KeyCode.A)) {
			rigidbody.angularVelocity = Vector3.zero;
			transform.Rotate(Vector3.down * 2.7f);
		}

		if (Input.GetKey(KeyCode.D)) {
			rigidbody.angularVelocity = Vector3.zero;
			transform.Rotate(Vector3.up * 2.7f);
		}

		if (Input.GetKey(KeyCode.W)) {
			rigidbody.AddForce(transform.forward * -5f);
		}

		if (Input.GetKey(KeyCode.S)) {
			rigidbody.AddForce(transform.forward * 5f);
		}

		Camera.main.transform.position = new Vector3 (transform.position.x - 10, 30, transform.position.z + 10);
		Camera.main.transform.LookAt (transform);
	}
}
