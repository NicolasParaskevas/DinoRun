using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DinoRun.States;
using DinoRun.Animations;
using Humper;
using Humper.Responses;

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
        public IBox Body { get; private set; }
        Texture2D Sprite;
        AnimatedSprite animSprite;
        Texture2D _DebugTexture;

        public Dino(Vector2 position, Texture2D sprite, Texture2D animationSheet, World world, Texture2D debug_Texture) 
        {
            Position.X = position.X;
            Position.Y = position.Y;
            InitY = position.Y;
            Sprite = sprite;
            Body = world.Create(Position.X+20, Position.Y, Sprite.Width-40, Sprite.Height);
            animSprite = new AnimatedSprite(animationSheet, 1, 2,0.2);
            _DebugTexture = debug_Texture;
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

        public IMovement CheckCollision()
        {
            var result = Body.Move(Position.X+20, Position.Y, (collision) => CollisionResponses.None);
            
            return result;
        }

        public void Draw(SpriteBatch spriteBatch, State state, bool debug) 
        {
            if (state == State.Game && OnGround == true)
                animSprite.Draw(spriteBatch, Position);
            else
                spriteBatch.Draw(Sprite, Position, Color.White);
            if (debug)
                spriteBatch.Draw(_DebugTexture, new Rectangle((int)Body.X,(int)Body.Y,(int)Body.Bounds.Width,(int)Body.Bounds.Height), new Color(Color.Green, 0.5f));
        }
    }
}
