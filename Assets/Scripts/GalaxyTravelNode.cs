using UnityEngine;
using System.Collections;

public class GalaxyTravelNode : MonoBehaviour {
  public string galaxyName;
  public string sceneName;
  public GameObject travelMenuPrefab;
  private bool isActive;
  private GameObject travelMenu;
  private GalaxyTravelMenu travelInterface;

  void Awake() {
    travelMenu = (GameObject) Instantiate (travelMenuPrefab, transform.position + new Vector3(0, 1, 0), travelMenuPrefab.transform.rotation);
    travelMenu.SetActive (false);
    isActive = false;

    travelInterface = travelMenu.GetComponent<GalaxyTravelMenu> ();
    travelInterface.nodeName.text = galaxyName;
    travelInterface.enterButton.onClick.AddListener (() => {
      Application.LoadLevel(sceneName);
    });
  }

  void OnMouseDown () {
    GameObject[] nodes = GameObject.FindGameObjectsWithTag ("_galaxy_travel_node");

    for (int i = 0; i < nodes.Length; i++) {
      GalaxyTravelNode node = nodes[i].GetComponent<GalaxyTravelNode> ();
      node.HideMenu();
    }

    isActive = !isActive;
    travelMenu.SetActive (isActive);
  }

  public void HideMenu () {
    isActive = false;
    travelMenu.SetActive (isActive);
  }
}
