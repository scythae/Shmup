using System;
using UnityEngine;
using UnityEngine.UI;

public static class Score {
	private static int val = 0;
	public static Text text;

	public static int Get() {
		return val;
	}

	public static void Add(int score) {
		val += score;
		ShowScore ();
	}

	private static void ShowScore() {
		if (text != null) {
			text.text = "Score:" + Environment.NewLine + val.ToString ();
		}
	}

	public static void Reset() {
		val = 0;
		ShowScore ();
	}
}

public static class HitPoints {
	public static Text text;

	public static void Show(int hitPoints) {
		if (text != null) {
			text.text = "HitPoints:" + Environment.NewLine + hitPoints.ToString ();
		}
	}
}

//public class ScoreText : Text {
//	private int val = 0;
//	public int ScoreGet() {
//		return val;
//	}
//
//	public void ScoreAdd(int score) {
//		val += score;
//		ScoreShow ();
//	}
//
//	private void ScoreShow() {
//		text = "Score:" + Environment.NewLine + val.ToString ();
//	}
//
//	public void ScoreReset() {
//		val = 0;
//		ScoreShow ();
//	}
//}