using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuffZone : Tiler {
	private const string itemPrefix = "buff_";
	private const string infinitySymbol = "\u221E";
	private const int shortCaptionLength = 5;
	private const int shortDurationLength = 4;
	private static Vector2 itemSize = new Vector2(0.5f, 0.5f); 
	
	private List<Buff> buffs;
	public UnityAction<List<Buff>> OnBuffListChange;

	public BuffZone() : base() {
		OnBuffListChange = new UnityAction<List<Buff>>(Reinitialize);
	}

	public static new BuffZone Create() {
		BuffZone result = Tiler.Create<BuffZone> (); 
		result.SetParent(Design.canvas);
		result.spacingRelativeToItemSize = new Vector2 (0, 0);
		result.rectTransform.offsetMin = new Vector2(6, 0);
		result.rectTransform.offsetMax = new Vector2(8, 2);
		result.colCount = 4;
		result.rowCount = 4;

		return result;
	}

	protected override void Update() {
		base.Update();
		UpdateCaptions ();
	}

	public static void ShowBuffTitle (Buff buff) {
		GameWarning.Show(buff.caption, 2f, GameWarning.rect_RightDownSmall);
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
			else {
				string timeLeft = buff.timeLeft.ToString();
				timeLeft = timeLeft.Substring(0, Mathf.Min(shortDurationLength, timeLeft.Length));
				Li.value = timeLeft;
			}
		}
	}

	private void Reinitialize (List<Buff> buffs) {
		if (buffs == null)
			return;

		List<GameObject> items = new List<GameObject> ();

		foreach (Buff buff in buffs)
			if (buff != null)
				items.Add(CreateLabeledInformation(buff).gameObject);

		SetItems (items);

		Rebuild ();

		this.buffs = buffs;
	}

	private static LabeledInformation CreateLabeledInformation(Buff buff) {
		LabeledInformation result = LabeledInformation.Create();
		result.gameObject.name = itemPrefix + buff.caption;
		result.rectTransform.offsetMax = result.rectTransform.offsetMin + itemSize;
		result.caption = buff.caption.Substring(0, shortCaptionLength);
		result.value = buff.duration.ToString();

		return result;
	}
}
