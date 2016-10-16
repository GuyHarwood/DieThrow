namespace GuyHarwood.DieThrow.Domain
{
    public interface IWinningStreakIdentifier
    {
        int Calculate(byte[] throwResults);
    }
}