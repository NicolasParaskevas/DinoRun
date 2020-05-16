using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoRun.Managers
{
    public static class ScoreManager
    {
        private static int HighScore { get; set; }
        private static int Score { get; set; }

        private static string path = Path
                .Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DinoRunGame");

        public static int GetHighScore()
        {
            Directory.CreateDirectory(path);
            if (File.Exists(path + @"\score.txt"))
            {
                try
                {
                    HighScore = Int32.Parse(File.ReadLines(path + @"\score.txt").First());
                }
                catch 
                {
                    HighScore = 0;
                }
            }
            else 
            {
                var file = File.Create(path + @"\score.txt");
                file.Close();
                HighScore = 0;
            }
            return HighScore;
        }

        public static void UpdateScore(GameTime gameTime)
        {
            // will be implemented
        }

        public static int SetHighScore(int score) 
        {
            if (score > HighScore) 
            {
                File.WriteAllText(path + @"\score.txt", score.ToString());
                HighScore = score;
            }
            return HighScore;
        }
    }
}
