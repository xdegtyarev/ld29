using UnityEngine;
using System.Collections;

public class SettingsPopupViewComposer : MonoBehaviour, PopupComposer {
	[SerializeField] tk2dTextMesh soundLabel;
	[SerializeField] tk2dUIToggleButton soundButton;
	[SerializeField] tk2dTextMesh musicLabel;
	[SerializeField] tk2dUIToggleButton musicButton;
	[SerializeField] tk2dUIUpDownButton backButton;
	[SerializeField] GameObject viewContainer;
	#region IViewComposer implementation

	public System.Type GetDataType()
	{
		return typeof(SettingsPopupViewComposer);
	}

	public void CreateView(object c)
	{

	}

	public void Hide()
	{
		throw new System.NotImplementedException();
	}

	#endregion

	public string GetTitle()
	{
		return "Settings";
	}

	public string GetTitleIcon()
	{
		return "null";
	}

	public Vector2 GetPopupBodySize()
	{
		return new Vector2(Screen.width-20f,150f);
	}
}
