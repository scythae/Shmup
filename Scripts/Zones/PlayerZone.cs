using UnityEngine;
using System.Collections;

public class PlayerZone : Zone{	
	public PlayerZone() : base() {		
		Gap = 0.5f;
		LoadFromGameObject ( Design.battlefield);
	}

	public Vector2 SpawnPosition() {
		float x = xCenter;
		float y = yMin + (yMax - yMin) / 16;

		return new Vector2 (x, y);
	}
}