using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

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
