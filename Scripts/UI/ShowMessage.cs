using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class ShowMessage : MonoBehaviour {
	private UnityAction OnClose;

	public static Vector2 panelSize = new Vector2(4, 2.5f);
	public static Vector2 panelPosition = new Vector2(2, 1);
	public static Vector2 gap = new Vector2(0.25f, 0.25f);

	void Update () {
		if (NeedClose ())
			DoClose ();
	}

	bool NeedClose () {
		return (Input.GetButtonDown ("Submit") || Input.GetButtonDown ("Cancel"));
	}

	void DoClose () {
		if (OnClose != null)
			OnClose ();

		Destroy (this.gameObject);
	}

	public static void Show(string Text, UnityAction ActionOnClose) {
		GameObject panel = Instantiate(Prefab.panel);
		GameObject textItem = Instantiate(Prefab.textItem);

		RectTransform rtp = panel.gameObject.transform as RectTransform;

		rtp.SetParent(Design.canvas.transform);
		rtp.offsetMin = panelPosition;
		rtp.offsetMax = panelPosition + panelSize;

		RectTransform rt = textItem.transform as RectTransform;
		rt.SetParent(rtp);	

		rt.offsetMin = gap;
		rt.offsetMax = panelSize - gap;

		Text text = textItem.GetComponent<Text>();
		text.text = Text;
		text.alignment = TextAnchor.MiddleLeft;

		panel.AddComponent<ShowMessage>().OnClose = ActionOnClose;
	}
}
