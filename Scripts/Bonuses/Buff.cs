using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ShipModifierType {smtMoveSpeed, smtIncomingDamage, smtOutcomingDamage};

public class Buff {
	public class Modifier {
		public ShipModifierType modifierType;
		public float value;
	}

	public string caption;
	public Modifier[] modifiers;
	public float duration;
	public float startTime;
}