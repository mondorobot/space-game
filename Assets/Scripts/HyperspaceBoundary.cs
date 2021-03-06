﻿using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class HyperspaceBoundary : MonoBehaviour {
  void OnTriggerEnter(Collider col) {
    if (col.gameObject.tag == "Player") {
      Debug.Log ("Entering hyperspace boundary...");

      ShipController player = col.gameObject.GetComponent<ShipController>();
      player.hyperspaceMenu.SetActive (true);

      HyperspaceBoundaryMenuInterface hyperspaceInterface = player.hyperspaceMenu.GetComponent<HyperspaceBoundaryMenuInterface>();
      hyperspaceInterface.enterButton.onClick.AddListener(() => {
        Debug.Log ("Warp speed, Scotty! Aye aye, cap'n!");
        Application.LoadLevel("Hyperspace Map");
      });
    }
  }

  void OnTriggerExit(Collider col) {
    if (col.gameObject.tag == "Player") {
      Debug.Log ("Leaving hyperspace boundary...");

      ShipController player = col.gameObject.GetComponent<ShipController>();
      player.hyperspaceMenu.SetActive (false);

      HyperspaceBoundaryMenuInterface hyperspaceInterface = player.hyperspaceMenu.GetComponent<HyperspaceBoundaryMenuInterface>();
      hyperspaceInterface.enterButton.onClick.RemoveAllListeners();
    }
  }
}
