using Microsoft.Xna.Framework.Input;
using MonoManicMiner.Spectrum;
using SK2D.Input;
using SK2D.StateMachine;

namespace MonoManicMiner.States
{
    class PauseState : BaseState
    {
        private SpectrumFont _paused;
        private SpectrumFont _pressKey;
        private KeyUp _pause;
        private int _roomId;

        public PauseState(IStateManager stateManager)
            : base(stateManager)
        {
            _paused = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"));
            _pressKey = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"));
            _paused.Text = $"`{FontColor.Cyan}- P A U S E D -";
            _pressKey.Text = $"`{FontColor.Yellow}Press 'P' to resume";

            _pause = new KeyUp(Keys.P);
            _pause.KeyReleased += (o, e) => StateManager.ChangeState("game", _roomId, false);

        }

        public override void Enter(params object[] args)
        {
            _roomId = (int)args[0];
            StateManager.Game.Renderer.AddImage(_paused, SK2D.Graphics.Layer.UI, 64, 11 * 8);
            StateManager.Game.Renderer.AddImage(_pressKey, SK2D.Graphics.Layer.UI, 6 * 8, 13 * 8);
        }

        public override void Run(float deltaTime)
        {
            _pause.Update();
            base.Run(deltaTime);
        }

        public override void Exit()
        {
            StateManager.Game.Renderer.Clear();
        }
    }
}
