
using Assets.Scripts.lib.Utilities;
using UnityEngine;

namespace Assets.Scripts.lib.Damage.Weapons
{
    public interface IDamage
    {
        int GetDamage();
    }
    public abstract class Weapon : MonoBehaviour, IDamage
    {
        public int BaseDamage { get; set; }
        public virtual decimal RangeReduction { get; set; } //per 10
        public virtual int MaxDistance { get; set; }
        private GameObject _player;

        public virtual int GetDamage()
        {
            int range = 0; //TODO: maybe add range, not sure how yet
            return (int) (BaseDamage - (RangeReduction * (range / 10)));
        }

        protected virtual void DeleteAfterMaxRange()
        {
            
        }

        protected virtual void FixedUpdate()
        {
            var dist = Vector3.Distance(GameUtility.Player.transform.position, transform.position);

            if (dist > MaxDistance)
            {
                Debug.Log("Destroying " + gameObject.name);
                Destroy(gameObject);
            }
        }
    }
}
