using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using StoreInventorySystem.Commands;
using StoreInventorySystem.Models;
using StoreInventorySystem.Services;

namespace StoreInventorySystem.ViewModels
{
    /// <summary>
    /// ViewModel сторінки налаштувань програми.
    /// Дозволяє змінити мову інтерфейсу та тему оформлення
    /// із збереженням у файл settings.json.
    /// </summary>
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private AppSettings _settings;
        private string _selectedLanguage;
        private string _selectedTheme;
        private string _statusMessage;

        /// <summary>Вибраний код мови: "uk" або "en".</summary>
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        /// <summary>Вибрана тема: "Light" або "Dark".</summary>
        public string SelectedTheme
        {
            get => _selectedTheme;
            set { _selectedTheme = value; OnPropertyChanged(); }
        }

        /// <summary>Статусне повідомлення після збереження.</summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        /// <summary>Команда збереження налаштувань та їх негайного застосування.</summary>
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

                App.ApplyTheme(SelectedTheme);
                App.ApplyLanguage(SelectedLanguage);

                SettingsService.Save(_settings);
                StatusMessage = "Налаштування збережено!";
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
