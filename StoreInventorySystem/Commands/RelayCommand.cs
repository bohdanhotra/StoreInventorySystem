using System;
using System.Windows.Input;

namespace StoreInventorySystem.Commands
{
    /// <summary>
    /// Універсальна реалізація ICommand для прив'язки команд у MVVM.
    /// Автоматично сповіщає UI про зміну стану через CommandManager.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <param name="execute">Дія, що виконується при спрацюванні команди.</param>
        /// <param name="canExecute">Предикат, що визначає чи доступна команда (необов'язковий).</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>Перевіряє чи можна виконати команду зараз.</summary>
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        /// <summary>Виконує команду з переданим параметром.</summary>
        public void Execute(object parameter) => _execute(parameter);
    }
}