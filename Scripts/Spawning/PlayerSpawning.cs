using UnityEngine;
using System.Collections;

public class PlayerSpawning : MonoBehaviour {
	private GameObject shipTemplate;

	void Start () {
		shipTemplate = (GameObject)Resources.Load ("pf_Player");
		SpawnPlayer();
	}

	void SpawnPlayer() {
		PlayerZone shipZone = new PlayerZone ();

		GameObject ship = Instantiate (shipTemplate, shipZone.SpawnPosition(), shipTemplate.transform.rotation) as GameObject; 

		Player player = ship.AddComponent<Player> ();
		player.zone = shipZone;
		ship.AddComponent<Weapon.PlayerSimpleGun> (); 
	}
}
