using UnityEngine;
using System.Collections;

public class HyperspaceBoundary : MonoBehaviour {
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Entering hyperspace boundary...");

			PlayerController player = col.gameObject.GetComponent<PlayerController>();
			player.hyperspaceMenu.SetActive (true);

			HyperspaceBoundaryMenuInterface hyperspaceInterface = player.hyperspaceMenu.GetComponent<HyperspaceBoundaryMenuInterface>();
			hyperspaceInterface.enterButton.onClick.AddListener(() => {
				Debug.Log ("Warp speed, Scotty! Aye aye, cap'n!");

				if (Application.loadedLevelName == "Thor Galaxy") {
					Application.LoadLevel("Apollo Galaxy");
				} else {
					Application.LoadLevel("Thor Galaxy");
				}
			});
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Leaving hyperspace boundary...");

			PlayerController player = col.gameObject.GetComponent<PlayerController>();
			player.hyperspaceMenu.SetActive (false);

			HyperspaceBoundaryMenuInterface hyperspaceInterface = player.hyperspaceMenu.GetComponent<HyperspaceBoundaryMenuInterface>();
			hyperspaceInterface.enterButton.onClick.RemoveAllListeners();
		}
	}
}