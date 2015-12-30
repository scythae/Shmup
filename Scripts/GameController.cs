using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	private const float delay_startGame = 1;
	private const float delay_gameOver = 1;
	private const string warningText_startGame = "Start!";
	private const string warningText_gameOver = "Game over.";
	private string[] defaultMenuSet = new string[] {"New game", "Exit"};
	private string[] pausedMenuSet = new string[] {"Resume", "New game", "Exit"};

	private Stage stage;

	void Start () {
		Design.stageInfo.SetActive (false);
		MainMenu.AddMenuAction ("New game", new UnityAction (NewGame));
		MainMenu.AddMenuAction ("Exit", new UnityAction (ExitGame));
		MainMenu.AddMenuAction ("Resume", new UnityAction (ResumeGame));

		//MainMenu.DelayBeforeShowing = 0;
		MainMenu.Show(defaultMenuSet);
	}

	void NewGame () {
		Stage.Destroy ();
		Score.Reset();
		GameWarning.Show (warningText_startGame, delay_startGame);
		Invoke ("CreateStage", delay_startGame);
	}

	void CreateStage () {
		stage = Stage.Create ();
		stage.OnPause = new UnityAction<bool> (ShowOrHidePauseMenu);
	}

	void ShowOrHidePauseMenu (bool paused) {
		if (paused) {
		//	MainMenu.DelayBeforeShowing = 0;
			MainMenu.Show (pausedMenuSet);
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
		Stage.Destroy ();
		Application.Quit ();
	}

	public void GameOver() {
		Stage.Destroy ();
		GameWarning.Show (warningText_gameOver, delay_gameOver);
//		MainMenu.DelayBeforeShowing = delay_gameOver;
		MainMenu.ShowAfterDelay(defaultMenuSet, delay_gameOver);
	}
}
