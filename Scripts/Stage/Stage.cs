using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum UnitSide {usNone, usPlayer, usEnemy, usBoth};

public class Stage : MonoBehaviour {
	private const float DelayBeforeGameOver = 1.5f;
	private const int ScoreForKillingEnemy = 100;
	public UnityAction<bool> OnPause;

	private bool fPaused = false;
	public bool Paused {
		get { return fPaused; }
		set { SetPaused (value); }
	}

	public static Stage Create () {
		GameObject go = new GameObject("Stage");
		StageInfo.instance.gameObject.transform.SetParent(go.transform);

		StageInfo.instance.Score = 0;
		StageInfo.instance.HitPoints = 0;

		Utils.AssignTransformFromTo(Design.visibleArea.transform, go.transform);
		Utils.ProvideCanvas(go);

		Stage result = go.AddComponent<Stage>();

		EnemySpawning enemySpawning = go.AddComponent<EnemySpawning>();
		enemySpawning.onShipDeath = new UnityAction<Ship>(result.OnEnemyDeath);
		PlayerSpawning playerSpawning = go.AddComponent<PlayerSpawning>();
		playerSpawning.onShipDeath = new UnityAction<Ship>(result.OnPlayerDeath);
		playerSpawning.onChangeHitPoints = new UnityAction<Ship>(result.OnPlayerChangeHitPoints);

		return result;
	}

	private void OnEnemyDeath (Ship ship) {
		StageInfo.instance.Score += ScoreForKillingEnemy;
	}

	private void OnPlayerDeath (Ship ship) {		
		PlaySound.PlayerDeath();
		DelayedAction.Do(Design.gameController.GameOver, DelayBeforeGameOver);
	}

	private void OnPlayerChangeHitPoints (Ship ship) {
		StageInfo.instance.HitPoints = (int) Mathf.Round(ship.hitPoints);
	}

	public void OnDestroy () {
		Destroy(this.gameObject);	
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

		Utils.SetEnabled(this.gameObject, !paused);

		if (OnPause != null) {
			OnPause.Invoke (paused);
		}
	}
}
