using UnityEngine;

public class StorageRoomShopInfoBarComposer: MonoBehaviour,IViewComposer
{
	[SerializeField] tk2dTextMesh capacityText;
	[SerializeField] GameObject viewContainer;
	#region implemented abstract members of InfoBarComposer
	public void CreateView(object c)
	{
		Debug.Log("CreateView");
		viewContainer.SetActive(true);
		var data = c as StorageRoomInfo;
		capacityText.text = "  " + data.capacity + " slots";
	}

	public System.Type GetDataType()
	{
		return typeof(StorageRoomInfo);
	}

	public void Hide()
	{
		viewContainer.SetActive(false);
	}

	#endregion
}