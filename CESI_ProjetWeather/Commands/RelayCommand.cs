using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CESI_ProjetWeather.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Func<object, Task> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var canExecute = _canExecute == null || _canExecute(parameter);
            System.Diagnostics.Debug.WriteLine($"CanExecute was called and returned {canExecute}");
            return canExecute;
        }

        public async void Execute(object parameter)
        {
            System.Diagnostics.Debug.WriteLine("Execute was called");
            await _execute(parameter);
        }
    }

}
