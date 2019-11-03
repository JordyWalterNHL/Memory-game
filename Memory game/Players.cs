using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Memory_game
{
    class Players
    {
        private PlayerData[] players = new PlayerData[2];
        private bool playerTwo;
        Grid playerTurnColor;
        private string winner;
        public PlayerData CurrentPlayer
        {
            get
            {
                return players[Convert.ToInt32(playerTwo)];
            }
        }

        public Players(string name1, string name2, TextBlock nameOne, TextBlock nameTwo, TextBlock scoreOne, TextBlock scoreTwo, TextBlock playerTurn, Grid playerTurnColor)
        {
            players[0] = new PlayerData(name1, nameOne, scoreOne, playerTurn);
            players[1] = new PlayerData(name2, nameTwo, scoreTwo, playerTurn);
            players[0].SetPlayerTurn();
            this.playerTurnColor = playerTurnColor;
            playerTurnColor.Background = new SolidColorBrush(Colors.Blue);
        }

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

        public void RightAnswer()
        {
            CurrentPlayer.GetMemory();
        }
        public void ClearMemory()
        {
            players[0].ClearMemory();
            players[1].ClearMemory();
        }
        
        public void SetPlayerOne()
        {
            playerTwo = false;
            CurrentPlayer.SetPlayerTurn();
            playerTurnColor.Background = new SolidColorBrush(Colors.Blue);
        }
        public int ReturnHighScore()
        {
            int one = players[0].FetchMemory();
            int two = players[1].FetchMemory();
            if (one > two)
            {
                winner = ("The winner is: ") + players[0].FetchName();
                return one;
            }
            else if (one < two)
            {
                winner = ("The winner is: ") + players[1].FetchName();
                return two;
            }
            else
            {
                winner = ("It's a draw!");
                return one;
            }
        }
        public string ReturnWinnerName()
        {
            return winner;
        }
    

        public void LoadGame(bool turnP1, string name1, string name2, int score1, int score2)
        {
            players[0].LoadPlayer(name1, score1);
            players[1].LoadPlayer(name2, score2);

            playerTwo = !turnP1;
            WrongAnswer();
        }

        public void SaveGame(out string nameOne, out int scoreOne, out string nameTwo, out int scoreTwo, out bool turnP2)
        {
            nameOne = players[0].FetchName();
            scoreOne = players[0].FetchMemory();

            nameTwo = players[1].FetchName();
            scoreTwo = players[1].FetchMemory();

            turnP2 = playerTwo;
        }
    }

    class PlayerData
    {
        private string name;
        private TextBlock nameBox;

        private int memories;
        private TextBlock scoreBox;

        private TextBlock playerTurn;

        public PlayerData(string playerName, TextBlock nameBox, TextBlock scoreBox, TextBlock playerTurn)
        {
            this.name = playerName;
            this.nameBox = nameBox;
            this.scoreBox = scoreBox;
            this.playerTurn = playerTurn;
            UpdateUI();
        }

        private void UpdateUI()
        {
            nameBox.Text = name;
            scoreBox.Text = memories.ToString();
        }

        public void GetMemory()
        {
            memories++;
            UpdateUI();
        }
        public void ClearMemory()
        {
            memories = 0;
            UpdateUI();
        }
        public int FetchMemory()
        {
            return memories;
        }
        public string FetchName()
        {
            return name;
        }
        public void SetPlayerTurn()
        {
            playerTurn.Text = "It's " + name + "'s turn!";
        }

        public void LoadPlayer(string name, int score)
        {
            this.name = name;
            this.memories = score;

            UpdateUI();
        }
    }
}
