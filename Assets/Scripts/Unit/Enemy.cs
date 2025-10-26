using System.Collections;
using UnicoStudio.GridSystem;
using UnicoStudio.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace UnicoStudio.Unit
{
    public class Enemy : UnitBase
    {
        private GridManager _gridManager;

        [Inject]
        private void Construct(GridManager gridManager)
        {
            _gridManager = gridManager;
        }
        
        protected override void Start()
        {
            base.Start();
            StartCoroutine(MoveToDefenderBase());
        }

        public override void Init(GridCell gridCell)
        {
            base.Init(gridCell);
        }

        private IEnumerator MoveToDefenderBase()
        {
            var nextCell = _gridManager.GetNextAvailableGridCellForEnemy(CurrentGridCell);
            if (nextCell == null)
            {
                Debug.LogWarning("No nextCell found!");
                yield break;
            }

            nextCell.IsOccupied = true;
            CurrentGridCell.IsOccupied = false;
            CurrentGridCell = nextCell;
            
            var enemyData = UnitDataSo as EnemyDataSo;
            var speed = enemyData.Speed;
            var moveDuration = 1f/speed;
            
                        
            Vector3 startPos = transform.position;
            Vector3 endPos = nextCell.transform.position;
            float elapsed = 0f;
            
            while (elapsed < moveDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / moveDuration);
                transform.position = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }
            transform.position = endPos;
            
            StartCoroutine(MoveToDefenderBase());
            
        }
        
    }

}
