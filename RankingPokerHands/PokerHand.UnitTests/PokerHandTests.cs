using NUnit.Framework;

namespace RankingPokerHands.UnitTests
{
    [TestFixture]
    public class PokerHandTests
    {
        [Test]
        public void Test_HighestStraightFlushWins()
        {
            // Arrange
            var target = new PokerHand("2H 3H 4H 5H 6H");
            var opponentHand = new PokerHand("KS AS TS QS JS");
            var expected = Result.Loss;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_StraightFlushWinsOf4OfAKind()
        {
            // Arrange
            var target = new PokerHand("2H 3H 4H 5H 6H");
            var opponentHand = new PokerHand("AS AD AC AH JD");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_Highest4OfAKindWins()
        {
            // Arrange
            var target = new PokerHand("AS AH 2H AD AC");
            var opponentHand = new PokerHand("JS JD JC JH 3D");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_4OfAKindWinsOfFullHouse()
        {
            // Arrange
            var target = new PokerHand("2S AH 2H AS AC");
            var opponentHand = new PokerHand("JS JD JC JH AD");
            var expected = Result.Loss;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_FullHouseWinsOfFlush()
        {
            // Arrange
            var target = new PokerHand("2S AH 2H AS AC");
            var opponentHand = new PokerHand("2H 3H 5H 6H 7H");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_HighestFlushWins()
        {
            // Arrange
            var target = new PokerHand("AS 3S 4S 8S 2S");
            var opponentHand = new PokerHand("2H 3H 5H 6H 7H");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_FlushWinsOfStraight()
        {
            // Arrange
            var target = new PokerHand("2H 3H 5H 6H 7H");
            var opponentHand = new PokerHand("2S 3H 4H 5S 6C");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }
        
        [Test]
        public void Test_EqualStraightIsTie()
        {
            // Arrange
            var target = new PokerHand("2S 3H 4H 5S 6C");
            var opponentHand = new PokerHand("3D 4C 5H 6H 2S");
            var expected = Result.Tie;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_StraightWinsOfThreeOfAKind()
        {
            // Arrange
            var target = new PokerHand("2S 3H 4H 5S 6C");
            var opponentHand = new PokerHand("AH AC 5H 6H AS");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_ThreeOfAKindWinsOfTwoPair()
        {
            // Arrange
            var target = new PokerHand("2S 2H 4H 5S 4C");
            var opponentHand = new PokerHand("AH AC 5H 6H AS");
            var expected = Result.Loss;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_TwoPairWinsOfPair()
        {
            // Arrange
            var target = new PokerHand("2S 2H 4H 5S 4C");
            var opponentHand = new PokerHand("AH AC 5H 6H 7S");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_HighestPairWins()
        {
            // Arrange
            var target = new PokerHand("6S AD 7H 4S AS");
            var opponentHand = new PokerHand("AH AC 5H 6H 7S");
            var expected = Result.Loss;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }
        
        [Test]
        public void Test_PairWinsOfNothing()
        {
            // Arrange
            var target = new PokerHand("2S AH 4H 5S KC");
            var opponentHand = new PokerHand("AH AC 5H 6H 7S");
            var expected = Result.Loss;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }
        
        [Test]
        public void Test_HighestCardLoses()
        {
            // Arrange
            var target = new PokerHand("2S 3H 6H 7S 9C");
            var opponentHand = new PokerHand("7H 3C TH 6H 9S");
            var expected = Result.Loss;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test_HighestCardWins()
        {
            // Arrange
            var target = new PokerHand("4S 5H 6H TS AC");
            var opponentHand = new PokerHand("3S 5H 6H TS AC");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }
         
        [Test]
        public void Test_EqualCardsIsTie()
        {
            // Arrange
            var target = new PokerHand("2S AH 4H 5S 6C");
            var opponentHand = new PokerHand("AD 4C 5H 6H 2C");
            var expected = Result.Tie;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [Test]
        public void Test()
        {
            // Arrange
            var target = new PokerHand("2S AH 4H 5S 3C");
            var opponentHand = new PokerHand("3D 3C 3H 6H 2C");
            var expected = Result.Win;

            // Act
            var result = target.CompareWith(opponentHand);

            // Assert
            Assert.AreEqual(result, expected);
        }

        //7C 7S KH 2H 7H, Opponent hand: 7C 7S 3S 7H 5S
        //TS KS 5S 9S AC, Opponent hand: JH 8S TH AH QH
        //AC KH QH AH AS, Opponent hand: 7C 7S KH 2H 7H
        //JH 8S TH AH QH, Opponent hand: KD 6S 9D TH AD
        //7C 7S 3S 7H 5S, Opponent hand: 7C 7S KH 2H 7H
        //JH 8S TH AH QH, Opponent hand: KD 6S 9D TH AD
    }
}
