using UnityEngine;

public class Repetition : MonoBehaviour {
	protected float delayBetweenActionRepetitions;
	protected UnityEngine.Events.UnityAction repeatableAction;
	protected float lastActionTime;

	protected virtual void Start () {
		lastActionTime = Time.time;
	}

	protected virtual void Update () {
	}

	protected virtual void FixedUpdate() {
		if (repeatableAction == null) {
			return;
		}

		if (Time.time - lastActionTime < delayBetweenActionRepetitions) {
			return;
		}

		repeatableAction.Invoke();

		lastActionTime = Time.time;
	}
}