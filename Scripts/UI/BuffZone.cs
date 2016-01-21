using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuffZone : PanelWithChildren {
	private const string itemPrefix = "buff_";
	private const string infinitySymbol = "\u221E";
	private const int shortCaptionLength = 5;
	private const int shortDurationLength = 4;
	private static Vector2 itemSize = new Vector2(0.5f, 0.5f); 
	private static Vector2 selfSize = new Vector2(2, 2); 
	
	private List<Buff> buffs;

	public static new BuffZone Create() {
		BuffZone result = PanelWithChildren.Create<BuffZone> (); 

		RectTransform rt = result.gameObject.transform as RectTransform;
		rt.offsetMax = rt.offsetMin + selfSize;

		result.SetAligner_FixedTile(itemSize);
		result.alignDirection = Aligner.AlignDirection.adTop;

		return result;
	}

	protected override void Update() {
		base.Update();
		UpdateCaptions ();
	}

	private void UpdateCaptions () {// place for optimization here
		if (buffs == null || items == null)
			return;

		foreach (Buff buff in buffs) {
			if (buff == null)
				continue;

			GameObject item = items.Find(x => x.gameObject.name == itemPrefix + buff.caption);
			if (item == null) 
				continue;
			
			LabeledInformation Li = item.GetComponent<LabeledInformation> ();
			if (Li == null)
				continue;

			if (buff.duration == Buff.unlimitedDuration) 
				Li.value = infinitySymbol;
			else				
				Li.value = string.Format("{0:F2}", buff.timeLeft);
		}
	}

	public void Reinitialize (List<Buff> buffs) {
		if (buffs == null)
			return;

		DestroyChildren ();

		items = new List<GameObject> ();
		foreach (Buff buff in buffs)
			if (buff != null)
				items.Add(CreateLabeledInformation(buff).gameObject);

		Rebuild ();

		this.buffs = buffs;
	}

	private static LabeledInformation CreateLabeledInformation(Buff buff) {
		LabeledInformation result = LabeledInformation.Create();
		result.gameObject.name = itemPrefix + buff.caption;
		result.caption = buff.caption.Substring(0, shortCaptionLength);
		result.value = buff.duration.ToString();

		return result;
	}
}