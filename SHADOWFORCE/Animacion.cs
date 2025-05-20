using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SHADOWFORCE
{
    
    public class Animacion
    {
        private Texture2D _texture;
        private float _timer;
        private float _frameTime;
        private int _frameCount;
        private int _currentFrame;
        private bool _isLooping;
        public Vector2 Origin { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }

        public Animacion(Texture2D texture, int frameWidth, int frameHeight, float frameTime, bool isLooping)
        {
            _texture = texture;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            _frameTime = frameTime;
            _isLooping = isLooping;
            _frameCount = _texture.Width / frameWidth;
            Origin = new Vector2(frameWidth / 2f, frameHeight / 2f);
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= _frameTime)
            {
                _timer = 0f;
                _currentFrame++;

                if (_currentFrame >= _frameCount)
                {
                    if (_isLooping) _currentFrame = 0;
                    else _currentFrame = _frameCount - 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effect, float rotation = 0f)
        {
            Rectangle sourceRect = new Rectangle(
                _currentFrame * FrameWidth,
                0,
                FrameWidth,
                FrameHeight
            );

            spriteBatch.Draw(
                _texture,
                position,
                sourceRect,
                Color.White,
                rotation,
                Origin,
                1f,
                effect,
                0f
            );
        }

        public void Reset() => _currentFrame = 0;
        public bool IsFinished => _currentFrame >= _frameCount - 1;
    }
}
