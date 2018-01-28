using System;
using System.Collections.Generic;
using UnityEngine;

public class StageDecoration : MonoBehaviour{
	private void Update () {				

	}

	public static StageDecoration AddTo (GameObject parentGameObject) {		
		GameObject gameObject = Instantiate(Prefab.staticDecoration);
		gameObject.transform.SetParent(parentGameObject.transform);
		StageDecoration result = gameObject.AddComponent<StageDecoration> ();

		return result;
	}
}

