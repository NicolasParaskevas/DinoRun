using Microsoft.Xna.Framework;
using DinoRun.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace DinoRun.Objects
{
    //Logic for the Heads up Display (Titles score high score text etc)
    public static class HUD
    {
        private static string Title = "Press SPACE to Start!";
        private static SpriteFont Font;
        private static Vector2 titleSize = new Vector2(0, 0);
        private static Vector2 titlePosition = new Vector2(0, 0);

        public static void Init(SpriteFont font)
        {
            Font = font;
            titleSize = font.MeasureString(Title);
            titlePosition.X = titleSize.X / 2;
            titlePosition.Y = 150;
        }

        //Drawing the title
        public static void DrawMenu(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int alpha = (int)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2) * 55) + 145;
            spriteBatch.DrawString(Font, Title, titlePosition, new Color(83, 83, 83, alpha));
        }

        //Drawing score & highscore
        public static void DrawGame(SpriteBatch spriteBatch, GameTime gameTime, int score)
        {
            spriteBatch.DrawString(Font, "Score: " + score.ToString(), new Vector2(0, 0), new Color(83, 83, 83));
            if(titlePosition.Y + titleSize.Y > 0) 
            {
                spriteBatch.DrawString(Font, Title, titlePosition, new Color(83, 83, 83));
                titlePosition.Y -= 10;
            }

        }

        //Drawing game over screen
        public static void DrawGameOVer(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
    }
}
