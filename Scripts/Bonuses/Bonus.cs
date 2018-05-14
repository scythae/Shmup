using UnityEngine;

public class Bonus: MonoBehaviour {
	public virtual void Interact(GameObject interactor) {
		Destroy (this.gameObject);
	}
}