using UnityEngine;

public class BuffPlacer: Bonus {
	public override void Interact(GameObject interactor) {
		base.Interact (interactor);
		ApplyBuff (interactor);
	}

	protected virtual void ApplyBuff(GameObject target) {		
	}
}