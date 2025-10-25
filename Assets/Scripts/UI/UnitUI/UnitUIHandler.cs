using UnicoStudio.Unit;
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
        private UnitBase _unitBase;
        
        private void Awake()
        {
            GetRequiredComponents();
        }
        
        public void Init(UnitBase unitData)
        {
            _unitBase = unitData;
            _unitImage.sprite = unitData.UnitDataSo.Icon;
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
            MessageBroker.Default.Publish(new UnitDragMessage(_unitBase));
        }

        public void OnDrag(PointerEventData eventData)
        {
            print("OnDrag");
            _rectTransform.anchoredPosition +=  eventData.delta /  _canvas.scaleFactor;
        }
    }
}
