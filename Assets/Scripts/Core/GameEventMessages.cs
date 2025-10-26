using UnicoStudio.GridSystem;
using UnicoStudio.ScriptableObjects;
using UnicoStudio.Unit;

namespace UnicoStudio
{


    public class NewLevelMessage
    {
        public LevelDataSO LevelData{get; private set;}

        public NewLevelMessage(LevelDataSO levelData)
        {
            LevelData = levelData;
        }
        
    }


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

    public class UnitSpawnedMessage
    {
        public UnitBase UnitInfo { get; private set; }

        public UnitSpawnedMessage(UnitBase unitInfo)
        {
            UnitInfo = unitInfo;
        }
    }
    
    public class UnitSpawnRequestMessage
    {
        public UnitBase UnitInfo { get; private set; }
        public GridCell GridCell { get; private set; }

        public UnitSpawnRequestMessage(UnitBase unitInfo, GridCell gridCell)
        {
            UnitInfo = unitInfo;
            GridCell = gridCell;
        }
        
    }

    public class EnemyDiedMessage
    {
        public Unit.Enemy Diedenemy {get; private set;}

        public EnemyDiedMessage(Unit.Enemy diedEnemy)
        {
            Diedenemy = diedEnemy;
        }
        
    }
    
    public class LevelCompletedMessage
    {
        public LevelCompletedMessage()
        {
            
        }
    }

    public class EnemyReachedToDefenderBaseMessage
    {
        public Unit.Enemy ReachedEnemy {get; private set;}
        public float DestroyAfterSeconds {get; private set;}

        public EnemyReachedToDefenderBaseMessage(Unit.Enemy enemy, float destroyAfterSeconds)
        {
            ReachedEnemy = enemy;
            DestroyAfterSeconds = destroyAfterSeconds;
        }
        
    }


}
