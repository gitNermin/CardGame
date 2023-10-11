using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame
{
    public class PlayerArea : MonoBehaviour
    {
        [SerializeField] private Card _card;
        [SerializeField] private Transform _playCardLocation;
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private TMP_Text _playerNameText;

        protected Dictionary<CardData, Card> _cards = new Dictionary<CardData, Card>();

        private GameObject _lastPlayedCard;
        
        public virtual void AddCard(CardData card)
        {
            var cardObject = Instantiate(_card, transform);
            _cards.Add(card, cardObject);
            cardObject.Image = card.Image;
        }

        public virtual void PlayCard(CardData card)
        {
            var cardTransform = _cards[card].transform;
            
            cardTransform.parent = transform.parent;
            cardTransform.position = _playCardLocation.position;
            cardTransform.rotation = _playCardLocation.rotation;
            cardTransform.localScale = _playCardLocation.localScale;

            _lastPlayedCard = cardTransform.gameObject;

            _cards.Remove(card);
        }

        public virtual void EndTurn(int points)
        {
            if(_lastPlayedCard) Destroy(_lastPlayedCard);
            _pointsText.text = points.ToString();
        }
    }
}
