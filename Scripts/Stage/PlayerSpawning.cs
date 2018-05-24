using UnityEngine;
using System.Collections;

public class PlayerSpawning : MonoBehaviour {
	private GameObject shipTemplate;
	private PlayerZone shipZone;
	public UnityEngine.Events.UnityAction<Ship> onShipDeath;
	public UnityEngine.Events.UnityAction<Ship> onChangeHitPoints;

	void Start () {
		shipTemplate = (GameObject)Resources.Load ("pf_Player");
		shipZone = new PlayerZone ();
		SpawnPlayer();
	}

	void SpawnPlayer() {
		GameObject ship = Instantiate (shipTemplate, shipZone.SpawnPosition(), shipTemplate.transform.rotation) as GameObject; 
		ship.transform.parent = this.gameObject.transform;

		Player player = ship.AddComponent<Player> ();
		player.zone = shipZone;
		player.onDeath = onShipDeath;
		player.onChangeHitPoints = this.onChangeHitPoints;
		ship.AddComponent<PlayerSimpleGun> (); 
	}
}
