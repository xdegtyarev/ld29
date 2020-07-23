using UnityEngine;
using System;

public class Character : MonoBehaviour,IQueueable,ISpawnable {
	public Action OnTargetReached;
	const float epsilon = 0.05f;
	[SerializeField] GameObject view;
	[SerializeField] float speed;
	Transform cachedTransform;
	Vector3 target;

	bool targetReached;
	public virtual void Awake(){
		cachedTransform = transform;
	}

	#region IQueueable implementation
	public virtual void OnEnqueue(BasementQueue queue)
	{
		SetTarget(queue.GetWorldPositionInQueue(this));
	}
	public virtual void OnDequeue(BasementQueue queue)
	{
	}
	#endregion

	#region ISpawnable implementation

	public virtual void OnBeingSpawned()
	{
	}

	public virtual GameObject GetGameObject()
	{
		return gameObject;
	}
	#endregion

	public void SetTarget(Vector3 t){
		targetReached = false;
		target = t;
	}

	public void Update(){
		if(!targetReached){
			if(Vector3.Distance(target, cachedTransform.position) > epsilon){
				cachedTransform.position -= (cachedTransform.position - target).normalized * speed * Time.deltaTime;
			}else{
				targetReached = true;
				if(OnTargetReached!=null)
					OnTargetReached();
			}
		}
	}
}
