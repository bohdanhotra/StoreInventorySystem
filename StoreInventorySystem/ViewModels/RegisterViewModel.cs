using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoreInventorySystem.Commands;
using StoreInventorySystem.Services;
using StoreInventorySystem.Views;

namespace StoreInventorySystem.ViewModels
{
    /// <summary>
    /// ViewModel сторінки реєстрації. Перевіряє введені дані,
    /// створює новий акаунт і показує результат операції.
    /// </summary>
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _errorMessage;
        private string _successMessage;

        /// <summary>Логін нового користувача.</summary>
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        /// <summary>Повідомлення про помилку валідації або реєстрації.</summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        /// <summary>Повідомлення про успішну реєстрацію.</summary>
        public string SuccessMessage
        {
            get => _successMessage;
            set { _successMessage = value; OnPropertyChanged(); }
        }

        /// <summary>Команда реєстрації нового акаунту.</summary>
        public ICommand RegisterCommand { get; }

        /// <summary>Команда переходу назад на сторінку входу.</summary>
        public ICommand GoToLoginCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
            GoToLoginCommand = new RelayCommand(ExecuteGoToLogin);
        }

        private void ExecuteRegister(object parameter)
        {
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            var passwordBox = parameter as PasswordBox;
            string password = passwordBox?.Password;

            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Пароль не може бути порожнім!";
                return;
            }

            if (password.Length < 3)
            {
                ErrorMessage = "Пароль надто короткий (мінімум 3 символи)!";
                return;
            }

            if (AuthService.Register(Username, password))
            {
                SuccessMessage = "Реєстрація успішна! Ви можете увійти.";
                passwordBox.Clear();
                Username = string.Empty;
            }
            else
            {
                ErrorMessage = "Користувач з таким логіном вже існує!";
            }
        }

        private bool CanExecuteRegister(object parameter) => !string.IsNullOrWhiteSpace(Username);

        private void ExecuteGoToLogin(object parameter)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new LoginPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}