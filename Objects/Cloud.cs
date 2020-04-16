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
        
        public Cloud(Vector2 position, Texture2D sprite, float speed)
        {
            Position = position;
            Speed = speed;
            Sprite = sprite;
        }

        public void Update(GameTime gameTime) 
        {
            Position.X -= Speed;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
