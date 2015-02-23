using UnityEngine;
using System.Collections.Generic;

public class PlayerCore : MonoBehaviour {
  public static PlayerCore self;

  // properties
  public int money;
  public Dictionary<string,StructInventoryItem> inventory;

  // singleton handler
  void Awake() {
    if (self == null) {
      DontDestroyOnLoad(gameObject);
      self = this;
      InitSelf();
    } else if (self != this) {
      Destroy(gameObject);
    }
  }

  void InitSelf() {
    Debug.Log ("Initializing player core...");
    inventory = new Dictionary<string,StructInventoryItem>();
    money = -1;
  }
}
