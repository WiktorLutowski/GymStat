using GymStat.Stores;
using GymStat.ViewModels;
using System.Windows;

namespace GymStat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore navigationStore;

        private readonly MainViewModel mainViewModel;

        public App()
        {
            navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);

            mainViewModel = new(navigationStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new()
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }

}
