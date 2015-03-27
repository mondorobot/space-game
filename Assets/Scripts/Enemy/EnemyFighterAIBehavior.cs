using UnityEngine;
using Assets.Scripts.Destroy;
using Assets.Scripts.Weapon;
using System.Collections.Generic;
using System.Linq;

using System;
namespace Assets.Scripts.Emeny
{
	public class EnemyFighterAIBehavior :MonoBehaviour
	{
		public int Range;
		public float FireRate;
		public Rigidbody Weapon;

		private float nextFire = 2.0f;

		public EnemyFighterAIBehavior ()
		{

		}

		public void AcquireTarget ()
		{
			var target = GameObject.FindGameObjectsWithTag ("Player").
				OrderBy (go => Vector3.Distance (go.transform.position, transform.position)).FirstOrDefault ();
			 
			float dist = Vector3.Distance (target.transform.position, transform.position);
	         
			if (dist <= Range) {
				var direction = (transform.position - target.transform.position).normalized;
				var rotation = Quaternion.LookRotation (direction);

				if (Time.time > nextFire) {
					Rigidbody bullet = (Rigidbody)Instantiate (Weapon, transform.position, Quaternion.identity);
					
					bullet.GetComponent<DestroyByContactBehavior> ().Owner = gameObject.GetInstanceID ();// ProjectileOwner.Self;
					bullet.transform.Rotate (rotation.eulerAngles + new Vector3 (0, 90, 0));
					bullet.GetComponent<Rigidbody> ().AddForce (1000f * (target.transform.position - transform.position).normalized);
					nextFire = Time.time + UnityEngine.Random.Range (1.0f, 2.0f);
				}

				if (dist > 10) {
					transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 30);
					transform.position = Vector3.MoveTowards (transform.position, target.transform.position, 8 * Time.deltaTime);
				}
			}
		}

		void Update ()
		{
			AcquireTarget ();
		}

		public void DamageTarget ()
		{
		}

		public void ChaseTarget ()
		{

		}

	}
}

