namespace GymStat.Models
{
    public class ExerciseResult(Exercise exercise, DateOnly date)
    {
        public Exercise Exercise { get; set; } = exercise;
        public List<Set> Sets { get; set; } = [];
        public DateOnly Date { get; set; } = date;
    }
}
