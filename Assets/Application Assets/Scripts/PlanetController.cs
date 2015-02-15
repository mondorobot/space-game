using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlanetController : MonoBehaviour {
	private GameEngine gameEngine;

	public GameObject landingMenuPrefab;
	private PlanetLandingInterface landingInterface;
	private GameObject landingMenu;

	private PlanetTradingInterface tradingInterface;
	private GameObject tradingMenu;

	void Awake () {
		GameObject mainEngine = GameObject.FindGameObjectWithTag ("GameController");
		gameEngine = mainEngine.GetComponent<GameEngine> ();

		tradingMenu = gameEngine.tradingScreen;
		tradingInterface = gameEngine.tradingScreen.GetComponent<PlanetTradingInterface> ();

		landingMenu = (GameObject) Instantiate (landingMenuPrefab, transform.position + new Vector3(10f,4f,-5f), landingMenuPrefab.transform.rotation);
		landingMenu.SetActive (false);

		landingInterface = landingMenu.GetComponent<PlanetLandingInterface> ();
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

	private void ShowTradingMenu (GameObject player) {
		HideLandingMenu ();

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