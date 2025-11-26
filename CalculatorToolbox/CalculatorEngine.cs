using System;

namespace CalculatorToolbox
{
    internal class CalculatorEngine
    {
        public decimal LastValue { get; set; } = 0;
        public string LastOperator { get; set; } = "";
        public decimal CurrentValue { get; set; } = 0;

        public decimal Calculate()
        {
            if (string.IsNullOrEmpty(LastOperator))
                return CurrentValue;

            switch (LastOperator)
            {
                case "+":
                    CurrentValue = LastValue + CurrentValue;
                    break;
                case "−":
                    CurrentValue = LastValue - CurrentValue;
                    break;
                case "×":
                    CurrentValue = LastValue * CurrentValue;
                    break;
                case "÷":
                    if (CurrentValue == 0)
                        throw new CalculatorException("不能除以零");
                    CurrentValue = LastValue / CurrentValue;
                    break;
                case "^":
                    CurrentValue = (decimal)Math.Pow((double)LastValue, (double)CurrentValue);
                    break;
                default:
                    throw new CalculatorException("未知的運算符號: " + LastOperator);
            }
            LastValue = 0;
            return CurrentValue;
        }

        public decimal Percent()
        {
            CurrentValue = CurrentValue / 100.0m;
            return CurrentValue;
        }

        public decimal ToggleSign()
        {
            CurrentValue = -CurrentValue;
            return CurrentValue;
        }

        public decimal Reciprocal()
        {
            if (CurrentValue == 0)
                throw new CalculatorException("不能除以零");
            CurrentValue = 1.0m / CurrentValue;
            return CurrentValue;
        }

        public decimal SquareRoot()
        {
            if (CurrentValue < 0)
                throw new CalculatorException("輸入不可為負數");
            CurrentValue = (decimal)Math.Sqrt((double)CurrentValue);
            return CurrentValue;
        }
    }

    internal class CalculatorException : Exception
    {
        public CalculatorException(string message) : base(message) { }
    }
}
