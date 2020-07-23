using UnityEngine;
using System.Collections.Generic;

public enum StoragableType{
	Empty,Weed,Opiate,Stimulator,Psychodelic,Material
}

public class Storage : MonoBehaviour {
	//fill from the bottom left point
	public StorageConfiguration storageConfiguration;

	public void Init(StorageRoomInfo roomInfo)
	{
		GameObject configuration = GameObject.Instantiate(roomInfo.storageConfiguration) as GameObject;
		configuration.transform.parent = transform;
		configuration.transform.localPosition = Vector3.zero;
		storageConfiguration = configuration.GetComponent<StorageConfiguration>();
	}

	public int TryAddToStorage(StoragableType type, int count){
		//First pass on
		foreach(var storageCell in storageConfiguration.storage){
			if(storageCell.type == type){
				int freeSpace = storageCell.getFreeSpace();
				if(freeSpace>=count){
					storageCell.count+=count;
					return 0;
				}else{
					storageCell.count+=freeSpace;
					count-=freeSpace;
				}
			}
		}

		//Not found any space in existing, trying to create new
		foreach(var storageCell in storageConfiguration.storage){
			if(storageCell.type == StoragableType.Empty){
				storageCell.type = type;
				int freeSpace = storageCell.getFreeSpace();
				if(freeSpace>=count){
					storageCell.count+=count;
					return 0;
				}else{
					storageCell.count+=freeSpace;
					count-=freeSpace;
				}
			}
		}

		return count;
	}
	
	public int RemoveFromStorage(StoragableType type, int count){
		for (int i = storageConfiguration.storage.Count-1; i > -1; i--) {
			var storageCell = storageConfiguration.storage[i];
			if (storageCell.type == type) {
				if (storageCell.count > count) {
					storageCell.count -= count;
					return 0;
				}
				else {
					count -= storageCell.count;
					storageCell.type = StoragableType.Empty;
					storageCell.count = 0;
				}
			}
		}
		return count;
	}
}
