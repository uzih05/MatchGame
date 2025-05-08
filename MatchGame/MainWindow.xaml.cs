using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MatchGame
{
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            TimeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                TimeTextBlock.Text = TimeTextBlock.Text + " - Play again?";
            }
            {

            }
        }

        private void SetUpGame()
        {
            List<string> animalsEmoji = new List<string>()
            {
                "🦑", "🦑",
                "🐡", "🐡",
                "🐘", "🐘",
                "🐋", "🐋",
                "🐪", "🐪",
                "🦕", "🦕",
                "🦘", "🦘",
                "🦔", "🦔",
            };

            Random random = new Random();
            // mainGrid는 x:Name="mainGrid"로 XAML에서 정의되어 있어야 합니다.
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "TimeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;

                    int index = random.Next(animalsEmoji.Count);
                    string nextEmoji = animalsEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalsEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
