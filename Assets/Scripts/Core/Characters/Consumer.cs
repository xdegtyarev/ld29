using UnityEngine;
using System.Collections;

public class Consumer : Character {
	public StoragableType drugType;
	public int quantity;
	public int pricePerItem;
	public float waitInQueueTime;

	public override void OnBeingSpawned(){
		base.OnBeingSpawned();
		quantity = Random.Range(1,10);
		pricePerItem = Random.Range(100,1000);
	}

	public override void OnEnqueue(BasementQueue queue)
	{

		StartCoroutine(WaitInQueue());
	}

	public override void OnDequeue(BasementQueue queue){
		LeaveQueue();
	}

	IEnumerator WaitInQueue()
	{
		yield return new WaitForSeconds(waitInQueueTime);
		LeaveQueue();
	}

	public void LeaveQueue()
	{
		Debug.Log("Leave queue");
		ConsumerQueue.instance.Remove(this);
		Destroy(gameObject);
	}
}
