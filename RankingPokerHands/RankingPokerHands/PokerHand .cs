using System;
using System.Linq;

namespace RankingPokerHands
{
    public enum Result 
    { 
        Win, 
        Loss, 
        Tie 
    }

    public enum HandRanking
    {
        RoyalStraightFlush = 10,
        StraightFlush = 9,
        FourOfAKind = 8
    }

    public class PokerHand
    {
        private readonly string[] _hand;
        private readonly string[] _royalStraightMapper = {"T", "J", "Q", "K", "A"};
        private readonly string[] _cardCompareMapper = { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A"};

        public PokerHand(string hand)
        {
            _hand = ConvertToSortedHand(hand);
        }

        private string[] ConvertToSortedHand(string hand)
        {
            return hand.Split(" ").OrderBy(p => Array.IndexOf(_cardCompareMapper, p[0])).ToArray();
        }

        public Result CompareWith(PokerHand hand)
        {
            return GetHandRanking() > hand.GetHandRanking() ? Result.Win : Result.Loss;
        }

        public HandRanking GetHandRanking()
        {
            return IsRoyalStraightFlush()
                ? HandRanking.RoyalStraightFlush
                : IsFourOfAKind() ? HandRanking.FourOfAKind : HandRanking.StraightFlush;
        }

        private bool IsFourOfAKind()
        {
            return _hand.Select(p => p[0]).GroupBy(p => p).Any(p => p.Count() >= 4);
        }

        private bool IsRoyalStraightFlush()
        {
            return IsFlush() && IsRoyalStraight();
        }

        private bool IsRoyalStraight()
        {
            return _hand.Select(p => p[0].ToString()).SequenceEqual<string>(_royalStraightMapper);
        }

        private bool IsFlush()
        {
            return _hand.Select(p => p[1]).Distinct().Count() == 1;
        }
    }
}
