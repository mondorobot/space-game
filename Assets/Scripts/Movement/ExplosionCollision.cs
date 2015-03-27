using System;
using UnityEngine;

namespace Assets.Scripts.Movement
{
	public class ExplosionCollision : MonoBehaviour
	{		
		void OnTriggerEnter(Collider col)
		{
			if (col.tag == "_bullet") {
				gameObject.GetComponent<Rigidbody> ().AddExplosionForce (1000f, col.gameObject.transform.position, 0);
				gameObject.GetComponent<Rigidbody> ().AddForce (100f * gameObject.transform.forward);
			} //else {
//			
//			}
//			if (col.tag == "Player") {
//				gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, col.gameObject.transform.position, 0);
//				gameObject.GetComponent<Rigidbody>().AddForce(100f * gameObject.transform.forward);
//			}
				
		}
	}
}

