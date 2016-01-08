using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BuffHatter : MonoBehaviour {
	private class BuffChangeEvent : UnityEvent<List<Buff>> {};
	private BuffChangeEvent OnBuffListChange = new BuffChangeEvent ();
	private BuffZone buffZone;

	public List<Buff> buffs = new List<Buff> ();

	void Start() {
		buffZone = BuffZone.Create();
		OnBuffListChange.AddListener (buffZone.OnBuffListChange);
	}

	protected void Update () {
		int deletedCount = buffs.RemoveAll (x => Time.time > x.startTime + x.duration);
		if (deletedCount > 0)
			OnBuffListChange.Invoke(buffs);
	}
		
	public float GetModifierValue(ShipModifierType shipModifierType) {
		float result = 1;

		foreach (Buff buff in buffs)
			foreach (Buff.Modifier modifier in buff.modifiers)
				if (modifier.modifierType == shipModifierType) 
					result *= modifier.value;

		return result;
	}

	public void ApplyBuff(Buff buff) {
		if (buff == null)
			return;

		Buff availableBuff = buffs.Find (x => x.caption == buff.caption);
		if (availableBuff == null) {
			availableBuff = buff;			
			buffs.Add (availableBuff);

			OnBuffListChange.Invoke(buffs);
		}

		availableBuff.startTime = Time.time;
	}

	public static void ApplyBuff(GameObject gameobject, Buff buff) {
		BuffHatter buffHatter = gameobject.GetComponent<BuffHatter> ();
		if (buffHatter != null)
			buffHatter.ApplyBuff(buff);
	}

	void OnDestroy () {
		Destroy (buffZone.gameObject);
	}
}


