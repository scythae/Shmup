using System;

public static class Score {
	private static int val = 0;

	public static int Get() {
		return val;
	}

	public static void Add(int score) {
		val += score;
		ShowScore ();
	}

	private static void ShowScore() {
		Design.scoreText.text = "Score:" + Environment.NewLine + val.ToString ();
	}

	public static void Reset() {
		val = 0;
		ShowScore ();
	}
}

public static class HitPoints {
	public static void Show(int hitPoints) {
		Design.hitPointsText.text = "HitPoints:" + Environment.NewLine + hitPoints.ToString ();
	}
}
