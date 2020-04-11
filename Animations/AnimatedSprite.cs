using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DinoRun.Animations
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private double ChangeFramePerSecond;
        private double Timer;

        public AnimatedSprite(Texture2D texture, int rows, int columns, double changeFramePerSecond)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            ChangeFramePerSecond = changeFramePerSecond;
            Timer = changeFramePerSecond;
        }

        public void Update(GameTime gameTime)
        {
            Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(Timer < 0) 
            {
                currentFrame++;
                Timer = ChangeFramePerSecond;
            }

            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
