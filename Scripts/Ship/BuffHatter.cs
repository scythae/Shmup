using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffHatter : MonoBehaviour {
	public List<Buff> buffs = new List<Buff> ();

	protected void Update () {
		buffs.RemoveAll (x => Time.time > x.startTime + x.duration);
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
		}

		availableBuff.startTime = Time.time;
	}

	public void RemoveBuff(Buff buff) {
		buffs.Remove (buff);
		buffs.TrimExcess ();
	}

	public static void ApplyBuff(GameObject gameobject, Buff buff) {
		BuffHatter buffHatter = gameobject.GetComponent<BuffHatter> ();
		if (buffHatter != null)
			buffHatter.ApplyBuff(buff);
	}

	public static void RemoveBuff(GameObject gameobject, Buff buff) {
		BuffHatter buffHatter = gameobject.GetComponent<BuffHatter> ();
		if (buffHatter != null)
			buffHatter.RemoveBuff(buff);
	}
}


