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
        FourOfAKind = 8,
        FullHouse = 7,
        Flush = 6,
        Straight = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        Nothing = 1
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
            return hand.Split(" ").OrderBy(p => Array.IndexOf(_cardCompareMapper, p[0])).ToArray();
        }

        public Result CompareWith(PokerHand hand)
        {
            var myHandType = GetHandRanking();
            var opponentHandType = hand.GetHandRanking();
            return myHandType > opponentHandType ? Result.Win : myHandType == opponentHandType ? CompareBySameHandType(hand) : Result.Loss;
        }

        private Result CompareBySameHandType(PokerHand hand)
        {
            if (IsFourOfAKind())
            {
                var myHand = _hand.Select(p => p[0])
                    .GroupBy(p => p)
                    .OrderByDescending(p => p.Count())
                    .Select(p => p.Key).ToArray();
                var opponentHand = hand.GetHand().Select(p => p[0])
                    .GroupBy(p => p)
                    .OrderByDescending(p => p.Count())
                    .Select(p => p.Key).ToArray();

                if (IsCardBigger(myHand[0],opponentHand[0]))
                {
                    return Result.Win;
                }
                
                if (IsCardEqual(myHand[0],opponentHand[0]))
                {
                    return IsCardBigger(myHand[1], opponentHand[1]) ? Result.Win :
                        IsCardEqual(myHand[1], opponentHand[1]) ? Result.Tie : Result.Loss;
                }

                return Result.Loss;
            }

            if (IsFullHouse())
            {
                var myHand = _hand.GroupBy(p => p[0]).OrderByDescending(p => p.Count()).ThenByDescending(p => Array.IndexOf(_cardCompareMapper, p.Key)).Select(p => p.Key).ToArray();
                var opponentHand = hand.GetHand().GroupBy(p => p[0]).OrderByDescending(p => p.Count()).ThenByDescending(p => Array.IndexOf(_cardCompareMapper, p.Key)).Select(p => p.Key).ToArray();

                if (IsCardBigger(myHand[0], opponentHand[0]))
                {
                    return Result.Win;
                }

                if (IsCardEqual(myHand[0], opponentHand[0]))
                {
                    return IsCardBigger(myHand[1], opponentHand[1]) ? Result.Win :
                    IsCardEqual(myHand[1], opponentHand[1]) ? Result.Tie : Result.Loss;
                }

                return Result.Loss;
            }

            if (IsFlush())
            {
                for (int i = 5; i > 0; i--)
                {
                    if (IsCardBigger(_hand[i - 1][0],hand.GetHand()[i - 1][0]))
                    {
                        return Result.Win;
                    }

                    if (IsCardSmaller(_hand[i - 1][0],hand.GetHand()[i - 1][0]))
                    {
                        return Result.Loss;
                    }
                }
            }

            if (IsStraight())
            {
                return IsCardBigger(_hand[0][0], hand.GetHand()[0][0]) ? Result.Win :
                    IsCardEqual(_hand[0][0], hand.GetHand()[0][0]) ? Result.Tie : Result.Loss;
            }

            if (IsThreeOfAKind())
            {
                var myHand = _hand.GroupBy(p => p[0]).OrderByDescending(p => p.Count()).ThenByDescending(p => Array.IndexOf(_cardCompareMapper, p.Key)).Select(p => p.Key).ToArray();
                var opponentHand = hand.GetHand().GroupBy(p => p[0]).OrderByDescending(p => p.Count()).ThenByDescending(p => Array.IndexOf(_cardCompareMapper, p.Key)).Select(p => p.Key).ToArray();
                for (int i = 0; i < 3; i++)
                {
                    if (IsCardBigger(myHand[i],opponentHand[i]))
                    {
                        return Result.Win;
                    }

                    if (IsCardSmaller(myHand[i],opponentHand[i]))
                    {
                        return Result.Loss;
                    }
                }
            }

            if (IsTwoPair())
            {
                var myHand = _hand.GroupBy(p => p[0]).OrderByDescending(p => p.Count()).ThenByDescending(p => Array.IndexOf(_cardCompareMapper, p.Key)).Select(p => p.Key).ToArray();
                var opponentHand = hand.GetHand().GroupBy(p => p[0]).OrderByDescending(p => p.Count()).ThenByDescending(p => Array.IndexOf(_cardCompareMapper, p.Key)).Select(p => p.Key).ToArray();
                for (int i = 0; i < 3; i++)
                {
                    if (IsCardBigger(myHand[i],opponentHand[i]))
                    {
                        return Result.Win;
                    }

                    if (IsCardSmaller(myHand[i],opponentHand[i]))
                    {
                        return Result.Loss;
                    }
                }
            }

            if (IsOnePair())
            {
                var myHand = _hand.GroupBy(p => p[0]).OrderByDescending(p => p.Count()).ThenByDescending(p => Array.IndexOf(_cardCompareMapper, p.Key)).Select(p => p.Key).ToArray();
                var opponentHand = hand.GetHand().GroupBy(p => p[0]).OrderByDescending(p => p.Count()).ThenByDescending(p => Array.IndexOf(_cardCompareMapper, p.Key)).Select(p => p.Key).ToArray();
                for (int i = 0; i < 4; i++)
                {
                    if (IsCardBigger(myHand[i],opponentHand[i]))
                    {
                        return Result.Win;
                    }

                    if (IsCardSmaller(myHand[i],opponentHand[i]))
                    {
                        return Result.Loss;
                    }
                }
            }

            if (IsNoThing())
            {
                for (int i = 5; i > 0; i--)
                {
                    if (IsCardBigger(_hand[i - 1][0],hand.GetHand()[i - 1][0]))
                    {
                        return Result.Win;
                    }

                    if (IsCardSmaller(_hand[i - 1][0],hand.GetHand()[i - 1][0]))
                    {
                        return Result.Loss;
                    }
                }
            }

            return Result.Tie;
        }

        private bool IsNoThing()
        {
            return HandRanking.Nothing == GetHandRanking();
        }

        public HandRanking GetHandRanking()
        {
            if (IsRoyalStraightFlush())
            {
                return HandRanking.RoyalStraightFlush;
            }

            if (IsStraightFlush())
            {
                return HandRanking.StraightFlush;
            }

            if (IsFourOfAKind())
            {
                return HandRanking.FourOfAKind;
            }

            if (IsFullHouse())
            {
                return HandRanking.FullHouse;
            }

            if (IsFlush())
            {
                return HandRanking.Flush;
            }

            if (IsStraight())
            {
                return HandRanking.Straight;
            }

            if (IsThreeOfAKind())
            {
                return HandRanking.ThreeOfAKind;
            }

            if (IsTwoPair())
            {
                return HandRanking.TwoPair;
            }

            if (IsOnePair())
            {
                return HandRanking.OnePair;
            }
            
            return HandRanking.Nothing;
        }

        private bool IsOnePair()
        {
            var hand = _hand.GroupBy(p => p[0]).Select(p => p.Count()).OrderByDescending(p => p).ToArray();
            return hand.Length == 4 && hand[0] == 2;
        }

        private bool IsTwoPair()
        {
            var hand = _hand.GroupBy(p => p[0]).Select(p => p.Count()).OrderByDescending(p => p).ToArray();
            return hand.Length == 3 && hand[0] == 2 && hand[1] == 2;
        }

        private bool IsThreeOfAKind()
        {
            var hand = _hand.GroupBy(p => p[0]).Select(p => p.Count()).OrderByDescending(p => p).ToArray();
            return hand[0] == 3 && hand.Length == 3;
        }

        private bool IsCardBigger(char myHand, char opponentHand)
        {
            return Array.IndexOf(_cardCompareMapper, myHand) >
                Array.IndexOf(_cardCompareMapper, opponentHand);
        }

        private bool IsCardEqual(char myHand, char opponentHand)
        {
            return Array.IndexOf(_cardCompareMapper, myHand) ==
                   Array.IndexOf(_cardCompareMapper, opponentHand);
        }

        private bool IsCardSmaller(char myHand, char opponentHand)
        {
            return Array.IndexOf(_cardCompareMapper, myHand) <
                   Array.IndexOf(_cardCompareMapper, opponentHand);
        }

        private bool IsStraightFlush()
        {
            return IsFlush() && IsStraight();
        }

        private bool IsStraight()
        {
            return _hand.Select((p, i) => Array.IndexOf(_cardCompareMapper, p[0]) - i).Distinct().Count() == 1;
        }

        private bool IsFullHouse()
        {
            var hand = _hand.GroupBy(p => p[0]).Select(p => p.Count()).OrderByDescending(p => p).ToArray();
            return hand[0] == 3 && hand[1] == 2;
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
