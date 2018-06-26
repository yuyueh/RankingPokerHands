using System;
using System.Collections.Generic;
using System.Linq;

namespace RankingPokerHands
{
    public enum Result 
    { 
        Win, 
        Loss, 
        Tie 
    }

    public class Card
    {
        public char Name { get; set; }
        public int Rank { get; set; }
        public char Suit { get; set; }

        public static Card Parse(string input)
        {
            return new Card
            {
                Name = input[0],
                Suit = input[1],
                Rank = "23456789TJQKA".IndexOf(input[0])
            };
        }
    }

    public class PokerHand
    {
        private readonly List<Card> _hand;
        private readonly List<Func<bool>> _compareList;

        public PokerHand(string handString)
        {
            _hand = handString.Split(' ')
                .Select(Card.Parse)
                .OrderByDescending(p => p.Rank)
                .ToList();

            _compareList = new List<Func<bool>>()
            {
                () => IsFlush() &&
                      IsStraight() &&
                      IsLastValueWith('A'), 
                () => IsFlush() &&
                      IsStraight(),
                () => IsSameValueWithTimes(4),
                () => IsSameValueWithTimes(3) &&
                      IsSameValueWithTimes(2),
                () => IsFlush(),
                () => IsStraight(),
                () => IsSameValueWithTimes(3),
                () => IsTwoPair(),
                () => IsSameValueWithTimes(2),
                () => true
            };
        }

        public int GetRank()
        {
            var rank = (_compareList.Count - _compareList.FindIndex(f => f())) * (int) Math.Pow(10, 7);
            var point = _hand.GroupBy(p => p.Rank)
                .OrderByDescending(p => p.Count())
                .ThenByDescending(p => p.Key)
                .Select((p, i) => new {Value = p.Key, Index = i})
                .Sum(p => p.Value * (int) Math.Pow(13, 5 - p.Index));
            return rank + point;
        }

        public Result CompareWith(PokerHand opponentHand)
        {
            return GetRank() > opponentHand.GetRank() ? 
                Result.Win : GetRank() == opponentHand.GetRank() ? 
                Result.Tie : Result.Loss;
        }

        private bool IsTwoPair()
        {
            return _hand.GroupBy(p => p.Rank).Count(p => p.Count() == 2) == 2;
        }

        private bool IsLastValueWith(char c)
        {
            return _hand.Last().Name.Equals(c);
        }

        private bool IsSameValueWithTimes(int times)
        {
            return _hand.GroupBy(p => p.Rank).Any(p => p.Count() == times);
        }

        private bool IsStraight()
        {
            return _hand.Select((p, i) => p.Rank + i).Distinct().Count() == 1;
        }

        private bool IsFlush()
        {
            return _hand.GroupBy(p => p.Suit).Count() == 1;
        }
    }
}
