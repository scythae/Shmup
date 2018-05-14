using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageInfo : PanelWithChildren  {
	private static StageInfo fInstance;
	public static StageInfo instance {
		get { 
			if (fInstance != null)
				return fInstance;

			fInstance = Create();
			return fInstance;
		}
	}

	private static Vector2 itemSize_Score = new Vector2(2, 0.75f); 
	private static Vector2 itemSize_HitPoints = new Vector2(2, 0.75f); 
	private static Vector2 itemSize_BuffCaption = new Vector2(2, 0.25f); 
	private static float buffCaptionDelay = 1f;
	private LabeledInformation LiScore;
	private LabeledInformation LiHitPoints;	
	private TemporaryText buffCaption;
	private BuffZone buffZone;


	private static new StageInfo Create() {
		StageInfo result = PanelWithChildren.Create<StageInfo> (); 
		result.gameObject.name = "panel_StageInfo";

		RectTransform rt = result.gameObject.transform as RectTransform;
		rt.offsetMin = new Vector2 (6, 0);
		rt.offsetMax = new Vector2 (8, 6);

		result.items = new List<GameObject> ();

		result.LiScore = CreateScore(); 
		result.items.Add (result.LiScore.gameObject);

		result.LiHitPoints = CreateHitPoints(); 
		result.items.Add (result.LiHitPoints.gameObject);

		result.buffCaption = CreateBuffCaption ();
		result.items.Add (result.buffCaption.gameObject);

		result.buffZone = BuffZone.Create();
		result.items.Add (result.buffZone.gameObject);

		result.alignDirection = Aligner.AlignDirection.adTop;
		result.SetAligner_Pivot ();
		result.Rebuild ();
	
		return result;
	}

	private static LabeledInformation CreateScore() {
		LabeledInformation result = LabeledInformation.Create(); 
		result.gameObject.name = "panel_Score";
		result.caption = "Score";
		SetRectTransformSize(result.gameObject, itemSize_Score);
		return result;
	}

	private static void SetRectTransformSize(GameObject gameObject, Vector2 size) {
		RectTransform rt = gameObject.transform as RectTransform;
		rt.offsetMax = rt.offsetMin + size;
	}

	private static LabeledInformation CreateHitPoints() {
		LabeledInformation result = LabeledInformation.Create(); 
		result.gameObject.name = "panel_HitPoints";
		result.caption = "Hit Points";
		SetRectTransformSize(result.gameObject, itemSize_HitPoints);
		return result;
	}

	private static TemporaryText CreateBuffCaption() {
		TemporaryText result = TemporaryText.Create ();
		result.gameObject.name = "text_BuffCaption";
		SetRectTransformSize(result.gameObject, itemSize_BuffCaption);
		return result;
	}

	public List<Buff> Buffs {
		set { buffZone.Reinitialize(value); }
	}

	public string BuffCaption {
		set { buffCaption.SetText(value, buffCaptionDelay); } 
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
