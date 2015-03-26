using Assets.Scripts.lib.Utilities;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
	public class RangedWeaponBehavior : MonoBehaviour
	{
        [SerializeField]
        private int _rangeReduction;
        [SerializeField]
        private int _maxDistance;

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
 
		protected virtual void FixedUpdate()
		{
			if (GameUtility.Player == null || Vector3.Distance(GameUtility.Player.transform.position, transform.position) > MaxDistance) {
				Destroy(gameObject);
			}
		}
	}
}
