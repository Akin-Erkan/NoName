using System;
using System.Collections.Generic;
using System.Linq;
using UnicoStudio.ScriptableObjects;
using UnicoStudio.Unit;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UnicoStudio.UI
{
    public class UnitUIController : MonoBehaviour
    {
        [SerializeField]
        private UnitUIHandler unitHandlerPrefab;
        
        [SerializeField] 
        private List<UnitDataSO> defenderDatas;
        
        private List<UnitUIHandler> _unitUIHandlers = new List<UnitUIHandler>();
        
        
        [SerializeField]
        private HorizontalLayoutGroup defenderHorizontalGroupLayout;

        private void Awake()
        {
            CreateViewForDefenderUnits();
        }

        private void Start()
        {
            MessageBroker.Default.Receive<LevelCompletedMessage>().Subscribe(OnLevelCompleted).AddTo(this);
            MessageBroker.Default.Receive<NewLevelMessage>().Subscribe(OnNewLevel).AddTo(this);
            MessageBroker.Default.Receive<UnitSpawnedMessage>().Subscribe(OnNewUnitSpawned).AddTo(this);
        }

        private void CreateViewForDefenderUnits()
        {
            foreach (var defender in defenderDatas)
            {
                var unitViewUI = Instantiate(unitHandlerPrefab, defenderHorizontalGroupLayout.transform);
                unitViewUI.Init(defender);
                unitViewUI.DisableHandler();
                _unitUIHandlers.Add(unitViewUI);
            }
        }

        private void OnLevelCompleted(LevelCompletedMessage msg)
        {
            foreach (var unitViewUI in _unitUIHandlers)
            {
                unitViewUI.UnitCount = 0;
            }
        }

        private void OnNewLevel(NewLevelMessage msg)
        {
            var levelData = msg.LevelData;

            foreach (var defenceDataSo in levelData.DefenderData)
            {
                var handler = _unitUIHandlers.Where(u => u.UnitData.ID == defenceDataSo.ID).FirstOrDefault();
                handler.UnitCount++;
            }
        }

        private void OnNewUnitSpawned(UnitSpawnedMessage msg)
        {
            if (msg.UnitInfo != null && msg.UnitInfo is Defender)
            {
                var defender = msg.UnitInfo as Defender;
                var handler = _unitUIHandlers.Where(u => u.UnitData.ID == defender.UnitDataSo.ID).FirstOrDefault();
                handler.UnitCount--;
            }
        }
        
        
        
    }
}