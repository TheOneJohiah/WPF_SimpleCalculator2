using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_SimpleCalculator2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Distance { feet, meters};
        private enum Mass { Pounds, Kilograms};

        Distance _distance;
        Mass _mass;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            double mass;
            double distancePerSecond;
            double velocity;

            if ((bool)RadioButton_Pounds.IsChecked)
            {
                double.TryParse(TextBox_Mass.Text, out mass);
                double.TryParse(TextBox_Velocity.Text, out distancePerSecond);
                mass = mass * 0.453592;
                distancePerSecond = distancePerSecond * 0.3048;
            }
            else
            {
                double.TryParse(TextBox_Mass.Text, out mass);
                double.TryParse(TextBox_Velocity.Text, out distancePerSecond);
            }

            velocity = mass * distancePerSecond;
            TextBox_Momentum.Text = $"{velocity.ToString()} m/s";

        }

        private void RadioButton_Units_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                UpdateUnits();
            }
        }

        private void UpdateUnits()
        {
            if ((bool)RadioButton_Pounds.IsChecked)
            {
                _distance = Distance.feet;
                _mass = Mass.Pounds;
            }
            else if ((bool)RadioButton_Kilograms.IsChecked)
            {
                _distance = Distance.meters;
                _mass = Mass.Kilograms;
            }

            Label_Velocity.Content = $"Velocity ({_distance}/s)";
            Label_Mass.Content = _mass;
        }
    }
}
