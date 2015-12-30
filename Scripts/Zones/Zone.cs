using UnityEngine;
using System.Collections;

public class Zone {
	public float xMin, xMax, xCenter, yMin, yMax, yCenter;

	protected float Gap = 0;
	private float xShift;
	private float yShift;

	public void LoadFromGameObject(GameObject gameObject) {	
		xShift = gameObject.transform.position.x;
		yShift = gameObject.transform.position.y;

		if (gameObject.GetComponent<Renderer> () != null) {
			LoadFromBounds(gameObject.GetComponent<Renderer> ().bounds);
		} else if (gameObject.GetComponent<BoxCollider2D> () != null) {
			LoadFromBounds(gameObject.GetComponent<BoxCollider2D> ().bounds);
		}
	}

	void LoadFromBounds(Bounds bounds) {
		xMax = bounds.size.x / 2 - Gap;
		xMin = -xMax;
		xCenter = bounds.center.x;

		xMax += xShift;
		xMin += xShift;
		xCenter += xShift;

		yMax = bounds.size.y / 2 - Gap;
		yMin = -yMax;		
		yCenter = bounds.center.y;

		yMax += yShift;
		yMin += yShift;
		yCenter += yShift;
		return;
	}
}


