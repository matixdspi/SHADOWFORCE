using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SHADOWFORCE
{
    

    public class Character
    {
        private Animacion _walkAnimation;
        private Animacion _rollAnimation;
        private Animacion _currentAnimation;

        public Vector2 Position { get; private set; }
        private float _speed = 150f;
        private float _rollDistance = 100f;
        private bool _isRolling;
        private Vector2 _rollDirection;
       

       
      
        public Character(Texture2D walkSheet, Texture2D rollSheet, Vector2 starPosition)
        {
            _walkAnimation = new Animacion(walkSheet, 32, 32, 0.1f, true);
            _rollAnimation = new Animacion(rollSheet, 32, 32, 0.05f, false);
            _currentAnimation = _walkAnimation;
            Position = starPosition;
        }

        public void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            var mouse = Mouse.GetState();
            Vector2 direction = Vector2.Zero;

            // Movimiento 8-direccional
            if (!_isRolling)
            {
                if (keyboard.IsKeyDown(Keys.W)) direction.Y -= 1;
                if (keyboard.IsKeyDown(Keys.S)) direction.Y += 1;
                if (keyboard.IsKeyDown(Keys.A)) direction.X -= 1;
                if (keyboard.IsKeyDown(Keys.D)) direction.X += 1;

                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                    Position += direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            // Rodar con click izquierdo
            if (mouse.LeftButton == ButtonState.Pressed && !_isRolling)
            {
                _isRolling = true;
                _rollAnimation.Reset();
                Vector2 mousePos = new Vector2(mouse.X, mouse.Y);
                _rollDirection = Vector2.Normalize(mousePos - Position);
            }

            if (_isRolling)
            {
                Position += _rollDirection * _rollDistance * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _currentAnimation = _rollAnimation;
                if (_rollAnimation.IsFinished) _isRolling = false;
            }
            else
            {
                _currentAnimation = _walkAnimation;
            }

            _currentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effect = SpriteEffects.None;
            float rotation = 0f;

            // Calcular rotación basada en dirección (opcional)
            if (_rollDirection != Vector2.Zero)
                rotation = (float)Math.Atan2(_rollDirection.Y, _rollDirection.X);

            _currentAnimation.Draw(spriteBatch, Position, effect, rotation);
        }
    }
}
