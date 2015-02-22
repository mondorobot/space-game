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

    for (var i = 0; i < GameEngine.knownItemTypes.Count; i++) {
      knownItem = GameEngine.knownItemTypes[i];
      price = Random.Range(knownItem.itemLow, knownItem.itemHigh);

      newItem = new StructSaleItem ();
      newItem.itemName = knownItem.itemName;
      newItem.itemPrice = price.ToString();

      itemsForSale.Add(newItem);
    }
  }

  void InitTradingMenuItems () {
    for (int i = 0; i < 3; i++) {
      StructSaleItem inventoryItem = itemsForSale[i];

      InitTradingMenuItem(inventoryItem, i * -40);
    }
  }

  void InitTradingMenuItem (StructSaleItem inventoryItem, int offset) {
    Image menuItem = (Image) Instantiate(saleItemPrefab, saleItemPrefab.transform.position + new Vector3(0, offset, 0), Quaternion.identity);
    menuItem.transform.SetParent (tradingMenu.transform, false);

    MenuItemForSale menuInterface = menuItem.GetComponent<MenuItemForSale>();
    menuInterface.itemName.text = inventoryItem.itemName;
    menuInterface.itemPrice.text = inventoryItem.itemPrice;

    menuInterface.buyButton.onClick.AddListener(() => {
      Debug.Log("here we go price tag, here we go!");
      Debug.Log(inventoryItem.itemPrice);
      Debug.Log(inventoryItem.itemName);
    });
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
