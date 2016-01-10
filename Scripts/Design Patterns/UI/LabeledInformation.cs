using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
//
//public class LabeledInformation : PanelWithChildren {
//	private Text fCaption;
//	private Text fValue;
//
//	public LabeledInformation () : base () {
////		colCount = 1;
////		rowCount = 2;
////		fixedTileSize = false;
////		spacingRelativeToItemSize = new Vector2 (0.1f, 0.1f);
//	}
//
//	public static new LabeledInformation Create() {
//		LabeledInformation result = PanelWithChildren.Create<LabeledInformation>();
//		result.gameObject.name = "panel_LabeledInformation";
//
//		result.SetAligner_AdaptiveTile(2, 1);
//		result.alignDirection = Aligner.AlignDirection.adTop;
//
//		result.items = new List<GameObject> ();
//
//		result.items.Add(Instantiate(Prefab.textItem));
//		result.fCaption = result.items.FindLast(x => true).GetComponent<Text> ();
//
//		result.items.Add(Instantiate(Prefab.textItem));
//		result.fValue = result.items.FindLast(x => true).GetComponent<Text> ();
//
//
//		return result as LabeledInformation;
//	}
//
//	public string caption {
//		get { return fCaption.text; }
//		set { fCaption.text = value; }
//	}
//	public string value {
//		get { return fValue.text; }
//		set { fValue.text = value; }
//	}
//}
//
//public class LabeledInformationTester : PanelWithChildrenTester {
//	public static new void Test () {
//		LabeledInformation Li = LabeledInformation.Create();
//		Li.gameObject.AddComponent<AlignIterator> ();
//
//		RectTransform rt = Li.gameObject.transform as RectTransform;
//		rt.offsetMin.Set(5,1);
//		rt.offsetMax = rt.offsetMin + new Vector2 (2,1);
//
//		Li.caption = "caption";
//		Li.value = "value";
//		Li.Rebuild ();
//
//	}
//}


public class LabeledInformation : Tiler {
	private Text fCaption;
	private Text fValue;

	public LabeledInformation () : base () {
		colCount = 1;
		rowCount = 2;
		fixedTileSize = false;
		spacingRelativeToItemSize = new Vector2 (0.1f, 0.1f);
	}

	public static new LabeledInformation Create() {
		LabeledInformation result = Tiler.Create<LabeledInformation>();
		result.gameObject.name = "panel_LabeledInformation";

		List<GameObject> list = new List<GameObject> ();

		list.Add(Instantiate(Prefab.textItem));
		result.fCaption = list.FindLast(x => true).GetComponent<Text> ();

		list.Add(Instantiate(Prefab.textItem));
		result.fValue = list.FindLast(x => true).GetComponent<Text> ();

		result.SetItems(list);
		return result as LabeledInformation;
	}

	public string caption {
		get { return fCaption.text; }
		set { fCaption.text = value; }
	}
	public string value {
		get { return fValue.text; }
		set { fValue.text = value; }
	}
}
