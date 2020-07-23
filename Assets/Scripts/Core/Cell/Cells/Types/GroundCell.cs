using UnityEngine;

public class GroundCell: Cell, ITouchable {
	const int defaultPrice = 2000;
	[SerializeField] int price;
	[SerializeField] tk2dSprite highlight;
	[SerializeField] tk2dTextMesh priceLabel;
	bool HasActiveNeighbours(){
		return (topNeighbour && topNeighbour.cellState!=CellState.Ground) ||
			(leftNeighbour && leftNeighbour.cellState!=CellState.Ground) ||
			(rightNeighbour && rightNeighbour.cellState!=CellState.Ground) ||
			(bottomNeighbour && bottomNeighbour.cellState==CellState.Corridor);
	}

	public override void UpdateView()
	{
		base.UpdateView();
		price = ((id - Basement.instance.settings.entranceSize) / Basement.instance.settings.horizontalSize + 1) * defaultPrice;
		priceLabel.text = "$"+price;
		bool isActive = HasActiveNeighbours();
		highlight.gameObject.SetActive(isActive);
		priceLabel.gameObject.SetActive(isActive);
	}

	public void ProcessTouch()
	{
		if(HasActiveNeighbours()){
			if(Profile.instance.TryProcessTransaction(price)){
				if((leftNeighbour && leftNeighbour.cellState!=CellState.Ground) ||
				   (rightNeighbour && rightNeighbour.cellState!=CellState.Ground)){
					cellState = CellState.Corridor;
					if(topNeighbour){
						if(topNeighbour.cellState!=CellState.Ground){
							topNeighbour.bottomNeighbour = null;
							topNeighbour = null;
						}
					}

					if(bottomNeighbour){
						if(bottomNeighbour.cellState!=CellState.Ground){
							bottomNeighbour.topNeighbour = null;
							bottomNeighbour = null;
						}
					}
				}else if((topNeighbour!=null && topNeighbour.cellState!=CellState.Ground && topNeighbour.cellState!=CellState.Room)){
					cellState = CellState.Ladder;
				}else if((bottomNeighbour!=null && bottomNeighbour.cellState!=CellState.Ground && bottomNeighbour.cellState!=CellState.Room)){
					cellState = CellState.Corridor;
					bottomNeighbour.cellState = CellState.Ladder;
					if(topNeighbour){
						if(topNeighbour.cellState!=CellState.Ground){
							topNeighbour.bottomNeighbour = null;
							topNeighbour = null;
						}
					}
					CellFactory.UpgradeCell(bottomNeighbour);
				}
				CellFactory.UpgradeCell(this);
			}
		}
	}
}
