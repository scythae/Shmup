using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MainMenu {
	private static SelectableTextList instance;
	private static Dictionary<string, UnityAction> menuItems = new Dictionary<string, UnityAction> ();

	public static void ShowAfterDelay (string[] menuItemsCaptions, float delay) {
		Show (menuItemsCaptions, delay);
	}

	public static void Show (string[] menuItemsCaptions,  float delay = 0) {
		Hide ();

		instance = TextList.Create<SelectableTextList> () ;
		instance.gameObject.name = "panel_MainMenu";
		instance.DelayBeforeShowing = delay; 
		instance.Initialize (menuItemsCaptions);
		instance.OnSelectItem = new UnityAction<string> (ExecuteAction);
	}

	static void ExecuteAction (string itemCaption) {
		UnityAction selectedAction = null;
		menuItems.TryGetValue (itemCaption, out selectedAction);
		if (selectedAction == null) {
			return;
		}

		selectedAction.Invoke();
		Hide ();
	}

	public static void Hide () {
		if (instance != null) {
			TextList.Destroy (instance);
		}
	}

	public static void AddMenuAction (string itemCaption, UnityAction itemAction) {
		menuItems.Add (itemCaption, itemAction);
	}

	public static void ClearAllActions () {
		menuItems.Clear ();
	}
}