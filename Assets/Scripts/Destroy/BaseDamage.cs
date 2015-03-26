using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Destroy {

	public enum ProjectileOwner {
		Self,
		Enemy,
		Environment
	}

	public class BaseDamage : MonoBehaviour {
		public int Damage;
		public DamageType DamageType;
		[HideInInspector]
		public ProjectileOwner Owner;
		
		public int GetDamage()
		{
			return Damage;
		}
	}
}

