using UnityEngine;

public class Factory : MonoBehaviour {
	public DrugType drugType;
	public StoragableType storagableDrugType;
	public float productionRate;//per minute; //replace with rates, per workers;
	public float consumptionRate;//per minute;
	public float currentProductionProgress;
	public float currentConsuptionProgress;

	public void Init(FactoryRoomInfo factoryRoomInfo)
	{
		productionRate = factoryRoomInfo.consumptionRate;
		drugType = factoryRoomInfo.drugType;
		switch (drugType) {
			case DrugType.Weed:
				storagableDrugType = StoragableType.Weed;
				break;
			case DrugType.Opiate:
				storagableDrugType = StoragableType.Opiate;
				break;
			case DrugType.Psychodelic:
				storagableDrugType = StoragableType.Psychodelic;
				break;
			case DrugType.Stimulator:
				storagableDrugType = StoragableType.Stimulator;
				break;
			default:
				throw new System.ArgumentOutOfRangeException();
		}
		consumptionRate = factoryRoomInfo.consumptionRate;
		currentProductionProgress = 0f;
	}

	bool CanConsume()
	{
		if (currentConsuptionProgress < 0f) {
			if (StorageManager.TryGetItem(StoragableType.Material,1)) {
				currentConsuptionProgress = 1f;
			}else{
				return false;
			}
		}
		else {
			currentConsuptionProgress = currentConsuptionProgress - consumptionRate * 0.01667f * Time.deltaTime;
		}
		return true;
	}

	void Produce()
	{
		if (currentProductionProgress < 1) {
			currentProductionProgress = currentProductionProgress + productionRate * 0.01667f * Time.deltaTime;
		}
		else {
			StorageManager.Add(storagableDrugType, 1);
			currentProductionProgress = 0;
		}
	}

	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if(CanConsume()){
			Produce();
		}
	}
}
