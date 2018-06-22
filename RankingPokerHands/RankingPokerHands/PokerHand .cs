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
                Rank = "23456789TJQK".IndexOf(input[0])
            };
        }
    }

    public class PokerHand
    {
        private List<Card> _hand;
        private int _rank;

        public PokerHand(string hand)
        {
            _hand = hand.Split(' ').Select(Card.Parse).OrderByDescending(p => p.Rank).ToList();

            var comparesList = new List<Func<PokerHand, bool>>()
            {
                (c) => _hand.GroupBy(p => p.Suit).Any(p => p.Count() == 5) &&
                       _hand.Select((p, i) => p.Rank - i).Distinct().Count() == 1 &&
                       _hand.Last().Name.Equals('A'),                                   // 皇家
                (c) => _hand.GroupBy(p => p.Suit).Any(p => p.Count() == 5) &&
                       _hand.Select((p, i) => p.Rank - i).Distinct().Count() == 1,      // 同花順
                (c) => _hand.GroupBy(p => p.Rank).Any(p => p.Count() == 4)              // 鐵支
            };

            _rank = comparesList.Count - comparesList.FindIndex(f => f(this));
        }

        public int GetRank()
        {
            return _rank;
        }

        public Result CompareWith(PokerHand opponentHand)
        {
            return GetRank() > opponentHand.GetRank() ? Result.Win : Result.Loss;
        }
    }
}
