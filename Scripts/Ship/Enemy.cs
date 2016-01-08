using UnityEngine;
using System.Collections;

public class Enemy : Ship {	
	protected override void Start () {
		base.Start ();
		Speed = 5; 
		hitPoints = 1;
		unitSide = UnitSide.usEnemy;
		DamageSource.AddToGameObject<DS_Ship> (this.gameObject, this.hitPoints, UnitSide.usPlayer);
		this.gameObject.AddComponent<TrajectoryLinear> ().Initialize (new Vector2(0.05f, 0), 0.05f);
		this.gameObject.AddComponent<TrajectoryCircle> ().Initialize (new Vector2(1, 1), 2);
	}

	protected override void FixedUpdate ()	{		
		base.FixedUpdate ();

		Vector2 movement = new Vector2 (0, 0);

		foreach (Trajectory trajectory in this.gameObject.GetComponents<Trajectory> ()) {
			movement.x += trajectory.GetAxis("Horizontal");
			movement.y += trajectory.GetAxis("Vertical");
		}

		this.Move (movement);
	}
}


