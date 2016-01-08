using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class TextList : Tiler {
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
		List<GameObject> items = new List<GameObject> ();

		foreach (string caption in captions) {
			items.Add(Instantiate (Prefab.textItem));
			items.FindLast(x => true).GetComponent<Text> ().text = caption;
		}

		base.SetItems(items);

		this.colCount = 1;
		this.rowCount = items.Count;
	}
		
	protected void MakeActive() {	
		this.gameObject.SetActive (true);
	}	
}