using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    [RequireComponent(typeof(Image))]
    public class Card : MonoBehaviour
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
    }
}
