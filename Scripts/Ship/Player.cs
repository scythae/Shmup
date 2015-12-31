using UnityEngine;
using System.Collections;

public class Player : Ship {
	private float PlayerAcceleration;

	public Player () : base () {
		Speed = 5;
		PlayerAcceleration = 2;
		hitPoints = 5;

		unitSide = UnitSide.usPlayer;
		DamageSource.AddToGameObject<DS_Ship> (this.gameObject, this.hitPoints, UnitSide.usEnemy);
	}

	protected override void Start () {
		base.Start ();
	}

	protected override void ReceiveDamage(float damage) {
		base.ReceiveDamage (damage);
	}

	protected override void OnEnable ()	{
		base.OnEnable ();
		Weapon.TrySetShooting (this.gameObject, Input.GetButton ("Fire"));
	}

	protected override void Update() {	
		base.Update ();

		HitPoints.Show ((int) Mathf.Round(hitPoints));

		if (Input.GetButtonDown ("Fire")) {	
			Weapon.TrySetShooting (this.gameObject, true);
		}
		if (Input.GetButtonUp ("Fire")) {			
			Weapon.TrySetShooting (this.gameObject, false);
		}

		if (Input.GetButtonDown ("Accelerate")) {
			Acceleration = PlayerAcceleration;	
		} 
		if (Input.GetButtonUp ("Accelerate")){
			Acceleration = DefaultAcceleration;	
		}
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
		Vector2 movement = new Vector2(0, 0);

		movement.x += Input.GetAxis ("Horizontal");
		movement.y += Input.GetAxis ("Vertical");

		this.Move (movement);
	}

	public override void Kill () {
		base.Kill();
		Design.gameController.GameOver ();
	}
}


