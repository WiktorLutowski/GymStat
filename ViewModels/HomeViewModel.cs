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
        /// <summary>
        /// Currently selected date in the home view. Changing this will update the displayed results.
        /// </summary>
        public DateOnly CurrentSelectedDate { get; set; }

        /// <summary>
        /// Collection of exercise results that belong to the selected date. Bound to the UI.
        /// </summary>
        public ObservableCollection<ExerciseResult> ResultsFromSelectedDate { get; set; }

        public RelayCommand NextDateCommand { get; set; }
        public RelayCommand PreviousDateCommand { get; set; }
        public RelayCommand RemoveExerciseCommand { get; set; }
        public RelayCommand EditExerciseCommand { get; set; }
        public RelayCommand AddExerciseCommand { get; set; }

        // Services and stores
        private readonly ExerciseResultsService exerciseResultsService;
        private readonly NavigationStore navigationStore;

        // All loaded results. Used to filter and present results for the selected date.
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
            if (obj is ExerciseResult exerciseResult)
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
