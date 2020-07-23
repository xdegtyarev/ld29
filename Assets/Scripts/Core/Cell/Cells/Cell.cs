using UnityEngine;

public enum CellState
{
	Ground,
	Corridor,
	Ladder,
	Room
}

public class Cell : MonoBehaviour
{
	public const float defaultCellWidth = 0.4f;
	public const float defaultCellHeight = 0.4f;

	public int id;
	public int size;
	public Cell[] neighbours = new Cell[4];
	public CellState cellState;

	public Cell topNeighbour{ get {return neighbours[0];} set {neighbours[0] = value;}}
	public Cell rightNeighbour{ get {return neighbours[1];}set {neighbours[1] = value;}}
	public Cell bottomNeighbour{ get {return neighbours[2];}set {neighbours[2] = value;}}
	public Cell leftNeighbour{ get {return neighbours[3];}set {neighbours[3] = value;}}
	
	public void OnDrawGizmos(){
		if(cellState!=CellState.Ground){
			foreach(var o in neighbours){
				if(o && o.cellState!=CellState.Ground){
				Gizmos.DrawLine(transform.position,o.transform.position);
				}
			}
		}
	}

	public virtual void UpdateView(){

	}
}