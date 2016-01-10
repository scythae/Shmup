using UnityEngine;
using UnityEngine.UI;

public class TemporaryText : MonoBehaviour  {
	private Text textComponent;
	private DelayedAction clearAction;

	public float delay = 0;

	public static TemporaryText Create () {
		return Create<TemporaryText> ();
	}

	public static T Create<T> () where T : TemporaryText {
		T result = Instantiate(Prefab.textItem).AddComponent<T> ();
		result.textComponent = result.gameObject.GetComponent<Text> ();
		result.textComponent.text = "";
		return result;
	}

	public string text {
		get { return textComponent.text; }
		set { Show (value, delay); }
	}

	private void Show(string text, float lifeTime) {
		if (textComponent == null)
			return;

		if (clearAction != null)
			Destroy (clearAction);

		textComponent.text = text;
		if (lifeTime > 0)
			clearAction = DelayedAction.Do(Clear, lifeTime );
	}

	private void Clear() {
		textComponent.text = "";
	}

	private void OnDestroy() {
		Destroy (this.gameObject);
	}
}