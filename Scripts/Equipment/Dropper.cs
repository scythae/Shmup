using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dropper : Equipment{
	private float minBonusSpeed = 2;
	private float maxBonusSpeed = 3;

	protected override void Start () {
		base.Start ();
		reloadTime = 0;
		damage = 0;
		Shooting = false;
	}

	public void SetDrop(GameObject prefab) {
		this.bulletTemplate = prefab;
	}

	public void Drop () {
		Bonus bonus = bulletTemplate.GetComponent<Bonus> ();
		if (bonus == null)
			return;

		bulletSpeed = Random.Range(minBonusSpeed, maxBonusSpeed);
		Shot ();
	}
}

public class Dropper<T> : Dropper where T: Bonus{

}