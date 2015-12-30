using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWarning: MonoBehaviour {
	public static void Show (string text, float time) {	
		GameWarning instance = CreateInstance ();
		instance.gameObject.GetComponent <Text> ().text = text; 
		instance.Invoke ("Hide", time);
	}

	private void Hide() {
		Destroy (this.gameObject);
	}	

	static GameWarning CreateInstance () {
		GameObject gameWarningObject = Instantiate(Prefab.gameWarning);
		gameWarningObject.transform.SetParent (Design.canvas.transform, false);
		return gameWarningObject.AddComponent <GameWarning> ();
	}
}
