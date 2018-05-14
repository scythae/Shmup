using System;
using UnityEngine;
using UnityEngine.UI;

public class Design
{
	public static GameObject landscape = GameObject.Find("Landscape");
	public static GameObject battlefield = GameObject.Find("Battlefield");
	public static GameObject MainCamera = GameObject.Find("MainCamera");
	public static GameObject visibleArea = GameObject.Find("VisibleArea");
	public static GameController gameController = GameObject.Find("GameController").GetComponent<GameController> ();
}

public class Prefab
{
	private static GameObject Load(string name) {
		return Resources.Load(name) as GameObject;
	}

	public static GameObject enemy = Load("pf_Enemy");
	public static GameObject player = Load("pf_Player");

	private static GameObject bullet = Load("pf_Bullet");
	public static GameObject enemyBullet = MakeBullet(Sprites.enemyBullet);
	public static GameObject playerBullet = MakeBullet(Sprites.playerBullet);
	public static GameObject invulnerability = LoadInvulnerability();

	public static GameObject panel = Load("pf_Panel");
	public static GameObject textItem = Load ("pf_TextItem");


	private static GameObject LoadInvulnerability() {
		GameObject result = MakeBullet(Sprites.invulnerability);
		result.AddComponent<Invulnerability>();
		return result;
	}

	private static GameObject MakeBullet(Sprite sprite) {	
		GameObject result = UnityEngine.Object.Instantiate(bullet) as GameObject;
		result.GetComponent<SpriteRenderer>().sprite = sprite;
		return result;
	}

	public static void SetScale(float scale) {
		GameObject[] objs = {enemy, player, bullet};

		foreach(GameObject obj in objs) {
			if (obj.transform == null) 
				continue;
			
			obj.transform.localScale = new Vector2 (scale, scale);
		}
	}

	public class Animations {
		public static AnimationClip explosion = Resources.Load("Animations/Explosion") as AnimationClip;
	}

	public class Sprites {
		private static Sprite Load(string name) {
			return Resources.Load<Sprite>(name);
		}

		public static Sprite invulnerability = Load("Sprites/Invulnerability");
		public static Sprite playerBullet = Load("Sprites/PlayerBullet");
		public static Sprite enemyBullet = Load("Sprites/EnemyBullet");
	}
}