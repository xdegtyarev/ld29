using UnityEngine;
using System.Collections.Generic;

public class StorageManager : MonoBehaviour {
	public List<Storage> storages;
	public static StorageManager instance;
	public void Awake(){
		instance = this;
	}
	public static void AddStorage(Storage storage){
		instance.storages.Add(storage);
	}

	public static bool Add(StoragableType type, int count)
	{
		foreach(var o in instance.storages){
			count = o.TryAddToStorage(type,count);
			if(count == 0){
				return true;
			}
		}
		return false;
	}

	public static bool TryGetItem(StoragableType type, int count)
	{
		int orig = count;
		foreach(var o in instance.storages){
			count = o.RemoveFromStorage(type,count);
			if(count == 0){
				return true;
			}
		}
		Add(type,orig-count);
		return false;
	}
}
