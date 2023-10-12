using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace CardGame
{
    [RequireComponent(typeof(Image))]
    public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Image _image;

        public Sprite Image
        {
            set
            {
                if (!_image)
                    _image = GetComponent<Image>();
                _image.sprite = value;
            }
        }

        public Action OnClick;

        private void Awake()
        {
            transform.localScale = 1.2f * Vector3.one;
            transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);
        }
    }
}
