namespace GymStat.Models
{
    /// <summary>
    /// Represents a completed exercise on a given date, containing one or more sets.
    /// </summary>
    public class ExerciseResult(Exercise exercise, DateOnly date)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Exercise Exercise { get; set; } = exercise;
        public List<Set> Sets { get; set; } = [];
        public DateOnly Date { get; set; } = date;
    }
}
