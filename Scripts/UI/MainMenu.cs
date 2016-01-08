using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MainMenu {
	private static SelectableTextList instance;
	private static Dictionary<string, UnityAction> menuItems = new Dictionary<string, UnityAction> ();

	public static Vector2 size = new Vector2(4, 1);
	public static Vector2 offsetMin = new Vector2(2, 1);
	public const string itemCaption_NewGame = "New game";
	public const string itemCaption_ExitGame = "Exit";
	public const string itemCaption_ResumeGame = "Resume";

	public static string[] defaultMenuSet = new string[] {itemCaption_NewGame, itemCaption_ExitGame};
	public static string[] pausedMenuSet = new string[] {itemCaption_ResumeGame, itemCaption_NewGame, itemCaption_ExitGame};

	public static void ShowAfterDelay (string[] menuItemsCaptions, float delay) {
		Show (menuItemsCaptions, delay);
	}

	public static void Show (string[] menuItemsCaptions, float delay = 0) {
		Hide ();

		instance = Tiler.Create<SelectableTextList> () ;
		instance.gameObject.name = "panel_MainMenu";
		instance.SetParent(Design.canvas);
		instance.rectTransform.offsetMin = offsetMin;
		instance.rectTransform.offsetMax = instance.rectTransform.offsetMin + size;
		instance.DelayBeforeShowing = delay; 
		instance.Initialize (menuItemsCaptions);

		instance.fixedTileSize = true;
		instance.tileSize = new Vector2(4, 0.5f);
		instance.Rebuild ();
		instance.OnSelectItem = new UnityAction<string> (ExecuteAction);
	}

	static void ExecuteAction (string itemCaption) {
		UnityAction selectedAction = null;
		menuItems.TryGetValue (itemCaption, out selectedAction);
		if (selectedAction == null)
			return;

		selectedAction.Invoke();
		Hide ();
	}

	public static void Hide () {
		if (instance != null) 
			TextList.Destroy (instance);
	}

	public static void AddMenuAction (string itemCaption, UnityAction itemAction) {
		menuItems.Add (itemCaption, itemAction);
	}

	public static void ClearAllActions () {
		menuItems.Clear ();
	}
}