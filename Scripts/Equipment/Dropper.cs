using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Weapon {
	public class Dropper : Weapon{
		private float minBonusSpeed = -2;
		private float maxBonusSpeed = -3;

		public Dropper() : base () {
			content = new List<DropInfo>();
		}

		private struct DropInfo {
			public GameObject prefab;
			public float dropChance;
		}

		private List<DropInfo> content;

		protected override void Start () {
			base.Start ();
			reloadTime = 0;
			damage = 0;
			Shooting = false;
		}

		public Dropper AddDrop(GameObject prefab, float dropChance) {
			DropInfo dropInfo;
			dropInfo.prefab = prefab;
			dropInfo.dropChance = dropChance;
			content.Add(dropInfo);

			return this;
		}

		public void Drop () {		
			foreach (DropInfo dropInfo in content) {
				GameObject bonusTemplate = dropInfo.prefab;
				Bonus bonus = bonusTemplate.GetComponent<Bonus> ();
				if (bonus == null)
					continue;

				if (Random.value < dropInfo.dropChance) {
					bulletTemplate = bonusTemplate;
					bulletSpeed = Random.Range(minBonusSpeed, maxBonusSpeed);
					Shot ();
				}
			}
		}
	}
}