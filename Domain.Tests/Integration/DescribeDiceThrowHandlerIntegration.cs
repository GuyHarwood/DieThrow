using Xunit;

namespace GuyHarwood.DieThrow.Domain.Tests.Integration
{
    public class DescribeDiceThrowHandlerIntegration
    {
        [Fact]
        public void ItCanSupportAMillionThrows()
        {
            var sut = new ThrowDiceHandler(new Dice(), new WinningStreakIdentifier());
            var oneMillion = 1000000;

            var result = sut.Handle(new ThrowDice()
            {
                NumberOfTimes = oneMillion
            });

            Assert.NotNull(result);
            Assert.InRange(result.Value, 2, oneMillion);
        }
    }
}
