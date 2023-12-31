﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CardGame
{
    public abstract class Player
    {
        protected PlayerArea _area;
        protected List<CardData> _cards = new List<CardData>();
        protected int _totalPoints;
        public int TotalPoints => _totalPoints;
        public bool HasCards => _cards.Count > 0;
        public virtual bool AddCard(CardData card)
        {
            if (_cards.Contains(card)) return false;
            _cards.Add(card);
            _area.AddCard(card);
            return true;
        }
        public virtual async Task<CardData> Play()
        {
            var card = await SelectCard();
            Play(card);
            return card;
        }
        protected abstract Task<CardData> SelectCard();
        public virtual void EndTurn(int points)
        {
            _totalPoints += points;
            _area.EndTurn(_totalPoints);
        }
        
        protected virtual bool Play(CardData card)
        {
            if (!_cards.Contains(card)) return false;
            _cards.Remove(card);
            _area.PlayCard(card);
            return true;
        }
        protected void PrintCards()
        {
            StringBuilder str = new StringBuilder();
            str.Append($"Cards: ");
            for (int i = 0; i < _cards.Count; i++)
            {
                str.Append(_cards[i]);
                if (i != _cards.Count - 1) str.Append(", ");
            }
            Debug.Log(str);
        }
    }
}