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
        StraightFlush = 9
    }

    public class PokerHand
    {
        private readonly string[] _hand;
        private readonly string[] _royalStraightMapper = {"T", "J", "Q", "K", "A"};
        private readonly string[] _cardCompareMapper = { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A"};

        public PokerHand(string hand)
        {
            _hand = hand.Split(" ").OrderBy(p => Array.IndexOf(_cardCompareMapper, p[0])).ToArray();
        }

        public Result CompareWith(PokerHand hand)
        {
            return this.GetHandRanking() > hand.GetHandRanking() ? Result.Win : Result.Loss;
        }

        public HandRanking GetHandRanking()
        {
            return _hand.Select(p => p[1]).Distinct().Count() == 1 &&
                   _hand.Select(p => p[0].ToString()).SequenceEqual<string>(_royalStraightMapper)
                ? HandRanking.RoyalStraightFlush
                : HandRanking.StraightFlush;
        }
    }
}
