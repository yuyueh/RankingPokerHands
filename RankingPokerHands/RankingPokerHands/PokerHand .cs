using System;

namespace RankingPokerHands
{
    public enum Result 
    { 
        Win, 
        Loss, 
        Tie 
    }

    public class PokerHand
    {
        public PokerHand(string hand)
        {
        }

        public Result CompareWith(PokerHand hand)
        {
            throw new NotImplementedException();
        }
    }
}
