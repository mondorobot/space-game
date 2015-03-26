using System;
using UnityEngine;

namespace Assets.Scripts.Movement
{
	public class ExplosionCollision : MonoBehaviour
	{		
		void OnTriggerEnter(Collider col)
		{
			if (col.tag == "Asteroid")
				gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, col.gameObject.transform.position, 0);
		}
	}
}

