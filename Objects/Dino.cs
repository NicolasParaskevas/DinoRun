using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DinoRun.States;
using DinoRun.Animations;

namespace DinoRun.Objects
{
    public class Dino
    {
        private Vector2 Position;
        private float Velocity = 0f;
        private float Gravity = 0.5f;
        private bool OnGround = false;
        private bool JumpPressed = false;
        private float InitY;
        Texture2D Sprite;
        AnimatedSprite animSprite;

        public Dino(Vector2 position, Texture2D sprite, Texture2D animationSheet) 
        {
            Position.X = position.X;
            Position.Y = position.Y;
            InitY = position.Y;
            Sprite = sprite;
            animSprite = new AnimatedSprite(animationSheet, 1, 2,0.1);
        }

        public void Update(GameTime gameTime)
        {
            if (Position.Y > InitY)
            {
                Position.Y = InitY;
                Velocity = 0.0f;
                OnGround = true;
                animSprite.Update(gameTime);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && OnGround)
            {
                Velocity = -15f;
                OnGround = false;
                JumpPressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space) && JumpPressed) 
            {
                if (Velocity < -6f)
                    Velocity = -6f;
                JumpPressed = false;
            }
            Velocity += Gravity;
            Position.Y += Velocity;

        }

        public void Draw(SpriteBatch spriteBatch, State state) 
        {
            if (state == State.Game)
                animSprite.Draw(spriteBatch, Position);
            else
                spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
