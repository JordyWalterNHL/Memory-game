using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Memory_game
{
    class Players
    {
        private PlayerData[] players = new PlayerData[2];
        private bool playerTwo;
        public PlayerData CurrentPlayer
        {
            get
            {
                return players[Convert.ToInt32(playerTwo)];
            }
        }

        public Players(string name1, string name2, TextBlock nameOne, TextBlock nameTwo, TextBlock scoreOne, TextBlock scoreTwo, TextBlock playerTurn)
        {
            players[0] = new PlayerData(name1, nameOne, scoreOne, playerTurn);
            players[1] = new PlayerData(name2, nameTwo, scoreTwo, playerTurn);
            players[0].SetPlayerTurn();
        }

        public void WrongAnswer()
        {
            playerTwo = !playerTwo;
            CurrentPlayer.SetPlayerTurn();
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
        }
    }

    class PlayerData
    {
        private string name;
        private TextBlock nameBox;

        private int memories;
        private TextBlock scoreBox;

        private TextBlock playerTurn;
        private float points;

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
        public void SetPlayerTurn()
        {
            playerTurn.Text = "It's " + name + "'s turn!";
        }
    }
}
