using System;
using UnityEngine;

namespace Assets.Scripts.lib.Behaviors
{
	public class CollisionBehavior : MonoBehaviour
	{
		public CollisionBehavior ()
		{
		}
		void OnTriggerEnter(Collider col)
		{
			var damage = col.gameObject.GetComponent<IDamage>();
			if (damage != null && damage.WhosWeapon () != ProjectileOwner.Self) {
				gameObject.GetComponent<Rigidbody>().AddForce(100f * col.gameObject.transform.forward);
				gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, col.gameObject.transform.position, 0);
			}
		}

	}
}

