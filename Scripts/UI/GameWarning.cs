using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWarning: MonoBehaviour {
	private static Vector2 size = new Vector2(4, 1);

	//public static Rect rect_CenterBig = new Rect(new Vector2 (2, 2.5f), size);
	public static Rect rect_RightDownSmall = new Rect(new Vector2 (6, 2), new Vector2 (2, 0.25f));

	public static GameWarning Show (string text, float time, Rect rect) {	
		GameWarning instance = Show(text, time);

		RectTransform rt = instance.transform as RectTransform;
		rt.offsetMin = rect.min;
		rt.offsetMax = rect.max;

		return instance;
	}

	public static GameWarning Show (string text, float time) {	
		GameWarning instance = CreateInstance ();
		instance.gameObject.GetComponent <Text> ().text = text; 
		instance.Invoke ("Hide", time);

		return instance;
	}

	private void Hide () {
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
