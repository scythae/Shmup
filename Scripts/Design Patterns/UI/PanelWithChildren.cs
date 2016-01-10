using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class Aligner {
	public enum AlignDirection {adTop, adLeft, adBottom, adRight};
	public PanelWithChildren panel;

	private bool RebuildAccessible () {
		if (panel != null)
		if (panel.items != null)
		if (panel.items.Count != 0)
		if (panel.gameObject.transform as RectTransform != null)
			return true;

		return false;
	}

	public virtual void Rebuild() {		
		if (RebuildAccessible()) 
			TryRebuild();
	}

	protected virtual void TryRebuild() {
	}
}

public class Aligner_Tile : Aligner {
	protected int linesCount;
	protected int tilesCountInLine;
	protected Vector2 tileSize;

	protected void SetDefaultData() {
		linesCount = 1;
		tilesCountInLine = 1;
		tileSize = Vector2.zero;
	}

	private void PrepareGrid() {
		if (this is Aligner_FixedTile)
			(this as Aligner_FixedTile).SetTileSize(tileSize);
		else if (this is Aligner_AdaptiveTile) 
			(this as Aligner_AdaptiveTile).SetGrid(linesCount, tilesCountInLine, panel);			
	}

	protected override void TryRebuild() {
		PrepareGrid ();

		int colCount;
		if (panel.alignDirection == AlignDirection.adBottom || panel.alignDirection == AlignDirection.adTop)
			colCount = tilesCountInLine;
		else
			colCount = linesCount;		

		RectTransform rtp = panel.gameObject.transform as RectTransform;
		int i = 0;
		foreach (GameObject item in panel.items.FindAll(x => (x.transform as RectTransform) != null)) {
			int rowIndex = (int) (i / colCount);
			int colIndex = i % colCount;

			RectTransform rt = item.transform as RectTransform;

			rt.SetParent(rtp);			
			rt.anchorMin = Vector2.zero;
			rt.anchorMax = Vector2.zero;

			rt.offsetMin = ChildMin(rtp, colIndex, rowIndex);
			rt.offsetMax = rt.offsetMin + new Vector2(tileSize.x, tileSize.y);	
			i++;
		}
	}

	private Vector2 ChildMin(RectTransform parentTransform, int colIndex, int rowIndex) {
		switch (panel.alignDirection) {
		case (AlignDirection.adTop) : return ChildMin_Top(parentTransform, colIndex, rowIndex); 				
		case (AlignDirection.adLeft) : return ChildMin_Left(parentTransform, colIndex, rowIndex); 
		case (AlignDirection.adBottom) : return ChildMin_Bottom(parentTransform, colIndex, rowIndex); 
		case (AlignDirection.adRight) : return ChildMin_Right(parentTransform, colIndex, rowIndex); 
		default : return Vector2.zero;
		}
	}

	private Vector2 ChildMin_Top(RectTransform parentTransform, int colIndex, int rowIndex) {
		return new Vector2 (
			ChildMinX (parentTransform, colIndex),
			parentTransform.rect.height - (tileSize.y + panel.spacing.y) * (rowIndex + 1)
		);
	}

	private Vector2 ChildMin_Bottom(RectTransform parentTransform, int colIndex, int rowIndex) {
		return new Vector2 (
			ChildMinX (parentTransform, colIndex),
			(tileSize.y + panel.spacing.y) * rowIndex
		);
	}

	private float ChildMinX(RectTransform parentTransform, int colIndex) {
		if (!panel.inverted)
			return (tileSize.x + panel.spacing.x) * colIndex;			
		else
			return parentTransform.rect.width - (tileSize.x + panel.spacing.x) * (colIndex + 1);
	}

	private Vector2 ChildMin_Left(RectTransform parentTransform, int colIndex, int rowIndex) {
		return new Vector2 (
			(tileSize.x + panel.spacing.x) * rowIndex,
			ChildMinY (parentTransform, colIndex)
		);
	}

