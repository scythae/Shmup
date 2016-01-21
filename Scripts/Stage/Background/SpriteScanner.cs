using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScanner {
	public static float error = 0;
	public static bool needSameAlpha = true;

	public SpriteScanner () {
	}

	public static List<Vector2> GetColoredPixelsCoords(Color color, GameObject gameObject) {
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		if (spriteRenderer == null)
			return null;
		else
			return GetColoredPixelsCoords(color, spriteRenderer.sprite);
	}

	public static List<Vector2> GetColoredPixelsCoords(Color color, Sprite sprite) {
		if (sprite == null)
			return null;
		else
			return GetColoredPixelsCoords(color, sprite.texture);
	}

	public static List<Vector2> GetColoredPixelsCoords(Color color, Texture2D texture2d) {
		Color pixel;
		List<Vector2> result = new List<Vector2> ();

		Debug.Log(texture2d.width + "; " + texture2d.height);

		for (int x = 0; x < texture2d.width; x++)
			for (int y = 0; y < texture2d.height; y++) {
				pixel = texture2d.GetPixel(x, y);

				if (ColorsEqual(pixel, color)) {
					result.Add(new Vector2(x, y));
				}
			}


		return result;
	}

	private static bool ColorsEqual(Color color1, Color color2) {		
		return (			
			ApproximatelyEqual(color1.r, color2.r) &&
			ApproximatelyEqual(color1.g, color2.g) &&
			ApproximatelyEqual(color1.b, color2.b) &&
			(ApproximatelyEqual(color1.a, color2.a) || !needSameAlpha)
		);
	}

	private static bool ApproximatelyEqual(float x1, float x2) {
		float diff = Mathf.Abs(x1 - x2);
		return (Mathf.Approximately(diff, error) || (diff < error));
	}
}

