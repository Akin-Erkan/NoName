using UnicoStudio.GridSystem;
using UnicoStudio.ScriptableObjects;
using UnicoStudio.Unit;

namespace UnicoStudio
{


    public class NewLevelMessage
    {
        public int CurrentLevel { get; set; }
        public LevelDataSO LevelData{get; private set;}

        public NewLevelMessage(int currentLevel,LevelDataSO levelData)
        {
            CurrentLevel = currentLevel;
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
        public float DestroyAfterSeconds {get; private set;}

        public EnemyDiedMessage(Unit.Enemy diedEnemy, float destroyAfterSeconds)
        {
            Diedenemy = diedEnemy;
            DestroyAfterSeconds = destroyAfterSeconds;
        }
        
    }
    
    
    public struct LevelCompleteInfo
    {
        public int Level {get; private set;}
        public int KilledEnemy {get; private set;}
        public int MissedEnemy  {get; private set;}

        public LevelCompleteInfo(int level,int killedEnemy,int missedEnemy)
        {
            Level = level;
            KilledEnemy = killedEnemy;
            MissedEnemy = missedEnemy;
        }
    }
    
    public class LevelCompletedMessage
    {
        public LevelCompleteInfo LevelCompleteInfo { get; private set; }
        public LevelCompletedMessage(LevelCompleteInfo levelCompleteInfo)
        {
            LevelCompleteInfo = levelCompleteInfo;
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
    
    public class GameOverMessage
    {
        public GameOverMessage()
        {
            
        }
    }

    public class RestartRequestMessage
    {
        public RestartRequestMessage()
        {
            
        }
    }

    public class GameOverScoreMessage
    {
        public int Score {get; private set;}

        public GameOverScoreMessage(int score)
        {
            Score = score;
        }
    }


}
