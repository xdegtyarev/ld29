using UnityEngine;
using System.Collections;

public class GaperQueue : BasementQueue {
	public static GaperQueue instance;

	public override void Awake(){
		base.Awake();
		instance = this;
	}

	
}
