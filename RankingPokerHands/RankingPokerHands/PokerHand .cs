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
        private readonly char[] _royalStraightMapper = {'T', 'J', 'Q', 'K', 'A'};
        private readonly char[] _cardCompareMapper = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'};

        public PokerHand(string hand)
        {
            _hand = ConvertToSortedHand(hand);
        }

        public string[] GetHand()
        {
            return _hand;
        }

        private string[] ConvertToSortedHand(string hand)
        {
            var temp = hand.Split(" ");
            var temp2 = temp.OrderBy(p =>
            {
                var k = Array.IndexOf(_cardCompareMapper, p[0]);
                return k;
            });
            var temp3 = temp2.ToArray();
            return temp3;
        }

        public Result CompareWith(PokerHand hand)
        {
            var myHandType = GetHandRanking();
            var opponentHandType = hand.GetHandRanking();
            return myHandType > opponentHandType ? Result.Win : myHandType == opponentHandType ? CompareBySameHandType(hand) : Result.Loss;
        }

        private Result CompareBySameHandType(PokerHand hand)
        {
            if (HandRanking.FourOfAKind == GetHandRanking())
            {
                var myHand = _hand.Select(p => p[0])
                    .GroupBy(p => p)
                    .OrderByDescending(p => p.Count())
                    .Select(p => p.Key).ToArray();
                var opponentHand = hand.GetHand().Select(p => p[0])
                    .GroupBy(p => p)
                    .OrderByDescending(p => p.Count())
                    .Select(p => p.Key).ToArray();

                if (Array.IndexOf(_cardCompareMapper, myHand[0]) > Array.IndexOf(_cardCompareMapper, opponentHand[0]))
                {
                    return Result.Win;
                }
                else if (Array.IndexOf(_cardCompareMapper, myHand[0]) == Array.IndexOf(_cardCompareMapper, opponentHand[0]))
                {
                    return Array.IndexOf(_cardCompareMapper, myHand[1]) > Array.IndexOf(_cardCompareMapper, opponentHand[1]) ? Result.Win :
                        Array.IndexOf(_cardCompareMapper, myHand[1]) == Array.IndexOf(_cardCompareMapper, opponentHand[1]) ? Result.Tie : Result.Loss;
                }
                else
                {
                    return Result.Loss;
                }
            }

            return Result.Tie;
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
            var result = IsFlush() && IsRoyalStraight();
            return result;
        }

        private bool IsRoyalStraight()
        {
            var result = _hand.Select(p => p[0]).SequenceEqual(_royalStraightMapper);
            return result;
        }

        private bool IsFlush()
        {
            var result = _hand.Select(p => p[1]).Distinct().Count() == 1;
            return result;
        }
    }
}
