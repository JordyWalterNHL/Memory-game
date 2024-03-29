﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Memory_game
{
    /// <summary>
    /// Class with the information of the players
    /// </summary>
    class Players
    {
        private PlayerData[] players = new PlayerData[2];       ///Reference to the 2 players
        private bool playerTwo;                                 ///Bool used to check who's turn it is
        Grid playerTurnColor;                                   
        private string winner;
        private string winnerName;

        /// <summary>
        /// Easy way to get the current player, use the bool to return an int, use the int in the array
        /// </summary>
        public PlayerData CurrentPlayer
        {
            get
            {
                return players[Convert.ToInt32(playerTwo)];
            }
        }

        /// <summary>
        /// Constructor to set all the basic information of the players
        /// </summary>
        /// <param name="name1">The name the first player chose</param>
        /// <param name="name2">The name the second player chose</param>
        /// <param name="nameOne">The TextBlock where the first name will be displayed</param>
        /// <param name="nameTwo">The TextBlock where the second name will be displayed</param>
        /// <param name="scoreOne">The TextBlock where the first score will be displayed</param>
        /// <param name="scoreTwo">The TextBlock where the second score will be displayed</param>
        /// <param name="playerTurn"></param>
        /// <param name="playerTurnColor"></param>
        public Players(string name1, string name2, TextBlock nameOne, TextBlock nameTwo, TextBlock scoreOne, TextBlock scoreTwo, TextBlock playerTurn, Grid playerTurnColor)
        {
            players[0] = new PlayerData(name1, nameOne, scoreOne, playerTurn);
            players[1] = new PlayerData(name2, nameTwo, scoreTwo, playerTurn);
            players[0].SetPlayerTurn();
            this.playerTurnColor = playerTurnColor;
            playerTurnColor.Background = new SolidColorBrush(Colors.Blue);
        }

        /// <summary>
        /// On the wrong answer flip the bool which decides who's turn it is
        /// Change the background color to show which players turn it is
        /// </summary>
        public void WrongAnswer()
        {
            playerTwo = !playerTwo;
            CurrentPlayer.SetPlayerTurn();
            if (playerTwo)
            {
                playerTurnColor.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                playerTurnColor.Background = new SolidColorBrush(Colors.Blue);
            }
        }

        /// <summary>
        /// On the right answer give the current player a point
        /// </summary>
        public void RightAnswer()
        {
            CurrentPlayer.GetMemory();
        }

        /// <summary>
        /// Clear both players memories, used for resetting
        /// </summary>
        public void ClearMemory()
        {
            players[0].ClearMemory();
            players[1].ClearMemory();
        }
        /// <summary>
        /// Sets current player to the first player
        /// </summary>
       public void SetPlayerOne()
        {
            playerTwo = false;
            CurrentPlayer.SetPlayerTurn();
            playerTurnColor.Background = new SolidColorBrush(Colors.Blue);
        }
        /// <summary>
        /// Returns the highest score, sets winner string
        /// </summary>
        /// <returns>int: the highest score</returns>
        public int ReturnHighScore()
        {
            int one = players[0].FetchMemory();
            int two = players[1].FetchMemory();
            if (one > two)
            {
                winnerName = players[0].FetchName();
                winner = ("The winner is: ") + winnerName;
                return one;
            }
            else if (one < two)
            {
                winnerName = players[1].FetchName();
                winner = ("The winner is: ") + winnerName;
                return two;
            }
            else
            {
                winner = ("It's a draw!");
                return one;
            }
        }
        /// <summary>
        /// Returns winner name with added string
        /// </summary>
        /// <returns>string: the name of the winner + string</returns>
        public string ReturnWinnerName()
        {
            return winner;
        }
        /// <summary>
        /// Returns only the winner's name
        /// </summary>
        /// <returns>string: the winner's name</returns>
        public string ReturnOnlyName()
        {
            return winnerName;
        }
    
        /// <summary>
        /// Get all the information needed to reload the players
        /// </summary>
        /// <param name="turnP1">bool to check if it is player 1's turn</param>
        /// <param name="name1">string name player 1</param>
        /// <param name="name2">string name player 2</param>
        /// <param name="score1">int amount of memories player 1</param>
        /// <param name="score2">int amount of memories player 2</param>
        public void LoadGame(bool turnP1, string name1, string name2, int score1, int score2)
        {
            players[0].LoadPlayer(name1, score1);
            players[1].LoadPlayer(name2, score2);

            playerTwo = !turnP1;
            WrongAnswer();
        }

        /// <summary>
        /// Set all the information needed to reload the players
        /// </summary>
        /// <param name="name1">string name player 1</param>
        /// <param name="score1">int amount of memories player 1</param>
        /// <param name="name2">string name player 2</param>
        /// <param name="score2">int amount of memories player 2</param>
        /// <param name="turnP2">bool to check if it is player 2's turn</param>
        public void SaveGame(out string name1, out int score1, out string name2, out int score2, out bool turnP2)
        {
            name1 = players[0].FetchName();
            score1 = players[0].FetchMemory();

            name2 = players[1].FetchName();
            score2 = players[1].FetchMemory();

            turnP2 = playerTwo;
        }
    }

    /// <summary>
    /// Data used by the Players class, easier way to set all the information instead of using a lot of variables in the Players script
    /// </summary>
    class PlayerData
    {
        /// <summary>
        /// name of the player
        /// </summary>
        private string name;
        /// <summary>
        /// textblock for the current player name
        /// </summary>
        private TextBlock nameBox;
        /// <summary>
        /// score of the current player
        /// </summary>
        private int memories;
        /// <summary>
        /// textblock for the current player score
        /// </summary>
        private TextBlock scoreBox;
        /// <summary>
        /// textblock to indicate whose turn it is
        /// </summary>
        private TextBlock playerTurn;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="playerName">string:Name of player</param>
        /// <param name="nameBox">TextBlock: box where names go</param>
        /// <param name="scoreBox">TextBlock: box where scores go</param>
        /// <param name="playerTurn">TextBlock: box where playerturn text goes</param>
        public PlayerData(string playerName, TextBlock nameBox, TextBlock scoreBox, TextBlock playerTurn)
        {
            this.name = playerName;
            this.nameBox = nameBox;
            this.scoreBox = scoreBox;
            this.playerTurn = playerTurn;
            UpdateUI();
        }
        /// <summary>
        /// Updates the scorebox and textbox in the UI
        /// </summary>
        private void UpdateUI()
        {
            nameBox.Text = name;
            scoreBox.Text = memories.ToString();
        }
        /// <summary>
        /// Adds one to score and updates UI
        /// </summary>
        public void GetMemory()
        {
            memories++;
            UpdateUI();
        }
        /// <summary>
        /// Clears all scores and updates UI
        /// </summary>
        public void ClearMemory()
        {
            memories = 0;
            UpdateUI();
        }
        /// <summary>
        /// Gets score
        /// </summary>
        /// <returns>int: player score</returns>
        public int FetchMemory()
        {
            return memories;
        }
        /// <summary>
        /// Gets name
        /// </summary>
        /// <returns>string: player name</returns>
        public string FetchName()
        {
            return name;
        }
        /// <summary>
        /// Sets the playerTurn textbox
        /// </summary>
        public void SetPlayerTurn()
        {
            playerTurn.Text = "It's " + name + "'s turn!";
        }
        /// <summary>
        /// Helps in loading the players from memory file
        /// </summary>
        /// <param name="name">name of player</param>
        /// <param name="score">score of player</param>
        public void LoadPlayer(string name, int score)
        {
            this.name = name;
            this.memories = score;

            UpdateUI();
        }
    }
}
