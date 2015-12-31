using UnityEngine;

public class Bonus: MonoBehaviour {
	public static T AddToGameObject<T>(GameObject gameObject) where T : Bonus {
		T bonus = gameObject.AddComponent<T> ();
		bonus.Initialize ();
		return bonus;
	}

	protected virtual void Initialize () {
	}

	public virtual void OnInteract(GameObject interactor) {

	}
}