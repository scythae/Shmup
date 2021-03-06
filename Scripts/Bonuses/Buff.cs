﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ShipModifierType {smtMoveSpeed, smtIncomingDamage, smtOutcomingDamage};

public class Buff : PausableRepetition {
	public class Modifier {
		public ShipModifierType modifierType;
		public float value;
	}
	public const float unlimitedDuration = 0;

	public string caption;
	public Modifier[] modifiers;
	public float duration;

	private float fTimeLeft = 0;
	public float timeLeft {	
		get { return Mathf.Max(0, fTimeLeft); }
	}

	protected override void Start () {
		base.Start ();
		repeatableAction = new UnityEngine.Events.UnityAction (CalculateTimeLeft);
		delayBetweenActionRepetitions = 0.1f;
		Refresh ();
	}

	private void CalculateTimeLeft () {
		if (duration == unlimitedDuration)
			return;
		
		fTimeLeft -= delayBetweenActionRepetitions;
	}

	public void Refresh() {
		fTimeLeft = duration;
	}

	protected override void FixedUpdate () {
		base.FixedUpdate ();

		if (fTimeLeft < 0)
			Destroy (this);
	}
}