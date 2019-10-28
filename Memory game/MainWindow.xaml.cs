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

namespace Memory_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MemoryGrid memoryGrid;
        public MainWindow()
        {
            InitializeComponent();

            Players players = new Players(NameOne, NameTwo, ScoreOne, ScoreTwo, PlayerTurn);
            memoryGrid = new MemoryGrid(GameGrid, 4, 4, players);
        }
        /// <summary>
        /// Handles the reset button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            memoryGrid.ResetBoard();
        }
        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            GameWindow.Visibility = Visibility.Visible;
        }
    }
}
