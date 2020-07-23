using UnityEngine;
using System.Collections.Generic;

public class BasementQueue : MonoBehaviour {
	[SerializeField] protected List<IQueueable> queue;
	[SerializeField] protected int maxQueueLength;
	[SerializeField] protected float placeWidth;
	[SerializeField] bool rightSided;
	public Transform cachedTransform;

	public virtual void Awake(){
		cachedTransform = transform;
		queue  = new List<IQueueable>();
	}

	public virtual Vector3 GetWorldPositionInQueue(IQueueable queueable)
	{
		return cachedTransform.position + (rightSided ? Vector3.right : Vector3.left)*GetPositionInQueue(queueable)*placeWidth;
	}

	public virtual int GetQueueLength(){
		return queue.Count;
	}

	public virtual int GetMaxQueueLength(){
		return maxQueueLength;
	}

	public virtual int GetPositionInQueue(IQueueable queueable){
		if(queue.Contains(queueable)){
			return queue.IndexOf(queueable);
		}else{
			return queue.Count;
		}
	}

	public virtual IQueueable GetCurrentConsumer(){
		return queue[0];
	}

	public virtual void Enqueue(IQueueable queueable){
		queue.Add(queueable);
		queueable.OnEnqueue(this);
	}

	public virtual void Dequeue(){
		if(queue.Count>0){
			var queueable = Peek();
			queue.RemoveAt(0);
			queueable.OnDequeue(this);
		}
	}

	public virtual IQueueable Peek(){
		if(queue.Count>0){
			return queue[0];
		}else{
			return null;
		}
	}

	public virtual void Remove(IQueueable queueable)
	{
		queue.Remove(queueable);
	}
}
