using Microsoft.Xna.Framework;
using MonoManicMiner.Spectrum;
using SK2D.Graphics;
using SK2D.StateMachine;
using SK2D.Tweens;

namespace MonoManicMiner.States
{
    class TitleScreenState : BaseState
    {
        private SpectrumFont _font;
        private int _minScrollPos;
        private Tween _tween;

        public TitleScreenState(IStateManager stateManager)
            : base(stateManager)
        {
            var background = StateManager.Game.ContentManager.LoadImage("background.png");
            var background2 = StateManager.Game.ContentManager.LoadImage("bgfill1.bmp");
            var background3 = StateManager.Game.ContentManager.LoadImage("bgfill2.bmp");
            var sun = StateManager.Game.ContentManager.LoadImage("sun.png");
            var air = StateManager.Game.ContentManager.LoadImage("titleair.bmp");
            var piano = StateManager.Game.ContentManager.LoadImage("piano.bmp");

            _font = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"), 8);
            _font.Text = "                                  .  .  .  .  .  .  .  .  .  . MANIC MINER . .  BUG-BYTE ltd. 1983 . . By Matthew Smith . . . Q to P = Left & Right . . Bottom row = Jump . . A to G = Pause . . H to L = Tune On/Off . . . Guide Miner Willy through 20 lethal caverns   .  .  .  .  .  .  .  .                                        ";

            _minScrollPos = _font.Text.Length * -8;

            StateManager.Game.Renderer.AddImage(background, Layer.Background);
            StateManager.Game.Renderer.AddImage(background2, Layer.Background, 19 * 8, 5 * 8);
            StateManager.Game.Renderer.AddImage(background3, Layer.Background, 22 * 8, 5 * 8);
            StateManager.Game.Renderer.AddImage(sun, Layer.Background, 60, 4 * 8);
            StateManager.Game.Renderer.AddImage(piano, Layer.Background, 0, 8 * 8);
            StateManager.Game.Renderer.AddImage(air, Layer.Background, 0, 16 * 8);
            StateManager.Game.Renderer.AddImage(_font, Layer.Background, 0, 16 * 8);
        }

        public override void Enter()
        {
            _font.Position = new Vector2(0, 16 * 8);
            _tween = StateManager.Game.Tweens.Add(0.1f, () =>
            {
                _font.Position = new Vector2(_font.Position.X - 8, _font.Position.Y);
                if (_font.Position.X < _minScrollPos)
                {
                    _font.Position = new Vector2(0, _font.Position.Y);
                }
            });
        }

        public override void Exit()
        {
            StateManager.Game.Tweens.Remove(_tween);
        }

        public override void Run(GameTime gameTime)
        {
            
        }
    }
}
