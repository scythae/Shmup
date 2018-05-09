using UnityEngine;

public class GameWarning: TemporaryText {
	private static Vector2 size = new Vector2(4, 1);
	private static GameWarning fInstance;

	public static void Show (string text, float time) {	
		if (fInstance == null)
			fInstance = Create ();

		fInstance.SetText(text, time);
	}

	private static new GameWarning Create() {
		GameWarning result = TemporaryText.Create<GameWarning> ();
		result.gameObject.transform.SetParent(Design.canvas.transform);

		RectTransform rt = result.gameObject.transform as RectTransform;
		RectTransform rtp = result.gameObject.transform.parent as RectTransform;
		rt.offsetMin = (rtp.offsetMax - rtp.offsetMin - size) / 2;
		rt.offsetMax = rt.offsetMin + size;

		return result;
	}
}
