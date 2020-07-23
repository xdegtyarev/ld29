using UnityEngine;

public class Employee : Character{

	public int hitPoint;
	public int level;
	public int priceToHire;

	public override void OnBeingSpawned(){
		priceToHire = Random.Range(1000,3000);
	}
}
