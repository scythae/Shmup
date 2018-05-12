using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum UnitSide {usNone, usPlayer, usEnemy, usBoth};

public class Stage : MonoBehaviour {
	public UnityAction<bool> OnPause;

	private bool fPaused = false;
	public bool Paused {
		get { return fPaused; }
		set { SetPaused (value); }
	}

	public static Stage Create () {
		Stage result = null;

		StageInfo.instance.Score = 0;
		StageInfo.instance.HitPoints = 0;
		result = StageInfo.instance.gameObject.AddComponent<Stage> (); 

		EnemySpawning enemySpawning = result.gameObject.AddComponent <EnemySpawning> ();
		enemySpawning.onShipDeath = new UnityAction<Ship>(result.OnEnemyDeath);
		PlayerSpawning playerSpawning = result.gameObject.AddComponent <PlayerSpawning> ();
		playerSpawning.onShipDeath = new UnityAction<Ship>(result.OnPlayerDeath);
		playerSpawning.onChangeHitPoints = new UnityAction<Ship>(result.OnPlayerChangeHitPoints);

		return result;
	}

	private void OnEnemyDeath (Ship ship) {
		StageInfo.instance.Score += 100;
	}

	private void OnPlayerDeath (Ship ship) {
		Design.gameController.GameOver ();
	}

	private void OnPlayerChangeHitPoints (Ship ship) {
		StageInfo.instance.HitPoints = (int) Mathf.Round(ship.hitPoints);
	}

	public void OnDestroy () {
		StageInhabitant.DestroyAll ();
		Destroy (this.gameObject);	
	}

	private void Update () {
		if (Paused)
			return;
			
		if (Input.GetButtonDown("Cancel")) {	
			SetPaused (!Paused);
		}
	}

	private void SetPaused (bool paused) {
		fPaused = paused;
		
		Rigidbody2D_ex.SetAllPaused(paused);
		PausableRepetition.SetAllPaused(paused);

		if (OnPause != null) {
			OnPause.Invoke (paused);
		}
	}
}
