using UnityEngine;
using System.Collections;

public class RoomCell : Cell, ITouchable {
	[SerializeField] tk2dSprite roomView;
	[SerializeField] GameObject roomMenu;
	[SerializeField] tk2dSlicedSprite selection;
	public RoomInfo roomInfo;
	bool roomMenuActive;
	// Use this for initialization
	public override void UpdateView()
	{
		base.UpdateView();
		(GetComponent<Collider>() as BoxCollider).size = new Vector3(roomInfo.size*Cell.defaultCellWidth,Cell.defaultCellHeight,0f);
		DoorBuilder.PlaceDoors(this);
		roomView.SetSprite(roomInfo.spriteName);
		roomMenu.transform.localPosition = new Vector3(roomInfo.size*0.5f*defaultCellWidth-0.08f,defaultCellHeight*0.5f-0.08f,0f);
		selection.dimensions = new Vector2(roomInfo.size*Cell.defaultCellWidth*100f,Cell.defaultCellHeight*100f);
		if(roomInfo is StorageRoomInfo){
			if(!GetComponent<Storage>()){
				var storage = gameObject.AddComponent<Storage>();
				storage.Init(roomInfo as StorageRoomInfo);
				StorageManager.AddStorage(storage);
			}
		}else if(roomInfo is FactoryRoomInfo){
			if(!GetComponent<Factory>()){
				var factory = gameObject.AddComponent<Factory>();
				factory.Init(roomInfo as FactoryRoomInfo);
			}
		}
	}

	#region ITouchable implementation
	public void ProcessTouch()
	{
		if(roomMenuActive){
			roomMenuActive = false;
			DeleteRoom();
		}else{
			roomMenuActive = true;
			roomMenu.SetActive(true);
			InfoBarControl.instance.Show(roomInfo);
			selection.gameObject.SetActive(true);
			TouchProcessor.PostTouchEvent += OnPostTouch;
		}

	}

	bool postTouchCheck;

	//Catching out touches
	void OnPostTouch()
	{
		if(postTouchCheck){
			if(roomMenuActive){
				roomMenu.SetActive(false);
				selection.gameObject.SetActive(false);
				roomMenuActive = false;
				postTouchCheck = false;
				InfoBarControl.instance.Hide();
				TouchProcessor.PostTouchEvent-=OnPostTouch;
			}
		}else{
			postTouchCheck = true;
		}
	}
	#endregion

	void DeleteRoom()
	{
		Basement.instance.RemoveRoom(this);
		TouchProcessor.PostTouchEvent-=OnPostTouch;
	}
}
