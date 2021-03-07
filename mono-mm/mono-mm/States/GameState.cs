using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using MonoManicMiner.Spectrum;
using SK2D.Graphics;
using SK2D.StateMachine;

namespace MonoManicMiner.States
{
    public class GameState : BaseState
    {
        private RoomBlocks _roomRenderer;
        private LivesIndicator _lives;
        private Image _air;
        private SpectrumFont _font;
        private MMMapFile _mapFile;

        public GameState(IStateManager stateManager)
            : base(stateManager)
        {
            var blocks = StateManager.Game.ContentManager.LoadTexture("blocks.png");
            var sun = StateManager.Game.ContentManager.LoadTexture("sun.png");
            var background = StateManager.Game.ContentManager.LoadTexture("background.png");
            var sixteen = StateManager.Game.ContentManager.LoadTexture("16x16.png");

            _mapFile = StateManager.Game.ContentManager.LoadJson<MMMapFile>("manicminer.json");
            _font = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"), 8);
            _roomRenderer = new RoomBlocks(blocks, background, sun, _mapFile);
            _air = StateManager.Game.ContentManager.LoadImage("titleair.bmp");
            _lives = new LivesIndicator(sixteen);
        }

        public override void Enter(params object[] args)
        {
            var roomId = (int)args[0];
            _roomRenderer.Room = roomId;
            _font.Text = _mapFile.rooms[roomId].name;
            _lives.Lives = 6;

            StateManager.Game.Renderer.AddImage(_roomRenderer, Layer.Background);
            StateManager.Game.Renderer.AddImage(_air, Layer.UI, 0, 16 * 8);
            StateManager.Game.Renderer.AddImage(_font, Layer.UI, 0, 16 * 8);
            StateManager.Game.Renderer.AddImage(_lives, Layer.UI, 0, 168);

            StateManager.Game.Tweens.Add(0.2f, () =>
            {
                _lives.Frame += 1;
                _lives.Frame %= 3;
            });
        }

        public override void Exit()
        {
            StateManager.Game.Renderer.Clear();
        }

        public override void Run(GameTime gameTime)
        {
            
        }
    }
}
