﻿using Humper;
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
        Texture2D _DebugTexture;
        World world;

        public bool isVisible = true;

        public Cactus(Texture2D sprite, World world, Texture2D debug_Texture) 
        {
            Sprite = sprite;
            Position = new Vector2(800,0);
            Position.Y = 380 - Sprite.Height;
            this.world = world;
            Body = world.Create(Position.X, Position.Y, Sprite.Width, Sprite.Height);
            _DebugTexture = debug_Texture;
        }

        public void Update(GameTime gameTime, float worldSpeed) 
        {
            if (Position.X < -Sprite.Width)
            {
                isVisible = false;
                world.Remove(Body);
            }
            Position.X -= 30 * worldSpeed * (1 / (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            Body.Move(Position.X, Position.Y, (collision) => CollisionResponses.None);
        }

        public void Draw(SpriteBatch spriteBatch, bool debug) 
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
            if(debug)
                spriteBatch.Draw(_DebugTexture, new Rectangle((int)Body.X, (int)Body.Y, (int)Body.Bounds.Width, (int)Body.Bounds.Height), new Color(Color.Blue, 0.5f));
        }
    }
}
