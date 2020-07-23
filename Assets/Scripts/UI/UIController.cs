using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
	Animator animator;
	void Awake(){
		animator = GetComponent<Animator>();
	}
	void OnNewGameClick(){
		
		animator.SetTrigger("ShowHUD");
	}

	void ToMainMenuClick(){
		
		animator.SetTrigger("ShowMainMenu");
	}

	void ToLeaderBoards(){
		
		animator.SetTrigger("ShowLeaderboard");	
	}

	void ToOptions(){
		animator.SetTrigger("ShowOptions");		
	}

	void ToCredits(){
		animator.SetTrigger("ShowCredits");			
	}
}
