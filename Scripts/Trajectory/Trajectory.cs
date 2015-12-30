using UnityEngine;
using System.Collections;

public class Trajectory : MonoBehaviour {	
	protected float position;
	protected float speed = 1;
	protected Vector2 startDirection;

	public void Initialize(Vector2 startDirection, float speed = 1) {
		position = 0;
		this.startDirection = startDirection;
		this.speed = speed;
	}

	public float GetAxis(string Direction) {
		switch (Direction) {
		case "Horizontal":
			return GetPosition().x;
		case "Vertical":
			return GetPosition().y;
		default:
			return 0;
		}
	}

	protected virtual Vector2 GetPosition(){
		return new Vector2 (0, 0);
	}
}
