using GymStat.Commands;
using GymStat.Models;
using GymStat.Services;
using System.Collections.ObjectModel;

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

            NextDateCommand = new RelayCommand((x) =>
            {
                CurrentSelectedDate = CurrentSelectedDate.AddDays(1);
                ReloadResults();
                OnPropertyChanged(nameof(CurrentSelectedDate));
            });

            PreviousDateCommand = new RelayCommand((x) =>
            {
                CurrentSelectedDate = CurrentSelectedDate.AddDays(-1);
                ReloadResults();
                OnPropertyChanged(nameof(CurrentSelectedDate));
            });

            RemoveExerciseCommand = new RelayCommand(RemoveExercise);
            EditExerciseCommand = new RelayCommand(EditExercise);
            AddExerciseCommand = new RelayCommand(AddExercise);


            _ = InitializeExercisesAsync();
        }

        public void ReloadResults()
        {
            ResultsFromSelectedDate.Clear();

            List<ExerciseResult> exerciseResultsFromSelectedDate = allExerciseResults.Where(r => r.Date == CurrentSelectedDate).ToList();

            foreach (var item in exerciseResultsFromSelectedDate)
                ResultsFromSelectedDate.Add(item);
        }

        private async Task InitializeExercisesAsync()
        {
            allExerciseResults = await exerciseResultsService.LoadAllResultsAsync();

            allExerciseResults.Add(new(new("bench press", "bench-press.jpg"), DateOnly.FromDateTime(DateTime.Now)));

            ReloadResults();
        }

        private void EditExercise(object? obj)
        {
            throw new NotImplementedException();
        }

        private void AddExercise(object? obj)
        {
            throw new NotImplementedException();
        }

        private void RemoveExercise(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
