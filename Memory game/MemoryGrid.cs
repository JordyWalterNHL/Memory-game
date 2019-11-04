using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Memory_game
{
    /// <summary>
    /// Makes the field and has most of the logic of the game to make it run smooth
    /// </summary>
    class MemoryGrid
    {
        private Grid grid;
        private Players player;

        private int rows, colums, cardAmount;
        private int firstCardId, secondCardId;

        private bool canClick = true;
        private bool secondClick;

        private string theme;

        /// <summary>
        /// List used to keep track of the cards that you turned
        /// </summary>
        private List<Image> TurnedCards = new List<Image>();
        /// <summary>
        /// Ordered list of all the cards
        /// </summary>
        private List<MemoryCard> GameCards = new List<MemoryCard>();
        /// <summary>
        /// Shuffled list of all the cards
        /// </summary>
        private List<MemoryCard> MemoryCards = new List<MemoryCard>();

        /// <summary>
        /// Container used to save all the important information, used to save/load the game
        /// </summary>
        private SaveData SaveData = new SaveData();
        public int savedTime = 0;

        /// <summary>
        /// Set all the images with the right theme so you can use them
        /// </summary>
        /// <returns>List with strings, the route to the image</returns>
        private List<string> GetImageSources()
        {
            List<string> imageSources = new List<string>();
            for (int i = 0; i < cardAmount; i++)
            {
                int imageNumber = i % (cardAmount / 2) + 1;
                string sourceString = "Images/" + theme + "/" + imageNumber + ".png";
                imageSources.Add(sourceString);
            }

            return imageSources;
        }

        /// <summary>
        /// Constructor to make the field
        /// </summary>
        /// <param name="grid1">Grid play area</param>
        /// <param name="rows">int amount of rows on the playing field</param>
        /// <param name="cols">int amount of colums on the playing field</param>
        /// <param name="player">Players reference to all the information of the players</param>
        /// <param name="theme">string name of the theme you are using</param>
        public MemoryGrid(Grid grid1, int rows, int cols, Players player, string theme)
        {
            grid = grid1;
            this.rows = rows;
            this.colums = cols;
            this.cardAmount = rows * cols;
            this.player = player;
            this.theme = theme;

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
            //Add cards to list
            List<string> imageSources = GetImageSources();
            for (int i = 0; i < cardAmount; i++)
            {
                GameCards.Add(new MemoryCard()
                {
                    back = "Images/" + theme + "/CardBack.png",
                    front = imageSources.First(),
                    id = i,
                    value = i % (cardAmount / 2) + 1
                });

                imageSources.RemoveAt(0);
            }

            //Shuffle list
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

            //Display cards
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
        /// Clears grid and Adds new cards
        /// </summary>
        public void ResetBoard()
        {
            grid.Children.Clear();
            GameCards.Clear();
            MemoryCards.Clear();
            TurnedCards.Clear();
            player.ClearMemory();
            player.SetPlayerOne();
            canClick = true;
            secondClick = false;
            AddCards();
        }
        public int HighestScore()
        {
            return player.ReturnHighScore();
        }
        public string WinnerName()
        {
            return player.ReturnWinnerName();
        }
        public string OnlyNameWinner()
        {
            return player.ReturnOnlyName();
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

            ///Make it so you can't press the same image twice, set a new image and add the card to the pressed cards list
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

                ///Start delay, so you can watch the card, check if the 2 cards are the same and act accordingly
                Task.Delay(500).ContinueWith(_ =>
                {
                    if (GameCards[firstCardId].value == GameCards[secondCardId].value)
                    {
                        TurnedCards[0].Source = null;
                        TurnedCards[1].Source = null;

                        GameCards[firstCardId].beenUsed = true;
                        GameCards[secondCardId].beenUsed = true;

                        player.RightAnswer();
                    }
                    else ///Reset the cards you just pressed, start other players turn
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

        /// <summary>
        /// Reset everything so you can use it again,
        /// Load the savefile with the right data,
        /// Set all the values with the data from the save file 
        /// </summary>
        public void LoadGame()
        {
            grid.Children.Clear();

            GameCards.Clear();
            MemoryCards.Clear();
            TurnedCards.Clear();

            player.ClearMemory();
            player.SetPlayerOne();
            canClick = true;
            secondClick = false;

            SaveData = SaveAndLoad.ReadFromBinaryFile<SaveData>("memory.sav");

            rows = SaveData.rows;
            colums = SaveData.colums;

            player.LoadGame(SaveData.playerTwo, SaveData.namePlayerOne, SaveData.namePlayerTwo, SaveData.scorePlayerOne, SaveData.scorePlayerTwo);
            savedTime = SaveData.timer;
            LoadCards();
        }

        /// <summary>
        /// Load the cards from the save file, and display them on the screen
        /// </summary>
        private void LoadCards()
        { 
            GameCards = SaveData.GameCards;
            MemoryCards = SaveData.MemoryCards;

            int cardcount = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    Image cardImage = new Image
                    {
                        Source = MemoryCards[cardcount].GetBackSource(),
                        Tag = MemoryCards[cardcount].id
                    };

                    cardcount++;
                    cardImage.MouseDown += new MouseButtonEventHandler(CustomCardClick);

                    Grid.SetRow(cardImage, i);
                    Grid.SetColumn(cardImage, j);
                    grid.Children.Add(cardImage);
                }
            }
        }

        /// <summary>
        /// Reset the been clicked bool for every card that isn't a pair yet (otherwise you won't be able to click that card again, if you've only clicked one card and then saved)
        /// Save the rows, colums, current cards, order in which the cards are shuffled and all the player information
        /// Write this to the save file
        /// </summary>
        public void SaveGame()
        {
            for (int i = 0; i < GameCards.Count; i++)
            {
                GameCards[i].beenClicked = GameCards[i].beenUsed;
            }

            SaveData.rows = rows;
            SaveData.colums = colums;

            SaveData.GameCards = GameCards;
            SaveData.MemoryCards = MemoryCards;

            player.SaveGame(out SaveData.namePlayerOne, out SaveData.scorePlayerOne, out SaveData.namePlayerTwo, out SaveData.scorePlayerTwo, out SaveData.playerTwo);
            SaveData.timer = savedTime;

            SaveAndLoad.WriteToBinaryFile("memory.sav", SaveData);
        }
    }
}
