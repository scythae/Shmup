using UnityEngine;
using System.Collections;

public class Ship : PausableRepetition {
	protected const float DefaultAcceleration = 1;

	protected float Speed;
	protected float Acceleration = DefaultAcceleration;
	public Trajectory trajectory;
	public Zone zone;
	public bool invulnerable = false;
	public float hitPoints = 1;
	public UnitSide unitSide = UnitSide.usNone;

	protected override void Start () {
		base.Start ();
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();
	}

	protected void Move (Vector2 movement) {
		Rigidbody2D rb2D = this.gameObject.GetComponent<Rigidbody2D> ();

		if (rb2D == null) {
			return;
		}

		rb2D.velocity = movement * Speed * Acceleration;
		rb2D.position = new Vector2 (
			Mathf.Clamp (rb2D.position.x, zone.xMin, zone.xMax),
			Mathf.Clamp (rb2D.position.y, zone.yMin, zone.yMax)
		); 
	}

	protected virtual void ReceiveDamage(float damage) {
		if (invulnerable) {
			return;
		}

		hitPoints -= damage;
		if (hitPoints <= 0) {
			Kill ();
		}
	}

	public virtual void Kill () {
		Destroy (this.gameObject);		
	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {
		OnDamageSource (other);
	}

	void OnDamageSource(Collider2D other) {
		DamageSource damageSource = other.gameObject.GetComponent<DamageSource> ();
		if (damageSource == null) {
			return;
		}

		if (damageSource.target != this.unitSide) {
			return;
		}

		this.ReceiveDamage (damageSource.damage);

		if (damageSource is DS_Bullet) {
			Destroy (damageSource.gameObject);
		}
	}
}


