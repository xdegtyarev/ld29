using UnityEngine;
using System.Collections;
using System;

public class TouchProcessor : MonoBehaviour
{
	public static event Action PostTouchEvent;
	public Collider cameraMoveCollider;
	public Camera gameplayCamera;
	public RaycastHit info;

	void OnEnable()
	{
		TouchManager.TouchMoveEvent += OnTouchMove;
		TouchManager.TouchBeganEvent += OnTouchBegin;
	}

	void OnTouchBegin (TouchInfo touch)
	{
		foreach(var info in Physics.RaycastAll(gameplayCamera.ScreenPointToRay(touch.position))){
			var touchProcessor = info.collider.gameObject.GetComponent(typeof(ITouchable)) as ITouchable;
			if(touchProcessor!=null){
				touchProcessor.ProcessTouch();
			}
		}
		if(PostTouchEvent!=null){
			PostTouchEvent();
		}
	}

	void OnTouchMove (TouchInfo touch)
	{
		if(Physics.Raycast(gameplayCamera.ScreenPointToRay(touch.position),out info)){
			if(info.collider == cameraMoveCollider){
				transform.position += new Vector3(touch.delta.x*4f/Screen.width,touch.delta.y*4f/Screen.height,0);
			}
		}
	}

	void OnDisable(){
		TouchManager.TouchMoveEvent -= OnTouchMove;
	}
}
