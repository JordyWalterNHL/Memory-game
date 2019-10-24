using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    class MemoryGrid
    {
        private Grid grid;
        private Players player;

        private int rows, colums, cardAmount;
        private int firstCardId, secondCardId;

        private bool canClick = true;
        private bool secondClick;

        private List<Image> TurnedCards = new List<Image>();
        private List<MemoryCard> GameCards = new List<MemoryCard>();
        private List<ImageSource> GetImageSources()
        {
            List<ImageSource> images = new List<ImageSource>();
            for (int i = 0; i < cardAmount; i++)
            {
                int imageNumber = i % (cardAmount / 2) + 1;
                ImageSource source = new BitmapImage(new Uri("Images/" + imageNumber + ".png", UriKind.Relative));
                images.Add(source);
            }

            return images;
        }

        public MemoryGrid(Grid grid, int rows, int cols, Players player)
        {
            this.grid = grid;
            this.rows = rows;
            this.colums = cols;
            this.cardAmount = rows * cols;
            this.player = player;

            CreateGameGrid();
            AddCards();
        }

        /// <summary>
        /// Create the grid, depending on the amount of rows and colums
        /// </summary>
        private void CreateGameGrid()
        {
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < colums; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            grid.ShowGridLines = true;
        }

        /// <summary>
        /// Add the cards to the grid
        /// Set the information in the custom MemoryCard class
        /// </summary>
        private void AddCards()
        {
            List<ImageSource> images = GetImageSources();
            //Add cards to list
            for (int i = 0; i < cardAmount; i++)
            {
                ImageSource front = images.First();
                images.RemoveAt(0);

                GameCards.Add(new MemoryCard(front, i)
                {
                    value = i % (cardAmount / 2) + 1
                });
            }

            //Shuffle list
            List<MemoryCard> MemoryCards = new List<MemoryCard>();
            List<MemoryCard> GameCardsCopy = new List<MemoryCard>();
            GameCardsCopy.AddRange(GameCards);

            Random random = new Random();
            for (int i = 0; i < GameCards.Count; i++)
            {
                int index = random.Next(0, GameCardsCopy.Count);

                MemoryCards.Add(GameCardsCopy[index]);
                GameCardsCopy.RemoveAt(index);
            }

            int cardCount = 0;

            //display cards
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    Image cardImage = new Image
                    {
                        Source = MemoryCards[cardCount].GetBackSource(),
                        Tag = MemoryCards[cardCount].id
                    };

                    cardCount++;

                    cardImage.MouseDown += new MouseButtonEventHandler(CustomCardClick);

                    Grid.SetRow(cardImage, i);
                    Grid.SetColumn(cardImage, j);
                    grid.Children.Add(cardImage);
                }
            }
        }

        /// <summary>
        /// Check if you can click the image, change the image to the front image
        /// When it is the second card check if the two cards are the same
        /// </summary>
        /// <param name="sender">The image you've pressed</param>
        private void CustomCardClick(object sender, MouseEventArgs e)
        {
            if (!canClick)
                return;

            Image card = (Image)sender;
            int cardId = (int)card.Tag;

            if (GameCards[cardId].beenClicked)
                return;

            GameCards[cardId].beenClicked = true;
            card.Source = GameCards[cardId].GetFrontSource();
            TurnedCards.Add(card);

            if (!secondClick)
            {
                firstCardId = cardId;
                secondClick = true;
            }
            else
            {
                canClick = false;
                secondCardId = cardId;
                Task.Delay(500).ContinueWith(_ =>
                {
                    if (GameCards[firstCardId].value == GameCards[secondCardId].value)
                    {
                        //TODO: Actions after you get a combination
                        TurnedCards[0].Source = null;
                        TurnedCards[1].Source = null;

                        player.RightAnswer();
                    }
                    else
                    {
                        TurnedCards[0].Source = GameCards[firstCardId].GetBackSource();
                        TurnedCards[1].Source = GameCards[secondCardId].GetBackSource();

                        GameCards[firstCardId].beenClicked = false;
                        GameCards[secondCardId].beenClicked = false;

                        player.WrongAnswer();

                    }

                    TurnedCards.Clear();

                    secondClick = false;
                    canClick = true;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
