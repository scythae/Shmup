using UnityEngine;
using System.Collections;

public class Player : Ship {
	private float PlayerAcceleration;

	protected override void Start () {
		base.Start ();
		this.gameObject.AddComponent<BuffHatter> ();
		DamageSource.AddToGameObject<DS_Ship> (this.gameObject, this.hitPoints, UnitSide.usEnemy);

		Speed = 5;
		PlayerAcceleration = 2;
		hitPoints = 5;
		unitSide = UnitSide.usPlayer;
	}

	protected override void ReceiveDamage(float damage) {
		base.ReceiveDamage (damage);
	}

	protected override void OnEnable ()	{
		base.OnEnable ();
		Weapon.Weapon.TrySetShooting (this.gameObject, Input.GetButton ("Fire"));
	}

	protected override void Update() {	
		base.Update ();

		if (Input.GetButtonDown ("Fire")) {	
			Weapon.Weapon.TrySetShooting (this.gameObject, true);
		}
		if (Input.GetButtonUp ("Fire")) {			
			Weapon.Weapon.TrySetShooting (this.gameObject, false);
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
	}

	protected override void OnTriggerEnter2D(Collider2D other) {
		base.OnTriggerEnter2D (other);
		OnBonus (other);
	}

	void OnBonus(Collider2D other) {
		Bonus bonus = other.gameObject.GetComponent<Bonus> ();
		if (bonus == null) 
			return;

		bonus.Interact (this.gameObject);
	}
}


