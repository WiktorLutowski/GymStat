using GymStat.Commands;
using GymStat.Models;
using GymStat.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace GymStat.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public DateOnly CurrentSelectedDate { get; set; }
        public ObservableCollection<ExerciseResult> ResultsFromSelectedDate { get; set; }

        public RelayCommand NextDateCommand { get; set; }
        public RelayCommand PreviousDateCommand { get; set; }
        public RelayCommand RemoveExerciseCommand { get; set; }
        public RelayCommand EditExerciseCommand { get; set; }
        public RelayCommand AddExerciseCommand { get; set; }

        private readonly ExerciseResultsService exerciseResultsService;
        private List<ExerciseResult> allExerciseResults;

        public HomeViewModel()
        {
            exerciseResultsService = new ExerciseResultsService();
            allExerciseResults = [];

            CurrentSelectedDate = DateOnly.FromDateTime(DateTime.Now);

            ResultsFromSelectedDate = [];

            NextDateCommand = new RelayCommand(x => ChangeDateByDays(1));
            PreviousDateCommand = new RelayCommand(x => ChangeDateByDays(-1));

            RemoveExerciseCommand = new RelayCommand(RemoveExercise);
            EditExerciseCommand = new RelayCommand(EditExercise);
            AddExerciseCommand = new RelayCommand(AddExercise);

            _ = InitializeExercisesAsync();
        }

        private async Task InitializeExercisesAsync()
        {
            allExerciseResults = await exerciseResultsService.LoadAllResultsAsync();

            allExerciseResults.Add(new(new("bench press", "bench-press.jpg"), DateOnly.FromDateTime(DateTime.Now)));

            RefreshResults();
        }

        private void RefreshResults()
        {
            ResultsFromSelectedDate.Clear();

            List<ExerciseResult> exerciseResultsFromSelectedDate = allExerciseResults.Where(r => r.Date == CurrentSelectedDate).ToList();

            foreach (var item in exerciseResultsFromSelectedDate)
                ResultsFromSelectedDate.Add(item);
        }

        private void ChangeDateByDays(int days)
        {
            CurrentSelectedDate = CurrentSelectedDate.AddDays(days);
            RefreshResults();
            OnPropertyChanged(nameof(CurrentSelectedDate));
        }

        private void EditExercise(object? obj)
        {
            throw new NotImplementedException();
        }

        private void AddExercise(object? obj)
        {
            throw new NotImplementedException();
        }

        private async void RemoveExercise(object? obj)
        {
            if(obj is Exercise exercise)
            {
                MessageBoxResult result = MessageBox.Show($"Czy napewno chcesz usunąc ćwiczenie: {exercise.ExerciseName}?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result != MessageBoxResult.Yes)
                    return;

                allExerciseResults.RemoveAll(r => r.Exercise == exercise && r.Date == CurrentSelectedDate);

                await exerciseResultsService.SaveAllResultsAsync(allExerciseResults);

                RefreshResults();
            }
        }
    }
}
