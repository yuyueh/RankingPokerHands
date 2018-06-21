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
        public char Suite { get; set; }
        public int Rank { get; set; }

        public static Card Parse(string card)
        {
            return new Card()
            {
                Name = card[0],
                Suite = card[1],
                Rank = "23456789TJQKA".IndexOf(card[0].ToString(), StringComparison.Ordinal)
            };
        }
    }

    public class PokerHand
    {
        private readonly List<Card> _cards;
        private readonly List<Func<bool>> _ranker;

        public PokerHand(string hand)
        {
            _cards = hand.Split(' ').Select(Card.Parse).OrderByDescending(p => p.Rank).ToList();
            var cardsGroupByRank = _cards.GroupBy(p => p.Rank)
                .OrderByDescending(p => p.Count())
                .ThenByDescending(p => p.Key)
                .ToDictionary(p => p.Key, p => p.Count());

            _ranker = new List<Func<bool>>()
            {
                () => IsFlush() && IsStraight() && 'a'.Equals(_cards.First().Name),                         // royal straight flush
                () => IsFlush() && IsStraight(),                                                            // straight flush
                () => cardsGroupByRank.Any(p => p.Value == 4),                                              // four of the kind
                () => cardsGroupByRank.Any(p => p.Value == 3) && cardsGroupByRank.Any(p => p.Value == 2),   // full house
                () => IsFlush(),                                                                            // flush
                () => IsStraight(),                                                                         // straight 
                () => cardsGroupByRank.Any(p => p.Value == 3),                                              // three of the kind
                () => cardsGroupByRank.Count(p => p.Value == 2) == 2,                                       // two pairs
                () => cardsGroupByRank.Any(p => p.Value == 2),                                              // one pair
                () => true                                                                                  // nothing
            };
        }

        public Result CompareWith(PokerHand hand)
        {
            if (GetRank() > hand.GetRank())
            {
                return Result.Win;
            }

            return GetRank() < hand.GetRank() ? Result.Loss : CompareWithSameRank(hand);
        }

        private Result CompareWithSameRank(PokerHand hand)
        {
            for (var i = 0; i < 5; i++)
            {
                if (_cards[i].Rank > hand._cards[i].Rank)
                {
                    return Result.Win;
                }
                if (_cards[i].Rank < hand._cards[i].Rank)
                {
                    return Result.Loss;
                }
            }

            return Result.Tie;
        }

        public int GetRank()
        {
            return _ranker.Count() - _ranker.FindIndex(f => f());
        }

        private bool IsFlush()
        {
            return _cards.All(p => p.Suite.Equals(_cards.First().Suite));
        }

        private bool IsStraight()
        {
            return _cards.Select(p => p.Name).SequenceEqual(new char[]{'A', '5', '4', '3', '2'}) ||
                   _cards.OrderBy(p => p.Rank).Select((p, i) => p.Rank - i).Distinct().Count() == 1;
        }
    }
}