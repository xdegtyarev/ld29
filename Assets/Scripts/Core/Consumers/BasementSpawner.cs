using UnityEngine;

public class BasementSpawner : MonoBehaviour {
	[SerializeField] GameObject[] presets;
	[SerializeField] float spawnWaitPeriod;
	[SerializeField] float timeSinceLastSpawn;

	public virtual void Update(){
		timeSinceLastSpawn += Time.deltaTime;
		if(timeSinceLastSpawn>=spawnWaitPeriod){
			Spawn();
			timeSinceLastSpawn = 0f;
		}
	}

	public virtual object Spawn(){
		return (Instantiate(presets[Random.Range(0,presets.Length)],transform.position,Quaternion.identity) as GameObject).GetComponent(typeof(ISpawnable));
	}
}