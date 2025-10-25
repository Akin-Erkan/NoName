using System.Collections.Generic;
using UnicoStudio.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UnicoStudio.UI
{
    public class UnitUIController : MonoBehaviour
    {
        [SerializeField]
        private UnitUIHandler unitHandlerPrefab;
        
        [SerializeField] 
        private List<Defender> defenders;
        
        private List<UnitUIHandler>  _units;
        
        
        [SerializeField]
        private HorizontalLayoutGroup defenderHorizontalGroupLayout;

        private void Awake()
        {
            CreateViewForDefenderUnits();
        }

        private void CreateViewForDefenderUnits()
        {
            foreach (var defender in defenders)
            {
                var unitViewUI = Instantiate(unitHandlerPrefab, defenderHorizontalGroupLayout.transform);
                unitViewUI.Init(defender);
                
            }
        }
        
    }
}