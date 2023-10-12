using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        }

        public virtual void PlayCard(CardData card)
        {
            var cardTransform = _cards[card].transform;
            
            cardTransform.parent = transform.parent;
            cardTransform.DOMove(_playCardLocation.position, 0.2f);
            cardTransform.DORotate(_playCardLocation.rotation.eulerAngles, 0.2f);
            ((RectTransform)cardTransform).DOSizeDelta(((RectTransform)_playCardLocation).sizeDelta, 0.2f);

            _lastPlayedCard = cardTransform.gameObject;
            _cards[card].Image = card.Image;
            _cards.Remove(card);
        }

        public virtual void EndTurn(int points)
        {
            if(_lastPlayedCard) Destroy(_lastPlayedCard);
            _pointsText.text = points.ToString();
        }
    }
}
