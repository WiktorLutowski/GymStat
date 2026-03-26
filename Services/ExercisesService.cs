using GymStat.Models;
using System.IO;
using System.Text.Json;

namespace GymStat.Services
{
    public class ExercisesService
    {
        private const string FILE_PATH = "exercises.json";

        public ExercisesService()
        {
            if (!File.Exists(FILE_PATH))
                File.Create(FILE_PATH).Close();
        }

        public async Task<List<Exercise>> LoadAllExercisesAsync()
        {
            using FileStream fileStream = new(FILE_PATH, FileMode.Open, FileAccess.Read);

            // If the file is empty, return an empty list to avoid deserialization errors
            if (fileStream.Length == 0) return [];

            return await JsonSerializer.DeserializeAsync<List<Exercise>>(fileStream) ?? [];
        }

        public async Task SaveAllExercisesAsync(List<Exercise> exercises)
        {
            using FileStream fileStream = new(FILE_PATH, FileMode.Create, FileAccess.Write);

            await JsonSerializer.SerializeAsync(fileStream, exercises);
        }
    }
}
