namespace GymStat.Models
{
    public class Set(int repetitions, float weight)
    {
        public float Weight { get; set; } = weight;
        public int Repetitions { get; set; } = repetitions;
    }
}
