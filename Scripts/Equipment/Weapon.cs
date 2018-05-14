using UnityEngine;
using System.Collections;

namespace Weapon {
	
public class Weapon : PausableRepetition {
	protected GameObject bulletTemplate;

	public float bulletSpeed;
	public float damage = 0;

	private bool fShooting = false;
	public bool Shooting {
		get { return fShooting; }
		set { fShooting = value; }
	}

	public float reloadTime {
		get { return delayBetweenActionRepetitions; }
		set { delayBetweenActionRepetitions = value; }
	}
		
	protected override void Start () {
		base.Start ();
		repeatableAction = new UnityEngine.Events.UnityAction (Shot);
	}

	protected override void FixedUpdate() {
		if (Shooting) {
			base.FixedUpdate ();
		}
	}

	protected virtual Quaternion BulletRotation() {
		return this.gameObject.transform.rotation;
	}

	protected virtual Vector3 BulletPosition() {
		return this.gameObject.transform.position;
	}
		
    protected virtual void Shot () {
		if (bulletTemplate == null) {
			return;
		}

		GameObject newBullet = (GameObject) Instantiate (bulletTemplate);
		Utils.AssignTransformFromTo(this.gameObject.transform, newBullet.transform);

		Rigidbody2D rb2D = newBullet.GetComponent<Rigidbody2D> ();
		Vector2 newBulletVelocity = this.gameObject.transform.up * bulletSpeed;
		Rigidbody2D_ex.SetScaledVelocity(rb2D, newBulletVelocity);

		AddComponentsToBullet (newBullet);
	}

	protected virtual void AddComponentsToBullet(GameObject bullet) {
		bullet.AddComponent<Rigidbody2D_ex> ();
	}

	public static void TrySetShooting(GameObject gameObject, bool shooting) {
		Weapon weapon = gameObject.GetComponent<Weapon> ();
		if (weapon != null) {
			weapon.Shooting = shooting;
		}
	}
}
}