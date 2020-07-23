using UnityEngine;
using System.Collections;

public class SupplyManager : MonoBehaviour {
	public static SupplyManager instance;

	[SerializeField] GameObject SupplyVan;
	[SerializeField] int defaultSupplyQuantity;
	[SerializeField] int defaultSupplyPrice;

	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool TryOrderSupplies(){
		if(Profile.instance.TryProcessTransaction(defaultSupplyQuantity*defaultSupplyPrice)){
			LaunchSupplyVan();
			return true;
		}else{
			return false;
		}
	}

	void LaunchSupplyVan(){
		if(StorageManager.Add(StoragableType.Material,1)){
			Debug.Log("Success");
		}
	}
}
