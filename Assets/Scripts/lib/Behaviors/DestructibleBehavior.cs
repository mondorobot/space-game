using System.Collections.Generic;
using Assets.Scripts.lib.Damage;
using UnityEngine;

namespace Assets.Scripts.lib.Behaviors
{
    public interface IDestructibleObject
    {
        int Life { get; set; }
        void Damage(DamageType type, int quantity);
    }

    public class DestructibleBehavior : MonoBehaviour, IDestructibleObject
    {
        [SerializeField] private int _life;

        public IDictionary<ResistanceType, Resistance> Resistances { get; set; }

        public int Life
        {
            get { return _life; }
            set { _life = value; }
        }

        protected virtual double GetDamageReduction(DamageType type)
        {
            var dr = 0;
            foreach (var resistance in Resistances)
            {
                if (resistance.Value.DamageType == type)
                    dr += resistance.Value.Quantity;
            }

            return 3*(.25 - ((75/(dr + 150))^2));
        }

        public void Damage(DamageType type, int quantity)
        {
            Life = Life - (int)(quantity*GetDamageReduction(type));
        }

        void OnTriggerEnter(Collider col)
        {
			var damage = col.gameObject.GetComponent<IDamage>();
            if (damage != null && damage.WhosWeapon() != ProjectileOwner.Self)
            {
                //Instantiate(sparksPrefab, transform.position, Quaternion.identity);

                Life -= damage.GetDamage();
                if (Life <= 0)
                    Destroy(gameObject);
                else 
                {
					Destroy(col.gameObject);
                }
            }

            // and randomly create a new one somewhere else
            //GameObject asteroid = (GameObject) Instantiate(asteroidPrefab, new Vector3(Random.Range (-80f, 80f), 0, Random.Range (-80f, 80f)), Quaternion.identity);
            //asteroid.transform.localScale = Vector3.one * Random.Range (0.5f, 2.5f);
        }
    }
}
