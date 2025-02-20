using System;
using System.Collections.Generic;
using System.IO;
using IOPath = System.IO.Path;
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
using Microsoft.Win32;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(rangeFromTextBox.Text) ||
                string.IsNullOrWhiteSpace(rangeToTextBox.Text) ||
                string.IsNullOrWhiteSpace(numberOfElementsTextBox.Text))
            {
                return;
            }

            int min = int.Parse(rangeFromTextBox.Text);
            int max = int.Parse(rangeToTextBox.Text);
            int count = int.Parse(numberOfElementsTextBox.Text);

            RandomNumberGenerator rng = new RandomNumberGenerator(progressText, statusBarText, progressBar);

            List<int> numbers = await rng.Generate(min, max, count);

            outputText.Text = string.Join(", ", numbers);
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close application?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void NewMenu_Click(object sender, RoutedEventArgs e)
        {
            numberOfElementsTextBox.Text = "";
            rangeFromTextBox.Text = "";
            rangeToTextBox.Text = "";
            outputText.Text = "";
            progressText.Text = "";
            statusBarText.Text = "Ready";
        }

        private void SaveMenu_Click(object sender, RoutedEventArgs e)
        {
            string path = IOPath.Combine(Directory.GetCurrentDirectory(), "RandomNumbers.txt");

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(outputText.Text);
            }
        }

        private void LoadMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                outputText.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }
    }
}
