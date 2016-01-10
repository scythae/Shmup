using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class Tiler : MonoBehaviour {
	private static Vector2 unassignedTileSize = new Vector2(-0.999f, -0.999f);

	protected Vector2 spacingRelativeToItemSize = new Vector2(0.3f, 0.3f);
	protected List<GameObject> items;

	public int colCount = 1;
	public int rowCount = 1;
	public bool fixedTileSize = true;
	public Vector2 tileSize = unassignedTileSize;

	public static Tiler Create () {
		return Create<Tiler> ();
	}

	public static T Create<T> () where T : Tiler{
		T result = ((GameObject) Instantiate(Prefab.panel)).AddComponent<T> ();

		RectTransform rt = result.gameObject.transform as RectTransform;
		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.zero;
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;

		return result;
	}
		
	protected virtual void Start () {
	}

	protected virtual void Update() {
	}

	protected virtual void FixedUpdate () {
	}

	public int itemCount {
		get {
			if (items == null)
				return 0;
			else
				return items.Count; 
		}
	}		

	public void SetItems (List<GameObject> items) {
		if (items == null)
			return;

		DestroyChildren ();

		foreach (GameObject item in items)
			item.transform.SetParent (this.gameObject.transform, false);

		this.items = items;
	}

	void DestroyChildren () {
		if (items != null)
			items.ForEach( delegate(GameObject item) { Destroy(item); });
	}

	public void Rebuild () {
		if (itemCount == 0) 
			return;

		RebuildChildren ();

		RectTransform rtp = this.gameObject.transform as RectTransform;

		Vector2 tileSize;
		if (fixedTileSize) {

			if (this.tileSize == unassignedTileSize) {
				RectTransform defaultItemRectTransform = items.Find(x => true).transform as RectTransform;
				tileSize = defaultItemRectTransform.offsetMax - defaultItemRectTransform.offsetMin;
			} else {
				tileSize = this.tileSize;
			}
				
			rtp.offsetMax = rtp.offsetMin + new Vector2(
				tileSize.x * (colCount + (colCount - 1) * spacingRelativeToItemSize.x),
				tileSize.y * (rowCount + (rowCount - 1) * spacingRelativeToItemSize.y)
			);
		} else {
			tileSize.x = (rtp.offsetMax - rtp.offsetMin).x / (colCount + (colCount - 1) * spacingRelativeToItemSize.x);	
			tileSize.y = (rtp.offsetMax - rtp.offsetMin).y / (rowCount + (rowCount - 1) * spacingRelativeToItemSize.y);
		}
			
		Vector2 spacing = new Vector2(
			spacingRelativeToItemSize.x * tileSize.x,
			spacingRelativeToItemSize.y * tileSize.y
		);

		int i = 0;

		foreach (GameObject item in items) {
			int rowIndex = (int) (i / colCount);
			int colIndex = i % colCount;

			RectTransform rt = item.transform as RectTransform;
			rt.anchorMin = new Vector2(0, 0);
			rt.anchorMax = new Vector2(0, 0);
			rt.offsetMax = rtp.offsetMax - rtp.offsetMin - new Vector2((tileSize.x + spacing.x) * (colCount - colIndex - 1), (tileSize.y + spacing.y) * rowIndex);
			rt.offsetMin = rt.offsetMax - new Vector2(tileSize.x, tileSize.y);
			i++;
		}
	}

	public void RebuildChildren () {
		if (items == null)
			return;

		foreach (GameObject chiltItem in items)
			foreach (Tiler childTiler in chiltItem.GetComponents<Tiler> ())
				childTiler.Rebuild ();

	}

	public void SetParent (GameObject other, bool worldPositionStays = false) {
		this.gameObject.transform.SetParent(other.transform, worldPositionStays);
	}

	public RectTransform rectTransform {
		get {return this.gameObject.transform as RectTransform; }
	}

	public RectTransform rectTransformParent {
		get {return this.gameObject.transform.parent as RectTransform; }
	}

	public GameObject parent {
		get {return this.gameObject.transform.parent.gameObject; }
	}

	protected virtual void OnDestroy () {
		DestroyChildren ();
		Destroy (this.gameObject);
	}
}

public static class TilerTester {
	static GameObject createLab() {
		LabeledInformation lab = LabeledInformation.Create();
		lab.caption = "Health";
		lab.value = "50";
//		lab.rectTransform.offsetMin = new Vector2 (0, 0);
//		lab.rectTransform.offsetMax = new Vector2 (1, 1);
//
		return lab.gameObject;
	}

	public static void Test () {
		List<GameObject> list = new List<GameObject> ();
		for (int i = 0; i < 10; i++) {
			list.Add(createLab());
		}

		Tiler tiler = Tiler.Create();
		tiler.colCount = 3;
		tiler.rowCount = 5;
		tiler.gameObject.transform.SetParent (Design.canvas.transform);
		tiler.rectTransform.offsetMin = new Vector2 (1, 1);
		tiler.rectTransform.offsetMax = new Vector2 (3, 4);
		tiler.SetItems (list);
		tiler.Rebuild ();
	}
}