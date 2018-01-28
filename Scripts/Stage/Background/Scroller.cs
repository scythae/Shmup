using UnityEngine;

public class Scroller: MonoBehaviour{
	private GameObject copyTop;
	private GameObject copyBottom;
	private float startY;
	private float itemHeight = 0;
	private float rangeVertical = 0;
	public float scrollStepPerSecond = -0.05f;

	private void Start () {
		RectTransform rectTransform = (this.gameObject.transform as RectTransform);
		if (rectTransform == null) {
			Destroy (this);
			return;
		}		

		itemHeight = rectTransform.rect.height;
		rangeVertical = 3*itemHeight;
		startY = this.gameObject.transform.position.y;

		copyTop = CloneSpecific("_Top");
		copyBottom = CloneSpecific("_Bottom");
	}

	private GameObject CloneSpecific (string suffix) {
		GameObject result = Instantiate (this.gameObject);
		result.name = this.gameObject.name + suffix;
		Destroy (result.GetComponent<Scroller> ());
		result.transform.SetParent(this.gameObject.transform.parent);
		return result;
	}

	private void Update () {
		if (itemHeight <= 0)
			return;

		float newY = startY + Time.time * scrollStepPerSecond;

		float yThis = Mathf.Repeat(newY, rangeVertical) - itemHeight;
		float yTop = Mathf.Repeat(newY + itemHeight, rangeVertical) - itemHeight;
		float yBottom = Mathf.Repeat(newY - itemHeight, rangeVertical) - itemHeight;

		SetPosY(this.gameObject, yThis);
		SetPosY(copyTop, yTop);
		SetPosY(copyBottom, yBottom);
	}

	private static void SetPosY (GameObject gameObject, float y) {		
		Vector3 newPosition = gameObject.transform.position;
		newPosition.y = y;
		gameObject.transform.position = newPosition;
	}
}
