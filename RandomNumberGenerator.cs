using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

public class RandomNumberGenerator
{
    private Random rnd = new Random();
    private TextBlock progressText;
    private TextBlock statusBarText;
    private ProgressBar progressBar;

    public RandomNumberGenerator(TextBlock progressText, TextBlock statusBarText, ProgressBar progressBar)
    {
        this.progressText = progressText;
        this.statusBarText = statusBarText;
        this.progressBar = progressBar;
    }

    public async Task<List<int>> Generate(int min, int max, int count)
    {
        statusBarText.Dispatcher.Invoke(() => statusBarText.Text = "Generating numbers");
        List<int> numbers = new List<int>();
        for (int i = 0; i < count; i++)
        {
            int number = rnd.Next(min, max);
            numbers.Add(number);

            // delay
            await Task.Delay(500);

            double progress = (i + 1) / (double)count * 100;
            progressText.Dispatcher.Invoke(() => progressText.Text = $"{progress.ToString("F2")}%");
            progressBar.Dispatcher.Invoke(() => progressBar.Value = progress);
        }
        statusBarText.Dispatcher.Invoke(() => statusBarText.Text = "Ready");
        return numbers;
    }
}
