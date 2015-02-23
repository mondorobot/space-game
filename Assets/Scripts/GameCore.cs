using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class GameCore : MonoBehaviour {
  public static GameCore self;

  // properties
  public static List<StructItemType> knownItemTypes;

  // singleton handler
  void Awake() {
    if (self == null) {
      DontDestroyOnLoad(gameObject);
      self = this;
      InitSelf();
      Load();
    } else if (self != this) {
      Destroy(gameObject);
    }
  }

  public void Save() {
    Debug.Log("Saving game, please do not turn off your computer...");
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(Application.persistentDataPath + "/playerInventory.dat");

    PlayerData data = new PlayerData();
    data.money = PlayerCore.self.money;
    data.inventory = PlayerCore.self.inventory;

    bf.Serialize(file, data);
    file.Close();
  }

  public void Load() {
    Debug.Log("Loading, please wait...");

    // uncomment this if you want to force restart, for example
    // if you've made changes to some of the data types or
    // the way the data is stored
    //NewGame();
    if (!File.Exists(Application.persistentDataPath + "/playerInventory.dat")) {
      Debug.Log("Could not find the save file...");
    } else {
      Debug.Log("Deserializing player data...");

      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(Application.persistentDataPath + "/playerInventory.dat", FileMode.Open);
      PlayerData data = (PlayerData) bf.Deserialize(file);
      file.Close();

      PlayerCore.self.money = data.money;
      PlayerCore.self.inventory = data.inventory;

      Debug.Log("DONE");
    }

    if (PlayerCore.self.money < 0) {
      NewGame();
    }
  }

  void NewGame() {
    Debug.Log("Starting a new game...");
    PlayerCore.self.money = 10000;
    PlayerCore.self.inventory = new Dictionary<string,StructInventoryItem>();
    Save();
  }

  void InitSelf() {
    InitKnownItemTypes();
  }

  void InitKnownItemTypes() {
    StructItemType newItemType;

    knownItemTypes = new List<StructItemType>();

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
