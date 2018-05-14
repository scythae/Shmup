﻿using UnityEngine;
using System.Collections;

public class EnemySpawning : PausableRepetition {
	private GameObject shipTemplate;
	private EnemyZone shipZone;
	private const float respawnPeriod = 0.9f;

	public UnityEngine.Events.UnityAction<Ship> onShipDeath;

	protected override void Start() {
		base.Start();
		repeatableAction = new UnityEngine.Events.UnityAction (Spawn);
		delayBetweenActionRepetitions = respawnPeriod;
		shipZone = new EnemyZone();
		shipTemplate = Prefab.enemy;
	}
		
	void Spawn() {	
		GameObject ship = Instantiate(shipTemplate, shipZone.SpawnPosition(), shipTemplate.transform.rotation) as GameObject; 

		ship.transform.parent = this.gameObject.transform;
		Enemy enemy = ship.AddComponent<Enemy>();
		enemy.zone = shipZone;
		enemy.onDeath = onShipDeath;
		ship.AddComponent<Weapon.EnemySimpleGun>();
		ship.AddComponent<Weapon.Dropper>().AddDrop(Prefab.invulnerability, 1.00f /*0.05f*/);
		ship.AddComponent<Rigidbody2D_ex>();
	}
}