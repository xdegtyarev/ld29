using UnityEngine;

public class FactoryRoomShopInfoBarComposer: MonoBehaviour,IViewComposer
{
	[SerializeField] tk2dTextMesh valueText;
	[SerializeField] tk2dTextMesh suspicionText;
	[SerializeField] tk2dSprite valueType;
	[SerializeField] GameObject viewContainer;
	#region implemented abstract members of InfoBarComposer
	public void CreateView(object c)
	{
		viewContainer.SetActive(true);
		var data = c as FactoryRoomInfo;
		valueText.text = "  " + data.productionRate + "/min";
		valueType.SetSprite(data.drugType.ToString());
		suspicionText.text = "  " + data.suspicionRate + "%/min";
	}


	public System.Type GetDataType()
	{
		return typeof(FactoryRoomInfo);
	}


	public void Hide()
	{
		viewContainer.SetActive(false);
	}

	#endregion
}