	private Vector2 ChildMin_Right(RectTransform parentTransform, int colIndex, int rowIndex) {
		return new Vector2 (
			parentTransform.rect.width - (tileSize.x + panel.spacing.x) * (rowIndex + 1),
			ChildMinY (parentTransform, colIndex)
		);
	}

	private float ChildMinY(RectTransform parentTransform, int colIndex) {
		if (!panel.inverted)
			return (tileSize.y + panel.spacing.y) * colIndex;
		else
			return parentTransform.rect.height - (tileSize.y + panel.spacing.y) * (colIndex + 1);
	}
}

public class Aligner_FixedTile : Aligner_Tile {
	public void SetTileSize(Vector2 tileSize) {
		if (tileSize.x * tileSize.y == 0) {
			SetDefaultData ();
			return;
		}

		this.tileSize = tileSize;

		RectTransform rtp = panel.gameObject.transform as RectTransform;
		if (rtp == null)
			return;

		linesCount = (int) (rtp.rect.height / tileSize.y);
		tilesCountInLine = (int) (rtp.rect.width / tileSize.x);
	}
}

public class Aligner_AdaptiveTile : Aligner_Tile {
	public void SetGrid (int linesCount, int tilesCountInLine, PanelWithChildren panel) {
		if (linesCount * tilesCountInLine == 0) {
			SetDefaultData ();
			return;
		}

		this.linesCount = linesCount;
		this.tilesCountInLine = tilesCountInLine;

		RectTransform rtp = panel.gameObject.transform as RectTransform;
		if (rtp == null)
			return;	
		
		tileSize.x = Mathf.Max(0, rtp.rect.width / (float) tilesCountInLine - panel.spacing.x);
		tileSize.y = Mathf.Max(0, rtp.rect.height / (float) linesCount - panel.spacing.y);			
	}
}

public class Aligner_Pivot : Aligner {
	private Vector2 itemOffset;

	protected override void TryRebuild() {
		RectTransform rtp = panel.gameObject.transform as RectTransform;
		itemOffset = Vector2.zero;

		foreach (GameObject item in panel.items.FindAll(x => (x.transform as RectTransform) != null)) {
			RectTransform rt = item.transform as RectTransform;
			rt.SetParent(rtp);			
			rt.anchorMin = Vector2.zero;
			rt.anchorMax = Vector2.zero;

			Pivot(rt, rtp);
		}
	}

	private void Pivot(RectTransform childTransform, RectTransform parentTransform) {
		if (panel.alignDirection == AlignDirection.adTop || panel.alignDirection == AlignDirection.adBottom )
			PivotVertical(childTransform, parentTransform);
		else
			PivotHorizontal(childTransform, parentTransform);
	}

	private void PivotVertical(RectTransform childTransform, RectTransform parentTransform) {
		Vector2 itemSize = new Vector2 (parentTransform.rect.width, childTransform.rect.height);

		Vector2 offsetMin = itemOffset;
		if (panel.alignDirection == AlignDirection.adTop)
			offsetMin.y = (parentTransform.rect.size - itemOffset - itemSize).y;
		
		childTransform.offsetMin = offsetMin;
		childTransform.offsetMax = childTransform.offsetMin + itemSize;

		itemOffset.y += (itemSize + panel.spacing).y;
	}

	private void PivotHorizontal(RectTransform childTransform, RectTransform parentTransform) {
		Vector2 itemSize = new Vector2 (childTransform.rect.width, parentTransform.rect.height);

		Vector2 offsetMin = itemOffset;
		if (panel.alignDirection == AlignDirection.adRight)
			offsetMin.x = (parentTransform.rect.size - itemOffset - itemSize).x;
		
		childTransform.offsetMin = offsetMin;
		childTransform.offsetMax = childTransform.offsetMin + itemSize;

		itemOffset.x += (itemSize + panel.spacing).x;
	}
}


public class PanelWithChildren : MonoBehaviour {
	private Aligner aligner;
	public List<GameObject> items;
	public Vector2 spacing = Vector2.zero;
	public Aligner.AlignDirection alignDirection = Aligner.AlignDirection.adBottom;
	public bool inverted = false;

