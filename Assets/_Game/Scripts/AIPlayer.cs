using System.Collections;
using System.Collections.Generic;
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

        public override async Task<CardData> Play()
        {
            await Task.Delay(2);
            var card = _cards[^1];
            Play(card);
            return card;
        }
    }
}
