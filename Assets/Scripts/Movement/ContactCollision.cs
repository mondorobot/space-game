using System;
using UnityEngine;

namespace Assets.Scripts.Movement
{
	public class ContactCollision : MonoBehaviour
	{
		void OnTriggerEnter(Collider col) //bullet or asteroid or ship
		{
//			var exploder = col.GetComponent<ExplosionCollision> (); //must be bullet
//			if (exploder == null) {
//				gameObject.GetComponent<Rigidbody>().AddForce(100f * gameObject.transform.forward);
//
//			}
				


		}

//		void OnCollisionEnter(Collision col) {
//			var exploder = col.GetComponent<ExplosionCollision> (); //must be bullet
//			if (exploder == null) {
//				gameObject.GetComponent<Rigidbody>().AddForce(100f * col.gameObject.transform.forward,ForceMode.Impulse);
//
//		}
	}
}

