using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoRun.Objects
{
    public class Cloud
    {
        private Vector2 Position;
        private Texture2D Sprite;
        private float Speed;
        public bool isVisible = true;
        
        public Cloud(Texture2D sprite, GameTime gameTime)
        {
            Sprite = sprite;
            Random rand = new Random(gameTime.TotalGameTime.Seconds);
            Position = new Vector2(800, rand.Next(50,250));
            Speed = (float)rand.NextDouble() + (float)0.1;
            isVisible = true;
        }

        public void Update(GameTime gameTime) 
        {
            
            if (Position.X < -Sprite.Width)
            {
                isVisible = false;
            }
            Position.X -= Speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
