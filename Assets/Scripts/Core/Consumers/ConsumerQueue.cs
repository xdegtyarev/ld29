using UnityEngine;
using System.Collections.Generic;

public class ConsumerQueue : BasementQueue {
	public static ConsumerQueue instance;
	public tk2dTextMesh text;

	public override void Awake(){
		base.Awake();
		instance = this;
	}
	
	public void Update(){
		if(queue.Count>0){
			var consumer = queue[0] as Consumer;
//			text.text = consumer.drugType + " $" + consumer.pricePerItem + " * " + consumer.quantity;
		}
	}

	public void Sell(){
		if(queue.Count>0){
			var currentConsumer = queue[0] as Consumer;
				if(currentConsumer){
					if(StorageManager.TryGetItem(currentConsumer.drugType,currentConsumer.quantity)){
						// Profile.instance.money += currentConsumer.quantity*currentConsumer.pricePerItem;
						currentConsumer.LeaveQueue();
					}
				}
		}
	}
}
