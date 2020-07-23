using UnityEngine;
using System.Collections;

public class EmptyRoomCell : Cell, ITouchable, IHighlightable {	
	public GameObject highlightFx;

	public override void UpdateView()
	{
		base.UpdateView();
		DoorBuilder.PlaceDoors(this);
	}	

	#region ITouchable implementation

	public void ProcessTouch()
	{
		Debug.Log("Placing room");

		Shop.instance.PlaceRoom(this);
	}

	#endregion

	#region IHighlightable implementation

	public void Highlight()
	{
		highlightFx.SetActive(true);
	}

	public void Dehighlight()
	{
		highlightFx.SetActive(false);
	}

	#endregion
}
