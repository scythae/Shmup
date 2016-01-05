using UnityEngine;
using System.Collections;

namespace Weapon {
	public class Dropper : Weapon{
		public GameObject[] Drop;

		protected override void Start () {
			base.Start ();
			reloadTime = 0;
			damage = 0;
			Shooting = false;
		}

		void OnDestroy () {		
			foreach (GameObject bonusTemplate in Drop) {
				Bonus bonus = bonusTemplate.GetComponent<Bonus> ();
				if (bonus == null)
					continue;

				if (Random.value < bonus.dropChance) {
					bulletTemplate = bonusTemplate;
					bulletSpeed = Random.Range(-5, -10);
					Shot ();
				}
			}
		}
	}
}