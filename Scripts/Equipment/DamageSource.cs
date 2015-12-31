using UnityEngine;

public class DamageSource : MonoBehaviour {
	public float damage = 0;
	public UnitSide target = UnitSide.usNone;

	public static T AddToGameObject<T>(GameObject gameObject, float damage, UnitSide target) where T : DamageSource {
		T result = gameObject.AddComponent<T> ();
		result.damage = damage;
		result.target = target;
		return result;
	}

	public static void DestroyAllMaintainedGameobjects () {
		foreach (DamageSource damageSource in FindObjectsOfType<DamageSource> ()) {
			Destroy (damageSource.gameObject);
		}
	}
}

public class DS_Bullet : DamageSource {
}

public class DS_Ship : DamageSource {
}