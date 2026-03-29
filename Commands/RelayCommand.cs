using System.Windows.Input;

namespace GymStat.Commands
{
    public class RelayCommand(Action<object?> execute, Func<object?, bool> canExecute = null!) : ICommand
    {
        // Delegate executed when the command runs
        private readonly Action<object?> execute = execute;

        // Optional predicate that determines whether the command can execute
        private readonly Func<object?, bool> canExecute = canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
