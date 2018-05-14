using UnityEngine;
using UnityEngine.Experimental.Director;

public class DisposableSound : Disposable
{
	public static void Play(AudioClip clip, float volumeScale = 1.0f) {
		GameObject go = new GameObject("DisposableSound");

		Utils.AssignTransformFromTo(Design.MainCamera.transform, go.transform);
		go.AddComponent<AudioSource>().PlayOneShot(clip, volumeScale);
		go.AddComponent<DisposableSound>().lifeTime = clip.length;
	}
}
