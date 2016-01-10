using UnityEngine;
using System.Collections.Generic;

public class PanelWithChildrenTester {
	public class AlignIterator : MonoBehaviour {
		private void Update () {
			PanelWithChildren panel = gameObject.GetComponent<PanelWithChildren> ();

			if (Input.GetMouseButtonDown(0)) {
				panel.alignDirection = (Aligner.AlignDirection) ((int)(panel.alignDirection + 1) % System.Enum.GetNames(typeof(Aligner.AlignDirection)).Length);
				Debug.Log (panel.alignDirection);
				panel.Rebuild ();
			}

			if (Input.GetMouseButtonDown(1)) {
				panel.inverted  = !panel.inverted;
				Debug.Log (panel.inverted);
				panel.Rebuild ();
			}

			if (Input.GetMouseButtonDown(2)) {
				RectTransform rt = this.gameObject.transform as RectTransform;
				Debug.Log (panel.inverted);
				rt.offsetMax *= 1.1f;
				panel.Rebuild ();
			}
		}
	}

	static GameObject CreateChild() {
		GameObject obj = GameObject.Instantiate(Prefab.panel);
		RectTransform rt = obj.transform as RectTransform;
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = rt.offsetMin + new Vector2 (Random.Range(0.05f, 0.5f), Random.Range(0.05f, 0.5f));

		return obj; 
	}

	public static void Test_FixedTile () {
		PanelWithChildren panel = Prepare ();
		panel.SetAligner_FixedTile(new Vector2(0.5f, 0.75f));
		panel.Rebuild();
	}

	public static void Test_AdaptiveTile () {
		PanelWithChildren panel = Prepare ();
		panel.SetAligner_AdaptiveTile(5, 3);
		panel.Rebuild();
	}

	public static void Test_Pivot () {
		PanelWithChildren panel = Prepare ();
		panel.SetAligner_Pivot();
		panel.Rebuild();
	}

	private static PanelWithChildren Prepare () {
		PanelWithChildren panel = PanelWithChildren.Create();
		panel.gameObject.AddComponent<AlignIterator> ();

		panel.items = new List<GameObject> ();
		for (int i = 0; i < 10; i++) 
			panel.items.Add(CreateChild());
		return panel;
	}
}