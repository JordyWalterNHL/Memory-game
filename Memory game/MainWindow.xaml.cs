using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace Memory_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int timer = 0;
        private int milliTimer = 0;
        DispatcherTimer dt = new DispatcherTimer();
        private List<string> names = new List<string>() { 
        "Hylke","David","Berber","Bas","Jort","Jordy","test","test2","test3","test4","test5"
        };
        private List<int> scores = new List<int>() { 
        8,3,7,3,2,4,5,1,1,1,1
        };
        private HighScore highscores;
        
        MemoryGrid memoryGrid;
        private string name1, name2, theme;
        private int size;
        
        public MainWindow()
        {
            highscores = new HighScore(names, scores);
            InitializeComponent();
            SortScores();
            SelectWindow.Visibility = Visibility.Collapsed;
            GameWindow.Visibility = Visibility.Collapsed;
            ExtraWindow.Visibility = Visibility.Collapsed;    
            EndWindow.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Handles the reset button click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            memoryGrid.ResetBoard();
            timer = 0;
            dt.Start();
        }
        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            SelectWindow.Visibility = Visibility.Visible;
            if (theme == "Halloween")
            {
                SelectHome.Source = new BitmapImage(new Uri("../../Images/Halloween/home.png", UriKind.Relative));
            }
            else
            {
                SelectHome.Source = new BitmapImage(new Uri("../../Images/home.png", UriKind.Relative));

            }
        }

        /// <summary>
        /// Timer + window visibility
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectWindowPlay(object sender, RoutedEventArgs e)
        {
            name1 = Name1.Text;
            name2 = Name2.Text;
            if (!String.IsNullOrEmpty(name1) && !String.IsNullOrEmpty(name2))
            {
                    int index = GridSelection.SelectedIndex;
                    int rows = 4;
                    int cols = 4;

                    switch (index)
                    {
                        case 0:
                            rows = 4;
                            cols = 4;
                            break;
                        case 1:
                            rows = 4;
                            cols = 5;
                            break;
                        case 2:
                            rows = 4;
                            cols = 6;
                            break;
                        case 3:
                            rows = 5;
                            cols = 6;
                            break;
                        case 4:
                            rows = 6;
                            cols = 6;
                            break;
                        default:
                            rows = 4;
                            cols = 4;
                            break;
                    }
                    if ((size*2) >= (rows * cols))
                    {
                        Players players = new Players(name1, name2, NameOne, NameTwo, ScoreOne, ScoreTwo, PlayerTurn, PlayerTurnColor);
                        memoryGrid = new MemoryGrid(GameGrid, rows, cols, players, theme);
                        SelectWindow.Visibility = Visibility.Collapsed;
                        GameWindow.Visibility = Visibility.Visible;

                        if (theme == "Halloween")
                        {
                            GameHome.Source = new BitmapImage(new Uri("../../Images/Halloween/home.png", UriKind.Relative));
                            
                            SelectHome.Source = new BitmapImage(new Uri("../../Images/Halloween/home.png", UriKind.Relative));
                            GameReset.Source = new BitmapImage(new Uri("../../Images/Halloween/reset.png", UriKind.Relative));
                        }
                        else
                        {
                            GameHome.Source = new BitmapImage(new Uri("../../Images/home.png", UriKind.Relative));
                            ExtrasHome.Source = new BitmapImage(new Uri("../../Images/home.png", UriKind.Relative));
                            SelectHome.Source = new BitmapImage(new Uri("../../Images/home.png", UriKind.Relative));
                            GameReset.Source = new BitmapImage(new Uri("../../Images/reset.png", UriKind.Relative));

                         }
                        dt.Interval = TimeSpan.FromMilliseconds(50);
                        dt.Tick += dtTicker;
                        dt.Start();
                    }
                    else
                    {
                        PlayerWarningBox2.Visibility = Visibility.Visible;
                        Task.Delay(2000).ContinueWith(_ =>
                        {
                            PlayerWarningBox2.Visibility = Visibility.Collapsed;
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
            }
            else
            {
                PlayerWarningBox.Visibility = Visibility.Visible;
                Task.Delay(2000).ContinueWith(_ =>
                {
                    PlayerWarningBox.Visibility = Visibility.Collapsed;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }      
        }
        private void ExtraButtonClick(object sender, RoutedEventArgs e)
        {
            if (theme == "Halloween")
            {
                ExtrasHome.Source = new BitmapImage(new Uri("../../Images/Halloween/home.png", UriKind.Relative));
            }
            else
            {
                ExtrasHome.Source = new BitmapImage(new Uri("../../Images/home.png", UriKind.Relative));
            }
            MainMenu.Visibility = Visibility.Collapsed;
            ExtraWindow.Visibility = Visibility.Visible;
        }
        private void ExtraHomeButtonClick(object sender, RoutedEventArgs e)
        {
            ExtraWindow.Visibility = Visibility.Collapsed;
            MainMenu.Visibility = Visibility.Visible;
        }
        private void ChangeBackground(object sender, SelectionChangedEventArgs e)
        {

            int themeindex = ThemeSelection.SelectedIndex;
            ImageBrush myBrush = new ImageBrush();
            ParentGrid.Background = myBrush;
            theme = "";

            switch (themeindex)
            {
                case 0:
                    theme = "Bicycles";
                    size = 8;
                    break;
                case 1:
                    theme = "Halloween";
                    size = 18;
                    break;
                case 2:
                    theme = "Thanksgiving";
                    size = 8;
                    break;
            }
            myBrush.ImageSource = new BitmapImage(new Uri("../../Images/" + theme + "/Background.jpg", UriKind.Relative));

            if (theme == "Halloween")
            {
                BigWindow.Foreground = Brushes.Red;
                ThemeSelection.Foreground = Brushes.Red;
                PlayButton.Foreground = Brushes.Red;
                ExtrasButton.Foreground = Brushes.Red;
                ExitButton.Foreground = Brushes.Red;
                SelectPlay.Foreground = Brushes.Red;
                ExtrasHome.Source = new BitmapImage(new Uri("../../Images/Halloween/home.png", UriKind.Relative));
            }
            else
            {
                BigWindow.Foreground = Brushes.Black;
                PlayButton.Foreground = Brushes.Black;
                ExtrasButton.Foreground = Brushes.Black;
                SelectPlay.Foreground = Brushes.Black;
                ExitButton.Foreground = Brushes.Black;
            }
        }
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void PlayHomeButtonClick(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Visible;
            SelectWindow.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Optellende timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtTicker(object sender, EventArgs e)
        {
            milliTimer++;

            if (milliTimer >= 9)
            {
                timer++;
                milliTimer = 0;
            }

            TimerLabel.Text = timer.ToString();

            if (Convert.ToInt32(ScoreOne.Text) + Convert.ToInt32(ScoreTwo.Text) == 8)
            {
                dt.Stop();
                Winner();
            }
        }
        private void SortScores()
        {
            int length = scores.Count;
            if (length>10)
            {
                length = 10;
            }
            for (int i = 0; i < scores.Count; i++)
            {
                Viewbox viewbox = new Viewbox();
                TextBlock text = new TextBlock();
                var rowDefinition = new RowDefinition();
                string name = highscores.GetName(i);
                int score = highscores.GetScore(i);
                HighScores.RowDefinitions.Add(rowDefinition);
                viewbox.SetValue(Grid.RowProperty, i);

                text.Text = name + " - " + score;
                viewbox.Child = text;
                HighScores.Children.Add(viewbox);
            }
        }
        private void Winner()
        {
            EndWindow.Visibility = Visibility.Visible;
            WinnerScore.Text = "The score is: " + memoryGrid.HighestScore().ToString();
            WinnerName.Text = memoryGrid.WinnerName();
        }
    }
}
