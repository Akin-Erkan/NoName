using UnicoStudio.GridSystem;
using UnicoStudio.Unit;

namespace UnicoStudio
{


    public class GridSelectionMessage
    {
        public GridCell SelectedGridCell { get; private set; }

        public GridSelectionMessage(GridCell selectedGridCell)
        {
            SelectedGridCell = selectedGridCell;
        }
        
    }

    public class UnitDragMessage
    {
        public UnitBase DraggedUnitInfo { get; private set; }

        public UnitDragMessage(UnitBase draggedUnitInfo)
        {
            DraggedUnitInfo = draggedUnitInfo;
        }
    }


}
