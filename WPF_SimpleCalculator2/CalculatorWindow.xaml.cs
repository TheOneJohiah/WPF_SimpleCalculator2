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
    public partial class CalculatorWindow : Window
    {
        private enum Distance { feet, meters};
        private enum Mass { Pounds, Kilograms};

        Distance _distance;
        Mass _mass;

        public CalculatorWindow()
        {
            InitializeComponent();
            UpdateUnits();
        }

        /// <summary>
        /// calculate the momentum and display the value
        /// </summary>
        private void Button_Calculate_Click(object sender, RoutedEventArgs e)
        {
            double mass;
            double distancePerSecond;
            double velocity;

            if (ValidInputs(out string userFeedback))
            {
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
                TextBox_Momentum.Text = $"{velocity.ToString()} kg m/s";
                SolutionWindow solutionWindow = new SolutionWindow(velocity);
                solutionWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show(userFeedback);
            }
        }

        /// <summary>
        /// validate all inputs and generate a user feedback message if necessary 
        /// </summary>
        /// <param name="userFeedback">user feedback message</param>
        /// <returns>valid inputs status</returns>
        private bool ValidInputs(out string userFeedback)
        {
            bool validInputs = true;
            userFeedback = "";

            //
            // set bool value and build out the user feedback message
            //
            if (!double.TryParse(TextBox_Mass.Text, out double tempMass))
            {
                validInputs = false;
                userFeedback += "It appears that the value entered for Mass is not a valid number." + Environment.NewLine;
            }
            if (!double.TryParse(TextBox_Velocity.Text, out double tempVelocity))
            {
                validInputs = false;
                userFeedback += "It appears that the value entered for Velocity is not a valid number." + Environment.NewLine;
            }

            return validInputs;
        }

        private void RadioButton_Units_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                UpdateUnits();
            }
        }

        /// <summary>
        /// update units in labels
        /// </summary>
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

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Mass.Text = null;
            TextBox_Momentum.Text = null;
            TextBox_Velocity.Text = null;
        }

        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }
    }
}
