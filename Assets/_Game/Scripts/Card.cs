using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace CardGame
{
    [RequireComponent(typeof(Image))]
    public class Card : MonoBehaviour, IPointerClickHandler
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
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}
