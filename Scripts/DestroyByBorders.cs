﻿using UnityEngine;
using System.Collections;

public class DestroyByBorders : MonoBehaviour {
	void OnTriggerExit2D(Collider2D other) {
		Destroy (other.gameObject);
	}
}
