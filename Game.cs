using DinoRun.Objects;
using DinoRun.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DinoRun
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dino dino;
        Ground ground;
        Ground ground2;
        State currentState = State.Menu;
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 400;
            
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            dino = new Dino(new Vector2(100, 300), Content.Load<Texture2D>("dino"));
            ground = new Ground(new Vector2(0, 360), Content.Load<Texture2D>("ground")); 
            ground2 = new Ground(new Vector2(800, 360), Content.Load<Texture2D>("ground"));
        }

        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (currentState)
            {
                case State.Menu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        currentState = State.Game;
                    break;
                case State.Game:
                    ground.Update(gameTime);
                    ground2.Update(gameTime);
                    dino.Update(gameTime);
                    break;
                case State.GameOver:
                    break;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            switch (currentState) 
            {
                case State.Menu:
                case State.Game:
                    ground.Draw(spriteBatch);
                    ground2.Draw(spriteBatch);
                    dino.Draw(spriteBatch);
                    break;
                case State.GameOver:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
