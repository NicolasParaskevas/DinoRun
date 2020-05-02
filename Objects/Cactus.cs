﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DinoRun.Objects
{
    public class Cactus
    {
        private Vector2 Position;
        private Texture2D Sprite;

        public bool isVisible = true;

        public Cactus(Texture2D sprite) 
        {
            Position = new Vector2(800,300);
            Sprite = sprite;
        }

        public void Update(GameTime gameTime, float worldSpeed) 
        {
            if (Position.X < -Sprite.Width)
            {
                isVisible = false;
            }
            Position.X -= 30 * worldSpeed * (1 / (float)gameTime.ElapsedGameTime.TotalMilliseconds); ;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Sprite,Position,Color.White);
        }
    }
}