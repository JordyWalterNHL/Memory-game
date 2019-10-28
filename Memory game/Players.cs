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

        public Players(TextBox nameOne, TextBox nameTwo, TextBox scoreOne, TextBox scoreTwo)
        {
            players[0] = new PlayerData("Berber", nameOne, scoreOne);
            players[1] = new PlayerData("Jort", nameTwo, scoreTwo);
        }

        public void WrongAnswer()
        {
            playerTwo = !playerTwo;
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
    }

    class PlayerData
    {
        private string name;
        private TextBox nameBox;

        private int memories;
        private TextBox scoreBox;

        private float points;

        public PlayerData(string playerName, TextBox nameBox, TextBox scoreBox)
        {
            this.name = playerName;
            this.nameBox = nameBox;
            this.scoreBox = scoreBox;
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
    }
}
