using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	private const float delay_startGame = 1;
	private const float delay_gameOver = 1;
	private const string warningText_startGame = "Start!";
	private const string warningText_gameOver = "Game over.";

	private Stage stage;

	void Start () {
		Prefab.SetScale(1f);

		MainMenu.AddMenuAction (MainMenu.itemCaption_NewGame, new UnityAction (NewGame));
		MainMenu.AddMenuAction (MainMenu.itemCaption_ExitGame, new UnityAction (ExitGame));
		MainMenu.AddMenuAction (MainMenu.itemCaption_ResumeGame, new UnityAction (ResumeGame));
		MainMenu.Show(MainMenu.defaultMenuSet);
	}

	void NewGame () {
		DestroyStage ();
		GameWarning.Show (warningText_startGame, delay_startGame);
		Invoke ("CreateStage", delay_startGame);
	}

	void CreateStage () {
		stage = Stage.Create ();
		stage.OnPause = new UnityAction<bool> (ShowOrHidePauseMenu);
	}

	void ShowOrHidePauseMenu (bool paused) {
		if (paused) {
			MainMenu.Show (MainMenu.pausedMenuSet);
		} else {
			MainMenu.Hide ();
		}
	}

	void ResumeGame () {
		if (stage != null) {
			stage.Paused = false;
		}
	}
		
	void ExitGame () {		
		Debug.Log ("ExitGame");
		DestroyStage ();
		Application.Quit ();
	}

	public void GameOver() {
		DestroyStage ();
		GameWarning.Show (warningText_gameOver, delay_gameOver);
		MainMenu.ShowAfterDelay(MainMenu.defaultMenuSet, delay_gameOver);
	}

	private void DestroyStage() {
		if (stage == null) 
			return;

		Destroy (stage);
		if (!stage.isActiveAndEnabled) 
			stage.OnDestroy ();
	}
}
