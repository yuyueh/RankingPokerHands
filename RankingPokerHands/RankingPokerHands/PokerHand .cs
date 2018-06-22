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

        public static Card Parse(string input)
        {
            return new Card
            {
                Name = input[0],
                Rank = "23456789TJQK".IndexOf(input[0])
            };
        }
    }

    public class PokerHand
    {
        private List<Card> _hand;

        public PokerHand(string hand)
        {
            _hand = hand.Split(' ').Select(Card.Parse).OrderByDescending(p => p.Rank).ToList();
        }

        public List<Card> GetHand()
        {
            return _hand;
        }

        public Result CompareWith(PokerHand opponentHand)
        {
            return _hand.First().Rank > opponentHand.GetHand().First().Rank ? Result.Win : Result.Loss;
        }
    }
}
