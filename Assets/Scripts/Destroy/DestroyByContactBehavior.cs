﻿using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Health;

namespace Assets.Scripts.Destroy
{
    public class DestroyByContactBehavior : BaseDamage
    {
        public IDictionary<ResistanceType, Resistance> Resistances { get; set; }

        public double GetDamageReduction(DamageType type)
        {
            var dr = 0;
            foreach (var resistance in Resistances)
            {
                if (resistance.Value.DamageType == type)
                    dr += resistance.Value.Quantity;
            }

            return 3*(.25 - ((75/(dr + 150))^2));
        }

//        public void Damage(DamageType type, int quantity)
//        {
//            Life = Life - (int)(quantity*GetDamageReduction(type));
//        }
		void DoDamage(Collider col) 
		{
//			var col = collision.collider;
			if (col.tag != gameObject.tag &&  Owner != col.gameObject.GetInstanceID()) { //asteroids don't hurt each other
				var health = col.gameObject.GetComponent<HealthBehavior>();
				
				if (health != null) {
					health.CurrentLife -= base.GetDamage();
					if (health.CurrentLife <= 0) {
						col.gameObject.GetComponent<HealthBehavior>().SendMessage("Update");
						Destroy(col.gameObject);
						
					}
				}
			}
			
			//bullets die on impact
			if (gameObject.tag == "_bullet" && col.tag != "Player" && col.tag != "Enemy")
				Destroy(gameObject);
		}

        void OnCollisionEnter(Collision collision) //can be asteroid, bullet
        {

			DoDamage (collision.collider);
			// and randomly create a new one somewhere else
            //GameObject asteroid = (GameObject) Instantiate(asteroidPrefab, new Vector3(Random.Range (-80f, 80f), 0, Random.Range (-80f, 80f)), Quaternion.identity);
            //asteroid.transform.localScale = Vector3.one * Random.Range (0.5f, 2.5f);
        }

		void OnTriggerEnter(Collider col) {
			DoDamage (col);
		}
    }
}
