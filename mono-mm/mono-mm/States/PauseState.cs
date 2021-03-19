using Microsoft.Xna.Framework.Input;
using MonoManicMiner.Spectrum;
using SK2D.Input;
using SK2D.StateMachine;

namespace MonoManicMiner.States
{
    class PauseState : BaseState
    {
        private SpectrumFont _font;
        private KeyUp _pause;
        private int _roomId;

        public PauseState(IStateManager stateManager)
            : base(stateManager)
        {
            _font = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"));
            _font.Text = $"`{FontColor.Cyan}- P A U S E D -";

            _pause = new KeyUp(Keys.P);
            _pause.KeyReleased += (o, e) => StateManager.ChangeState("game", _roomId, false);

        }

        public override void Enter(params object[] args)
        {
            _roomId = (int)args[0];
            StateManager.Game.Renderer.AddImage(_font, SK2D.Graphics.Layer.UI);
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
