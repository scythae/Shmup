using UnityEngine;
using System.Collections;

public class EnemySpawning : PausableRepetition {
	private GameObject shipTemplate;
	private EnemyZone shipZone;
	private const float respawnPeriod = 0.9f;

	protected override void Start () {
		base.Start ();
		repeatableAction = new UnityEngine.Events.UnityAction (Spawn);
		delayBetweenActionRepetitions = respawnPeriod;
		shipZone = new EnemyZone ();
		shipTemplate = Prefab.enemy;
	}
		
	void Spawn() {	
		GameObject ship = Instantiate (shipTemplate, shipZone.SpawnPosition(), shipTemplate.transform.rotation) as GameObject; 

		Enemy enemy = ship.AddComponent<Enemy> ();
		enemy.zone = shipZone;
		ship.AddComponent<Weapon.EnemySimpleGun> ();
		ship.AddComponent<Weapon.Dropper> ().content = new GameObject[] {Prefab.invulnerability};
		ship.AddComponent<Rigidbody2D_ex> ();
	}
}