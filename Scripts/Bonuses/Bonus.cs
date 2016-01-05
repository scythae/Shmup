using UnityEngine;

public class Bonus: MonoBehaviour {
	protected const float unlimitedDuration = 0;
	protected const float zeroDropChance = 0;
	protected Ship interactedShip = null;

	public float duration = unlimitedDuration;
	public float dropChance = zeroDropChance;
	public GameObject prefab = null;

	public static T AddToGameObject<T>(GameObject gameObject) where T : Bonus {
		T bonus = gameObject.AddComponent<T> ();
		return bonus;
	}

	public virtual bool OnInteract(GameObject interactor) {
		interactedShip = interactor.GetComponent<Ship> ();
		if (interactedShip == null) {
			return false;
		}

		DestroyUnwantedComponents ();

		if (duration != unlimitedDuration) {
			Invoke ("OnFinishDuration", duration);
		} else {
			Destroy (this.gameObject);
		}

		return true;
	}

	void DestroyUnwantedComponents () {
		DestroyComponents<Renderer> ();
		DestroyComponents<Collider2D> ();
		DestroyComponents<Rigidbody2D> ();
	}

	void DestroyComponents<T> () where T : Component{
		foreach (T component in gameObject.GetComponents<T> ()) {
			Destroy (component);
		}
	}

	protected virtual void OnFinishDuration() {
		Destroy (this.gameObject);
	}
}