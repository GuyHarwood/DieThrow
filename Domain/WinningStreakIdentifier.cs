namespace GuyHarwood.DieThrow.Domain
{
    public class WinningStreakIdentifier : IWinningStreakIdentifier
    {
        public int Calculate(byte[] throws)
        {
            var biggestStreak = 0;
            var currentStreak = 0;

            foreach (var t in throws)
            {
                if (t == 6)
                {
                    currentStreak++;
                }
                else
                {
                    currentStreak = 0;
                }

                if (currentStreak > biggestStreak)
                {
                    biggestStreak = currentStreak;
                }
            }
                
            return biggestStreak > 1 ? biggestStreak : 0;
        }
    }
}