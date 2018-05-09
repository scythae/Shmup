using UnityEngine;
using UnityEngine.UI;

public class TemporaryText : MonoBehaviour  {
	private DelayedAction delayedAction;

	public static TemporaryText Create () {
		return Create<TemporaryText>();
	}

	public static T Create<T>() where T : TemporaryText {
		T result = Instantiate(Prefab.textItem).AddComponent<T>();
		result.Clear();
		return result;
	}

	public void SetText(string text, float lifeTime) {
		this.gameObject.GetComponent<Text>().text = text;		
		SetDelayedAction(lifeTime);
	}

	private void SetDelayedAction(float delay) {
		if (delay <= 0)
			return;
		
		if (delayedAction != null)
			Destroy (delayedAction);

		delayedAction = DelayedAction.Do(DoAfterDelay, delay);
	}

	private void DoAfterDelay() {
		if (this != null && this.gameObject != null)
			Clear();		
	}

	private void Clear() {
		this.gameObject.GetComponent<Text>().text = "";		
	}

	private void OnDestroy() {
		Destroy(this.gameObject);
	}
}