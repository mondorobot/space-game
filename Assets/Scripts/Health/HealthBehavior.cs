using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Health {

	public class HealthBehavior : MonoBehaviour
	{
		public Slider slider;

		public int Life;
		[HideInInspector]
		public int CurrentLife;

		void Start() {
			CurrentLife = Life;
		}

		public void TakeDamage(int removeLife){
			CurrentLife -= removeLife;
		}

		void Update() {
			if (slider != null) {
				slider.value =  (100 * (CurrentLife / (float)Life));
			}
		}
	}
}

