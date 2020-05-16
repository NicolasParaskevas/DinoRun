using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace DinoRun.Objects
{
    //Logic for the Heads up Display (Titles score high score text etc)
    public static class HUD
    {
        private static SpriteFont Font;
        private static string Title = "Press SPACE to Start!";
        private static Vector2 titleSize = new Vector2(0, 0);
        private static Vector2 titlePosition = new Vector2(0, 0);

        private static string GameOverTitle = "Game Over!";
        private static Vector2 gameOverSize = new Vector2(0, 0);
        private static Vector2 gameOverPosition = new Vector2(0, 0);

        public static void Init(SpriteFont font)
        {
            Font = font;
            titleSize = font.MeasureString(Title);
            titlePosition.X = 400 - titleSize.X / 2;
            titlePosition.Y = 150;

            gameOverSize = font.MeasureString(GameOverTitle);
            gameOverPosition.X = 400 - gameOverSize.X / 2;
            gameOverPosition.Y = 150;
        }

        //Drawing the title
        public static void DrawMenu(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int alpha = (int)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2) * 55) + 145;
            spriteBatch.DrawString(Font, Title, titlePosition, new Color(83, 83, 83, alpha));
        }

        //Drawing score & highscore
        public static void DrawGame(SpriteBatch spriteBatch, GameTime gameTime, int score, int highscore)
        {
            DrawScore(spriteBatch, score, highscore);
            if (titlePosition.Y + titleSize.Y > 0) 
            {
                spriteBatch.DrawString(Font, Title, titlePosition, new Color(83, 83, 83));
                titlePosition.Y -= 10;
            }

        }

        //Drawing game over screen
        public static void DrawGameOVer(SpriteBatch spriteBatch, GameTime gameTime, int score, int highscore)
        {
            DrawScore(spriteBatch, score, highscore);
            spriteBatch.DrawString(Font, GameOverTitle, gameOverPosition, new Color(83, 83, 83));
        }

        private static void DrawScore(SpriteBatch spriteBatch,int score, int highscore) 
        {
            spriteBatch.DrawString(Font,score.ToString(), new Vector2(0, 0), new Color(83, 83, 83));
            spriteBatch.DrawString(Font, "BEST: " + highscore.ToString(), new Vector2(0, 30), new Color(83, 83, 83));
        }
    }
}
