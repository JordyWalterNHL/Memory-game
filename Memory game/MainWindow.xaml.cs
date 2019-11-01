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
        private Dictionary<int,string> highscores = new Dictionary<int,string>()
        {
            {2, "David"},
            {8, "Hylke"},
            {7, "Berber"},
            {3, "Bas"},
            {4, "Jort"},
            {5, "Jordy"}
        };
                
        MemoryGrid memoryGrid;
        string name1, name2;
        
        public MainWindow()
        {
            InitializeComponent();
            SortScores();
            SelectWindow.Visibility = Visibility.Collapsed;
            GameWindow.Visibility = Visibility.Collapsed;
            ExtraWindow.Visibility = Visibility.Collapsed;          
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
                    int size = 8;
                    int themeindex = ThemeSelection.SelectedIndex; 
                    string theme = "";

                    ImageBrush myBrush = new ImageBrush();
                    ParentGrid.Background = myBrush;

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
                    if ((size*2) >= (rows * cols))
                    {
                        Players players = new Players(name1, name2, NameOne, NameTwo, ScoreOne, ScoreTwo, PlayerTurn, PlayerTurnColor);
                        memoryGrid = new MemoryGrid(GameGrid, rows, cols, players, theme);
                        myBrush.ImageSource = new BitmapImage(new Uri("../../Images/" + theme + "/Background.jpg", UriKind.Relative));
                        SelectWindow.Visibility = Visibility.Collapsed;
                        GameWindow.Visibility = Visibility.Visible;

                        dt.Interval = TimeSpan.FromMilliseconds(50);
                        dt.Tick += dtTicker;
                        dt.Start();
                    }
                    else
                    {
                        myBrush.ImageSource = new BitmapImage(new Uri("../../Images/Bicycles/Background.jpg", UriKind.Relative));
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
            MainMenu.Visibility = Visibility.Collapsed;
            ExtraWindow.Visibility = Visibility.Visible;
        }
        private void ExtraHomeButtonClick(object sender, RoutedEventArgs e)
        {
            ExtraWindow.Visibility = Visibility.Collapsed;
            MainMenu.Visibility = Visibility.Visible;
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
            }
        }
        private void SortScores()
        {
            List<int> keyList = new List<int>(highscores.Keys);
            List<string> valueList = new List<string>();
            int temp;
            for (int j = 0; j <= keyList.Count - 2; j++)
            {
                for (int i = 0; i <= highscores.Keys.Count - 2; i++)
                {
                    if (keyList[i] < keyList[i + 1])
                    {
                        temp = keyList[i + 1];
                        keyList[i + 1] = keyList[i];
                        keyList[i] = temp;
                    }
                }
            }
            for (int k = 0; k < 6; k++)
            {
                Viewbox viewbox = new Viewbox();
                TextBlock text = new TextBlock();
                viewbox.SetValue(Grid.RowProperty, k);
                text.Text = highscores[keyList[k]] + " - " + keyList[k];
                viewbox.Child = text;
                HighScores.Children.Add(viewbox);
            }
        }
    }
}
