using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
	public static Shop instance;
	public GameObject shopItemPrefab;
	public IHighlightable currentHighlight;
	public RoomInfo currentlySelectedRoom;
	public RoomInfo[] roomData;	
	public InfoBarControl infoBar;
	public tk2dUILayoutContainerSizer sizer;
	
	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		//Todo replace with preinstanced pool and move to factory or factory method;
		foreach (var r in roomData) {
			var go = Instantiate(shopItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			var shopItem = go.GetComponent<ShopItem>();
			shopItem.UpdateView(r);
			shopItem.transform.parent = sizer.transform;
			shopItem.transform.localPosition = Vector3.zero;
			sizer.AddLayout(shopItem.GetComponent<tk2dUILayout>(),tk2dUILayoutItem.FixedSizeLayoutItem());
		}
	}

	public void Select(RoomInfo room, IHighlightable highlightable){
		if(currentHighlight!=null){
			Deselect();
		}
		//todo: fix
		currentHighlight = highlightable;
		currentlySelectedRoom = room;
		infoBar.Show(room);
		Basement.instance.HighlightFreeRooms(room.size);
	}

	public void PlaceRoom(Cell currentCell)
	{
		//Transaction
		if(currentlySelectedRoom!=null){
			Basement.instance.PlaceRoom(currentCell,currentlySelectedRoom);
			currentlySelectedRoom = null;
		}
	}
	
	public void Deselect(){
		if(currentHighlight!=null){
			currentHighlight.Dehighlight();
		}
		infoBar.Hide();
		currentHighlight = null;
		currentlySelectedRoom = null;
		Basement.instance.DehighlightFreeRooms();
	}
}
