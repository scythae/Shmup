using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	private const float delay_startGame = 1;
	private const float delay_gameOver = 1;
	private const string warningText_startGame = "Start!";
	private const string warningText_gameOver = "Game over.";

	private Stage stage;

	void Start () {
		Prefab.SetScale(0.5f);
//		TilerTester.Test();
//		LabeledInformationTester.Test ();
//		PanelWithChildrenTester.Test ();

		MainMenu.AddMenuAction (MainMenu.itemCaption_NewGame, new UnityAction (NewGame));
		MainMenu.AddMenuAction (MainMenu.itemCaption_ExitGame, new UnityAction (ExitGame));
		MainMenu.AddMenuAction (MainMenu.itemCaption_ResumeGame, new UnityAction (ResumeGame));
		MainMenu.Show(MainMenu.defaultMenuSet);
	}

	void NewGame () {
		Stage.Destroy ();
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
		Stage.Destroy ();
		Application.Quit ();
	}

	public void GameOver() {
		Stage.Destroy ();
		GameWarning.Show (warningText_gameOver, delay_gameOver);
		MainMenu.ShowAfterDelay(MainMenu.defaultMenuSet, delay_gameOver);
	}
}
