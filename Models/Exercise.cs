namespace GymStat.Models
{
    /// <summary>
    /// Represents an exercise with a display name and an associated image filename stored in the app resources.
    /// </summary>
    public class Exercise(string exerciseName, string exerciseImageFileName)
    {
        public string ExerciseName { get; set; } = exerciseName;
        public string ExerciseImageFileName { get; set; } = exerciseImageFileName;

        public override string ToString()
        {
            return ExerciseName;
        }
    }
}
