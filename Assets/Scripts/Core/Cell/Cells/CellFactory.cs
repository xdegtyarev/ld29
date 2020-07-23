using UnityEngine;
using System.Collections.Generic;
public class CellFactory: MonoBehaviour{
	public List<CellStateViewBuilderPair> cellPrefabsBase;
	static Dictionary<CellState,Cell> cellPrefabs = new Dictionary<CellState, Cell>();
	public static CellFactory instance;
	public void Awake(){
		instance = this;
		foreach(var o in cellPrefabsBase){
			cellPrefabs.Add(o.state,o.viewBuilder);
		}
	}

	public static Cell CreateCell(int id, Vector3 pos, int size = 1,CellState defaultState = CellState.Ground){
		Cell cell = ((GameObject)(GameObject.Instantiate(cellPrefabs[defaultState].gameObject, pos, Quaternion.identity))).GetComponent<Cell>();
		cell.id = id;
		cell.cellState = defaultState;
		cell.size = size;
		return cell;
	}

	public static void UpgradeCell(Cell cell){
		Cell newCell = CreateCell(cell.id,cell.transform.position,cell.size,cell.cellState);
		newCell.neighbours = cell.neighbours;
		if(newCell.leftNeighbour){
			newCell.leftNeighbour.rightNeighbour = newCell;
		}
		if(newCell.rightNeighbour){
			newCell.rightNeighbour.leftNeighbour = newCell;
		}
		if(newCell.topNeighbour){
			newCell.topNeighbour.bottomNeighbour = newCell;
		}
		if(newCell.bottomNeighbour){
			newCell.bottomNeighbour.topNeighbour = newCell;
		}
		Basement.instance.cells.RemoveAt(cell.id);
		Destroy(cell.gameObject); //replace with pooling;
		Basement.instance.cells.Insert(newCell.id,newCell);
		foreach(var o in newCell.neighbours){
			if(o){
				o.UpdateView();
			}
		}
	}	
}

