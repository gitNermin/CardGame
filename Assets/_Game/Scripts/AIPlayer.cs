using System.Threading.Tasks;

namespace CardGame
{
    public class AIPlayer : Player
    {
        protected override void OnGameStarted()
        {
            base.OnGameStarted();
            _cards.Sort();
            PrintCards();
        }

        protected override async Task<CardData> SelectCard()
        {
            await Task.Delay(1000);
            return _cards[^1];
        }
    }
}
