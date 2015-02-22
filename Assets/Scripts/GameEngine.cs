using UnityEngine;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour {
  public static List<StructItemType> knownItemTypes;

  public static GameEngine self;

  void Awake() {
    if (self == null) {
      DontDestroyOnLoad(gameObject);
      self = this;
      InitGame();
    } else if (self != this) {
      Destroy(gameObject);
    }
  }

  void InitGame() {
    knownItemTypes = new List<StructItemType>();

    StructItemType newItemType;

    newItemType = new StructItemType();
    newItemType.itemName = "Obsidian";
    newItemType.itemLow = 100;
    newItemType.itemHigh = 1000;
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType ();
    newItemType.itemName = "Food";
    newItemType.itemLow = 5;
    newItemType.itemHigh = 25;
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType ();
    newItemType.itemName = "Cotton";
    newItemType.itemLow = 20;
    newItemType.itemHigh = 60;
    knownItemTypes.Add(newItemType);
  }
}
