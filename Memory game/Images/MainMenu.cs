using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest
{
    class MainMenu
    {
        private int cardInput;
        private int CardAmount
        {
            get
            {
                if (cardInput < 4)
                    return 4;

                if (cardInput % 2 == 0)
                    return cardInput;
                else
                    return cardInput - 1;
            }
        }

        public MainMenu (int cardInput)
        {
            this.cardInput = cardInput;
            AskInput();
        }

        private void AskInput()
        {

        }

        public int GetRows()
        {
            return (int)Math.Sqrt(CardAmount);
        }
        
        public int GetColums()
        {
            return CardAmount / GetRows();
        }
    }
}
