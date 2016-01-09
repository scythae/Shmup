using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffHatter : MonoBehaviour {
	private const float buffRecalculatePeriod = 0.1f;
	private class BuffChangeEvent : UnityEvent<List<Buff>> {};
	private BuffChangeEvent OnBuffListChange = new BuffChangeEvent ();
	private BuffZone buffZone;

	public List<Buff> buffs = new List<Buff> ();

	void Start() {
		buffZone = BuffZone.Create ();
		OnBuffListChange.AddListener (buffZone.OnBuffListChange);
	}

	void Update () {
		int deletedCount = buffs.RemoveAll (x =>  x == null);
		if (deletedCount > 0)
			OnBuffListChange.Invoke(buffs);
	}

	public void ApplyBuff<T> () where T : Buff {
		T buff = buffs.Find (x => x is T) as T;
		if (buff == null) {
			buff = Buff.Create<T> ();					
			buffs.Add (buff);

			BuffZone.ShowBuffTitle(buff);
			OnBuffListChange.Invoke(buffs);
		} else {
			buff.Refresh ();
		}
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
		BuffHatter buffHatter = gameobject.GetComponent<BuffHatter> ();
		if (buffHatter != null)
			buffHatter.ApplyBuff<T> ();
	}

	public static void RemoveBuff<T> (GameObject gameobject) where T: Buff {
		BuffHatter buffHatter = gameobject.GetComponent<BuffHatter> ();
		if (buffHatter != null)
			buffHatter.RemoveBuff<T> ();
	}

	void OnDestroy () {
		if (buffZone != null)
			Destroy (buffZone.gameObject);
	}
}