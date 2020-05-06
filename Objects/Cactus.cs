using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humper;
using Humper.Responses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DinoRun.Objects
{
    public class Cactus
    {
        private Vector2 Position;
        private Texture2D Sprite;
        public IBox Body { get; private set; }

        public bool isVisible = true;

        public Cactus(Texture2D sprite, World world) 
        {
            Sprite = sprite;
            Position = new Vector2(800,0);
            Position.Y = 380 - Sprite.Height;
            Body = world.Create(Position.X, Position.Y, Sprite.Width, Sprite.Height);
        }

        public void Update(GameTime gameTime, float worldSpeed) 
        {
            if (Position.X < -Sprite.Width)
            {
                isVisible = false;
            }
            Position.X -= 30 * worldSpeed * (1 / (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            Body.Move(Position.X, Position.Y, (collision) => CollisionResponses.None);
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
            spriteBatch.Draw(Sprite,new Vector2(Body.X,Body.Y),new Color(Color.Blue,0.5f));
        }
    }
}
