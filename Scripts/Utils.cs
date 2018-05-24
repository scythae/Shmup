using System;
using UnityEngine;
using UnityEngine.UI;

public class Utils
{
	public static void AssignTransformFromTo(Transform source, Transform destination) {
		destination.parent = source.parent;
		destination.localScale = source.localScale;
		destination.localPosition = source.localPosition;
		destination.localRotation = source.localRotation;

		RectTransform srt = source as RectTransform;
		if (!srt)
			return;

		RectTransform drt = destination as RectTransform;
		if (!drt)
			drt = destination.gameObject.AddComponent<RectTransform>();
		drt.offsetMax = srt.offsetMax;
		drt.offsetMin = srt.offsetMin;
		drt.anchorMax = srt.anchorMax;
		drt.anchorMin = srt.anchorMin;
		drt.pivot = srt.pivot;
	}

	public static void ProvideCanvas(GameObject target) {
		Free(target.GetComponent<Canvas>());
		Free(target.GetComponent<CanvasScaler>());
		
		Canvas canvas = target.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.WorldSpace;

		CanvasScaler canvasScaler = target.AddComponent<CanvasScaler>();
		canvasScaler.dynamicPixelsPerUnit = 300;
		canvasScaler.referencePixelsPerUnit = 1;
	}

	public static void FreeAndNil(ref UnityEngine.Object obj) {
		Free(obj);
		obj = null;
	}

	public static void Free(UnityEngine.Object obj) {
		if (obj != null)
			UnityEngine.Object.Destroy(obj);
	}

	public static void CheckObject(UnityEngine.Object obj) {
		Debug.Log(obj.ToString());
	}

	public static void SetEnabled(GameObject go, bool enabled) {
		foreach (PausableRepetition script in go.GetComponentsInChildren<PausableRepetition>())
			script.enabled = enabled;

		foreach (Pausable script in go.GetComponentsInChildren<Pausable>())
			script.enabled = enabled;

		foreach (Rigidbody2D rigidbody in go.GetComponentsInChildren<Rigidbody2D>())
			Rigidbody2D_ex.SetPaused(rigidbody, !enabled);
	}
}
