using System;
using Microsoft.Xna.Framework;
using MonoManicMiner.Spectrum;
using SK2D.Graphics;
using SK2D.StateMachine;

namespace MonoManicMiner.States
{
    public class GameOverState : BaseState
    {
        private SpriteSheet _pedastal;
        private SpriteSheet _boot;
        private SpriteSheet _willy;
        private SpectrumFont _roomName;
        private SpectrumFont _gameOver;
        private Image _air;

        private int _count = 2;

        public GameOverState(IStateManager stateManager)
            : base(stateManager)
        {
            _roomName = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"));
            _gameOver = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"));
            _pedastal = new SpriteSheet(StateManager.Game.ContentManager.LoadTexture("16x16.png"));
            _willy = new SpriteSheet(StateManager.Game.ContentManager.LoadTexture("16x16.png"));
            _boot = new SpriteSheet(StateManager.Game.ContentManager.LoadTexture("16x16.png"));
            _air = StateManager.Game.ContentManager.LoadImage("titleair.bmp");
        }

        public override void Enter(params object[] args)
        {
            var name = args[0].ToString();
            var score = (int)args[1];
            var hiScore = (int)args[2];

            _roomName.Text = name;

            _pedastal.SetFrame(460);
            _boot.SetFrame(461);
            _willy.SetFrame(0);

            StateManager.Game.Renderer.Clear();

            AddImage(_air, Layer.UI, 0, 16 * 8);
            AddImage(_roomName, Layer.UI, 0, 16 * 8);
            AddImage(_pedastal, Layer.UI, 120, 14 * 8);
            AddImage(_willy, Layer.UI, 120, 12 * 8);
            AddImage(_boot, Layer.UI, 120, 0);
        }

        public override void Exit()
        {
            StateManager.Game.Renderer.ClearBackBuffer = true;
            StateManager.Game.Renderer.Clear();
        }

        public override void Run(float deltaTime)
        {
            if (_count > 0)
            {
                _count--;
                return;
            }

            StateManager.Game.Renderer.ClearBackBuffer = false;

            var y = _boot.Position.Y;
            y += 128 * deltaTime;
            if (y >= 96)
            {
                y = 96;
                _willy.Hidden = true;
            }
            _boot.Position = new Vector2(_boot.Position.X, y);
        }
    }
}
