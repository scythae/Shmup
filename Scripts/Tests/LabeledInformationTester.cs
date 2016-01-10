using UnityEngine;

public class LabeledInformationTester : PanelWithChildrenTester {
	public static void Test () {
		LabeledInformation Li = LabeledInformation.Create();
		Li.gameObject.AddComponent<AlignIterator> ();

		RectTransform rt = Li.gameObject.transform as RectTransform;
		rt.offsetMin = new Vector2 (5,1);
		rt.offsetMax = rt.offsetMin + new Vector2 (2,1);

		Li.caption = "caption";
		Li.value = "value";
		Li.Rebuild ();
	}
}