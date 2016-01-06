using UnityEngine;

public class StageInhabitant : MonoBehaviour {
	public static void DestroyAll () {
		foreach (StageInhabitant stageInhabitant in FindObjectsOfType<StageInhabitant> ()) {
			Destroy (stageInhabitant.gameObject);
		}
	}
}
