using UnityEngine;

public class Profile : MonoBehaviour {
	int money = 50000;
	public tk2dTextMesh textViewr;
	public static Profile instance;

	void Awake(){
		instance = this;
	}
	void Update () {
		textViewr.text = "$" + money;
	}
	public bool TryProcessTransaction(int transaction){
		if(money-transaction>0){
			money-=transaction;
			return true;
		}else{
			return false;
		}
	}
}
