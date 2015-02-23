using UnityEngine;
using UnityEngine.UI;
using System;
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
  private List<StructSaleItem> itemsForSale;

  void Start () {
    player = GameObject.FindWithTag("Player");

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

    InitTradingInventory();
    InitTradingMenuItems ();
  }

  void InitTradingInventory () {
    int price;
    StructItemType knownItem;
    StructSaleItem newItem;

    itemsForSale = new List<StructSaleItem>();
    saleItemPrefab = tradingInterface.itemForSalePrefab;

    for (var i = 0; i < GameCore.knownItemTypes.Count; i++) {
      knownItem = GameCore.knownItemTypes[i];
      price = UnityEngine.Random.Range(knownItem.itemLow, knownItem.itemHigh);

      newItem = new StructSaleItem ();
      newItem.itemName = knownItem.itemName;
      newItem.itemPrice = price;

      itemsForSale.Add(newItem);
    }
  }

  void InitTradingMenuItems () {
    for (int i = 0; i < 3; i++) {
      StructSaleItem inventoryItem = itemsForSale[i];

      InitTradingMenuItem(inventoryItem, i * -40);
    }
  }

  void InitTradingMenuItem (StructSaleItem saleItem, int offset) {
    Image menuItem = (Image) Instantiate(saleItemPrefab, saleItemPrefab.transform.position + new Vector3(0, offset, 0), Quaternion.identity);
    menuItem.transform.SetParent (tradingMenu.transform, false);

    MenuItemForSale menuInterface = menuItem.GetComponent<MenuItemForSale>();
    menuInterface.itemName.text = saleItem.itemName;
    menuInterface.itemPrice.text = saleItem.itemPrice.ToString();

    menuInterface.buyButton.onClick.AddListener(() => {
      HandleBuyItem(saleItem);
    });
  }

  StructInventoryItem GetInventoryItem(string itemName) {
    StructInventoryItem inventoryItem;
    StructInventoryItem emptyItem;

    if (!PlayerCore.self.inventory.ContainsKey(itemName)) {
      emptyItem = new StructInventoryItem();

      emptyItem.itemName = itemName;
      emptyItem.itemQty = 0;

      PlayerCore.self.inventory.Add(itemName, emptyItem);
    }

    inventoryItem = PlayerCore.self.inventory[itemName];

    return inventoryItem;
  }

  void HandleBuyItem(StructSaleItem saleItem) {
    string itemName;

    itemName = saleItem.itemName;
    StructInventoryItem inventoryItem = GetInventoryItem(itemName);

    Debug.Log ("Ok, it's a deal! 1 " + itemName + " for $" + saleItem.itemPrice);

    PlayerCore.self.money -= saleItem.itemPrice;

    inventoryItem.itemQty += 1;
    PlayerCore.self.inventory[itemName] = inventoryItem;

    Debug.Log("Bank Balance: $" + PlayerCore.self.money);
    Debug.Log("Player now has: " + inventoryItem.itemQty + " " + itemName + "(s)");

    GameCore.self.Save();
  }

  // Show Hide
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
