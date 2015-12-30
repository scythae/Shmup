using UnityEngine;
using System.Collections;

public class Weapon_EnemySimpleGun : Weapon {
	protected override void Start () {
		base.Start ();
		bulletTemplate =  (GameObject) Resources.Load("pf_EnemyBullet");
		bulletSpeed = -5;
		reloadTime = 1f;
		damage = 1;
		Shooting = true;
	}

	protected override void AddComponentsToBullet(GameObject bullet) {
		base.AddComponentsToBullet (bullet);
		DamageSource.AddToGameObject (bullet, this.damage, UnitSide.usPlayer, true);
	}
}