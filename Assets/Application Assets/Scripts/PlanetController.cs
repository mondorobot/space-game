using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

struct StructSaleItem {
	public string itemName;
	public string itemPrice;
};

public class PlanetController : MonoBehaviour {
	private GameEngine gameEngine;

	public GameObject landingMenuPrefab;
	private PlanetLandingInterface landingInterface;
	private GameObject landingMenu;

	private PlanetTradingInterface tradingInterface;
	private GameObject tradingMenu;

	private Image saleItemPrefab;
	private StructSaleItem[] itemsForSale;

	void Awake () {
		GameObject mainEngine = GameObject.FindGameObjectWithTag ("GameController");
		gameEngine = mainEngine.GetComponent<GameEngine> ();

		tradingMenu = gameEngine.tradingScreen;
		tradingInterface = gameEngine.tradingScreen.GetComponent<PlanetTradingInterface> ();

		float scale = transform.localScale.x / 2;
		landingMenu = (GameObject) Instantiate (landingMenuPrefab, transform.position + new Vector3(scale,scale,scale*-1f), landingMenuPrefab.transform.rotation);
		landingMenu.SetActive (false);

		landingInterface = landingMenu.GetComponent<PlanetLandingInterface> ();

		saleItemPrefab = tradingInterface.itemForSalePrefab;

		itemsForSale = new StructSaleItem[3];

		itemsForSale[0] = new StructSaleItem ();
		itemsForSale[0].itemName = "Obsidian Ore";
		itemsForSale[0].itemPrice = "100";

		itemsForSale[1] = new StructSaleItem ();
		itemsForSale[1].itemName = "Food";
		itemsForSale[1].itemPrice = "25";

		itemsForSale[2] = new StructSaleItem ();
		itemsForSale[2].itemName = "Cotton";
		itemsForSale[2].itemPrice = "37";
	}

	public void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			ShowLandingMenu (col.gameObject);
		}
	}
	
	public void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Player") {
			HideLandingMenu ();
		}
	}
	
	private void ShowLandingMenu (GameObject player) {
		landingMenu.SetActive (true);

		landingInterface.enterButton.onClick.AddListener (() => {
			player.rigidbody.velocity = Vector3.zero;
			player.rigidbody.angularVelocity = Vector3.zero;
			player.SetActive(false);

			Debug.Log ("Roger that. You are cleared for landing.");
			
			ShowTradingMenu (player);
		});
	}
	
	private void HideLandingMenu () {
		landingInterface.enterButton.onClick.RemoveAllListeners ();
		landingMenu.SetActive (false);
	}

	private void InitTradingMenu () {
		for (int i = 0; i < 3; i++) {
			StructSaleItem inventoryItem = itemsForSale[i];

			InitTradingMenuItem(inventoryItem, i * -40);
		}
	}

	private void InitTradingMenuItem (StructSaleItem inventoryItem, int offset) {
		Image menuItem = (Image) Instantiate(saleItemPrefab, saleItemPrefab.transform.position + new Vector3(0, offset, 0), Quaternion.identity);
		menuItem.transform.SetParent (tradingMenu.transform, false);
		
		MenuItemForSale menuInterface = menuItem.GetComponent<MenuItemForSale>();
		menuInterface.itemName.text = inventoryItem.itemName;
		menuInterface.itemPrice.text = inventoryItem.itemPrice;
		
		menuInterface.buyButton.onClick.AddListener(() => {
			Debug.Log ("here we go price tag, here we go!");
			Debug.Log (inventoryItem.itemPrice);
		});
	}

	private void ShowTradingMenu (GameObject player) {
		HideLandingMenu ();
		InitTradingMenu ();

		tradingMenu.SetActive (true);

		tradingInterface.exitButton.onClick.AddListener (() => {
			Debug.Log ("Bye bye.");
			HideTradingMenu (player);
		});
	}
	
	private void HideTradingMenu (GameObject player) {
		tradingInterface.exitButton.onClick.RemoveAllListeners ();

		tradingMenu.SetActive (false);
		player.SetActive(true);
	}
}