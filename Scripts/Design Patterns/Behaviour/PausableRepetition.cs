using UnityEngine;

public class PausableRepetition : Repetition {
	protected float pausedTime;

	protected override void Start () {
		base.Start ();
		pausedTime = Time.time;
	}

	protected virtual void OnDisable () {
		pausedTime = Time.time;
	}

	protected virtual void OnEnable () {
		lastActionTime += (Time.time - pausedTime);
	}
		
	public bool Paused {
		get { return !enabled; }
		set { enabled = !value; }
	}
}

public class Pausable: MonoBehaviour {	
}