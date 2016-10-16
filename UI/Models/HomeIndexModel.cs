
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class HomeIndexModel
    {
        [Range(2,1000000,ErrorMessage = "Throws must be between 2 and 1 million")]
        public int ThrowCount { get; set; }
        public string LargestStreak { get; set; }
    }
}