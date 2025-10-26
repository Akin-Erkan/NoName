using UnicoStudio.Enemy;
using UnicoStudio.GridSystem;
using UnicoStudio.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace UnicoStudio.Unit
{
    public class UnitTargetingSystem : MonoBehaviour
    {
        private GridManager _gridManager;
        private EnemySpawnController _spawnController;

        [Inject]
        private void Construct(EnemySpawnController spawnController, GridManager gridManager)
        {
            _spawnController = spawnController;
            _gridManager = gridManager;
        }
        
        public Enemy GetNearestEnemy(Defender defender)
        {
            var defenderData = defender.UnitDataSo as DefenceDataSo;
            int range = defenderData.Range;
            var dir = defenderData.Direction;

            Enemy nearest = null;

            switch (dir)
            {
                case AttackDirection.Forward:
                    nearest = FindNearestEnemyInColumn(defender, range);
                    break;

                case AttackDirection.All:
                    nearest = FindNearestEnemyAround(defender, range);
                    break;
            }

            return nearest;
        }
        
        private Enemy FindNearestEnemyInColumn(Defender defender, int range)
        {
            var startY = defender.CurrentGridCell.GridPosition.y;
            var x = defender.CurrentGridCell.GridPosition.x;

            for (int y = startY - 1; y >= Mathf.Max(0, startY - range); y--)
            {
                var cell = _gridManager.GetCell(x, y);
                if (cell == null) continue;

                var unit = cell.UnitBase;
                if (unit is Enemy enemy)
                    return enemy;
            }

            return null;
        }
        
        private Enemy FindNearestEnemyAround(Defender defender, int range)
        {
            var defPos = defender.CurrentGridCell.GridPosition;
            Enemy nearest = null;
            int bestDist = int.MaxValue;

            foreach (var enemy in _spawnController.CurrentEnemies)
            {
                var enemyPos = enemy.CurrentGridCell.GridPosition;

                int dx = Mathf.Abs(enemyPos.x - defPos.x);
                int dy = Mathf.Abs(enemyPos.y - defPos.y);

                if (enemyPos.y >= defPos.y)
                    continue;

                int dist = dx + dy;
                if (dist <= range && dist < bestDist)
                {
                    bestDist = dist;
                    nearest = enemy;
                }
            }

            return nearest;
        }
    }
}
