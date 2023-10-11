using System.Threading.Tasks;
using UnityEngine;

namespace CardGame
{
    public class LocalPlayer : Player
    {

        private CardData _selectedCard;

        protected override void OnGameStarted()
        {
            base.OnGameStarted();
            ((LocalPlayerArea)_area).OnCardSelected += OnCardSelected;
        }

        private void OnCardSelected(CardData card)
        {
            _selectedCard = card;
            ((LocalPlayerArea)_area).Interactable = false;
        }

        protected override async Task<CardData> SelectCard()
        {
            ((LocalPlayerArea)_area).Interactable = true;
            while (_selectedCard == null)
            {
                await Task.Delay(500);
            }
            var card = _selectedCard;
            Debug.Log(card);
            _selectedCard = null;
            return card;
        }
    }
}
