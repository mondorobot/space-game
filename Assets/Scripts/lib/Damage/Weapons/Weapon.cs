
using Assets.Scripts.lib.Utilities;
using UnityEngine;

namespace Assets.Scripts.lib.Damage.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public int BaseDamage { get; set; }
        public virtual decimal RangeReduction { get; set; } //per 10
        public virtual int MaxDistance { get; set; }
        private GameObject _player;

        protected virtual int GetDamage(int range)
        {
            return (int) (BaseDamage - (RangeReduction * (range / 10)));
        }

        protected virtual void DeleteAfterMaxRange()
        {
            
        }

        protected virtual void FixedUpdate()
        {
			Debug.Log ("Weapon Fixed Update");
            var dist = Vector3.Distance(GameUtility.Player.transform.position, transform.position);

            if (dist > MaxDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}
