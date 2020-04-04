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
        private float Gravity = 0.1f;
        private bool OnGround = false;
        private float InitY;
        Texture2D Sprite;

        public Dino(float x, float y, Texture2D sprite) 
        {
            Position.X = x;
            Position.Y = y;
            InitY = y;
            Sprite = sprite;
        }
        public void Update(GameTime gameTime)
        {
            if (Position.Y > InitY)
            {
                Position.Y = InitY;
                Velocity = 0.0f;
                OnGround = true;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.Up)) && OnGround == true)
            {
                Velocity = -100f;
                OnGround = false;
            }
            //Change the if satement, it should only fire when the key is released on when it is up
            if (Keyboard.GetState().IsKeyUp(Keys.Space) || Keyboard.GetState().IsKeyUp(Keys.Up)) 
            {
                if (Velocity < -6f)
                    Velocity = -6f;
            }
            Velocity += Gravity;
            Position.Y += Velocity;

        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
