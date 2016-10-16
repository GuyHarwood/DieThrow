using System.ComponentModel.DataAnnotations;
using GuyHarwood.DieThrow.Domain.Core;

namespace GuyHarwood.DieThrow.Domain
{
    public class ThrowDice : Command
    {
        [Range(2, 1000000)]
        public int NumberOfTimes { get; set; }
    }
}