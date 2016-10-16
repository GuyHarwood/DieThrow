using System;

namespace GuyHarwood.DieThrow.Domain
{
    public interface IDice
    {
        byte Throw();
    }

    public class Dice : IDice
    {
        private readonly Random _random;

        public Dice()
        {
            _random = new Random();
        }

        public byte Throw()
        {
            return (byte)_random.Next(1, 7);
        }
    }
}