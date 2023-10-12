using System.Threading.Tasks;

namespace CardGame
{
    public class AIPlayer : Player
    {
        private bool _hasSortedCards = false;
        public AIPlayer(PlayerArea area)
        {
            _area = area;
        }
        protected override async Task<CardData> SelectCard()
        {
            if (!_hasSortedCards)
            {
                _cards.Sort();
                _hasSortedCards = true;
            }
            
            await Task.Delay(1000);
            return _cards[^1];
        }
    }
}
