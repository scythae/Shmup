using UnityEngine;

public class Disposable : MonoBehaviour
{
	protected float lifeTime;

	private void Start() {
		DelayedAction.Do(DestroySelf, lifeTime);		
	}

	private void DestroySelf() {		
		if (this.gameObject)
			Destroy(this.gameObject);
	}
}
