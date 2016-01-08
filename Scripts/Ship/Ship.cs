using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : PausableRepetition {
	protected const float DefaultAcceleration = 1;
	protected float Speed;
	protected float Acceleration = DefaultAcceleration;

	public Trajectory trajectory;
	public Zone zone;

	private float fHitPoints = 1;
	public float hitPoints {
		get { return fHitPoints; }
		set { fHitPoints = value; if (onChangeHitPoints != null) onChangeHitPoints(this); }
	}

	public UnitSide unitSide = UnitSide.usNone;
	public UnityEngine.Events.UnityAction<Ship> onDeath;
	public UnityEngine.Events.UnityAction<Ship> onChangeHitPoints;

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

		Rigidbody2D_ex.SetScaledVelocity(rb2D, movement * Speed * Acceleration);

		rb2D.position = new Vector2 (
			Mathf.Clamp (rb2D.position.x, zone.xMin, zone.xMax),
			Mathf.Clamp (rb2D.position.y, zone.yMin, zone.yMax)
		); 
	}

	protected virtual void ReceiveDamage(float damage) {
		damage *= GetModifierValue (ShipModifierType.smtIncomingDamage);

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
		if (onDeath != null)
			onDeath(this);

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


