using GymStat.Models;
using System.IO;
using System.Text.Json;

namespace GymStat.Services
{
    public class ExerciseResultsService
    {
        private readonly string FILE_PATH;

        public ExerciseResultsService()
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataFolder, "GymStat");
            FILE_PATH = Path.Combine(folderPath, "exercise_results.json");

            // Ensure the directory and file exist
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!File.Exists(FILE_PATH))
                File.Create(FILE_PATH).Close();
        }

        public async Task<List<ExerciseResult>> LoadAllResultsAsync()
        {
            using FileStream fileStream = new(FILE_PATH, FileMode.Open, FileAccess.Read);

            // If the file is empty, return an empty list to avoid deserialization errors
            if (fileStream.Length == 0) return [];

            return await JsonSerializer.DeserializeAsync<List<ExerciseResult>>(fileStream) ?? [];
        }

        public async Task SaveAllResultsAsync(List<ExerciseResult> results)
        {
            using FileStream fileStream = new(FILE_PATH, FileMode.Create, FileAccess.Write);

            await JsonSerializer.SerializeAsync(fileStream, results);
        }
    }
}
