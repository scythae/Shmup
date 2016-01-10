using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextList : PanelWithChildren {
	protected const int firstItemNumber = 0;
	public float DelayBeforeShowing = 0;

	public virtual void Initialize (string[] captions) {
		SetItems (captions);
		Invoke ("MakeActive", DelayBeforeShowing);
	}

	protected override void Start () {
		base.Start ();
		this.gameObject.SetActive (false);
	}

	protected void SetItems (string[] captions) {
		items = new List<GameObject> ();

		foreach (string caption in captions) {
			items.Add(Instantiate (Prefab.textItem));
			items.FindLast(x => true).GetComponent<Text> ().text = caption;
		}
	}
		
	protected void MakeActive() {	
		this.gameObject.SetActive (true);
	}	
}