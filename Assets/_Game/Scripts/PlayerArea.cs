using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CardGame
{
    public class PlayerArea : MonoBehaviour
    {
        [SerializeField] private Card _cardPrefab;
        
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private TMP_Text _playerNameText;
        [SerializeField] private Image _playerAvatar;
        [SerializeField] private Transform _playerLocation;
        
        protected Dictionary<CardData, Card> _cards = new Dictionary<CardData, Card>();

        private GameObject _lastPlayedCard;

        private PlayArea _playArea;
        public Transform PlayerLocation => _playerLocation;

        public void Initialize(PlayArea playArea, string playerName, Sprite playerAvatar = null)
        {
            _playArea = playArea;
            _playerNameText.text = playerName;
            if (playerAvatar) _playerAvatar.sprite = playerAvatar;
        }
        
        public virtual void AddCard(CardData card)
        {
            var cardObject = Instantiate(_cardPrefab, transform);
            _cards.Add(card, cardObject);
        }

        public virtual void PlayCard(CardData card)
        {
            //flip the card
            _cards[card].Image = card.Image;
            
            if(!_playArea) 
                Destroy(_cards[card].gameObject);
            else
                _playArea.PlayCard(_cards[card].transform);
           
            _cards.Remove(card);
        }

        public virtual void EndTurn(int points)
        {
            _pointsText.text = points.ToString();
        }
    }
}
