using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DinoRun.Objects
{
    public class Dino
    {
        private Vector2 Position;
        private float Velocity = 0f;
        private float InitY;
        private bool HasJumped = false;
        Texture2D Sprite;

        public Dino(float x, float y, float jumplimit, Texture2D sprite) 
        {
            Position.X = x;
            Position.Y = y;
            InitY = y;
            Sprite = sprite;
        }

        public void Update(GameTime gameTime)
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Up)) && HasJumped == false)
            {
                Position.Y -= 5f;
                Velocity = -10f;
                HasJumped = true;
            }
            if (HasJumped == false)
                Velocity = 0;
            else 
                Velocity += 0.30f;

            if (Position.Y >= InitY) 
            {
                HasJumped = false;
            }
            Position.Y += Velocity;
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
