namespace GuyHarwood.DieThrow.Domain.Core
{
    public interface IHandler<in TCommand, out TReturn> where TReturn : new()
    {
        TReturn Handle(TCommand command);
    }
}