using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class LabeledInformation : PanelWithChildren {
	private Text fCaption;
	private Text fValue;

	public static new LabeledInformation Create() {
		LabeledInformation result = PanelWithChildren.Create<LabeledInformation>();
		result.gameObject.name = "panel_LabeledInformation";

		result.SetAligner_AdaptiveTile(2, 1);
		result.alignDirection = Aligner.AlignDirection.adTop;

		result.items = new List<GameObject> ();

		GameObject item = Instantiate(Prefab.textItem);
		item.name = "text_Caption";
		result.fCaption = item.GetComponent<Text> ();
		result.items.Add(item);

		item = Instantiate(Prefab.textItem);
		item.name = "text_Value";
		result.fValue = item.GetComponent<Text> ();
		result.items.Add(item);

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