using UnityEngine;
using System.Collections.Generic;
using System;

public class ComposedViewController : MonoBehaviour {
		public static ComposedViewController instance;
		[SerializeField] GameObject view;
		[SerializeField] GameObject[] composerBase;
		Dictionary<Type,IViewComposer> composers = new Dictionary<Type, IViewComposer>();
		IViewComposer currentComposer;

		void Awake(){
			instance = this;
			foreach(var o in composerBase){
				IViewComposer c = o.GetComponent(typeof(IViewComposer)) as IViewComposer;
				composers.Add(c.GetDataType(),c);
			}
		}

		public virtual void Show(object c){
			Type t = c.GetType();
			currentComposer = composers[t];
			if(currentComposer!=null){
				currentComposer.CreateView(c);
				view.SetActive(true);
			}else{
				Debug.Log("no composer for this type");
				Hide();
			}

		}

		public virtual void Hide(){
			if(currentComposer!=null){
				currentComposer.Hide();
			}
			currentComposer = null;
			view.SetActive(false);

		}
}