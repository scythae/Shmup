using UnityEngine;

public class Bonus: MonoBehaviour {
	public enum DamageTarget {dtNone, dtPlayer, dtEnemy, dtBoth};

	public float damage = 0;
	public bool isBullet = false;
	public UnitSide target = UnitSide.usNone;

	public static DamageSource AddToGameObject(GameObject gameObject, float damage, UnitSide target, bool isBullet = false ) {
		DamageSource bodyDamage = gameObject.AddComponent<DamageSource> ();
		bodyDamage.damage = damage;
		bodyDamage.target = target;
		bodyDamage.isBullet = isBullet;
		return bodyDamage;
	}
}