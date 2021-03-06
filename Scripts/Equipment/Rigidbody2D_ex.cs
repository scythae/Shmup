﻿using UnityEngine;

public class Rigidbody2D_ex : MonoBehaviour {
	private Vector2 savedVelocity;

	void Start () {
		SaveVelocity (); // delete this string if need optimization
	}

	private Rigidbody2D GetRigidbody2D () {
		return this.gameObject.GetComponent<Rigidbody2D> ();
	}

	private void SaveVelocity() {
		Rigidbody2D rigidbody2D = GetRigidbody2D ();

		if (rigidbody2D != null) {
			savedVelocity = rigidbody2D.velocity;
		}
	}

	private void LoadVelocity() {
		Rigidbody2D rigidbody2D = GetRigidbody2D ();

		if (rigidbody2D != null) {
			rigidbody2D.velocity = savedVelocity;
		}
	}

	private void SetPaused(bool paused) {
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

	public static void SetPaused(Rigidbody2D rb, bool paused) {
		Rigidbody2D_ex ex = rb.gameObject.GetComponent<Rigidbody2D_ex>();
		if (!ex)
			ex = rb.gameObject.AddComponent<Rigidbody2D_ex>();
		
		ex.SetPaused(paused);
	}

	public static void SetScaledVelocity(Rigidbody2D rigidbody2D, Vector2 velocity) {
		velocity.x *= rigidbody2D.gameObject.transform.localScale.x;
		velocity.y *= rigidbody2D.gameObject.transform.localScale.y;
		rigidbody2D.velocity = velocity;
	}
}