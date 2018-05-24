using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : Ship {
	private delegate void SwitchAction(bool switchState);
	private Dictionary<string, SwitchAction> buttonActions;

	protected override void Start () {
		base.Start ();
		this.gameObject.AddComponent<BuffWielder> ();
		DamageSource.AddToGameObject<DS_Ship> (this.gameObject, this.hitPoints, UnitSide.usEnemy);

		Speed = 5;
		hitPoints = 5;
		unitSide = UnitSide.usPlayer;

		buttonActions = new Dictionary<string, SwitchAction> ();
		buttonActions.Add("Fire", SetShooting);
		buttonActions.Add("Accelerate", SetBuff<Buffs.Madness>);

	}

	protected virtual void OnEnable()	{
		ProcessButtonsOnEnable();
	}

	private void ProcessButtonsOnEnable() {
		if (buttonActions == null)
			return;

		foreach (KeyValuePair<string, SwitchAction> buttonAction in buttonActions)
			buttonAction.Value(Input.GetButton(buttonAction.Key));
	}

	protected virtual void Update() {	
		ProcessButtons();
	}

	private void ProcessButtons() {
		if (buttonActions == null)
			return;

		foreach (KeyValuePair<string, SwitchAction> buttonAction in buttonActions)
			ExecuteButtonAction(buttonAction.Key, buttonAction.Value);
	}

	private void ExecuteButtonAction (string ButtonName, SwitchAction action) {
		if (Input.GetButtonDown (ButtonName))
			action(true);
		if (Input.GetButtonUp (ButtonName))
			action(false);
	}

	private void SetShooting (bool state) {
		Equipment.TrySetShooting (this.gameObject, state);
	}

	private void SetBuff<T> (bool state) where T : Buff{
		if (state)
			BuffWielder.ApplyBuff<T>(this.gameObject);
		else
			BuffWielder.RemoveBuff<T>(this.gameObject);
	}

	protected void FixedUpdate () {
		Vector2 movement = new Vector2(0, 0);

		movement.x += Input.GetAxis ("Horizontal");
		movement.y += Input.GetAxis ("Vertical");

		this.Move (movement);
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