using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffWielder : MonoBehaviour {
	public List<Buff> buffs = new List<Buff> ();

	void Update () {
		int deletedCount = buffs.RemoveAll (x =>  x == null);
		if (deletedCount > 0)
			StageInfo.instance.Buffs = buffs;
	}

	public void ApplyBuff<T> () where T : Buff {
		T buff = buffs.Find (x => x is T) as T;
		if (buff == null) {
			buff = this.gameObject.AddComponent<T>();				
			buffs.Add (buff);

			StageInfo.instance.Buffs = buffs;
		} else
			buff.Refresh ();

		StageInfo.instance.BuffCaption = buff.caption;
	}

	public void RemoveBuff<T> () where T : Buff{
		foreach (Buff buff in buffs.FindAll(x => x is T))
			Destroy (buff);
	}

	public float GetModifierValue (ShipModifierType shipModifierType) {
		float result = 1;

		foreach (Buff buff in buffs)
			foreach (Buff.Modifier modifier in buff.modifiers)
				if (modifier.modifierType == shipModifierType) 
					result *= modifier.value;

		return result;
	}

	public static void ApplyBuff<T>(GameObject gameobject) where T: Buff{
		BuffWielder buffWielder = gameobject.GetComponent<BuffWielder> ();
		if (buffWielder != null)
			buffWielder.ApplyBuff<T> ();
	}

	public static void RemoveBuff<T> (GameObject gameobject) where T: Buff {
		BuffWielder buffWielder = gameobject.GetComponent<BuffWielder> ();
		if (buffWielder != null)
			buffWielder.RemoveBuff<T> ();
	}
}