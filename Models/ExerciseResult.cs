namespace GymStat.Models
{
    public class ExerciseResult(Exercise exercise, int repetitions, float weight, DateOnly date)
    {
        public Exercise Exercise { get; set; } = exercise;
        public float Weight { get; set; } = weight;
        public int Repetitions { get; set; } = repetitions;
        public DateOnly Date { get; set; } = date;
    }
}
