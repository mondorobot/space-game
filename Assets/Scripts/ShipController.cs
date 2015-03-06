using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipController : MonoBehaviour {
  public Rigidbody laserPrefab;
  public GameObject hyperspaceMenuPrefab;
  public GameObject hyperspaceMenu;

  void Start() {
    hyperspaceMenu = (GameObject) Instantiate(hyperspaceMenuPrefab, Vector3.zero, Quaternion.identity);
  }

  void Update () {
    if (Input.GetKeyDown((KeyCode.Space))) {
      Debug.Log ("shoot!");
      Rigidbody bullet = (Rigidbody) Instantiate(laserPrefab, transform.position, Quaternion.identity);

      bullet.transform.Rotate(transform.rotation.eulerAngles);
      bullet.GetComponent<Rigidbody>().AddForce(1000f * transform.right);
    }

    float rotateScale = 185f;
    float rotateSpeed = rotateScale * Time.deltaTime;

    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
      GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
      transform.Rotate(Vector3.down * rotateSpeed);
    }

    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
      GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
      transform.Rotate(Vector3.up * rotateSpeed);
    }

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
      GetComponent<Rigidbody>().AddForce(transform.right * 5f);
    }

    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
      GetComponent<Rigidbody>().AddForce(transform.right * -5f);
    }

    Camera.main.transform.position = new Vector3 (transform.position.x - 20, 40, transform.position.z - 20);
    Camera.main.transform.LookAt (transform);
  }
}
