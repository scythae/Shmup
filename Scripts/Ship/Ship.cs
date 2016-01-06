using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : PausableRepetition {
	protected const float DefaultAcceleration = 1;

	protected float Speed;
	protected float Acceleration = DefaultAcceleration;
	public Trajectory trajectory;
	public Zone zone;
	public float hitPoints = 1;
	public UnitSide unitSide = UnitSide.usNone;

	protected override void Start () {	
		base.Start ();
		this.gameObject.AddComponent<StageInhabitant> ();
		this.gameObject.AddComponent<Rigidbody2D_ex> ();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
	}

	protected void Move (Vector2 movement) {
		Rigidbody2D rb2D = this.gameObject.GetComponent<Rigidbody2D> ();

		if (rb2D == null)
			return;

		rb2D.velocity = movement * Speed * Acceleration;
		rb2D.position = new Vector2 (
			Mathf.Clamp (rb2D.position.x, zone.xMin, zone.xMax),
			Mathf.Clamp (rb2D.position.y, zone.yMin, zone.yMax)
		); 
	}

	protected virtual void ReceiveDamage(float damage) {
		damage *= GetModifierValue (ShipModifierType.smIncomingDamage);

		hitPoints -= damage;
		if (hitPoints <= 0)
			Kill ();
	}

	private float GetModifierValue(ShipModifierType shipModifierType) {
		float result = 1;

		BuffHatter buffHatter = this.gameObject.GetComponent<BuffHatter> ();

		if (buffHatter != null)
			result = buffHatter.GetModifierValue(shipModifierType);

		return result;
	}

	public virtual void Kill () {
		TryDrop ();
		Destroy (this.gameObject);		
	}

	private void TryDrop() {
		foreach (Weapon.Dropper dropper in this.gameObject.GetComponents<Weapon.Dropper> () )
			dropper.Drop ();
	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {
		OnDamageSource (other);
	}

	void OnDamageSource(Collider2D other) {
		DamageSource damageSource = other.gameObject.GetComponent<DamageSource> ();
		if (damageSource == null)
			return;

		if (damageSource.target != this.unitSide)
			return;

		this.ReceiveDamage (damageSource.damage);

		if (damageSource is DS_Bullet)
			Destroy (damageSource.gameObject);
	}
}


