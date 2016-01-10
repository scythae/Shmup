using UnityEngine;

public class DelayedAction : MonoBehaviour  {
	private static GameObject obj;

	public delegate void Action();
	private Action action;
	private float finishTime;

	public static DelayedAction Do(Action action, float delay) {
		if (obj == null) {			
			obj = new GameObject();
			obj.name = "objectForDelayedActions";
		}

		DelayedAction result = obj.AddComponent<DelayedAction> ();
		result.DoActionNonStatic (action, delay);
		return result;
	}

	private void DoActionNonStatic(Action action, float delay) {
		this.action = action;
		finishTime = Time.time + delay;
	}

	private void Update() {
		if (action == null)
			return;

		if (Time.time < finishTime) 
			return;

		action.DynamicInvoke(null);	
		enabled = false;
		Destroy (this);
	}
}