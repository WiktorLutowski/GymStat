using GymStat.Commands;
using GymStat.Models;
using GymStat.Services;
using GymStat.Stores;
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
        private readonly NavigationStore navigationStore;
        private List<ExerciseResult> allExerciseResults;

        public HomeViewModel(NavigationStore navigationStore)
        {
            exerciseResultsService = new ExerciseResultsService();
            this.navigationStore = navigationStore;
            allExerciseResults = [];

            CurrentSelectedDate = DateOnly.FromDateTime(DateTime.Now);

            ResultsFromSelectedDate = [];

            NextDateCommand = new RelayCommand(x => ChangeDateByDays(1));
            PreviousDateCommand = new RelayCommand(x => ChangeDateByDays(-1));

            RemoveExerciseCommand = new RelayCommand(RemoveExercise);
            EditExerciseCommand = new RelayCommand(EditExercise);
            AddExerciseCommand = new RelayCommand(AddExercise);

            _ = InitializeExerciseResultsAsync();
        }

        public HomeViewModel(NavigationStore navigationStore, DateOnly date) : this(navigationStore) => CurrentSelectedDate = date;

        private async Task InitializeExerciseResultsAsync()
        {
            allExerciseResults = await exerciseResultsService.LoadAllResultsAsync();

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
            if (obj is ExerciseResult result)
                navigationStore.CurrentViewModel = new ExerciseFormViewModel(navigationStore, result);
        }

        private void AddExercise(object? obj)
        {
            navigationStore.CurrentViewModel = new ExerciseFormViewModel(navigationStore, CurrentSelectedDate);
        }

        private async void RemoveExercise(object? obj)
        {
            if(obj is ExerciseResult exerciseResult)
            {
                MessageBoxResult result = MessageBox.Show($"Czy napewno chcesz usunąc ćwiczenie: {exerciseResult.Exercise.ExerciseName}?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result != MessageBoxResult.Yes)
                    return;

                allExerciseResults.RemoveAll(r => r.Id == exerciseResult.Id);

                await exerciseResultsService.SaveAllResultsAsync(allExerciseResults);

                RefreshResults();
            }
        }
    }
}
