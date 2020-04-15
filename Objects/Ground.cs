using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoRun.Objects
{
    public class Ground
    {
        Texture2D Sprite;
        Vector2 Position;

        public Ground(Vector2 position, Texture2D sprite) 
        {
            Sprite = sprite;
            Position = position;
        }

        public void Update(GameTime gameTime, int worldSpeed) 
        {
            if(Position.X < -Sprite.Width) 
            {
                Position.X = Sprite.Width;
            }
            Position.X -= 30*worldSpeed*(1/(float)gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
