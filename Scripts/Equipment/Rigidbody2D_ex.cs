using UnityEngine;

public class Rigidbody2D_ex : MonoBehaviour {
	private Vector2 savedVelocity;

	void Start () {
		SaveVelocity (); // delete this string if need optimization
	}

	private Rigidbody2D GetRigidbody2D () {
		return this.gameObject.GetComponent<Rigidbody2D> ();
	}

	public void SaveVelocity() {
		Rigidbody2D rigidbody2D = GetRigidbody2D ();

		if (rigidbody2D != null) {
			savedVelocity = rigidbody2D.velocity;
		}
	}

	public void LoadVelocity() {
		Rigidbody2D rigidbody2D = GetRigidbody2D ();

		if (rigidbody2D != null) {
			rigidbody2D.velocity = savedVelocity;
		}
	}

	public void SetPaused(bool paused) {
		if (paused) {
			SaveVelocity ();
			GetRigidbody2D ().velocity = Vector2.zero;
		} else {
			LoadVelocity ();
		}
	}

	public static void SetAllPaused(bool paused) {
		foreach (Rigidbody2D_ex rigidbody2D_ex in FindObjectsOfType<Rigidbody2D_ex> ()) {
			rigidbody2D_ex.SetPaused (paused);
		}
	}
}