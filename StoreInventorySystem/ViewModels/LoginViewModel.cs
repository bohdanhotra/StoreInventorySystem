using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using StoreInventorySystem.Commands;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    /// <summary>
    /// ViewModel сторінки входу. Обробляє авторизацію користувача
    /// та навігацію на панель керування або сторінку реєстрації.
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _errorMessage;

        /// <summary>Логін, введений користувачем.</summary>
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        /// <summary>Повідомлення про помилку авторизації.</summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        /// <summary>Команда входу в систему.</summary>
        public ICommand LoginCommand { get; }

        /// <summary>Команда переходу на сторінку реєстрації.</summary>
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            GoToRegisterCommand = new RelayCommand(ExecuteGoToRegister);
        }

        private void ExecuteLogin(object parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            string password = passwordBox?.Password;

            if (AuthService.Login(Username, password))
            {
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.NavigateToDashboard();
            }
            else
            {
                ErrorMessage = "Невірний логін або пароль!";
            }
        }

        private bool CanExecuteLogin(object parameter) => !string.IsNullOrWhiteSpace(Username);

        private void ExecuteGoToRegister(object parameter)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new Views.RegisterPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}