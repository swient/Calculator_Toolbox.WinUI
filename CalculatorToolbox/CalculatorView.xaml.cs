using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace CalculatorToolbox
{
    public sealed partial class CalculatorView : UserControl
    {
        private readonly CalculatorEngine engine = new();
        private string expression = "";
        private string userInput = "0";

        public CalculatorView()
        {
            InitializeComponent();
        }

        private void UpdateDisplay()
        {
            txtExpression.Text = expression;
            txtDisplay.Text = userInput;
        }

        private void ShowError(string error)
        {
            expression = txtExpression.Text;
            userInput = "0";
            engine.CurrentValue = 0;
            txtDisplay.Text = error;
        }

        private void UpdateCurrentValue()
        {
            if (decimal.TryParse(userInput, out var v))
            {
                engine.CurrentValue = v;
            }
            else
            {
                ShowError("輸入格式錯誤");
            }
        }

        private void InputNumber(string num)
        {
            if (userInput == "0")
            {
                userInput = num;
            }
            else
            {
                userInput += num;
            }

            UpdateCurrentValue();
            UpdateDisplay();
        }

        private void InputOperator(string op)
        {
            try
            {
                // 替換運算符號
                if (userInput == "0" && !string.IsNullOrEmpty(expression) &&
                    (expression.EndsWith(" + ") || expression.EndsWith(" − ") || expression.EndsWith(" × ") || expression.EndsWith(" ÷ ") || expression.EndsWith(" ^ ")))
                {
                    expression = string.Concat(expression[..^2], op, " ");
                    engine.LastOperator = op;
                    UpdateDisplay();
                    return;
                }
                // 連續運算
                else if (!string.IsNullOrEmpty(engine.LastOperator))
                {
                    expression = engine.Calculate() + " " + op + " ";
                }
                // 首次運算
                else
                {
                    expression = engine.CurrentValue + " " + op + " ";
                }
                userInput = "0";
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

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(engine.LastOperator))
                {
                    expression = engine.LastValue + " " + engine.LastOperator + " " + engine.CurrentValue + " =";
                    userInput = engine.Calculate().ToString();
                    engine.LastOperator = "";
                    UpdateCurrentValue();
                }
                else
                {
                    expression = engine.CurrentValue + " =";
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

        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (userInput.Length > 1)
            {
                userInput = userInput[..^1];
            }
            else
            {
                userInput = "0";
            }
            UpdateCurrentValue();
            UpdateDisplay();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            expression = "";
            userInput = "0";
            engine.LastOperator = "";
            engine.LastValue = 0;
            UpdateCurrentValue();
            UpdateDisplay();
        }

        private void BtnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            userInput = "0";
            UpdateCurrentValue();
            UpdateDisplay();
        }

        private void BtnPercent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userInput = engine.Percent().ToString();
                UpdateCurrentValue();
                UpdateDisplay();
            }
            catch (Exception)
            {
                ShowError("錯誤");
            }
        }

        private void BtnToggleSign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userInput = engine.ToggleSign().ToString();
                UpdateCurrentValue();
                UpdateDisplay();
            }
            catch (Exception)
            {
                ShowError("錯誤");
            }
        }

        private void BtnReciprocal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userInput = engine.Reciprocal().ToString();
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

        private void BtnSquareRoot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userInput = engine.SquareRoot().ToString();
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

        private void BtnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!userInput.Contains('.'))
            {
                userInput += ".";
            }
            UpdateCurrentValue();
            UpdateDisplay();
        }

        // ===== 數字輸入 =====
        private void BtnZero_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("0");
        }

        private void BtnOne_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("1");
        }

        private void BtnTwo_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("2");
        }

        private void BtnThree_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("3");
        }

        private void BtnFour_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("4");
        }

        private void BtnFive_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("5");
        }

        private void BtnSix_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("6");
        }

        private void BtnSeven_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("7");
        }

        private void BtnEight_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("8");
        }

        private void BtnNine_Click(object sender, RoutedEventArgs e)
        {
            InputNumber("9");
        }

        // ===== 運算符號 =====
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            InputOperator("+");
        }

        private void BtnSubtract_Click(object sender, RoutedEventArgs e)
        {
            InputOperator("−");
        }

        private void BtnMultiply_Click(object sender, RoutedEventArgs e)
        {
            InputOperator("×");
        }

        private void BtnDivide_Click(object sender, RoutedEventArgs e)
        {
            InputOperator("÷");
        }

        private void BtnPower_Click(object sender, RoutedEventArgs e)
        {
            InputOperator("^");
        }
    }
}
