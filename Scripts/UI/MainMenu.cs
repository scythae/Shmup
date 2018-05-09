using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MainMenu {
	private static SelectableTextList panel;
	private static Dictionary<string, UnityAction> menuItems = new Dictionary<string, UnityAction> ();
	private static string[] CurrentMenuSet; 

	public static Vector2 itemSize = new Vector2(4, 0.5f);
	public static Vector2 offsetMin = new Vector2(2, 1);
	public const string itemCaption_NewGame = "New game";
	public const string itemCaption_ExitGame = "Exit";
	public const string itemCaption_ResumeGame = "Resume";
	public const string itemCaption_Help = "Help";

	public static string[] defaultMenuSet = new string[] {itemCaption_NewGame, itemCaption_Help, itemCaption_ExitGame};
	public static string[] pausedMenuSet = new string[] {itemCaption_ResumeGame, itemCaption_NewGame, itemCaption_Help, itemCaption_ExitGame};

	public static void ShowAfterDelay (string[] menuItemsCaptions, float delay) {
		Show (menuItemsCaptions, delay);
	}

	public static void Show (string[] menuItemsCaptions = null, float delay = 0) {
		Hide ();

		if (menuItemsCaptions != null) 
			CurrentMenuSet = menuItemsCaptions;

		panel = PanelWithChildren.Create<SelectableTextList> () ;
		panel.gameObject.name = "panel_MainMenu";
		panel.DelayBeforeShowing = delay; 
		panel.Initialize (CurrentMenuSet);

		RectTransform rt = panel.gameObject.transform as RectTransform;
		rt.SetParent(Design.canvas.transform);
		rt.offsetMin = offsetMin;
		rt.offsetMax = rt.offsetMin + new Vector2 (itemSize.x, itemSize.y * panel.itemCount);

		panel.SetAligner_FixedTile(itemSize);
		panel.alignDirection = Aligner.AlignDirection.adTop;
		panel.Rebuild ();

		panel.OnSelectItem = new UnityAction<string> (ExecuteAction);
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
		if (panel != null) 
			TextList.Destroy (panel);
	}

	public static void AddMenuAction (string itemCaption, UnityAction itemAction) {
		menuItems.Add (itemCaption, itemAction);
	}

	public static void ClearAllActions () {
		menuItems.Clear ();
	}
}