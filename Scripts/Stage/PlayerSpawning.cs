using UnityEngine;
using System.Collections;

public class PlayerSpawning : MonoBehaviour {
	private GameObject shipTemplate;
	public UnityEngine.Events.UnityAction<Ship> onShipDeath;
	public UnityEngine.Events.UnityAction<Ship> onChangeHitPoints;

	void Start () {
		shipTemplate = (GameObject)Resources.Load ("pf_Player");
		SpawnPlayer();
	}

	void SpawnPlayer() {
		PlayerZone shipZone = new PlayerZone ();

		GameObject ship = Instantiate (shipTemplate, shipZone.SpawnPosition(), shipTemplate.transform.rotation) as GameObject; 

		Player player = ship.AddComponent<Player> ();
		player.zone = shipZone;
		player.onDeath = onShipDeath;
		player.onChangeHitPoints = this.onChangeHitPoints;
		ship.AddComponent<Weapon.PlayerSimpleGun> (); 
	}
}
