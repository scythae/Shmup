using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class TextList : MonoBehaviour {
	protected const int firstItemNumber = 1;
	protected const float spacingBetweenItemsRelativeToHeight = 0.3f;
	public float DelayBeforeShowing = 0;

	public static T Create<T> () where T: TextList {
		T result = ((GameObject) Instantiate(Prefab.panel)).AddComponent<T> ();
		result.gameObject.transform.SetParent (Design.canvas.transform, false);

		return result;
	}

	public virtual void Initialize (string[] captions) {
		AddItems (captions);
		Invoke ("MakeActive", DelayBeforeShowing);
		Rearrange ();
	}

	protected virtual void Start () {
		this.gameObject.SetActive (false);
	}

	protected virtual void Update () {
	}

	protected virtual void FixedUpdate () {
	}

	protected int ItemCount () {
		return this.gameObject.GetComponentsInChildren<Text> ().Length;
	}		

	protected void AddItems (string[] captions) {
		foreach (string caption in captions) {
			Text newItem = Instantiate (Prefab.textItem).GetComponent<Text> ();
			newItem.text = caption;
			newItem.gameObject.transform.SetParent (this.gameObject.transform, false);
		}
	}

	protected void Rearrange () {	
		int i = firstItemNumber;
		float textItemHeight = Prefab.textItem.GetComponent<RectTransform> ().rect.height;
		float spacing = spacingBetweenItemsRelativeToHeight * textItemHeight;
		float itemTop;

		foreach (Text text in this.gameObject.GetComponentsInChildren<Text> ()) {
			text.transform.SetSiblingIndex (i);
			RectTransform rt = text.gameObject.GetComponent<RectTransform> ();
			itemTop = -(textItemHeight + spacing) * (i - 1);
			rt.offsetMax = new Vector2 (0, itemTop); 	
			rt.offsetMin = new Vector2 (0, itemTop - textItemHeight);	
			i++;
		}
	}

	protected void MakeActive() {	
		this.gameObject.SetActive (true);
	}	

	protected virtual void OnDestroy () {
		Destroy (this.gameObject);
	}
}