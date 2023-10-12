using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CardGame
{
    [RequireComponent(typeof(Canvas))]
    public class Message : MonoBehaviour
    {
        [SerializeField]private TMP_Text _text;
        [SerializeField] private RectTransform _messageImage;
        private Canvas _canvas;
        private void Awake()
        {
            GameManager.OnGameStatusUpdate += OnGameStatusUpdate;
            _canvas = GetComponent<Canvas>();
        }

        private void OnGameStatusUpdate(string status)
        {
            _text.text = status;
            
            var size = _messageImage.sizeDelta;
            size.x = 200;
            
            _messageImage.localScale = Vector3.zero;
            _messageImage.sizeDelta = size;

            _messageImage.DOKill();
            
            var sequence = DOTween.Sequence();
            sequence.Append(_messageImage.DOScale(1, 0.05f).SetEase(Ease.OutBack));
            sequence.Append(_messageImage.DOSizeDelta(new Vector2(512, size.y), 0.3f ));
            sequence.AppendInterval(1.5f);
            sequence.Append(_messageImage.DOSizeDelta(new Vector2(200, size.y), 0.15f ));
            sequence.Append(_messageImage.DOScale(0, 0.02f).SetEase(Ease.OutBack));

            sequence.Play();
            sequence.OnComplete(Close);
            _canvas.enabled = true;
        }

        void Close()
        {
            _canvas.enabled = false;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStatusUpdate -= OnGameStatusUpdate;
        }
    }
}
