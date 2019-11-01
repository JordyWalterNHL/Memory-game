using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Memory_game
{
    class Players
    {
        private readonly PlayerData[] players = new PlayerData[2];
        private bool playerTwo;
        readonly Grid playerTurnColor;
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
    }

    class PlayerData
    {
        private readonly string name;
        private readonly TextBlock nameBox;

        private int memories;
        private readonly TextBlock scoreBox;

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
        static void DavidSave()
        {
            int i;
            using (StreamWriter writer = new StreamWriter("sav001.sav"))
            {
                writer.WriteLine();
                writer.WriteLine(); ;
                writer.WriteLine("Do not edit saves.");
            }
            using (var sr = new StreamReader("sav001.sav"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                { var columns = line.Split(new char[] { ':' });  }
            }

        }
    }

}
