using System.Linq;
using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;

namespace UnicoStudio.Unit
{
    public class UnitSpawnController : MonoBehaviour
    {
        [SerializeField]
        private Transform unitSpawnParent;
        
        private LevelDataSO _currentLevelData;
        

        private void Start()
        {
            MessageBroker.Default.Receive<NewLevelMessage>().Subscribe(OnNewLevelMessage).AddTo(this);
            MessageBroker.Default.Receive<UnitSpawnMessage>().Subscribe(OnUnitSpawnMessage).AddTo(this);
        }

        private void OnNewLevelMessage(NewLevelMessage msg)
        {
            _currentLevelData = Instantiate(msg.LevelData);
        }

        private void OnUnitSpawnMessage(UnitSpawnMessage msg)
        {
            if (_currentLevelData == null)
            {
                Debug.LogError("No level data set!");
            }

            //check unitinfo type

            if (msg.UnitInfo is Defender)
            {
                var defender = msg.UnitInfo as Defender;
                if (defender != null && _currentLevelData.DefenderData.Count > 0)
                { //TODO: this is working but refactor this if statement and defender control from SO and other parts if needed.
                    var defenderData = _currentLevelData.DefenderData.FirstOrDefault(d => d.ID == defender.UnitDataSo.ID);
                    if (defenderData == null)
                    {
                        Debug.LogWarning("Defender data not found!");
                        return;
                    }
                    var gridCell = msg.GridCell;
                    gridCell.IsOccupied = true;
                    var defenderInstance = Instantiate(defender,gridCell.transform.position,gridCell.transform.rotation,unitSpawnParent);
                    defenderInstance.Init(gridCell);
                    _currentLevelData.DefenderData.Remove(defenderData);
                }
            }


        }
        
        
        
        
    }
}
