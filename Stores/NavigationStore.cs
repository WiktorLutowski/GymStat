using GymStat.ViewModels;

namespace GymStat.Stores
{
    public partial class NavigationStore
    {
        private ViewModelBase _currentViewModel = default!;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action? CurrentViewModelChanged;

        private void OnCurrentViewModelChanged() => CurrentViewModelChanged?.Invoke();
    }
}
