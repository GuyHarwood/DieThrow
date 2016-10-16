using System.Linq;
using Moq;
using Xunit;

namespace GuyHarwood.DieThrow.Domain.Tests
{
    public class DescribeThrowDiceHandler
    {
        public DescribeThrowDiceHandler()
        {
            _diceMock = new Mock<IDice>();
            _winningStreakIdentifierMock = new Mock<IWinningStreakIdentifier>();
            _sut = new ThrowDiceHandler(_diceMock.Object, _winningStreakIdentifierMock.Object);
        }

        private readonly ThrowDiceHandler _sut;
        private readonly Mock<IDice> _diceMock;
        private readonly Mock<IWinningStreakIdentifier> _winningStreakIdentifierMock;

        [Fact]
        public void ItShouldThrowDiceAndIdentifyHighestStreak()
        {
            const int diceThrowCount = 2;
            var throwResults = new byte[] {1, 2};

            _diceMock.SetupSequence(x => x.Throw())
                .Returns(throwResults.First())
                .Returns(throwResults.Last());

            _sut.Handle(new ThrowDice
            {
                NumberOfTimes = diceThrowCount
            });

            _diceMock.Verify(x => x.Throw(), Times.Exactly(diceThrowCount));
            _winningStreakIdentifierMock.Verify(x => x.Calculate(throwResults), Times.Once);
        }
    }
}