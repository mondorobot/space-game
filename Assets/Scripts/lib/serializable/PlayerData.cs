using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData {
  public int money;
  public Dictionary<string,StructInventoryItem> inventory;
}
