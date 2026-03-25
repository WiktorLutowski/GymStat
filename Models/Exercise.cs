namespace GymStat.Models
{
    public class Exercise(string exerciseImageFileName, string exerciseName)
    {
        public string ExerciseName { get; set; } = exerciseName;
        public string ExerciseImageFileName { get; set; } = exerciseImageFileName;
    }
}
