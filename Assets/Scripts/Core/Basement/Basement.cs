using UnityEngine;
using System.Collections.Generic;

public class Basement : MonoBehaviour
{
	public const float floorOffset = 0.02f; //2px offset
	public static Basement instance;
	public BasementSettings settings;
	public ConsumerQueue consumerQueue;
	public BasementQueue gaperQueue;
	public Cell entranceCell;
	public Cell backdoorCell;
	public List<Cell> cells;
	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		InitBasement();
	}

	void InitBasement()
	{
		int cellDepth = 0;
		int cellCount = 0;
		cells = new List<Cell>();

		var lastPos = new Vector3(0f - (int)settings.entranceSize * 0.5f * Cell.defaultCellWidth, 0f - cellDepth * (Cell.defaultCellHeight + floorOffset) + (Cell.defaultCellHeight + floorOffset)*0.5f, 0f);
		entranceCell = CellFactory.CreateCell(cellCount, lastPos, 1, CellState.Corridor);
		gaperQueue.transform.position = entranceCell.transform.position - new Vector3(Cell.defaultCellWidth,Cell.defaultCellHeight*0.5f);
		Cell currentCell = entranceCell;
		cells.Add(currentCell);
		cellCount++;
		//initializing entrance level
		for (int i = 1; i < settings.entranceSize; i++) {
			lastPos += Vector3.right * Cell.defaultCellWidth;
			Cell c = CellFactory.CreateCell(cellCount, lastPos, 1, CellState.Corridor);
			currentCell.neighbours[1] = c;
			c.neighbours[3] = currentCell;
			currentCell = c;
			cells.Add(currentCell);
			cellCount++;
		}
		backdoorCell = currentCell;
		consumerQueue.transform.position = backdoorCell.transform.position + new Vector3(Cell.defaultCellWidth,-Cell.defaultCellHeight * 0.5f);
		//Creating basement
		for (int d = 1; d < settings.depth; d++) {
			cellDepth++;
			lastPos = new Vector3(0f - (int)(settings.horizontalSize * 0.5f) * Cell.defaultCellWidth, 0f - cellDepth * (Cell.defaultCellHeight + floorOffset) + (Cell.defaultCellHeight + floorOffset)*0.5f, 0f);
			Cell c = CellFactory.CreateCell(cellCount, lastPos);
			currentCell = c;
			cells.Add(c);	
			if (d > 1) {
				cells[cellCount - settings.horizontalSize].neighbours[2] = currentCell;
				currentCell.neighbours[0] = cells[cellCount - settings.horizontalSize];
			}
			cellCount++;
			for (int i = 1; i < settings.horizontalSize; i++) {
				lastPos += Vector3.right * Cell.defaultCellWidth;
				c = CellFactory.CreateCell(cellCount, lastPos);
				currentCell.neighbours[1] = c;
				c.neighbours[3] = currentCell;
				currentCell = c;
				cells.Add(currentCell);
				//neighboursAbove
				if (d > 1) {
					cells[cellCount - settings.horizontalSize].neighbours[2] = currentCell;
					currentCell.neighbours[0] = cells[cellCount - settings.horizontalSize];
				}else if (d == 1 && cellCount == settings.entranceSize + (int)(settings.horizontalSize*0.5f) + 1){
					backdoorCell.neighbours[2] = currentCell;
					currentCell.neighbours[0] = backdoorCell;
				}
				cellCount++;
			}
		}

		//creating views;
		for (int i = 0; i < cells.Count; i++) {
			CellFactory.UpgradeCell(cells[i]);
		}
			
	}

	public void HighlightFreeRooms(int roomSize)
	{
		int roomSpaceCounter = 0;
		for (int i = settings.entranceSize; i<cells.Count; i++){
			Cell cell = cells [i];
	 		if (cell.cellState == CellState.Corridor && (!cell.bottomNeighbour || cell.bottomNeighbour.cellState!=CellState.Ladder) && cell.leftNeighbour){
	 			roomSpaceCounter++;	
	 		} else {
				if(!cell.leftNeighbour){
					if(cell is IHighlightable){
						(cell as IHighlightable).Highlight();
					}
				}
	 			if (roomSpaceCounter >= roomSize) {
	 				for (int j = 1; j < roomSpaceCounter+1; j++) {
						if(cells[cell.id-j]is IHighlightable){
							(cells [cell.id - j]as IHighlightable).Highlight();
						}
	 				}	
	 			}
	 			roomSpaceCounter = 0;
	 		}
	 	}
	}
	public void DehighlightFreeRooms()
	{
		 foreach (var o in cells) {
			if(o is IHighlightable){
		 		(o as IHighlightable).Dehighlight();
			}
		 }
	}
	

	public void PlaceRoom(Cell cell, RoomInfo room)
	{
		List<Cell> cellsToReplace = new List<Cell>();
		for(int horizontalOffset=0;horizontalOffset<room.size;horizontalOffset++){
			cellsToReplace.Clear();
			for(int i = 0; i<room.size; i++){
				int id = cell.id+i-horizontalOffset;
				if(id<cells.Count){
					if(cells[id].cellState == CellState.Corridor && (!cells[id].bottomNeighbour || cells[id].bottomNeighbour.cellState != CellState.Ladder)){
						cellsToReplace.Add(cells[id]);
					}
				}else{
					Debug.Log("Trying for id:" + id);
				}
			}

			if(cellsToReplace.Count == room.size){
				Cell left,right;
				Cell newCell = CellFactory.CreateCell(cellsToReplace[0].id,(cellsToReplace[room.size-1].transform.position+cellsToReplace[0].transform.position) * 0.5f,room.size,CellState.Room);
				left = cellsToReplace[0].leftNeighbour;
				right = cellsToReplace[room.size-1].rightNeighbour;
				newCell.leftNeighbour = left;
				newCell.rightNeighbour = right;

				if(left){
					left.rightNeighbour = newCell;
					left.UpdateView();
				}
				if(right){
					right.leftNeighbour = newCell;
					right.UpdateView();
				}

				for (int i = 0; i < cellsToReplace.Count; i++) {
					if(cellsToReplace[i].topNeighbour){
						if(cellsToReplace[i].topNeighbour.bottomNeighbour){
							cellsToReplace[i].topNeighbour.bottomNeighbour = null;
							cellsToReplace[i].topNeighbour.UpdateView();
						}
					}
					if(cellsToReplace[i].bottomNeighbour){
						if(cellsToReplace[i].bottomNeighbour.topNeighbour){
							cellsToReplace[i].bottomNeighbour.topNeighbour = null;
							cellsToReplace[i].bottomNeighbour.UpdateView();
						}
					}
					Destroy(cellsToReplace[i].gameObject);
				}

				cells.RemoveRange(newCell.id,newCell.size);
				cells.Insert(newCell.id,newCell);
				(newCell as RoomCell).roomInfo = room;
				newCell.UpdateView();

				UpdateCellIds();
				Shop.instance.Deselect();
				break;
			}
		}
	}

	public void RemoveRoom(RoomCell roomCell){
		var cellsToReplace = new List<Cell>();
		for (int i = 0; i < roomCell.roomInfo.size; i++) {
			cellsToReplace.Add(CellFactory.CreateCell(roomCell.id+i,roomCell.transform.position-Vector3.right*roomCell.size*0.5f*Cell.defaultCellWidth+Vector3.right*i*Cell.defaultCellWidth+Vector3.right*Cell.defaultCellWidth*0.5f,1,CellState.Corridor));
			//Setting neighbours
			if(i>0){
				cellsToReplace[i-1].rightNeighbour = cellsToReplace[i];
				cellsToReplace[i].leftNeighbour = cellsToReplace[i-1];
			}
		}
		cellsToReplace[0].leftNeighbour = roomCell.leftNeighbour;
		cellsToReplace[roomCell.roomInfo.size-1].rightNeighbour = roomCell.rightNeighbour;
		cells.Remove(roomCell);
		cells.InsertRange(roomCell.id,cellsToReplace);
		foreach(var o in cellsToReplace){
			o.UpdateView();
		}
		UpdateCellIds();
		Destroy(roomCell.gameObject);
	}

	public void UpdateCellIds(){
		for (int i = 0; i < cells.Count; i++) {
			cells[i].id = i;
		}
	}
}