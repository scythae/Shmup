using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : Ship {

	protected override void Start () {
		base.Start ();
		this.gameObject.AddComponent<BuffHatter> ();
		DamageSource.AddToGameObject<DS_Ship> (this.gameObject, this.hitPoints, UnitSide.usEnemy);

		Speed = 5;
		hitPoints = 5;
		unitSide = UnitSide.usPlayer;
	}

	protected override void ReceiveDamage(float damage) {
		base.ReceiveDamage (damage);
	}

	protected override void OnEnable ()	{
		base.OnEnable ();
		SetShooting (Input.GetButton ("Fire"));
		SetBuff<Buffs.Madness>(Input.GetButton ("Accelerate"));
	}

	protected override void Update() {	
		base.Update ();

		string ButtonName = "Fire";
		UnityAction<bool> action = new UnityAction<bool> (SetShooting);
		if (Input.GetButtonDown (ButtonName)) {	
			action.Invoke(true);
		}
		if (Input.GetButtonUp (ButtonName)) {			
			action.Invoke(false);
		}

		ButtonName = "Accelerate";
		action = new UnityAction<bool> (SetBuff<Buffs.Madness>);
		if (Input.GetButtonDown (ButtonName)) {
			action.Invoke(true);
		} 
		if (Input.GetButtonUp (ButtonName)){
			action.Invoke(false);
		}
	}

	private void SetShooting (bool state) {
		Weapon.Weapon.TrySetShooting (this.gameObject, state);
	}

	private void SetBuff<T> (bool state) where T : Buff{
		if (state)
			BuffHatter.ApplyBuff<T>(this.gameObject);
		else
			BuffHatter.RemoveBuff<T>(this.gameObject);
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