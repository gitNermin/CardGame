using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CardGame
{
    public abstract class Player : MonoBehaviour
    {
        [SerializeField] private int _id;
        protected List<CardData> _cards = new List<CardData>();
        public bool HasCards => _cards.Count > 0;

        protected void Awake()
        {
            GameManager.OnGameStarted += OnGameStarted;
        }

        protected virtual void OnGameStarted()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            PrintCards();
#endif
        }
        public virtual bool AddCard(CardData card)
        {
            if (_cards.Contains(card)) return false;
            _cards.Add(card);
            return true;
        }

        public abstract Task<CardData> Play();
        protected virtual bool Play(CardData card)
        {
            if (!_cards.Contains(card)) return false;
            _cards.Remove(card);
            return true;
        }
        protected void PrintCards()
        {
            StringBuilder str = new StringBuilder();
            str.Append($"Player {_id}, Cards: ");
            for (int i = 0; i < _cards.Count; i++)
            {
                str.Append(_cards[i]);
                if (i != _cards.Count - 1) str.Append(", ");
            }
            Debug.Log(str);
        }
        private void OnDestroy()
        {
            GameManager.OnGameStarted -= OnGameStarted;
        }
    }
}