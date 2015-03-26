using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Health {

	public class HealthBehavior : MonoBehaviour
	{
		public Slider slider;
		public Text gameOver;
		public Button playAgain;
		public Text playAgainText;

		public int Life;
		[HideInInspector]
		public int CurrentLife;

		void Start() {
			if (gameOver != null) {
				gameOver.enabled = false;
				playAgain.onClick.AddListener (() => {
					GameCore.self.InitNewGame();
				});
				playAgain.enabled = false;
				playAgainText.enabled = false;
			}

		}
		void Awake() {
			CurrentLife = Life;
		}

		public void TakeDamage(int removeLife){
			CurrentLife -= removeLife;
		}

		void Update() {
			if (slider != null) {
				slider.value =  (100 * (CurrentLife / (float)Life));

				if (CurrentLife <= 0)
				{
					gameOver.enabled = true;
					playAgain.enabled = true;
					playAgainText.enabled = true;
				}
			}
		}
	}
}

