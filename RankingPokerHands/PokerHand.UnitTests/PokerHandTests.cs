using NUnit.Framework;

namespace RankingPokerHands.UnitTests
{
    [TestFixture]
    public class PokerHandTests
    {
        [Test]
        public void HighestStraightFlushWins()
        {
            // Arrange
            var myHand = new PokerHand("2H 3H 4H 5H 6H");
            var opponentHand = new PokerHand("KS AS TS QS JS");
            var expected = Result.Loss;

            // Act
            var result = myHand.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }
    }
}
