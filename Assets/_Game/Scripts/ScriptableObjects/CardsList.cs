using System;
using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(fileName = "CardsData", menuName = "Card Game/Cards List", order = 0)]
    public class CardsList : ScriptableList<CardData>
    {
        [field: SerializeField] public Sprite Back;
    }

    [Serializable]
    public class CardData : IComparable<CardData>
    {
        [field: SerializeField] public CardSuit Suit { get; private set; }
        [field: SerializeField] public CardNumber Number { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }

        public int CompareTo(CardData other)
        {
            if (this == other) return 0;
            if (this > other) return 1;
            return -1;
        }

        public override string ToString()
        {
            return $"{Number} of {Suit}";
        }

        public static bool operator >(CardData a, CardData b)
        {
            if (a.Number == b.Number)
            {
                return a.Suit > b.Suit;
            }
            return a.Number > b.Number;
        }

        public static bool operator <(CardData a, CardData b)
        {
            if (a.Number == b.Number)
            {
                return a.Suit < b.Suit;
            }
            return a.Number < b.Number;
        }
    }

    public enum CardSuit
    {
        Clubs, Diamonds, Hearts, Spades
    }
    
    public enum CardNumber
    {
        Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }
}