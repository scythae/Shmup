using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWarning: MonoBehaviour {
	private static Vector2 size = new Vector2(4, 1);

	public static void Show (string text, float time) {	
		GameWarning instance = CreateInstance ();
		instance.gameObject.GetComponent <Text> ().text = text; 
		instance.Invoke ("Hide", time);
	}

	private void Hide() {
		Destroy (this.gameObject);
	}	

	static GameWarning CreateInstance () {
		GameObject gameWarningObject = Instantiate(Prefab.textItem);
		gameWarningObject.transform.SetParent (Design.canvas.transform);

		RectTransform rt = gameWarningObject.transform as RectTransform;
		RectTransform rtp = gameWarningObject.transform.parent as RectTransform;

		rt.offsetMin = (rtp.offsetMax - rtp.offsetMin - size) / 2;
		rt.offsetMax = rt.offsetMin + size;

		return gameWarningObject.AddComponent <GameWarning> ();
	}
}