	public void SetAligner_FixedTile (Vector2 tileSize) {
		Aligner_FixedTile aligner = new Aligner_FixedTile();

		aligner = new Aligner_FixedTile();
		aligner.panel = this;
		aligner.SetTileSize(tileSize);
		this.aligner = aligner;
	}

	public void SetAligner_AdaptiveTile (int linesCount, int tilesCountInLine) {
		Aligner_AdaptiveTile aligner = new Aligner_AdaptiveTile();

		aligner = new Aligner_AdaptiveTile();
		aligner.panel = this;
		aligner.SetGrid(linesCount, tilesCountInLine, this);
		this.aligner = aligner;
	}

	public void SetAligner_Pivot () {
		Aligner_Pivot aligner = new Aligner_Pivot();

		aligner = new Aligner_Pivot();
		aligner.panel = this;
		this.aligner = aligner;
	}

	public static PanelWithChildren Create () {
		return Create<PanelWithChildren> ();
	}

	public static T Create<T> () where T : PanelWithChildren{		
		T result = GameObject.Instantiate(Prefab.panel).AddComponent<T> ();

		RectTransform rt = result.gameObject.transform as RectTransform;
		rt.SetParent(Design.canvas.transform);

		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.zero;
		rt.offsetMin = new Vector2 (1, 1);
		rt.offsetMax = rt.offsetMin + new Vector2 (1, 2);

		return result;
	}

	protected virtual void Start () {		
	}

	protected virtual void Update() {
	}

	protected virtual void FixedUpdate () {
	}

	public void Rebuild () {
		RebuildChildren ();

		if (aligner != null)
			aligner.Rebuild ();
	}

	private void RebuildChildren () {
		if (items != null)
			foreach (GameObject childItem in items)
				foreach (PanelWithChildren childPanel in childItem.GetComponents<PanelWithChildren> ())
					childPanel.Rebuild ();
	}

	protected virtual void OnDestroy () {
		DestroyChildren ();
		Destroy (this.gameObject);
	}

	private void DestroyChildren () {
		if (items != null)
			items.ForEach( delegate(GameObject item) { Destroy(item); });
	}
}

public class PanelWithChildrenTester {
	public class AlignIterator : MonoBehaviour {
		private void Update () {
			PanelWithChildren panel = gameObject.GetComponent<PanelWithChildren> ();

			if (Input.GetMouseButtonDown(0)) {
				panel.alignDirection = (Aligner.AlignDirection) ((int)(panel.alignDirection + 1) % System.Enum.GetNames(typeof(Aligner.AlignDirection)).Length);
				Debug.Log (panel.alignDirection);
				panel.Rebuild ();
			}

			if (Input.GetMouseButtonDown(1)) {
				panel.inverted  = !panel.inverted;
				Debug.Log (panel.inverted);
				panel.Rebuild ();
			}

			if (Input.GetMouseButtonDown(2)) {
				RectTransform rt = this.gameObject.transform as RectTransform;
				Debug.Log (panel.inverted);
				rt.offsetMax *= 1.1f;
				panel.Rebuild ();
			}
		}
	}

	static GameObject CreateChild() {
		GameObject obj = GameObject.Instantiate(Prefab.panel);
		RectTransform rt = obj.transform as RectTransform;
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = rt.offsetMin + new Vector2 (Random.Range(0.05f, 0.5f), Random.Range(0.05f, 0.5f));

		return obj; 
	}

	public static void Test () {
		PanelWithChildren panel = PanelWithChildren.Create();
		panel.gameObject.AddComponent<AlignIterator> ();


		panel.items = new List<GameObject> ();
		for (int i = 0; i < 10; i++) 
			panel.items.Add(CreateChild());

		panel.SetAligner_AdaptiveTile(5, 3);
//		panel.SetAligner_FixedTile(Vector2.zero /*new Vector2(0.5f, 0.75f)*/);
//		panel.SetAligner_Pivot();
		panel.Rebuild();
	}
}