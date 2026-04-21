using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using StoreInventorySystem.Commands;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        // Властивість для повідомлень про помилку
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            GoToRegisterCommand = new RelayCommand(ExecuteGoToRegister);
        }

        // Обробка пароля через parameter (бо PasswordBox не підтримує безпечний DataBinding напряму)
        private void ExecuteLogin(object parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            string password = passwordBox?.Password;

            if (AuthService.Login(Username, password))
            {
                // Логіка переходу на HomePage
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow?.NavigateToHome();
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