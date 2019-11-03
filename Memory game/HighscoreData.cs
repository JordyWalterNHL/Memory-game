using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_game
{
    [Serializable]
    class HighscoreData
    {
        public Dictionary<int, string> highScore = new Dictionary<int, string>();
    }
}
