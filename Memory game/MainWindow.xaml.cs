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
        /// <summary>
        /// Timer that counts the seconds
        /// </summary>
        private int timer = 0;
        /// <summary>
        /// Timer that counts the milliseconds
        /// </summary>
        private int milliTimer = 0;
        /// <summary>
        /// initializes the size at 8
        /// </summary>
        private int size = 8;
        /// <summary>
        /// initializes row size at 4
        /// </summary>
        private int rows = 4;
        /// <summary>
        /// initializes column size at 4
        /// </summary>
        private int cols = 4;
        /// <summary>
        /// Dispatcher timer
        /// </summary>
        DispatcherTimer dt = new DispatcherTimer();
        /// <summary>
        /// HighScore for tracking and displaying the highscores
        /// </summary>
        private HighScore highscores = new HighScore();
        /// <summary>
        /// starts a MemoryGrid to fill later on
        /// </summary>
        MemoryGrid memoryGrid;
        /// <summary>
        /// strings for name 1, name 2 and the theme.
        /// </summary>
        private string name1, name2, theme;
        /// <summary>
        /// Constructs WPF
        /// </summary>
        public MainWindow()
        {
            //SaveAndLoad.WriteToBinaryFile("highscores.sav", highscores);

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
        /// <summary>
        /// Goes home from Game Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Saves game to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameSaveClick(object sender, RoutedEventArgs e)
        {
            memoryGrid.savedTime = timer;
            memoryGrid.SaveGame();
        }
        /// <summary>
        /// Loads Game from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLoadClick(object sender, RoutedEventArgs e)
        {
            memoryGrid.LoadGame();
            timer = memoryGrid.savedTime - 1;
            dt.Start();
        }
        /// <summary>
        /// Shows select window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                            size = 18;
                            break;                           
                       
                    }
                    if ((size*2) >= (rows * cols))
                    {
                        Players players = new Players(name1, name2, NameOne, NameTwo, ScoreOne, ScoreTwo, PlayerTurn, PlayerTurnColor);
                        memoryGrid = new MemoryGrid(GameGrid, rows, cols, players, theme);
                        myBrush.ImageSource = new BitmapImage(new Uri("../../Images/" + theme + "/Background.jpg", UriKind.Relative));
                        SelectWindow.Visibility = Visibility.Collapsed;
                        GameWindow.Visibility = Visibility.Visible;

                        if (theme == "Halloween")
                        {
                            GameHome.Source = new BitmapImage(new Uri("../../Images/Halloween/home.png", UriKind.Relative));
                            GameLoad.Source = new BitmapImage(new Uri("../../Images/Halloween/load.png", UriKind.Relative));
                            GameSave.Source = new BitmapImage(new Uri("../../Images/Halloween/save.png", UriKind.Relative));
                            ExtrasHome.Source = new BitmapImage(new Uri("../../Images/Halloween/home.png", UriKind.Relative));
                            SelectHome.Source = new BitmapImage(new Uri("../../Images/Halloween/home.png", UriKind.Relative));
                            GameReset.Source = new BitmapImage(new Uri("../../Images/Halloween/reset.png", UriKind.Relative));
                        }
                        else
                        {
                            GameHome.Source = new BitmapImage(new Uri("../../Images/home.png", UriKind.Relative));
                            GameLoad.Source = new BitmapImage(new Uri("../../Images/load.png", UriKind.Relative));
                            GameSave.Source = new BitmapImage(new Uri("../../Images/save.png", UriKind.Relative));
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
        /// <summary>
        /// Opens Extras Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Goes home from extra screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtraHomeButtonClick(object sender, RoutedEventArgs e)
        {
            ExtraWindow.Visibility = Visibility.Collapsed;
            MainMenu.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Change event for theme selection, changes background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    size = 18;
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
                ThemeSelection.Foreground = Brushes.Black;
                PlayButton.Foreground = Brushes.Black;
                ExtrasButton.Foreground = Brushes.Black;
                SelectPlay.Foreground = Brushes.Black;
                ExitButton.Foreground = Brushes.Black;
            }
        }
        /// <summary>
        /// Exits Game, shuts down current application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        /// <summary>
        /// Goes home from select window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            if (Convert.ToInt32(ScoreOne.Text) + Convert.ToInt32(ScoreTwo.Text) == (rows*cols)/2)
            {
                dt.Stop();
                Winner();
            }
        }
        /// <summary>
        /// Loads highscores, truncates at 10, adds scores to grid
        /// </summary>
        private void SortScores()
        {
            LoadHighscore();

            int length = highscores.EntriesAmount();

            if (length>10)
            {
                length = 10;
            }
            for (int i = 0; i < length; i++)
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
        /// <summary>
        /// Resets highscores and saves to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetHighscores(object sender, RoutedEventArgs e)
        {
            highscores.Reset();
            SaveAndLoad.WriteToBinaryFile("highscores.sav",highscores);
            HighScores.Children.Clear();
            SortScores();
        }
        /// <summary>
        /// Loads highscores from file
        /// </summary>
        private void LoadHighscore()
        {
            highscores = SaveAndLoad.ReadFromBinaryFile<HighScore>("highscores.sav");
        }

        /// <summary>
        /// Displays end game window and saves high scores
        /// </summary>
        private void Winner()
        {
            EndWindow.Visibility = Visibility.Visible;
            WinnerScore.Text = "The score is: " + memoryGrid.HighestScore().ToString();
            WinnerName.Text = memoryGrid.WinnerName();

            string name = memoryGrid.OnlyNameWinner();
            if (name != null)
            {
                highscores.AddNewHighscore(memoryGrid.OnlyNameWinner(), memoryGrid.HighestScore());
                SaveAndLoad.WriteToBinaryFile("highscores.sav", highscores);
            }
        }
    }
}
