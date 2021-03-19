using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoManicMiner.Spectrum;
using SK2D.Graphics;
using SK2D.Input;
using SK2D.StateMachine;

namespace MonoManicMiner.States
{
    public class GameState : BaseState
    {
        private RoomBlocks _roomRenderer;
        private LivesIndicator _lives;
        private BaddieRenderer _baddieRenderer;
        private Image _air;
        private SpectrumFont _font;
        private MMMapFile _mapFile;
        private MinerWillyRenderer _willy;
        private ExitRenderer _exit;
        private AirRenderer _airMeter;
        private MMRoom _currentRoom;
        private ScoreRenderer _scoreRenderer;
        private KeyUp _pauseKey;

        private int _roomId = -1;
        private int _hiScore;

        public GameState(IStateManager stateManager)
            : base(stateManager)
        {
            var blocks = StateManager.Game.ContentManager.LoadTexture("blocks.png");
            var sun = StateManager.Game.ContentManager.LoadTexture("sun.png");
            var background = StateManager.Game.ContentManager.LoadTexture("background.png");
            var sixteen = StateManager.Game.ContentManager.LoadTexture("16x16.png");
            var pick = stateManager.Game.ContentManager.LoadSfx("pick.wav");
            var airMeter = new Texture2D(stateManager.Game.GraphicsDevice, 256, 4);
            airMeter.Fill(Color.White);

            _font = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"));
            _roomRenderer = new RoomBlocks(blocks, background, sun);
            _air = StateManager.Game.ContentManager.LoadImage("titleair.bmp");
            _lives = new LivesIndicator(sixteen);
            _baddieRenderer = new BaddieRenderer(sixteen);
            _willy = new MinerWillyRenderer(sixteen, pick);
            _exit = new ExitRenderer(sixteen);
            _airMeter = new AirRenderer(airMeter);
            _scoreRenderer = new ScoreRenderer(StateManager.Game.ContentManager.LoadTexture("font.png"));

            _willy.ScoreUpdated += Score_Update;

            _pauseKey = new KeyUp(Keys.P);
            _pauseKey.KeyReleased += (o, e) => StateManager.ChangeState("paused", _roomId);
        }

        public override void Enter(params object[] args)
        {
            _roomId = (int)args[0];
            var loadMapFile = (bool)args[1];

            if (loadMapFile)
            {
                _mapFile = StateManager.Game.ContentManager.LoadJson<MMMapFile>("manicminer.json");
                _font.Text = _mapFile.rooms[_roomId].name;
                _lives.Lives = 6;
                _airMeter.AirLeft = _mapFile.rooms[_roomId].airCount;
                _currentRoom = _mapFile.rooms[_roomId].Copy();

                _roomRenderer.SetRoom(_currentRoom, _roomId);
                _baddieRenderer.SetRoom(_currentRoom);
                _willy.SetRoom(_currentRoom, _roomId, ChangeState);
                _exit.SetRoom(_currentRoom, _roomId);
            }

            StateManager.Game.Renderer.AddImage(_roomRenderer, Layer.Background);
            StateManager.Game.Renderer.AddImage(_air, Layer.UI, 0, 16 * 8);

            var offset = 24 + (228 - _airMeter.AirLeft);
            StateManager.Game.Renderer.AddImage(_airMeter, Layer.UI, offset, 10 + (16 * 8));
            StateManager.Game.Renderer.AddImage(_font, Layer.UI, 0, 16 * 8);
            StateManager.Game.Renderer.AddImage(_lives, Layer.UI, 0, 168);
            StateManager.Game.Tweens.AddClamp(0.2f, 0, 3, frame => _lives.Frame = frame);
            StateManager.Game.Tweens.AddClamp(0.2f, 0, 3, frame => _roomRenderer.KeyAnimFrame = frame);

            StateManager.Game.Renderer.AddImage(_scoreRenderer, Layer.UI, 0, 19 * 8);
            StateManager.Game.Renderer.AddImage(_baddieRenderer, Layer.Sprite);
            StateManager.Game.Renderer.AddImage(_willy, Layer.Sprite);
            StateManager.Game.Renderer.AddImage(_exit, Layer.Sprite);

            StateManager.Game.Tweens.Add(1, () => _airMeter.AirLeft--);
        }

        public override void Exit()
        {
            StateManager.Game.Renderer.Clear();
        }

        public override void Run(float deltaTime)
        {
            _pauseKey.Update();
            _exit.Flashing = (_currentRoom.keys.Length == 0);
        }

        private void ChangeState(GameStateType gameStateType)
        {
            switch(gameStateType)
            {
                case GameStateType.LevelDone:
                    _roomId++;
                    // TODO: AIR ...
                    StateManager.ChangeState("game", _roomId, true);
                    break;
            }
        }

        private void Score_Update(object sender, int newScore)
        {
            if (newScore > _hiScore)
            {
                _hiScore = newScore;
            }
            _scoreRenderer.UpdateScore(newScore, _hiScore);
        }
    }
}
