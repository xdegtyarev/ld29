using UnityEngine;
using System.Collections;

public class ShopItem : MonoBehaviour, IHighlightable
{
	public RoomInfo roomInfo;
	public tk2dSprite roomView;
	public tk2dTextMesh title;
	public tk2dTextMesh price;
	public GameObject selection;
	public bool isSelected;

	#region IHighlightable implementation

	public void Highlight()
	{
		Select();
	}

	public void Dehighlight()
	{
		selection.SetActive(false);
		isSelected = false;
	}

	#endregion

	public void UpdateView(RoomInfo info){
		roomInfo = info;
		roomView.SetSprite(info.spriteName);	
				
		title.text = info.name;
		price.text = "" + info.price;
	}

	public void OnClick()
	{
		if(isSelected){
			Deselect();
		}else{
			Select();
		}
	}

	public void Select()
	{
		isSelected = true;
		selection.SetActive(true);
		Shop.instance.Select(roomInfo,this);
	}

	public void Deselect()
	{
		selection.SetActive(false);
		isSelected = false;
		Shop.instance.Deselect();
	}
}
