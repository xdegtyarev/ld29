using UnityEngine;

public interface PopupComposer : IViewComposer {
	string GetTitle();
	string GetTitleIcon();
	Vector2 GetPopupBodySize();
}
