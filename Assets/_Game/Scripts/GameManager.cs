using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnGameStarted;
        public static event Action OnGameFinished;
        
        [Tooltip("Players in anti-clockwise order")]
        [SerializeField] private Player[] _players;
        [SerializeField] private CardsList _cardsList;

        private async void Awake()
        {
            await DealCards();
            StartGame();
        }
        async Task DealCards()
        {
            int dealer = Random.Range(0, _players.Length);
            var cards = _cardsList.Items;
            while (cards.Count > 0)
            {
                var card = cards[Random.Range(0, cards.Count)];
                _players[++dealer % _players.Length].AddCard(card);
                cards.Remove(card);
                await Task.Delay(2);
            }
        }
        void StartGame()
        {
            OnGameStarted?.Invoke();
        }
    }
}
