using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CalculatorToolbox.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private readonly CalculatorEngine engine = new();
        private string _expression = "";
        private string _userInput = "0";

        public string Expression
        {
            get => _expression;
            set { _expression = value; OnPropertyChanged(); }
        }

        public string UserInput
        {
            get => _userInput;
            set { _userInput = value; OnPropertyChanged(); }
        }

        public ICommand NumberCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand ClearEntryCommand { get; }
        public ICommand PercentCommand { get; }
        public ICommand ToggleSignCommand { get; }
        public ICommand ReciprocalCommand { get; }
        public ICommand SquareRootCommand { get; }
        public ICommand DecimalCommand { get; }

        public CalculatorViewModel()
        {
            NumberCommand = new RelayCommand(param => InputNumber(param?.ToString() ?? "0"));
            OperatorCommand = new RelayCommand(param => InputOperator(param?.ToString() ?? "+"));
            EqualsCommand = new RelayCommand(_ => CalculateResult());
            BackspaceCommand = new RelayCommand(_ => Backspace());
            ClearCommand = new RelayCommand(_ => Clear());
            ClearEntryCommand = new RelayCommand(_ => ClearEntry());
            PercentCommand = new RelayCommand(_ => Percent());
            ToggleSignCommand = new RelayCommand(_ => ToggleSign());
            ReciprocalCommand = new RelayCommand(_ => Reciprocal());
            SquareRootCommand = new RelayCommand(_ => SquareRoot());
            DecimalCommand = new RelayCommand(_ => InputDecimal());
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName!));
        public event PropertyChangedEventHandler? PropertyChanged;

        private void UpdateCurrentValue()
        {
            if (decimal.TryParse(UserInput, out var v))
                engine.CurrentValue = v;
            else
                ShowError("輸入格式錯誤");
        }

        private void UpdateDisplay()
        {
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(UserInput));
        }

        private void ShowError(string error)
        {
            Expression = _expression;
            UserInput = "0";
            engine.CurrentValue = 0;
            UserInput = error;
        }

        private void InputNumber(string num)
        {
            if (UserInput == "0")
                UserInput = num;
            else
                UserInput += num;
            UpdateCurrentValue();
            UpdateDisplay();
        }

        private void InputOperator(string op)
        {
            try
            {
                if (UserInput == "0" && !string.IsNullOrEmpty(Expression) &&
                    (Expression.EndsWith(" + ") || Expression.EndsWith(" − ") || Expression.EndsWith(" × ") || Expression.EndsWith(" ÷ ") || Expression.EndsWith(" ^ ")))
                {
                    Expression = string.Concat(Expression[..^2], op, " ");
                    engine.LastOperator = op;
                    UpdateDisplay();
                    return;
                }
                else if (!string.IsNullOrEmpty(engine.LastOperator))
                {
                    Expression = engine.Calculate() + " " + op + " ";
                }
                else
                {
                    Expression = engine.CurrentValue + " " + op + " ";
                }
                UserInput = "0";
                engine.LastValue = engine.CurrentValue;
                engine.LastOperator = op;
                UpdateCurrentValue();
                UpdateDisplay();
            }
            catch (CalculatorException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception)
            {
                ShowError("錯誤");
            }
        }

        private void CalculateResult()
        {
            try
            {
                if (!string.IsNullOrEmpty(engine.LastOperator))
                {
                    Expression = engine.LastValue + " " + engine.LastOperator + " " + engine.CurrentValue + " =";
                    UserInput = engine.Calculate().ToString();
                    engine.LastOperator = "";
                    UpdateCurrentValue();
                }
                else
                {
                    Expression = engine.CurrentValue + " =";
                }
                UpdateDisplay();
            }
            catch (CalculatorException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception)
            {
                ShowError("錯誤");
            }
        }

        private void Backspace()
        {
            if (UserInput.Length > 1)
                UserInput = UserInput[..^1];
            else
                UserInput = "0";
            UpdateCurrentValue();
            UpdateDisplay();
        }

        private void Clear()
        {
            Expression = "";
            UserInput = "0";
            engine.LastOperator = "";
            engine.LastValue = 0;
            UpdateCurrentValue();
            UpdateDisplay();
        }

        private void ClearEntry()
        {
            UserInput = "0";
            UpdateCurrentValue();
            UpdateDisplay();
        }

        private void Percent()
        {
            try
            {
                UserInput = engine.Percent().ToString();
                UpdateCurrentValue();
                UpdateDisplay();
            }
            catch (Exception)
            {
                ShowError("錯誤");
            }
        }

        private void ToggleSign()
        {
            try
            {
                UserInput = engine.ToggleSign().ToString();
                UpdateCurrentValue();
                UpdateDisplay();
            }
            catch (Exception)
            {
                ShowError("錯誤");
            }
        }

        private void Reciprocal()
        {
            try
            {
                UserInput = engine.Reciprocal().ToString();
                UpdateCurrentValue();
                UpdateDisplay();
            }
            catch (CalculatorException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception)
            {
                ShowError("錯誤");
            }
        }

        private void SquareRoot()
        {
            try
            {
                UserInput = engine.SquareRoot().ToString();
                UpdateCurrentValue();
                UpdateDisplay();
            }
            catch (CalculatorException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception)
            {
                ShowError("錯誤");
            }
        }

        private void InputDecimal()
        {
            if (!UserInput.Contains('.'))
                UserInput += ".";
            UpdateCurrentValue();
            UpdateDisplay();
        }
    }
}