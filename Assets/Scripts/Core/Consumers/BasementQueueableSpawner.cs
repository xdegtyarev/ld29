using UnityEngine;
using System.Collections;

public class BasementQueueableSpawner : BasementSpawner {
	[SerializeField] BasementQueue queue;
	public override object Spawn(){
		if(queue.GetQueueLength() < queue.GetMaxQueueLength()){
			var queueable = base.Spawn() as IQueueable;
			queue.Enqueue(queueable);
			return queueable;
		}
		return null;
	}
}