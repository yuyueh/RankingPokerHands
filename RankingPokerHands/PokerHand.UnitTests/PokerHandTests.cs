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

        [Test]
        public void StraightFlushWinsOf4OfAKind()
        {
            // Arrange
            var myHand = new PokerHand("2H 3H 4H 5H 6H");
            var opponentHand = new PokerHand("AS AD AC AH JD");
            var expected = Result.Win;

            // Act
            var result = myHand.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Highest4OfAKindWins()
        {
            // Arrange
            var myHand = new PokerHand("AS AH 2H AD AC");
            var opponentHand = new PokerHand("JS JD JC JH 3D");
            var expected = Result.Win;

            // Act
            var result = myHand.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }
            
        [Test]
        public void FourOfAKindWinsOfFullHouse()
        {
            // Arrange
            var myHand = new PokerHand("2S AH 2H AS AC");
            var opponentHand = new PokerHand("JS JD JC JH AD");
            var expected = Result.Loss;

            // Act
            var result = myHand.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void HighestFlushWins()
        {
            // Arrange
            var myHand = new PokerHand("AS 3S 4S 8S 2S");
            var opponentHand = new PokerHand("2H 3H 5H 6H 7H");
            var expected = Result.Win;

            // Act
            var result = myHand.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }
        
    }
}
