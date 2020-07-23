using UnityEngine;
using System.Collections;

public class LadderCell : Cell {	
	public override void UpdateView()
	{
		base.UpdateView();
		DoorBuilder.PlaceDoors(this);
	}		
}
