namespace GuyHarwood.DieThrow.Domain
{
    public class HighestStreak
    {
        public int Value { get; set; }

        public override bool Equals(object obj)
        {
            var highestStreak = obj as HighestStreak;

            return highestStreak != null && string.Equals(Value, highestStreak.Value);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrWhiteSpace(Value.ToString()))
                return 0;

            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}