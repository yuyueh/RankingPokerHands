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
        private readonly int _rank;
        private readonly int _baseScore;

        public PokerHand(string handString)
        {
            var hand = handString.Split(' ')
                .Select(Card.Parse)
                .OrderByDescending(p => p.Rank)
                .ToList();
            var comparesList = new List<Func<bool>>()
            {
                () => hand.GroupBy(p => p.Suit).Any(p => p.Count() == 5) &&
                       hand.Select((p, i) => p.Rank - i).Distinct().Count() == 1 &&
                       hand.Last().Name.Equals('A'),                                   // 皇家
                () => hand.GroupBy(p => p.Suit).Any(p => p.Count() == 5) &&
                       hand.Select((p, i) => p.Rank - i).Distinct().Count() == 1,      // 同花順
                () => hand.GroupBy(p => p.Rank).Any(p => p.Count() == 4),              // 鐵支
                () => hand.GroupBy(p => p.Rank).Any(p => p.Count() == 3) &&
                       hand.GroupBy(p => p.Rank).Any(p => p.Count() == 2)              // Full House
            };

            _rank = (comparesList.Count - comparesList.FindIndex(f => f())) * (int)Math.Pow(10, 7);
            _baseScore = hand.GroupBy(p => p.Rank)
                .OrderByDescending(p => p.Count())
                .ThenByDescending(p => p.Key)
                .Select((p, i) => new {Value = p.Key, Index = i})
                .Sum(p => p.Value * (int) Math.Pow(13, 5 - p.Index));
        }

        public int GetRank()
        {
            return _rank + _baseScore;
        }

        public Result CompareWith(PokerHand opponentHand)
        {
            return GetRank() > opponentHand.GetRank() ? Result.Win : Result.Loss;
        }
    }
}
