using UnityEngine;
using System.Collections.Generic;
public class StorageCell: MonoBehaviour
{
	public int cellLimit;
	public StoragableType type;
	public float cellWidth;
	int elementCount;
	public int count{
		get{
			return elementCount;
		} 
		set{
			elementCount = value; 
			UpdateView();
		}
	}

	public int getFreeSpace(){
		return cellLimit-count;
	}
	public List<GameObject> views;

	public void UpdateView(){
		if(elementCount>views.Count){
			while(elementCount != views.Count){
				var view = StorageCellViewFactory.CreateView(type);
				view.transform.parent = transform;
				view.transform.localPosition = new Vector3(-cellWidth*0.5f+cellWidth/cellLimit*(views.Count+1),0f,0f);
				views.Add(view);
			}
		}else if(elementCount<views.Count){
			while(elementCount != views.Count){
				Destroy(views[views.Count-1]);
				views.RemoveAt(views.Count-1);
			}
		}

	}
}



