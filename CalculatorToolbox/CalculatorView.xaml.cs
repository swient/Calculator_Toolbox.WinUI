using Microsoft.UI.Xaml.Controls;
using CalculatorToolbox.ViewModels;

namespace CalculatorToolbox
{
    public sealed partial class CalculatorView : UserControl
    {
        public CalculatorView()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
        }
    }
}
