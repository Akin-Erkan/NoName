using System.Collections.Generic;
using System.Linq;
using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;
using Zenject;

namespace UnicoStudio.Unit
{
    public class UnitSpawnController : MonoBehaviour
    {
        [SerializeField]
        private Transform unitSpawnParent;
        
        private List<Defender> _currentDefenders = new List<Defender>();

        public List<Defender> CurrentDefenders
        {
            get { return _currentDefenders; }
            private set { _currentDefenders = value; }
        } 
        
        
        private LevelDataSO _currentLevelData;
        private DiContainer _container;


        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _container = diContainer;
        }
        

        private void Start()
        {
            MessageBroker.Default.Receive<NewLevelMessage>().Subscribe(OnNewLevelMessage).AddTo(this);
            MessageBroker.Default.Receive<UnitSpawnRequestMessage>().Subscribe(OnUnitSpawnMessage).AddTo(this);
        }

        private void OnNewLevelMessage(NewLevelMessage msg)
        {
            _currentLevelData = Instantiate(msg.LevelData);
    
            foreach (var defender in CurrentDefenders)
            {
                Destroy(defender.gameObject);
            }
    
            CurrentDefenders.Clear();
        }

        private void OnUnitSpawnMessage(UnitSpawnRequestMessage msg)
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
                    
                    var defenderInstance = _container.InstantiatePrefabForComponent<Unit.Defender>(defender,gridCell.transform.position,gridCell.transform.rotation,unitSpawnParent);
                    defenderInstance.Init(gridCell);
                    CurrentDefenders.Add(defenderInstance);
                    gridCell.IsOccupied = true;
                    gridCell.UnitBase = defenderInstance;
                    MessageBroker.Default.Publish(new UnitSpawnedMessage(defenderInstance));
                    _currentLevelData.DefenderData.Remove(defenderData);
                }
            }


        }
        
        
        
        
    }
}
