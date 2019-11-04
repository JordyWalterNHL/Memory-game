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
        private List<string> names = new List<string>();
        private List<int> scores = new List<int>();

        public HighScore()
        {
            SortScores(names, scores);
        }

        //public HighScore(List<string> names, List<int> scores)
        //{
        //    this.names = names;
        //    this.scores = scores;
        //    SortScores(names, scores);
        //}
        private void SortScores(List<string> names, List<int> scores)
        {
            int i, j, tempscore;
            string tempname;
            int N = scores.Count;

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
        public int GetScore(int index)
        {
            int score;
            score = scores[index];
            return score;
        }
        public string GetName(int index)
        {
            string name;
            name = names[index];
            return name;
        }

        public int EntriesAmount()
        {
            return scores.Count;
        }

        public void AddNewHighscore(string name, int score)
        {
            names.Add(name);
            scores.Add(score);

            SortScores(names, scores);
        }
    }
}
