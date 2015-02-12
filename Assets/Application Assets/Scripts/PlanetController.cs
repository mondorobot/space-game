using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetController : MonoBehaviour {
	public GameObject planetMenuPrefab;
	private PlanetLandingMenuInterface planetMenu;
	private GameObject menuInstance;
	
	public void OnTriggerEnter (Collider col) {
		if (menuInstance == null && col.gameObject.tag == "Player") {
			menuInstance = (GameObject) Instantiate (planetMenuPrefab, transform.position + new Vector3(-5f,5f,-5f), planetMenuPrefab.transform.rotation);
			planetMenu = menuInstance.GetComponent<PlanetLandingMenuInterface> ();

			planetMenu.landButton.onClick.AddListener (() => {
				col.gameObject.rigidbody.velocity = Vector3.zero;
				Debug.Log ("Roger that. You are cleared for landing.");
			});
		}
	}

	public void OnTriggerExit (Collider col) {
		if (menuInstance != null && col.gameObject.tag == "Player") {
			Destroy(menuInstance);
		}
	}
}