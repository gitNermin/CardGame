using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        private const int PLAYERS_COUNT = 4;
        public static event Action OnGameStarted;
        public static event Action OnGameFinished;

        public static event Action<string> OnGameStatusUpdate;

        [Tooltip("Areas in anti-clockwise order")] 
        [SerializeField] private PlayerArea[] _playerAreas;
        
        [SerializeField] private PlayArea _playArea;
        [SerializeField] private CardsList _cardsList;

        private Player[] _players = new Player[PLAYERS_COUNT];
        private int _dealerIndex;

        private async void Awake()
        {
            Setup();
            await DealCards();
            await PlayGame();
            EndGame();
        }

        void Setup()
        {
            for (int i = 0; i < PLAYERS_COUNT; i++)
            {
                if (i == 0)
                {
                    _players[i] = new LocalPlayer((LocalPlayerArea)_playerAreas[i]);
                }
                else
                {
                    _players[i] = new AIPlayer(_playerAreas[i]);
                }
                
                _playerAreas[i].Initialize(_playArea, $"Player {i}");
            }
        }
        async Task DealCards()
        {
            _dealerIndex = Random.Range(0, _players.Length);
            
            int player = _dealerIndex;
            
            OnGameStatusUpdate?.Invoke($"Player {_dealerIndex}, dealing cards!");
            
            var cards = _cardsList.Items;
            
            while (cards.Count > 0)
            {
                var card = cards[Random.Range(0, cards.Count)];
                _players[++player % _players.Length].AddCard(card);
                cards.Remove(card);
                
                await Task.Delay(500);
            }
        }
        async Task PlayGame()
        {
            OnGameStarted?.Invoke();
            
            int roundsCount = _cardsList.Count / _players.Length;
            var cards = new CardData[_players.Length];
            for (int i = 0; i < roundsCount; i++)
            {
                for (int j = 0; j < _players.Length; j++)
                {
                    var playerIndex = (_dealerIndex + 1 + j) % _players.Length;
                    if (playerIndex == 0)
                    {
                        await Task.Delay(500);
                        OnGameStatusUpdate?.Invoke("Your Turn!");
                    }
                    cards[playerIndex] = await _players[playerIndex].Play();
                }

                int winnerIndex = GetTurnWinner(cards);
                
                await Task.Delay(2000);
                
                _playArea.CollectCards(_playerAreas[winnerIndex].PlayerLocation);
                
                await Task.Delay(1000);
                
                for (int j = 0; j < _players.Length; j++)
                {
                    _players[j].EndTurn( j == winnerIndex? 1 : 0);
                }
            }
            
        }
        private int GetTurnWinner(CardData[] cards)
        {
            int winner = 0;
            for (int i = 1; i < cards.Length; i++)
            {
                if (cards[i] > cards[winner])
                    winner = i;
            }
            return winner;
        }

        void EndGame()
        {
            int winner = 0;
            for (int i = 1; i < _players.Length; i++)
            {
                if (_players[i].TotalPoints > _players[winner].TotalPoints)
                {
                    winner = i;
                }
            }

            if (winner == 0)
            {
                OnGameStatusUpdate?.Invoke("You Win!");
            }
            else
            {
                OnGameStatusUpdate?.Invoke($"Player {winner} Wins!");
            }
        }
    }
}
