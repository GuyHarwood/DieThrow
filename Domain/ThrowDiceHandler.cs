using System.Collections.Generic;
using GuyHarwood.DieThrow.Domain.Core;

namespace GuyHarwood.DieThrow.Domain
{
    public class ThrowDiceHandler : IHandler<ThrowDice, HighestStreak>
    {
        private readonly IDice _dice;
        private readonly IWinningStreakIdentifier _winningStreakIdentifier;

        public ThrowDiceHandler(IDice dice, IWinningStreakIdentifier winningStreakIdentifier)
        {
            _dice = dice;
            _winningStreakIdentifier = winningStreakIdentifier;
        }

        public HighestStreak Handle(ThrowDice throwDiceCommand)
        {
            var results = new List<byte>(throwDiceCommand.NumberOfTimes);

            for (int i = 0; i < throwDiceCommand.NumberOfTimes; i++)
            {
                var result = _dice.Throw();
                results.Add(result);
            }

            var streak = _winningStreakIdentifier.Calculate(results.ToArray());
            return new HighestStreak()
            {
                Value = streak
            };
        }
    }
}
