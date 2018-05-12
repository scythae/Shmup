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



	}
}
