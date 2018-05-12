using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Director;

public class DisposableAnimation : Disposable
{
	public static void Play(AnimationClip clip, Transform place) {
		GameObject go = new GameObject("DisposableAnimation");
		
		Utils.AssignTransformFromTo(place, go.transform);

		var clipPlayable = new AnimationClipPlayable(clip);

		go.AddComponent<SpriteRenderer>();
		go.AddComponent<Animator>().Play(clipPlayable);

		go.AddComponent<DisposableAnimation>().lifeTime = clip.length;
	}
}
