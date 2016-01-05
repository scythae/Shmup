using UnityEngine;
using System.Collections;

namespace Weapon {
	
public class EnemySimpleGun : Weapon {
	protected override void Start () {
		base.Start ();
		bulletTemplate = Prefab.enemyBullet;
		bulletSpeed = -5;
		reloadTime = 1f;
		damage = 1;
		Shooting = true;
	}

	protected override void AddComponentsToBullet(GameObject bullet) {
		base.AddComponentsToBullet (bullet);
		DamageSource.AddToGameObject<DS_Bullet> (bullet, this.damage, UnitSide.usPlayer);
	}
}

}