namespace GymStat.Models
{
    /// <summary>
    /// Represents a single set in an exercise result: repetitions and applied weight.
    /// </summary>
    public class Set(int repetitions, float weight)
    {
        public float Weight { get; set; } = weight;
        public int Repetitions { get; set; } = repetitions;
    }
}
