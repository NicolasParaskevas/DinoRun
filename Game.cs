﻿using DinoRun.Objects;
using DinoRun.States;
using Humper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DinoRun
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dino dino;
        Ground ground;
        Ground ground2;
        List<Cloud> clouds;
        List<Cactus> cacti;
        SpriteFont Font;
        State currentState = State.Menu;
        int Score = 0;
        double ScoreTimer = 100;
        double CloudTimer = 1000;
        double CactusTimer = 2000;
        float WorldSpeed = 2;
        bool SpaceReleased = false;
        bool Debug = true;
        //Textures
        List<Texture2D> cactiSprites;
        Texture2D cloudTexture;
        World world;
        Texture2D _DebugTexture;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 400;
            world = new World(800, 400);
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            if (Debug) 
            {
                _DebugTexture = new Texture2D(GraphicsDevice, 1, 1);
                _DebugTexture.SetData(new Color[] { Color.DarkSlateGray });
            }
            dino = new Dino(new Vector2(100, 300), Content.Load<Texture2D>("dino"), Content.Load<Texture2D>("dino_run_sheet"),world, _DebugTexture);
            ground = new Ground(new Vector2(0, 360), Content.Load<Texture2D>("ground")); 
            ground2 = new Ground(new Vector2(800, 360), Content.Load<Texture2D>("ground"));
            
            cactiSprites = new List<Texture2D>();
            cactiSprites.Add(Content.Load<Texture2D>("cactus"));
            cactiSprites.Add(Content.Load<Texture2D>("cactus2"));
            cactiSprites.Add(Content.Load<Texture2D>("cactus3"));
            cacti = new List<Cactus>();

            cloudTexture = Content.Load<Texture2D>("cloud");
            clouds = new List<Cloud>();
            
            Font = Content.Load<SpriteFont>("Font");
            HUD.Init(Font);
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
                    AddCloud(gameTime);
                    foreach (var cloud in clouds)
                        cloud.Update(gameTime);
                    foreach (var cloud in clouds) 
                    {
                        if (cloud.isVisible == false) 
                        {
                            clouds.Remove(cloud);
                            break;
                        }
                    }
                    AddCactus(gameTime);
                    foreach (var cactus in cacti)
                    {
                        cactus.Update(gameTime, WorldSpeed);
                    }
                    foreach (var cactus in cacti)
                    {
                        if (cactus.isVisible == false)
                        {
                            cacti.Remove(cactus);
                            break;
                        }
                    }
                    dino.Update(gameTime);
                    var result = dino.CheckCollision();
                    if (result.HasCollided)
                    {
                        currentState = State.GameOver;
                        Console.WriteLine("Body collided!" + gameTime.TotalGameTime.TotalSeconds.ToString());
                    }
                    UpdateScore(gameTime);
                    break;
                case State.GameOver:
                    if (Keyboard.GetState().IsKeyUp(Keys.Space)) SpaceReleased = true; 
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && SpaceReleased) 
                    {
                        ResetGame();
                        SpaceReleased = false;
                        currentState = State.Game;
                    }
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
                    ground.Draw(spriteBatch);
                    ground2.Draw(spriteBatch);
                    dino.Draw(spriteBatch, currentState);
                    HUD.DrawMenu(spriteBatch, gameTime);
                    break;
                case State.Game:
                    ground.Draw(spriteBatch);
                    ground2.Draw(spriteBatch);
                    
                    foreach (var cloud in clouds) 
                        cloud.Draw(spriteBatch);

                    foreach (var cactus in cacti)
                        cactus.Draw(spriteBatch);

                    dino.Draw(spriteBatch,currentState);
                    HUD.DrawGame(spriteBatch, gameTime, Score);
                    break;
                case State.GameOver:
                    ground.Draw(spriteBatch);
                    ground2.Draw(spriteBatch);
                    foreach (var cloud in clouds)
                        cloud.Draw(spriteBatch);

                    foreach (var cactus in cacti)
                        cactus.Draw(spriteBatch);
                    dino.Draw(spriteBatch, currentState);
                    HUD.DrawGameOVer(spriteBatch,gameTime,Score);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Cactus Logic
        
        private void AddCactus(GameTime gameTime) 
        {
            var rand = new Random(gameTime.TotalGameTime.Seconds);
            CactusTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (CactusTimer <= 0)
            {
                cacti.Add(new Cactus(cactiSprites[rand.Next(3)],world, _DebugTexture)); //max is not inclusive in Next function
                if(WorldSpeed < 5)
                    CactusTimer = 2000;
                else
                    CactusTimer = 1000 + rand.NextDouble() * 500;
            }
        }

        //Score Logic
        private void UpdateScore(GameTime gameTime) 
        {
            ScoreTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (ScoreTimer <= 0) 
            {
                Score++;
                if (Score % 100 == 0 && WorldSpeed < 10)
                    WorldSpeed+=0.5f;

                ScoreTimer = 100;
            }
        }

        //Cloud Logic
        private void AddCloud(GameTime gameTime) 
        {
            var rand = new Random(gameTime.TotalGameTime.Seconds);
            CloudTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if(CloudTimer <= 0)
            {
                clouds.Add(new Cloud(cloudTexture, gameTime));
                CloudTimer = 10000 + rand.NextDouble()*20000; //Spawining time is inbetween 10 to 30 seconds
            }
        }

        private void ResetGame() 
        {
            currentState = State.Reset;
            clouds.Clear();
            foreach (var cactus in cacti)
                world.Remove(cactus.Body);
            cacti.Clear();
            WorldSpeed = 2;
            Score = 0;
        }
    }
}
