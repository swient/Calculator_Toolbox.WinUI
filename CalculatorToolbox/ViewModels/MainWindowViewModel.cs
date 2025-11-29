using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CalculatorToolbox.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private bool _isMenuVisible;
        private string _currentPage;
        private string _calculatorBtnBg = "LightGray";
        private string _currencyBtnBg = "WhiteSmoke";
        private string _unitBtnBg = "WhiteSmoke";

        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set { _isMenuVisible = value; OnPropertyChanged(); }
        }

        public string CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(); }
        }

        public string CalculatorBtnBg
        {
            get => _calculatorBtnBg;
            set { _calculatorBtnBg = value; OnPropertyChanged(); }
        }

        public string CurrencyBtnBg
        {
            get => _currencyBtnBg;
            set { _currencyBtnBg = value; OnPropertyChanged(); }
        }

        public string UnitBtnBg
        {
            get => _unitBtnBg;
            set { _unitBtnBg = value; OnPropertyChanged(); }
        }

        public ICommand ToggleMenuCommand { get; }
        public ICommand ShowCalculatorCommand { get; }
        public ICommand ShowCurrencyCommand { get; }
        public ICommand ShowUnitCommand { get; }

        public MainWindowViewModel()
        {
            _currentPage = "Calculator";
            ToggleMenuCommand = new RelayCommand(_ => IsMenuVisible = !IsMenuVisible);
            ShowCalculatorCommand = new RelayCommand(_ =>
            {
                CalculatorBtnBg = "LightGray";
                CurrencyBtnBg = "WhiteSmoke";
                UnitBtnBg = "WhiteSmoke";
                CurrentPage = "Calculator";
                IsMenuVisible = false;
            });
            ShowCurrencyCommand = new RelayCommand(_ =>
            {
                CalculatorBtnBg = "WhiteSmoke";
                CurrencyBtnBg = "LightGray";
                UnitBtnBg = "WhiteSmoke";
                CurrentPage = "Currency";
                IsMenuVisible = false;
            });
            ShowUnitCommand = new RelayCommand(_ =>
            {
                CalculatorBtnBg = "WhiteSmoke";
                CurrencyBtnBg = "WhiteSmoke";
                UnitBtnBg = "LightGray";
                CurrentPage = "Unit";
                IsMenuVisible = false;
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop!));
    }
}