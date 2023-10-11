using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardGame
{
    public class PlayerArea : MonoBehaviour
    {
        [SerializeField] private Card _card;
        [SerializeField] private Transform _playCardLocation;
        [SerializeField] private TMP_Text _text;

        private Dictionary<CardData, Card> _cards = new Dictionary<CardData, Card>();

        private GameObject _lastPlayedCard;
        
        public void AddCard(CardData card)
        {
            var cardObject = Instantiate(_card, transform);
            _cards.Add(card, cardObject);
            cardObject.Image = card.Image;
        }

        public void PlayCard(CardData card)
        {
            var cardTransform = _cards[card].transform;
            
            cardTransform.parent = transform.parent;
            cardTransform.position = _playCardLocation.position;
            cardTransform.rotation = _playCardLocation.rotation;
            cardTransform.localScale = _playCardLocation.localScale;

            _lastPlayedCard = cardTransform.gameObject;

            _cards.Remove(card);
        }

        public void EndTurn(int points)
        {
            if(_lastPlayedCard) Destroy(_lastPlayedCard);
            _text.text = points.ToString();
        }
    }
}
