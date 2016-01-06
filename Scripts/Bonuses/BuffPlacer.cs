using UnityEngine;

public class BuffPlacer: Bonus {
	public override void Interact(GameObject interactor) {
		base.Interact (interactor);

		Buff buff = newBuff ();
		BuffHatter.ApplyBuff(interactor, buff);
	}

	protected virtual Buff newBuff() {
		return null;
	}
}