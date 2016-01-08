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

	protected virtual void Start () {
		this.gameObject.SetActive (false);
	}

	protected virtual void Update () {
	}

	protected virtual void FixedUpdate () {
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

//
//public class TextList : MonoBehaviour {
//	protected const int firstItemNumber = 1;
//	protected const float spacingBetweenItemsRelativeToHeight = 0.3f;
//	public float DelayBeforeShowing = 0;
//
//	public static T Create<T> () where T: TextList {
//		T result = ((GameObject) Instantiate(Prefab.panel)).AddComponent<T> ();
//
//		RectTransform rt = result.gameObject.transform as RectTransform;
//		rt.anchorMin = Vector2.zero;
//		rt.anchorMax = Vector2.zero;
//		rt.offsetMin = Vector2.zero;
//		rt.offsetMax = Vector2.zero;
//
//		return result;
//	}
//
//	public virtual void Initialize (string[] captions) {
//		AddItems (captions);
//		Invoke ("MakeActive", DelayBeforeShowing);
//		Rearrange ();
//	}
//
//	protected virtual void Start () {
//		this.gameObject.SetActive (false);
//	}
//
//	protected virtual void Update () {
//	}
//
//	protected virtual void FixedUpdate () {
//	}
//
//	protected int ItemCount () {
//		return this.gameObject.GetComponentsInChildren<Text> ().Length;
//	}		
//
//	protected Text ItemByIndex (int itemIndexFromOne) {
//		return this.gameObject.transform.GetChild (itemIndexFromOne - 1).gameObject.GetComponent<Text> ();
//	}
//
//	protected void AddItems (string[] captions) {
//		foreach (string caption in captions) {
//			Text newItem = Instantiate (Prefab.textItem).GetComponent<Text> ();
//			newItem.text = caption;
//			newItem.gameObject.transform.SetParent (this.gameObject.transform, false);
//		}
//	}
//
//	protected void Rearrange () {	
//		int i = firstItemNumber;
//
//		RectTransform rtp = this.gameObject.transform as RectTransform;
//
//		int itemCount = ItemCount();
//		float textItemHeight = (rtp.offsetMax - rtp.offsetMin).y / (itemCount + (itemCount - 1) * spacingBetweenItemsRelativeToHeight);
//		float textItemWidth = (rtp.offsetMax - rtp.offsetMin).x;
//		float spacing = spacingBetweenItemsRelativeToHeight * textItemHeight;
//
//		foreach (Text text in this.gameObject.GetComponentsInChildren<Text> ()) {
//			text.transform.SetSiblingIndex (i);
//
//			RectTransform rt = text.gameObject.transform as RectTransform; ///// RectTransform rt
//			rt.anchorMin = new Vector2(0, 0);
//			rt.anchorMax = new Vector2(0, 0);
//			rt.offsetMax = rtp.offsetMax - rtp.offsetMin - new Vector2(0, (textItemHeight + spacing) * (i - 1));
//			rt.offsetMin = rt.offsetMax - new Vector2(textItemWidth, textItemHeight);
//
//			i++;
//		}
//	}
//
//	protected void MakeActive() {	
//		this.gameObject.SetActive (true);
//	}	
//
//	protected virtual void OnDestroy () {
//		Destroy (this.gameObject);
//	}
//}