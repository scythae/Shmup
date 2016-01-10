using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectableTextList : TextList {
	private int currentItem = firstItemNumber;
	public UnityAction<string> OnSelectItem;

	public override void Initialize(string[] captions) {
		base.Initialize (captions);
		DistinguishSelectedItem ();
	}

	void DistinguishSelectedItem () {	
		int i = firstItemNumber;
		foreach (GameObject textItem in this.items) {
			Text text = textItem.GetComponent<Text> ();
			if (text == null)
				continue;
			
			if (i == currentItem) 
				DistinguishTextItem (text);
 			else 
				NormalizeTextItem (text);
			i++;
		}
	}

	void DistinguishTextItem (Text text) {	
		text.fontStyle = FontStyle.Bold;
	}

	void NormalizeTextItem (Text text) {	
		text.fontStyle = FontStyle.Normal;
	}

	protected override void Update () {
		base.Update ();

		if (NeedChangeSelection ())
			ChangeSelection ();

		if (NeedSubmitSelection ())
			SubmitSelection ();
	}

	bool NeedChangeSelection () {
		return Input.GetButtonDown ("Vertical");
	}

	void ChangeSelection () {
		if (Input.GetAxis ("Vertical") > 0) 
			currentItem--;
		else
			currentItem++;

		currentItem = Mathf.Clamp (currentItem, firstItemNumber, firstItemNumber + itemCount - 1);

		DistinguishSelectedItem ();
		Input.ResetInputAxes ();
	}

	bool NeedSubmitSelection () {
		return Input.GetButtonDown ("Submit");
	}

	void SubmitSelection () {
		if (OnSelectItem != null)
			OnSelectItem (CurrentItemCaption ());
	}

	string CurrentItemCaption () {
		string result = "";

		Text text = items[currentItem].GetComponent<Text> ();
		if (text != null)
			result = text.text;
		
		return result;
	}
}
