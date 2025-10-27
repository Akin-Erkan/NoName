using TMPro;
using UnicoStudio.ScriptableObjects;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnicoStudio.UI
{
    [RequireComponent(typeof(Image))]
    public class UnitUIHandler : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler,IEndDragHandler, IDragHandler
    {
        private Canvas  _canvas;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private Image _unitImage;
        private Vector3 _startPos;
        
        [Header("Data")]
        private UnitDataSO _unitData;
        public UnitDataSO UnitData
        {
            get => _unitData;
            private set => _unitData = value;
        }

        
        private int _unitCount;
        public int UnitCount
        {
            get => _unitCount;
            set
            {
                _unitCount = value;
                SetRemainingUnitText(_unitCount.ToString());
                if(value > 0)
                    EnableHandler();
                else
                    DisableHandler();
            }
        }


        [Header("Components")] 
        [SerializeField]
        private TextMeshProUGUI remainingUnitText;



        private void Awake()
        {
            GetRequiredComponents();
        }
        
        public void Init(UnitDataSO unitData)
        {
            UnitData = unitData;
            _unitImage.sprite = unitData.Icon;
        }

        public void DisableHandler()
        {
            _unitImage.raycastTarget = false;
            _unitImage.color = Color.gray;
        }

        public void EnableHandler()
        {
            _unitImage.raycastTarget = true;
            _unitImage.color = Color.white;
        }

        public void SetRemainingUnitText(string text)
        {
            remainingUnitText.text = text;
        }

        private void GetRequiredComponents()
        {
            _unitImage = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
            _canvas = gameObject.GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            print("OnPointerEnter");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            print("OnBeginDrag");
            _startPos = _rectTransform.anchoredPosition;
            _canvasGroup.blocksRaycasts = false;
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            print("OnEndDrag");
            _rectTransform.anchoredPosition = _startPos; 
            _canvasGroup.blocksRaycasts = true;
            MessageBroker.Default.Publish(new UnitDragMessage(UnitData.UnitPrefab));
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition +=  eventData.delta /  _canvas.scaleFactor;
        }
    }
}
