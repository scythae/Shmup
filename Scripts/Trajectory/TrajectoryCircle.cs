using UnityEngine;
using System.Collections;

public class TrajectoryCircle : Trajectory {	
	protected override Vector2 GetPosition(){
		position += speed;
		if (position >= 360) {
			position -= 360;
		}

		float angle = position * Mathf.PI / 180;
		return new Vector2 ( startDirection.x * Mathf.Cos (angle), startDirection.y * Mathf.Sin (angle));
	}
}

