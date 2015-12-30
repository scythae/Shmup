using System;
using UnityEngine;
using UnityEngine.UI;

public class Design
{
	public static GameObject background = GameObject.Find("Background");
	public static Text scoreText = GameObject.Find("Score").GetComponent<Text>();
	public static Text hitPointsText = GameObject.Find("HitPoints").GetComponent<Text>();

	public static GameObject stageInfo = GameObject.Find("StageInfo");
	public static GameObject battlefield = GameObject.Find("Battlefield");

	public static GameObject canvas = GameObject.Find("Canvas");
	public static GameController gameController = GameObject.Find("GameController").GetComponent<GameController> ();
}

public class Prefab
{
	public static GameObject enemy = Resources.Load("pf_Enemy") as GameObject;
	public static GameObject player = Resources.Load("pf_Player") as GameObject;
	public static GameObject enemyBullet = Resources.Load("pf_EnemyBullet") as GameObject;
	public static GameObject playerBullet = Resources.Load("pf_PlayerBullet") as GameObject;
	public static GameObject panel = Resources.Load("pf_Panel") as GameObject;
	public static GameObject gameWarning = Resources.Load("pf_GameWarning") as GameObject;
	public static GameObject textItem = Resources.Load ("pf_TextItem") as GameObject;
}