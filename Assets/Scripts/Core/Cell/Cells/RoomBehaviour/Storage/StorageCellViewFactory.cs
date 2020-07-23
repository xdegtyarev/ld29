using UnityEngine;
using System.Collections.Generic;

public class StorageCellViewFactory : MonoBehaviour {
	public List<StoragableTypeViewPair> viewBase;
	public void Awake(){
		views = new Dictionary<StoragableType, GameObject>();
		foreach(var o in viewBase){
			views.Add(o.type,o.view);
		}
	}
	static Dictionary<StoragableType,GameObject> views;

	public static GameObject CreateView(StoragableType type)
	{
		return Object.Instantiate(views[type]) as GameObject;
	}
}
