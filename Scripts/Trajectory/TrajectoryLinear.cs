using UnityEngine;
using System.Collections;

public class TrajectoryLinear : Trajectory {	
	protected override Vector2 GetPosition(){
		position += speed;
		if (position >= 360) {
			position -= 360;
		}

		return startDirection * Mathf.Cos (position * Mathf.PI / 180);
	}
}
