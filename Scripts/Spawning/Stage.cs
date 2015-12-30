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
			
		GameObject stagePanel = (GameObject) Instantiate(Prefab.panel);
		stagePanel.name = "panel_Stage";
		stagePanel.AddComponent <EnemySpawning> ();
		stagePanel.AddComponent <PlayerSpawning> ();
		stagePanel.transform.SetParent (Design.gameController.transform, false);

		instance = stagePanel.AddComponent<Stage> ();
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
		instance.DeactivateStageInfo();
		Destroy (instance.gameObject);
		DamageSource.DestroyAllMaintainedGameobjects ();
	}

	void Start () {
		ActivateStageInfo ();
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

	void DeactivateStageInfo() {
		Design.stageInfo.SetActive (false);
	}

	void ActivateStageInfo() {
		Design.stageInfo.SetActive (true);
	}
}
