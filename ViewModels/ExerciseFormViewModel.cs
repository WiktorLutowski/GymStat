using GymStat.Commands;
using GymStat.Models;
using GymStat.Services;
using GymStat.Stores;
using System.Collections.ObjectModel;
using System.Windows;

namespace GymStat.ViewModels
{
    public class ExerciseFormViewModel : ViewModelBase
    {
        public ObservableCollection<Exercise> Exercises { get; set; }
        public ObservableCollection<Set> Sets { get; set; }
        public Exercise? SelectedExercise { get { return selectedExercise; } set { selectedExercise = value; OnPropertyChanged(); } }
        
        private Exercise? selectedExercise;

        public RelayCommand AddSetCommand { get; set; }
        public RelayCommand AddResultCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand RemoveSetCommand { get; set; }

        private readonly ExercisesService exercisesService;
        private readonly ExerciseResultsService exerciseResultsService;
        private readonly NavigationStore navigationStore;
        private readonly Task initTask;
        private readonly DateOnly exerciseDate;
        private ExerciseResult? originalResult;

        public ExerciseFormViewModel(NavigationStore navigationStore, DateOnly exerciseDate)
        {
            exercisesService = new();
            exerciseResultsService = new();
            this.navigationStore = navigationStore;
            this.exerciseDate = exerciseDate;

            Exercises = [];
            Sets = [];
            Sets.Add(new(0, 0));

            AddSetCommand = new(x => { if (Sets.Count < 10) Sets.Add(new(0, 0)); });
            BackCommand = new(x => navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, exerciseDate));
            AddResultCommand = new(AddResult);
            RemoveSetCommand = new(x => { if (x != null && x is int i) Sets.RemoveAt(i - 1); });

            initTask = InitializeExercisesAsync();
        }

        public ExerciseFormViewModel(NavigationStore navigationStore, ExerciseResult result) : this(navigationStore, result.Date)
        {
            _ = SetupEditModeAsync(result);
        }

        private async Task InitializeExercisesAsync()
        {
            List<Exercise> exercises = await exercisesService.LoadAllExercisesAsync();

            foreach (var exercise in exercises)
                Exercises.Add(exercise);
        }

        private async Task SetupEditModeAsync(ExerciseResult result)
        {
            await initTask;

            originalResult = result;

            SelectedExercise = Exercises.FirstOrDefault(e => e.ExerciseName == result.Exercise.ExerciseName);

            Sets.Clear();
            foreach (var set in result.Sets)
                Sets.Add(set);
        }

        private async void AddResult(object? obj)
        {
            if (SelectedExercise == null)
            {
                MessageBox.Show("Wybierz ćwiczenie", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Sets.Count == 0)
            {
                MessageBox.Show("Podaj przynajmniej jedną serię", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ExerciseResult result = new(SelectedExercise, exerciseDate)
            {
                Sets = Sets.ToList()
            };

            List<ExerciseResult> allResults = await exerciseResultsService.LoadAllResultsAsync();

            // Edit mode
            if (originalResult != null)
            {
                int index = allResults.FindIndex(r => r.Id == originalResult.Id);

                allResults[index] = result;
            }
            else
                allResults.Add(result);

            await exerciseResultsService.SaveAllResultsAsync(allResults);

            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, exerciseDate);
        }
    }
}
