using UnityEngine;
using System.Collections;

public class Weapon_PlayerSimpleGun : Weapon {
	protected override void Start () {
		base.Start ();
		bulletTemplate =  (GameObject) Resources.Load("pf_PlayerBullet");
		bulletSpeed = 15;
		reloadTime = 0.25f;
		damage = 1;
	}

	protected override void AddComponentsToBullet(GameObject bullet) {
		base.AddComponentsToBullet (bullet);
		DamageSource.AddToGameObject (bullet, this.damage, UnitSide.usEnemy, true);
	}
}