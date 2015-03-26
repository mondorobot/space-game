using Assets.Scripts.lib.Damage;
using Assets.Scripts.lib.Utilities;
using UnityEngine;

namespace Assets.Scripts.lib.Behaviors
{
	public enum ProjectileOwner {
		Self,
		Enemy,
		Environment
	}
	public interface IDamage
	{
		int GetDamage();
		ProjectileOwner WhosWeapon();
	}
	public class WeaponBehavior : MonoBehaviour, IDamage
	{
	    [SerializeField] 
        private int _baseDamage;
        [SerializeField]
        private int _rangeReduction;
        [SerializeField]
        private int _maxDistance;
	    [SerializeField] 
        private DamageType _damageType;
		public ProjectileOwner Owner;

		private GameObject _player;

	    public int MaxDistance
	    {
	        get { return _maxDistance; }
	        set { _maxDistance = value; }
	    }

	    public int RangeReduction
	    {
	        get { return _rangeReduction; }
	        set { _rangeReduction = value; }
	    }

	    public int BaseDamage
	    {
	        get { return _baseDamage; }
	        set { _baseDamage = value; }
	    }

	    public DamageType DamageType
	    {
	        get { return _damageType; }
	        set { _damageType = value; }
	    }

		public ProjectileOwner WhosWeapon(){
			return Owner;
		}

	    public virtual int GetDamage()
		{
			var range = 0; //TODO: maybe add range, not sure how yet
			return (BaseDamage - (RangeReduction * (range / 10)));
		}
	
		protected virtual void FixedUpdate()
		{
			var dist = Vector3.Distance(GameUtility.Player.transform.position, transform.position);
			
			if (dist > MaxDistance)
			{
				if ( gameObject != null )
				    Destroy(gameObject);
			}
		}
	}
}
