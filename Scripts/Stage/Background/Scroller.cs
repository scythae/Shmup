using UnityEngine;

public class Scroller: MonoBehaviour{
	private GameObject copyTop;
	private GameObject copyBottom;
	private float itemHeight = 0;
	public float scrollStepPerSecond = -0.05f;

	private void Start () {
		copyTop = Instantiate (this.gameObject);
		copyTop.name = this.gameObject.name + "_Top";
		Destroy (copyTop.GetComponent<Scroller> ());
		copyTop.transform.SetParent(this.gameObject.transform.parent);

		copyBottom = Instantiate (this.gameObject);
		copyBottom.name = this.gameObject.name + "_Bottom";
		Destroy (copyBottom.GetComponent<Scroller> ());
		copyBottom.transform.SetParent(this.gameObject.transform.parent);

		itemHeight = (this.gameObject.transform as RectTransform).rect.height;

	}

	private void Update () {
		if (itemHeight <= 0)
			return;

		Vector3 newPosition = this.gameObject.transform.position;
		newPosition.y = Mathf.Repeat(Time.time * scrollStepPerSecond, itemHeight);
		this.gameObject.transform.position = newPosition;
		copyTop.transform.position = newPosition + new Vector3 (0, itemHeight, 0);
		copyBottom.transform.position = newPosition - new Vector3 (0, itemHeight, 0);
	}
}