using Xunit;

namespace GuyHarwood.DieThrow.Domain.Tests
{
    public class DescribeWinningStreakIdentifier
    {
        public DescribeWinningStreakIdentifier()
        {
            _sut = new WinningStreakIdentifier();
        }

        private readonly WinningStreakIdentifier _sut;

        [Theory]
        [InlineData(new byte[] {6}, 0)]
        [InlineData(new byte[] {6, 6}, 2)]
        public void TheSmallestPossibleStreakIsTwoSixes(byte[] throws, byte expectedOutput)
        {
            var actual = _sut.Calculate(throws);

            Assert.Equal(expectedOutput, actual);
        }

        [Theory]
        [InlineData(new byte[] {1, 1}, 0)]
        [InlineData(new byte[] {2, 2}, 0)]
        [InlineData(new byte[] {3, 3}, 0)]
        [InlineData(new byte[] {4, 4}, 0)]
        [InlineData(new byte[] {5, 5}, 0)]
        [InlineData(new byte[] {6, 6}, 2)]
        public void ItShouldOnlyIdentifyStreaksOfSixes(byte[] throws, byte expectedOutput)
        {
            var actual = _sut.Calculate(throws);

            Assert.Equal(expectedOutput, actual);
        }

        [Fact]
        public void CallingMillionThrows()
        {
            var dice = new Dice();
            var oneMillion = 1000000;
            for (var i = 0; i < oneMillion; i++)
            {
                var result = dice.Throw();
                Assert.InRange(result, 1, 6);
            }
        }

        [Fact]
        public void ItHandlesDuplicateStreaks()
        {
            var input = new byte[] {1, 6, 6, 6, 1, 6, 6, 6, 1};
            const byte expected = 3;

            var actual = _sut.Calculate(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ItIdentifiesBiggestStreak()
        {
            var input = new byte[] {1, 6, 6, 6, 1, 6, 6, 6, 6, 1, 6, 6, 6, 6, 6, 1};
            const byte expected = 5;

            var actual = _sut.Calculate(input);

            Assert.Equal(expected, actual);
        }
    }
}