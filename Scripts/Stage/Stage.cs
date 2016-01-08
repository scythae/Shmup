using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Stage : MonoBehaviour {
	public UnityAction<bool> OnPause;

	private static Stage instance;

	private StageInfo stageInfo;
	public StageInfo info {
		get {return this.stageInfo; }
	}

	private bool fPaused = false;
	public bool Paused {
		get { return fPaused; }
		set { SetPaused (value); }
	}

	public static Stage Create () {
		if (instance != null) {
			return instance;
		}			

		StageInfo stageInfo = StageInfo.Create();
		stageInfo.Score = 0;
		stageInfo.HitPoints = 0;

		instance = stageInfo.gameObject.AddComponent<Stage> ();
		instance.stageInfo = stageInfo;

		EnemySpawning enemySpawning = stageInfo.gameObject.AddComponent <EnemySpawning> ();
		enemySpawning.onShipDeath = new UnityAction<Ship>(instance.OnEnemyDeath);
		PlayerSpawning playerSpawning = stageInfo.gameObject.AddComponent <PlayerSpawning> ();
		playerSpawning.onShipDeath = new UnityAction<Ship>(instance.OnPlayerDeath);
		playerSpawning.onChangeHitPoints = new UnityAction<Ship>(instance.OnPlayerChangeHitPoints);



		return instance;
	}

	private void OnEnemyDeath (Ship ship) {
		stageInfo.Score += 100;
	}

	private void OnPlayerDeath (Ship ship) {
		Design.gameController.GameOver ();
	}

	private void OnPlayerChangeHitPoints (Ship ship) {
		stageInfo.HitPoints = (int) Mathf.Round(ship.hitPoints);
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
		StageInhabitant.DestroyAll ();
		Destroy (instance.gameObject);	
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
