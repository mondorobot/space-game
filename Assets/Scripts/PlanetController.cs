using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour {
  public GameObject player;

  public GameObject landingMenuPrefab;
  private PlanetLandingInterface landingInterface;
  private GameObject landingMenu;

  public GameObject tradingMenuPrefab;
  private PlanetTradingInterface tradingInterface;
  private GameObject tradingMenu;

  private Image saleItemPrefab;
  private Image playerItemPrefab;
  private List<StructSaleItem> itemsForSale;
  private Image[] inventoryMenuItems;

  void Start () {
    player = GameObject.FindWithTag("Player");

    InitTradingMarketSettings();

    InitLandingMenu();
    InitTradingMenu();
  }

  // Events
  //////////////////////////////////////////////////////////////////////////////
  void OnTriggerEnter (Collider col) {
    if (col.gameObject.tag == "Player") {
      ShowLandingMenu();
    }
  }

  void OnTriggerExit (Collider col) {
    if (col.gameObject.tag == "Player") {
      HideLandingMenu ();
    }
  }

  // Landing Menu
  //////////////////////////////////////////////////////////////////////////////
  void InitLandingMenu() {
    float scale = transform.localScale.x / 2;
    landingMenu = (GameObject) Instantiate (landingMenuPrefab, transform.position + new Vector3(scale,scale,scale*-1f), landingMenuPrefab.transform.rotation);
    landingMenu.SetActive(false);
    landingInterface = landingMenu.GetComponent<PlanetLandingInterface> ();
    landingInterface.enterButton.onClick.AddListener(HandleShowTradingMenu);
  }

  void HideLandingMenu() {
    landingMenu.SetActive(false);
  }

  void ShowLandingMenu () {
    landingMenu.SetActive (true);
  }

  // Trading Menu
  //////////////////////////////////////////////////////////////////////////////
  void InitTradingMenu() {
    tradingMenu = (GameObject) Instantiate(tradingMenuPrefab, Vector3.zero, Quaternion.identity);
    tradingMenu.SetActive(false);

    tradingInterface = tradingMenu.GetComponent<PlanetTradingInterface> ();
    tradingInterface.exitButton.onClick.AddListener(HideTradingMenu);

    UpdateBankBalance();
    InitTradingInventory();
    RenderTradingMenuItems();
    RenderTradingMenuPlayerItems();
  }

  // Planet Inventory
  //////////////////////////////////////////////////////////////////////////////
  void InitTradingMarketSettings() {
  }

  void InitTradingInventory () {
    int price;
    StructItemType knownItem;
    StructSaleItem newItem;

    itemsForSale = new List<StructSaleItem>();
    saleItemPrefab = tradingInterface.itemForSalePrefab;
    playerItemPrefab = tradingInterface.itemToSellPrefab;

    for (var i = 0; i < GameCore.knownItemTypes.Count; i++) {
      knownItem = GameCore.knownItemTypes[i];
      price = UnityEngine.Random.Range(knownItem.itemLow, knownItem.itemHigh);

      newItem = new StructSaleItem ();
      newItem.itemName = knownItem.itemName;
      newItem.itemPrice = price;

      itemsForSale.Add(newItem);
    }
  }

  // render menu
  void RenderTradingMenuItems () {
    for (int i = 0; i < itemsForSale.Count; i++) {
      StructSaleItem inventoryItem = itemsForSale[i];

      RenderTradingMenuItem(inventoryItem, i);
    }
  }

  // render item
  void RenderTradingMenuItem (StructSaleItem saleItem, int i) {
    int pos = i * -40;
    Image menuItem = (Image) Instantiate(saleItemPrefab, saleItemPrefab.transform.position + new Vector3(0, pos, 0), Quaternion.identity);
    menuItem.transform.SetParent (tradingMenu.transform, false);

    MenuItemForSale menuInterface = menuItem.GetComponent<MenuItemForSale>();
    menuInterface.itemName.text = saleItem.itemName;
    menuInterface.itemPrice.text = saleItem.itemPrice.ToString("C0");

    menuInterface.buyButton.onClick.AddListener(() => {
      HandleBuyItem(saleItem);
    });
  }

  // Player Inventory
  //////////////////////////////////////////////////////////////////////////////
  void RenderTradingMenuPlayerItems () {
    int inventoryCount;

    StructInventoryItem inventoryItem;
    StructItemType itemType;

    DestroyMenuPlayerItems();

    inventoryCount = GameCore.self.inventory.Count;
    inventoryMenuItems = new Image[inventoryCount];

    Debug.Log("GameCore.self.inventory.Count");
    Debug.Log(GameCore.self.inventory.Count);

    int c = 0;
    for (int i = 0; i < GameCore.knownItemTypes.Count; i++) {
      itemType = GameCore.knownItemTypes[i];

      if (GameCore.self.inventory.ContainsKey(itemType.itemName)) {
        inventoryItem = GameCore.self.inventory[itemType.itemName];

        if (inventoryItem.itemQty > 0 ) {
          RenderTradingMenuPlayerItem(inventoryItem, c);
          c++;
        }
      }
    }
  }

  void UpdateBankBalance () {
    string balance = GameCore.self.money.ToString("C0");
    tradingInterface.playerBankBalance.text = balance;
  }

  void DestroyMenuPlayerItems () {
    if (inventoryMenuItems == null) {
      return;
    }

    for (int i = 0; i < inventoryMenuItems.Length; i++) {
      if (inventoryMenuItems[i] != null && inventoryMenuItems[i].gameObject != null) {
        Destroy(inventoryMenuItems[i].gameObject);
      }
    }
  }

  void RenderTradingMenuPlayerItem (StructInventoryItem inventoryItem, int i) {
    int pos = i * -40;
    Image menuItem = (Image) Instantiate(playerItemPrefab, playerItemPrefab.transform.position + new Vector3(340, pos, 0), Quaternion.identity);
    menuItem.transform.SetParent (tradingMenu.transform, false);

    MenuItemToSell menuInterface = menuItem.GetComponent<MenuItemToSell>();
    menuInterface.itemName.text = inventoryItem.itemName;
    menuInterface.itemQty.text = inventoryItem.itemQty.ToString();

    menuInterface.sellButton.onClick.AddListener(() => {
      HandleSellItem(inventoryItem);
    });

    inventoryMenuItems[i] = menuItem;
  }

  // Buying and Selling
  //////////////////////////////////////////////////////////////////////////////
  void HandleBuyItem(StructSaleItem saleItem) {
    string itemName;

    if ((GameCore.self.money - saleItem.itemPrice) < 0) {
      Debug.Log ("I'm not gonna fall for that, you don't have enough money. Go away.");
      return;
    }

    itemName = saleItem.itemName;
    StructInventoryItem inventoryItem = GetInventoryItem(itemName);

    Debug.Log ("Ok, it's a deal! 1 " + itemName + " for $" + saleItem.itemPrice);

    GameCore.self.money -= saleItem.itemPrice;

    inventoryItem.itemQty += 1;
    GameCore.self.inventory[itemName] = inventoryItem;

    Debug.Log("Bank Balance: $" + GameCore.self.money);
    Debug.Log("Player now has: " + inventoryItem.itemQty + " " + itemName + "(s)");

    RenderTradingMenuPlayerItems();
    UpdateBankBalance();

    GameCore.self.Save();
  }

  void HandleSellItem(StructInventoryItem inventoryItem) {
    string itemName;
    StructSaleItem saleItem;

    itemName = inventoryItem.itemName;
    saleItem = FindItemForSale(itemName);

    if (inventoryItem.itemQty <= 0) {
      Debug.Log ("Sorry, Bud, but I wasn't born yesterday. Can't sell what you don't have.");
      return;
    }

    Debug.Log ("Great, we'll take 1 of your " + itemName + "s for $" + saleItem.itemPrice);

    GameCore.self.money += saleItem.itemPrice;

    inventoryItem.itemQty -= 1;
    GameCore.self.inventory[itemName] = inventoryItem;

    Debug.Log("Bank Balance: $" + GameCore.self.money);
    Debug.Log("Player now has: " + inventoryItem.itemQty + " " + itemName + "(s)");

    RenderTradingMenuPlayerItems();
    UpdateBankBalance();

    GameCore.self.Save();
  }

  //void UpdateMenuItemPlayerItem(string itemName) {
  //  MenuItemToSell menuInterface;
  //  Image menuItem;
  //  int menuItemIndex;
  //  StructInventoryItem inventoryItem;

  //  inventoryItem = GameCore.self.inventory[itemName];
  //  menuItemIndex = FindIndexOfMenuPlayerItem(itemName);
  //  Debug.Log("menuItemIndex");
  //  Debug.Log(menuItemIndex);

  //  if (menuItemIndex < 0) {
  //    Debug.Log("whoops!");
  //    return;
  //  }

  //  menuItem = inventoryMenuItems[menuItemIndex];
  //  menuInterface = menuItem.GetComponent<MenuItemToSell>();
  //  menuInterface.itemQty.text = inventoryItem.itemQty.ToString();
  //}

  // Util Lookups
  //////////////////////////////////////////////////////////////////////////////
  int FindIndexOfMenuPlayerItem(string name) {
    Image menuItem;
    MenuItemToSell menuItemInterface;

    int len = inventoryMenuItems.Length;

    for (int i = 0; i < len; i++) {
      menuItem = inventoryMenuItems[i];
      menuItemInterface = menuItem.GetComponent<MenuItemToSell>();
      if (menuItemInterface.itemName.text == name) {
        return i;
      }
    }

    return -1;
  }

  StructSaleItem FindItemForSale(string name) {
    StructSaleItem saleItem;

    int len = itemsForSale.Count;

    for (int i = 0; i < len; i++) {
      saleItem = itemsForSale[i];
      if (saleItem.itemName == name) {
        return saleItem;
      }
    }

    return null;
  }

  StructInventoryItem GetInventoryItem(string itemName) {
    StructInventoryItem inventoryItem;
    StructInventoryItem emptyItem;

    if (!GameCore.self.inventory.ContainsKey(itemName)) {
      emptyItem = new StructInventoryItem();

      emptyItem.itemName = itemName;
      emptyItem.itemQty = 0;

      GameCore.self.inventory.Add(itemName, emptyItem);
    }

    inventoryItem = GameCore.self.inventory[itemName];

    return inventoryItem;
  }

  // Show Hide
  //////////////////////////////////////////////////////////////////////////////
  void HandleShowTradingMenu() {
    Debug.Log ("Roger that. You are cleared for landing.");

    HideLandingMenu ();
    DisablePlayer ();
    ShowTradingMenu ();
  }

  void DisablePlayer() {
    player.rigidbody.velocity = Vector3.zero;
    player.rigidbody.angularVelocity = Vector3.zero;
    player.SetActive(false);
  }

  void ShowTradingMenu () {
    tradingMenu.SetActive (true);
  }

  void HideTradingMenu () {
    tradingMenu.SetActive (false);
    player.SetActive(true);
  }
}
