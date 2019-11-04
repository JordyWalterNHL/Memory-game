using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_game
{
    [Serializable]
    class HighScore
    {
        /// <summary>
        /// List of names
        /// </summary>
        private List<string> names = new List<string>();
        /// <summary>
        /// List of scores
        /// </summary>
        private List<int> scores = new List<int>();
        /// <summary>
        /// constructor
        /// </summary>
        public HighScore()
        {
            SortScores();
        }
        /// <summary>
        /// Sorts the scores
        /// </summary>
        private void SortScores()
        {
            int i, j, tempscore;
            string tempname;
            int N = scores.Count;
            //Bubble sorting, switches scores and names at the same time, so they keep the same index
            for (j = N - 1; j > 0; j--)
            {
                for (i = 0; i < j; i++)
                {
                    if (scores[i] < scores[i + 1])
                    {
                        tempscore = scores[i + 1];
                        scores[i + 1] = scores[i];
                        scores[i] = tempscore;
                        tempname = names[i + 1];
                        names[i + 1] = names[i];
                        names[i] = tempname;
                    }
                }
            }
        }
        /// <summary>
        /// Gets the score at an int index
        /// </summary>
        /// <param name="index">int: index for the score to return</param>
        /// <returns>int: the score at the indicated index</returns>
        public int GetScore(int index)
        {
            int score;
            score = scores[index];
            return score;
        }
        /// <summary>
        /// Gets the name at an int index
        /// </summary>
        /// <param name="index">int: index for the name to return</param>
        /// <returns>string: the name at the indicated index</returns>
        public string GetName(int index)
        {
            string name;
            name = names[index];
            return name;
        }
        /// <summary>
        /// Returns the amount of entries in scores
        /// </summary>
        /// <returns>int: the count of scores</returns>
        public int EntriesAmount()
        {
            return scores.Count;
        }
        /// <summary>
        /// Adds a new highscore and sorts the lists again
        /// </summary>
        /// <param name="name">name to add</param>
        /// <param name="score">score to add</param>
        public void AddNewHighscore(string name, int score)
        {
            names.Add(name);
            scores.Add(score);

            SortScores();
        }
        /// <summary>
        /// Resets both names and scores
        /// </summary>
        public void Reset()
        {
            names.Clear();
            scores.Clear();
        }
    }
}
