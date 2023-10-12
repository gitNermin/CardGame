using System.Threading.Tasks;
using UnityEngine;

namespace CardGame
{
    public class LocalPlayer : Player
    {
        private CardData _selectedCard;

        public LocalPlayer(LocalPlayerArea area)
        {
            _area = area;
            area.OnCardSelected += OnCardSelected;
        }

        private void OnCardSelected(CardData card)
        {
            Debug.Log(card);
            _selectedCard = card;
            ((LocalPlayerArea)_area).Interactable = false;
        }

        protected override async Task<CardData> SelectCard()
        {
            ((LocalPlayerArea)_area).Interactable = true;
            //TODO: Replace with UniRx Event Observable
            while (_selectedCard == null)
            {
                await Task.Delay(500);
            }
            var card = _selectedCard;
            _selectedCard = null;
            return card;
        }
    }
}
