namespace Buffs {
public class Madness : Buff {
	public Madness () : base () {
		caption = "Madness";
		modifiers = new Modifier[] {
			new Modifier() {modifierType = ShipModifierType.smtMoveSpeed, value = 2},
			new Modifier() {modifierType = ShipModifierType.smtIncomingDamage, value = 2},
			new Modifier() {modifierType = ShipModifierType.smtOutcomingDamage, value = 2}
		};

		duration = 0;
	}
}
}