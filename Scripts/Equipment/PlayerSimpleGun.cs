using UnityEngine;
using System.Collections;
	
public class PlayerSimpleGun : Equipment {
	protected override void Start () {
		base.Start ();
		bulletTemplate = Prefab.playerBullet;
		bulletSpeed = 15;
		reloadTime = 0.25f;
		damage = 1;
	}

	protected override void AddComponentsToBullet(GameObject bullet) {
		base.AddComponentsToBullet (bullet);
		DamageSource.AddToGameObject<DS_Bullet> (bullet, this.damage, UnitSide.usEnemy);
		PlaySound.BulletShot();
	}
}
