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

        private int _dealerIndex;

        private async void Awake()
        {
            await DealCards();
            StartGame();
        }
        async Task DealCards()
        {
            _dealerIndex = Random.Range(0, _players.Length);
            int player = _dealerIndex;
            var cards = _cardsList.Items;
            while (cards.Count > 0)
            {
                var card = cards[Random.Range(0, cards.Count)];
                _players[++player % _players.Length].AddCard(card);
                cards.Remove(card);
                await Task.Delay(500);
            }
        }
        async void StartGame()
        {
            OnGameStarted?.Invoke();
            int roundsCount = _cardsList.Count / _players.Length;
            var cards = new CardData[_players.Length];
            for (int i = 0; i < roundsCount; i++)
            {
                for (int j = 0; j < _players.Length; j++)
                {
                    var playerIndex = (_dealerIndex + 1 + j) % _players.Length;
                    cards[playerIndex] = await _players[playerIndex].Play();
                }
                
                for (int j = 0; j < _players.Length; j++)
                {
                    _players[j].EndTurn(0);
                }
            }
            
        }
    }
}
