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
        SpriteFont Font;
        State currentState = State.Menu;
        int Score = 0;
        double ScoreTimer = 100;
        int WorldSpeed = 1;
        //Title
        Vector2 titleSize = new Vector2(0, 0);
        string Title = "Press SPACE to Start!";


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
            dino = new Dino(new Vector2(100, 300), Content.Load<Texture2D>("dino"), Content.Load<Texture2D>("dino_run_sheet"));
            ground = new Ground(new Vector2(0, 360), Content.Load<Texture2D>("ground")); 
            ground2 = new Ground(new Vector2(800, 360), Content.Load<Texture2D>("ground"));
            Font = Content.Load<SpriteFont>("Font");

            titleSize = Font.MeasureString(Title);
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
                    ground.Update(gameTime, WorldSpeed);
                    ground2.Update(gameTime, WorldSpeed);
                    dino.Update(gameTime);
                    UpdateScore(gameTime);
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
                    spriteBatch.DrawString(Font, Title, new Vector2(titleSize.X/2, 100), new Color(83, 83, 83));
                    ground.Draw(spriteBatch);
                    ground2.Draw(spriteBatch);
                    dino.Draw(spriteBatch, currentState);
                    break;
                case State.Game:
                    ground.Draw(spriteBatch);
                    ground2.Draw(spriteBatch);
                    dino.Draw(spriteBatch,currentState);
                    spriteBatch.DrawString(Font, "Score: " + Score.ToString(),new Vector2(0, 0), new Color(83,83,83));
                    break;
                case State.GameOver:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpdateScore(GameTime gameTime) 
        {
            ScoreTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (ScoreTimer <= 0) 
            {
                Score++;
                ScoreTimer = 100;
            }
        }
        public void ResetScore() 
        {
            Score = 0;
        }
    }
}
