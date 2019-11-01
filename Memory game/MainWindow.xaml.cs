using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;


namespace Memory_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int timer = 0;
        DispatcherTimer dt = new DispatcherTimer();

        MemoryGrid memoryGrid;
        string name1, name2;
        
        public MainWindow()
        {
            InitializeComponent();
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
            timer = -1;
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
            if (!String.IsNullOrEmpty(name1))
            {
                if (!String.IsNullOrEmpty(name2))
                {
                    int index = GridSelection.SelectedIndex;
                    int rows = 4;
                    int cols = 4;
                    int size = 8;
                    int themeindex = ThemeSelection.SelectedIndex;
                    string theme = "";
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
                        SelectWindow.Visibility = Visibility.Collapsed;
                        GameWindow.Visibility = Visibility.Visible;

                        dt.Interval = TimeSpan.FromSeconds(1);
                        dt.Tick += dtTicker;
                        dt.Start();

                    }
                    else
                    {
                        PlayerWarningBox2.Visibility = Visibility.Visible;
                        Task.Delay(4000).ContinueWith(_ =>
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
 
        /// <summary>
        /// Optellende timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtTicker(object sender, EventArgs e)
        {
            timer++;

            TimerLabel.Text = timer.ToString();

            if (Convert.ToInt32(ScoreOne.Text) + Convert.ToInt32(ScoreTwo.Text) == 8)
            {
                dt.Stop();
            }
        }
    }
}
