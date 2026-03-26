namespace GymStat.Models
{
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
