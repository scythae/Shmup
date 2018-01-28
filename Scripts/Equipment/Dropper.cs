﻿using UnityEngine;
using System.Collections;

namespace Weapon {
	public class Dropper : Weapon{
		private float minBonusSpeed = -2;
		private float maxBonusSpeed = -3;

		public GameObject[] content;

		protected override void Start () {
			base.Start ();
			reloadTime = 0;
			damage = 0;
			Shooting = false;
		}

		public void Drop () {		
			foreach (GameObject bonusTemplate in content) {
				Bonus bonus = bonusTemplate.GetComponent<Bonus> ();
				if (bonus == null)
					continue;

				if (Random.value < bonus.dropChance) {
					bulletTemplate = bonusTemplate;
					bulletSpeed = Random.Range(minBonusSpeed, maxBonusSpeed);
					Shot ();
				}
			}
		}
	}
}