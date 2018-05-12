using System;
using UnityEngine;
using UnityEngine.UI;

public class Design
{
	public static GameObject landscape = GameObject.Find("Landscape");
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
	public static GameObject invulnerability = Resources.Load("pf_Invulnerability") as GameObject;

	public static GameObject staticDecoration = Resources.Load("pf_StaticDecoration") as GameObject;

	public static GameObject panel = Resources.Load("pf_Panel") as GameObject;
	public static GameObject textItem = Resources.Load ("pf_TextItem") as GameObject;
	public static GameObject stageInfo = Resources.Load ("pf_StageInfo") as GameObject;
	

	public static void SetScale(float scale) {
		GameObject[] objs = {enemy, player, enemyBullet, playerBullet, invulnerability};

		foreach(GameObject obj in objs) {
			if (obj.transform == null) 
				continue;
			
			obj.transform.localScale = new Vector2 (scale, scale);
		}
	}

	public class Animations {
		public static AnimationClip explosion = Resources.Load("Animations/Explosion") as AnimationClip;
	}
}