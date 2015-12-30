using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Stage : MonoBehaviour {
	public UnityAction<bool> OnPause;

	private static Stage instance;

	private bool fPaused = false;
	public bool Paused {
		get { return fPaused; }
		set { SetPaused (value); }
	}

	public static Stage Create () {
		if (instance != null) {
			return instance;
		}
			
		GameObject stageInfo = (GameObject) Instantiate(Prefab.stageInfo);
		stageInfo.transform.SetParent (Design.canvas.transform, false);
		stageInfo.name = "panel_Stage";
		stageInfo.AddComponent <EnemySpawning> ();
		stageInfo.AddComponent <PlayerSpawning> ();
		instance = stageInfo.AddComponent<Stage> ();

		Score.text = GameObject.Find ("Score").GetComponent<Text> ();
		HitPoints.text = GameObject.Find ("HitPoints").GetComponent<Text> ();
		Score.Reset ();

		return instance;
	}

	public static void Destroy () {
		if (instance == null) {
			return;
		}

		if (!instance.isActiveAndEnabled) {
			instance.OnDestroy ();
		}
		Destroy (instance);
	}

	void OnDestroy () {
		Destroy (instance.gameObject);
		DamageSource.DestroyAllMaintainedGameobjects ();
	}

	void Update () {
		if (Input.GetButtonDown("Cancel")) {	
			SetPaused (!fPaused);
		}
	}

	void SetPaused (bool paused) {
		fPaused = paused;

		Rigidbody2D_ex.SetAllPaused (fPaused);
		PausableRepetition.SetAllPaused (fPaused);

		if (OnPause != null) {
			OnPause.Invoke (fPaused);
		}
	}
}
