using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoManicMiner.Spectrum;
using SK2D.Graphics;
using SK2D.StateMachine;
using SK2D.Tweens;

namespace MonoManicMiner.States
{
    class TitleScreenState : BaseState
    {
        private readonly Image _background;
        private readonly Image _background2;
        private readonly Image _background3;
        private readonly Image _sun;
        private readonly Image _air;
        private readonly Image _piano;
        private SpectrumFont _font;
        private int _minScrollPos;
        private int _startLevel;
        private Tween _tween;

        public TitleScreenState(IStateManager stateManager)
            : base(stateManager)
        {
            _background = StateManager.Game.ContentManager.LoadImage("background.png");
            _background2 = StateManager.Game.ContentManager.LoadImage("bgfill1.bmp");
            _background3 = StateManager.Game.ContentManager.LoadImage("bgfill2.bmp");
            _sun = StateManager.Game.ContentManager.LoadImage("sun.png");
            _air = StateManager.Game.ContentManager.LoadImage("titleair.bmp");
            _piano = StateManager.Game.ContentManager.LoadImage("piano.bmp");

            _font = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"));
            _font.Text = "                                  .  .  .  .  .  .  .  .  .  . MANIC MINER . .  BUG-BYTE ltd. 1983 . . By Matthew Smith . . . Q to P = Left & Right . . Bottom row = Jump . . A to G = Pause . . H to L = Tune On/Off . . . Guide Miner Willy through 20 lethal caverns   .  .  .  .  .  .  .  .                                        ";

            _minScrollPos = _font.Text.Length * -8;
        }

        public override void Enter(params object[] args)
        {
            StateManager.Game.Renderer.AddImage(_background, Layer.Background);
            StateManager.Game.Renderer.AddImage(_background2, Layer.Background, 19 * 8, 5 * 8);
            StateManager.Game.Renderer.AddImage(_background3, Layer.Background, 22 * 8, 5 * 8);
            StateManager.Game.Renderer.AddImage(_sun, Layer.Background, 60, 4 * 8);
            StateManager.Game.Renderer.AddImage(_piano, Layer.Background, 0, 8 * 8);
            StateManager.Game.Renderer.AddImage(_air, Layer.Background, 0, 16 * 8);
            StateManager.Game.Renderer.AddImage(_font, Layer.Background, 0, 16 * 8);

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
            StateManager.Game.Renderer.Clear();
        }

        public override void Run(float deltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                StateManager.ChangeState("game", _startLevel);
            }
            base.Run(deltaTime);
        }
    }
}
