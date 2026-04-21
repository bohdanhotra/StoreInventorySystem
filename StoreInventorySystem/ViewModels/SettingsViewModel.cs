using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StoreInventorySystem.Commands;
using StoreInventorySystem.Models;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private AppSettings _settings;

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set { _selectedTheme = value; OnPropertyChanged(); }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public SettingsViewModel()
        {
            _settings = SettingsService.Load();
            SelectedLanguage = _settings.Language;
            SelectedTheme    = _settings.Theme;

            SaveCommand = new RelayCommand(_ =>
            {
                _settings.Language = SelectedLanguage;
                _settings.Theme    = SelectedTheme;

                // Застосовуємо тему та мову одразу
                App.ApplyTheme(SelectedTheme);
                App.ApplyLanguage(SelectedLanguage);

                // Зберігаємо у файл
                SettingsService.Save(_settings);

                StatusMessage = "Налаштування збережено!";
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
