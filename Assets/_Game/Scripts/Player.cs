using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _id;
        protected List<CardData> _cards = new List<CardData>();
        public bool HasCards => _cards.Count > 0;

        protected void Awake()
        {
            GameManager.OnGameStarted += OnGameStarted;
        }

        private void OnGameStarted()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            for (int i = 0; i < _cards.Count; i++)
            {
                Debug.Log($"{_id} {_cards[i]}");
            }
#endif
        }

        public virtual bool AddCard(CardData card)
        {
            if (_cards.Contains(card)) return false;
            _cards.Add(card);
            return true;
        }

        protected virtual bool Play(CardData card)
        {
            if (!_cards.Contains(card)) return false;
            _cards.Remove(card);
            return true;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStarted -= OnGameStarted;
        }
    }
}