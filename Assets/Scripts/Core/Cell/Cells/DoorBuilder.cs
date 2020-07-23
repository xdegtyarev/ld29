using UnityEngine;

public class DoorBuilder: MonoBehaviour
{
	static DoorBuilder instance;
	public GameObject doorView;
	public void Awake(){
		instance = this;
	}
	public static void PlaceDoors(Cell cell){
		instance.placeDoors(cell);
	}
	void placeDoors(Cell cell)
	{
		if (cell.rightNeighbour && cell.rightNeighbour.cellState != CellState.Ground) {
			var door = (GameObject)Object.Instantiate(doorView);
			door.transform.parent = cell.transform;
			door.transform.localPosition = new Vector3(cell.size * Cell.defaultCellWidth * 0.5f, 0f, 0f);
		}
		if (cell.leftNeighbour && cell.leftNeighbour.cellState != CellState.Ground) {
			var door = (GameObject)Object.Instantiate(doorView);
			door.transform.parent = cell.transform;
			door.transform.localPosition = new Vector3(-cell.size * Cell.defaultCellWidth * 0.5f, 0f, 0f);
		}
	}
}

