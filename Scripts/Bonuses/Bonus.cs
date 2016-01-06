using UnityEngine;

public class Bonus: MonoBehaviour {
	public float dropChance = 0;
	public GameObject prefab = null;

	public static T AddToGameObject<T>(GameObject gameObject) where T : Bonus {
		T bonus = gameObject.AddComponent<T> ();
		return bonus;
	}

	public virtual void Interact(GameObject interactor) {
		DestroyComponents<Renderer> ();
		DestroyComponents<Collider2D> ();
		DestroyComponents<Rigidbody2D> ();
		Destroy (this.gameObject);
	}


	void DestroyComponents<T> () where T : Component{
		foreach (T component in gameObject.GetComponents<T> ()) {
			Destroy (component);
		}
	}
}