using UnityEngine;
using System.Collections;

public class EnemyZone : Zone{	
	public EnemyZone() : base() {		
		Gap = 0.5f;
		LoadFromGameObject ( Design.battlefield);
	}

	public Vector2 SpawnPosition() {
		float x = Random.Range (xMin, xMax);
		float y = Random.Range (yCenter, yMax);
		return new Vector2 (x, y);
	}

	public Quaternion SpawnRotation() {
		return new Quaternion(0, 0, 180, 0);
	}
}