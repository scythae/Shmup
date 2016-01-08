using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageInfo : Tiler {
	private static Vector2 itemSize = new Vector2(2, 0.75f); 
	private LabeledInformation LiScore;
	private LabeledInformation LiHitPoints;	

	public static new StageInfo Create() {
		StageInfo result = Tiler.Create<StageInfo> (); 
		result.SetParent(Design.canvas);
		result.spacingRelativeToItemSize = new Vector2 (0, 0);
		result.rectTransform.offsetMin = new Vector2(6, 0);
		result.rectTransform.offsetMax = new Vector2(8, 6);
		result.colCount = 1;
		result.rowCount = 5;

		List<GameObject> items = new List<GameObject> ();

		result.LiScore = CreateLabeledInformation("panel_Score");
		result.LiScore.caption = "Score";
		items.Add(result.LiScore.gameObject);

		result.LiHitPoints = CreateLabeledInformation("panel_HitPoints");
		result.LiHitPoints.caption = "Hit Points";
		items.Add(result.LiHitPoints.gameObject);

		result.SetItems (items);
		result.Rebuild ();

		return result;
	}

	private static LabeledInformation CreateLabeledInformation(string gameObjectName) {
		LabeledInformation result = LabeledInformation.Create();
		result.gameObject.name = gameObjectName;
//		 = Vector2.zero;
		result.rectTransform.offsetMax = result.rectTransform.offsetMin + itemSize;

		return result;
	}

	public int Score {
		get { return int.Parse(LiScore.value); }
		set { LiScore.value = value.ToString (); }
	}
	public int HitPoints {
		get { return int.Parse(LiHitPoints.value); }
		set { LiHitPoints.value = value.ToString (); }
	}
}
