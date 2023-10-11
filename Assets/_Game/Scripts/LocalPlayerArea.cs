using System;
using UnityEngine;

namespace CardGame
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LocalPlayerArea : PlayerArea
    {
        private CanvasGroup _cg;
        public event Action<CardData> OnCardSelected; 
        public bool Interactable
        {
            set
            {
                if(!_cg)
                    _cg = GetComponent<CanvasGroup>();
                _cg.interactable = value;
                _cg.blocksRaycasts = value;
            }
        }
        private void Awake()
        {
            Interactable = false;
        }

        public override void AddCard(CardData card)
        {
            base.AddCard(card);
            _cards[card].OnClick = () =>
            {
                Debug.Log(card);
                SelectCard(card);
            };
        }

        void SelectCard(CardData card)
        {
            OnCardSelected?.Invoke(card);
        }

        public override void PlayCard(CardData card)
        {
            _cards[card].OnClick = null;
            base.PlayCard(card);
        }
    }
}
