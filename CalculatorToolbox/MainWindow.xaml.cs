using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace CalculatorToolbox
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = MenuPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void BtnCalculator_Click(object sender, RoutedEventArgs e)
        {
            BtnCalculator.Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray);
            BtnCurrencyConverter.Background = new SolidColorBrush(Microsoft.UI.Colors.WhiteSmoke);
            BtnUnitConverter.Background = new SolidColorBrush(Microsoft.UI.Colors.WhiteSmoke);
            MenuPanel.Visibility = Visibility.Collapsed;
            CalculatorViewHost.Visibility = Visibility.Visible;
        }

        private void BtnCurrencyConverter_Click(object sender, RoutedEventArgs e)
        {
            BtnCalculator.Background = new SolidColorBrush(Microsoft.UI.Colors.WhiteSmoke);
            BtnCurrencyConverter.Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray);
            BtnUnitConverter.Background = new SolidColorBrush(Microsoft.UI.Colors.WhiteSmoke);
            MenuPanel.Visibility = Visibility.Collapsed;
            CalculatorViewHost.Visibility = Visibility.Collapsed;
        }

        private void BtnUnitConverter_Click(object sender, RoutedEventArgs e)
        {
            BtnCalculator.Background = new SolidColorBrush(Microsoft.UI.Colors.WhiteSmoke);
            BtnCurrencyConverter.Background = new SolidColorBrush(Microsoft.UI.Colors.WhiteSmoke);
            BtnUnitConverter.Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray);
            MenuPanel.Visibility = Visibility.Collapsed;
            CalculatorViewHost.Visibility = Visibility.Collapsed;
        }
    }
}
