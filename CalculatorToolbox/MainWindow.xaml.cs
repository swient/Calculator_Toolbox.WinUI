using Microsoft.UI.Xaml;
using CalculatorToolbox.ViewModels;

namespace CalculatorToolbox
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (this.Content is FrameworkElement fe)
            {
                fe.DataContext = new MainWindowViewModel();
            }
        }
    }
}
