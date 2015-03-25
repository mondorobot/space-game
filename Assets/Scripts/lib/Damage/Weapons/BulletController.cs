using UnityEngine;

namespace Assets.Scripts.lib.Damage.Weapons
{
    public class BulletController : Weapon {

        public BulletController()
        {
            MaxDistance = 50;
            BaseDamage = 1;
        }

        private GameObject player;

        protected virtual void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
