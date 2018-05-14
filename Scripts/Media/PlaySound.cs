using UnityEngine;
using System;

public class PlaySound 
{
	private class Clips {
		private static AudioClip Load(string ResourceName) {
			return Resources.Load(ResourceName) as AudioClip;
		}

		public static AudioClip PlayerDeath = Load("Sounds/Death");
		public static AudioClip MinusHP = Load("Sounds/MinusHP");
		public static AudioClip BulletShot = Load("Sounds/Shot");
		public static AudioClip Bonus = Load("Sounds/Bonus");
	}

	public static void BulletShot() { 
		DisposableSound.Play(Clips.BulletShot); 
	}

	public static void PlayerDeath() { 
		DisposableSound.Play(Clips.PlayerDeath, 0.7f); 
	}

	public static void InvulnerabilityAppears() { 
		DisposableSound.Play(Clips.Bonus); 
	}

}
