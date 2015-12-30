using UnityEngine;

public class DamageSource : MonoBehaviour {
	public float damage = 0;
	public bool isBullet = false;
	public UnitSide target = UnitSide.usNone;

	public static DamageSource AddToGameObject(GameObject gameObject, float damage, UnitSide target, bool isBullet = false ) {
		DamageSource result = gameObject.AddComponent<DamageSource> ();
		result.damage = damage;
		result.target = target;
		result.isBullet = isBullet;
		return result;
	}

	public static void DestroyAllMaintainedGameobjects () {
		foreach (DamageSource damageSource in FindObjectsOfType<DamageSource> ()) {
			Destroy (damageSource.gameObject);
		}
	}
}

public class Bullet : DamageSource {
	void Start () {
		isBullet = true;
	}
}
