using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class GameCore : MonoBehaviour {
  public static GameCore self;

  public int money;
  public Dictionary<string,StructInventoryItem> inventory;

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
			self = this;
      //Destroy(gameObject);
    }
  }

  public void Save() {
    Debug.Log("Saving game, please do not turn off your computer...");
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(Application.persistentDataPath + "/playerInventory.dat");

    PlayerData data = new PlayerData();
    data.money = GameCore.self.money;
    data.inventory = GameCore.self.inventory;

    bf.Serialize(file, data);
    file.Close();
  }

	public void InitNewGame()
	{
		Application.LoadLevel ("Apollo Galaxy");
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

      GameCore.self.money = data.money;
      GameCore.self.inventory = data.inventory;

      Debug.Log("DONE");
    }

    if (GameCore.self.money < 0) {
      NewGame();
    }
  }

  public void NewGame() {
    Debug.Log("Starting a new game...");
    GameCore.self.money = 10000;
    GameCore.self.inventory = new Dictionary<string,StructInventoryItem>();
    Save();
  }

  void InitSelf() {
    InitKnownItemTypes();
  }

  void InitKnownItemTypes() {
    StructItemType newItemType;

    knownItemTypes = new List<StructItemType>();

    newItemType = new StructItemType();
    newItemType.itemName = "Obsidian Brick";
    newItemType.itemLow = 950;
    newItemType.itemHigh = 970;
    newItemType.itemCategory = "mineral";
    newItemType.itemSubCategory = "ore";
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType();
    newItemType.itemName = "Ultonimum Ingot";
    newItemType.itemLow = 2980;
    newItemType.itemHigh = 3200;
    newItemType.itemCategory = "mineral";
    newItemType.itemSubCategory = "ore";
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType();
    newItemType.itemName = "Megalantium Ore";
    newItemType.itemLow = 390;
    newItemType.itemHigh = 420;
    newItemType.itemCategory = "mineral";
    newItemType.itemSubCategory = "ore";
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType ();
    newItemType.itemName = "Zardoz Beetle";
    newItemType.itemLow = 23;
    newItemType.itemHigh = 25;
    newItemType.itemCategory = "food";
    newItemType.itemSubCategory = "animal";
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType ();
    newItemType.itemName = "Snaz Plant";
    newItemType.itemLow = 17;
    newItemType.itemHigh = 20;
    newItemType.itemCategory = "food";
    newItemType.itemSubCategory = "plant";
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType ();
    newItemType.itemName = "Quimjom Juice";
    newItemType.itemLow = 5;
    newItemType.itemHigh = 10;
    newItemType.itemCategory = "food";
    newItemType.itemSubCategory = "plant";
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType ();
    newItemType.itemName = "Groking Cloth";
    newItemType.itemLow = 55;
    newItemType.itemHigh = 60;
    newItemType.itemCategory = "textile";
    newItemType.itemSubCategory = "organic";
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType ();
    newItemType.itemName = "Derpalated Nylon";
    newItemType.itemLow = 47;
    newItemType.itemHigh = 52;
    newItemType.itemCategory = "textile";
    newItemType.itemSubCategory = "synthetic";
    knownItemTypes.Add(newItemType);

    newItemType = new StructItemType ();
    newItemType.itemName = "Thneed Yarn";
    newItemType.itemLow = 80;
    newItemType.itemHigh = 84;
    newItemType.itemCategory = "textile";
    newItemType.itemSubCategory = "organic";
    knownItemTypes.Add(newItemType);
  }
}
