using System.Collections.Generic;
using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoManicMiner.Spectrum;
using SK2D;
using SK2D.Graphics;
using SK2D.Input;
using SK2D.StateMachine;
using SK2D.Tweens;

namespace MonoManicMiner.States
{
    public class GameState : BaseState
    {
        private static float TRICKDOWN_COOLDOWN = 0.03f;
        private static Color DARK_COLOUR = Color.Blue;

        private SoundEffect _pickUpKey;
        private SoundEffect _die;

        private List<IPausable> _pauseables = new List<IPausable>();
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
        private KeyUp _quitKey;
        private KeyUp _yesKey;
        private KeyUp _noKey;

        private Tween _livesAnimationTween;
        private Tween _keyAnimationTween;
        private Tween _airMeterTween;

        private int _roomId = -1;
        private int _score;
        private int _hiScore;
        private QuitGame _quitGameRenderer;
        private bool _quitShowing;
        private bool _doEnd;
        private float _trickleDownTime;

        public GameState(IStateManager stateManager)
            : base(stateManager)
        {
            var blocks = StateManager.Game.ContentManager.LoadTexture("blocks.png");
            var sun = StateManager.Game.ContentManager.LoadTexture("sun.png");
            var background = StateManager.Game.ContentManager.LoadTexture("background.png");
            var sixteen = StateManager.Game.ContentManager.LoadTexture("16x16.png");
            var airMeter = new Texture2D(stateManager.Game.GraphicsDevice, 256, 4);
            airMeter.Fill(Color.White);

            _pickUpKey = StateManager.Game.ContentManager.LoadSfx("pick.wav");
            _die = StateManager.Game.ContentManager.LoadSfx("die.wav");

            _font = new SpectrumFont(StateManager.Game.ContentManager.LoadTexture("font.png"));
            _roomRenderer = new RoomBlocks(blocks, background, sun);
            _air = StateManager.Game.ContentManager.LoadImage("titleair.bmp");
            _lives = new LivesIndicator(sixteen);
            _baddieRenderer = new BaddieRenderer(sixteen);
            _willy = new MinerWillyRenderer(sixteen);
            _exit = new ExitRenderer(sixteen);
            _airMeter = new AirRenderer(airMeter);
            _scoreRenderer = new ScoreRenderer(StateManager.Game.ContentManager.LoadTexture("font.png"));

            _pauseables.Add(_willy);
            _pauseables.Add(_baddieRenderer);
            _pauseables.Add(_roomRenderer);
            _pauseables.Add(_exit);

            var blackBackground = new Texture2D(StateManager.Game.GraphicsDevice, 128, 24);
            blackBackground.Fill(Color.Black);
            _quitGameRenderer = new QuitGame(StateManager.Game.ContentManager.LoadTexture("font.png"), blackBackground);
            _quitGameRenderer.Hidden = true;

            _willy.IncrementScore += Score_Update;

            _willy.OnDeath += Willy_Death;

            _pauseKey = new KeyUp(Keys.P, () =>
            {
                StateManager.ChangeState("paused", _roomId);
            });

            _quitKey = new KeyUp(Keys.Escape, () =>
            {
                ToggleQuit(!_quitShowing);
            });

            _yesKey = new KeyUp(Keys.Y, () =>
            {
                ToggleQuit(false);
                StateManager.ChangeState("title");
            });

            _noKey = new KeyUp(Keys.N, () =>
            {
                ToggleQuit(false);
            });
        }

        public override void Enter(params object[] args)
        {
            _roomId = (int)args[0];
            var loadMapFile = (bool)args[1];

            var lives = 2;

            if (args.Length > 2)
            {
                _score = (int)args[2];
                _hiScore = (int)args[3];
                lives = (int)args[4];
            }

            if (loadMapFile)
            {
                _mapFile = StateManager.Game.ContentManager.LoadJson<MMMapFile>("manicminer.json");
                _font.Text = _mapFile.rooms[_roomId].name;
                _lives.Lives = lives;
                _airMeter.AirLeft = _mapFile.rooms[_roomId].airCount;
                _currentRoom = _mapFile.rooms[_roomId].Copy();

                _roomRenderer.SetRoom(_currentRoom, _roomId);
                _baddieRenderer.SetRoom(_currentRoom);
                _willy.SetRoom(_currentRoom, _roomId, ChangeState);
                _exit.SetRoom(_currentRoom, _roomId);
                _scoreRenderer.UpdateScore(_score, _hiScore);
            }

            _livesAnimationTween = StateManager.Game.Tweens.AddClamp(0.2f, 0, 3, frame => _lives.Frame = frame);
            _keyAnimationTween = StateManager.Game.Tweens.AddClamp(0.2f, 0, 3, frame => _roomRenderer.KeyAnimFrame = frame);
            _airMeterTween = StateManager.Game.Tweens.Add(1, () => _airMeter.AirLeft--);

            _pauseables.Add(_livesAnimationTween);
            _pauseables.Add(_keyAnimationTween);
            _pauseables.Add(_airMeterTween);

            // Force all paused items to become unpaused. This is for when
            // the level changes
            _pauseables.ForEach(p => p.Paused = false);

            AddImage(_roomRenderer, Layer.Background);
            AddImage(_air, Layer.UI, 0, 16 * 8);

            AddImage(_airMeter, Layer.UI, 28, 10 + (16 * 8));
            AddImage(_font, Layer.UI, 0, 16 * 8);
            AddImage(_lives, Layer.UI, 0, 168);

            AddImage(_scoreRenderer, Layer.UI, 0, 19 * 8);
            AddImage(_baddieRenderer, Layer.Sprite);
            AddImage(_willy, Layer.Sprite);
            AddImage(_exit, Layer.Sprite);
            AddImage(_quitGameRenderer, Layer.UI);
        }

        public override void Exit()
        {
            _pauseables.Remove(_livesAnimationTween);
            _pauseables.Remove(_keyAnimationTween);
            _pauseables.Remove(_airMeterTween);

            StateManager.Game.Renderer.Clear();
            StateManager.Game.Tweens.Remove(_airMeterTween);
            StateManager.Game.Tweens.Remove(_keyAnimationTween);
            StateManager.Game.Tweens.Remove(_livesAnimationTween);
        }

        public override void Run(float deltaTime)
        {
            if (_doEnd)
            {
                TrickleDownAir(deltaTime);
                return;
            }

            if (!_quitShowing)
            {
                _pauseKey.Update();
            }
            else
            {
                _yesKey.Update();
                _noKey.Update();
            }

            _quitKey.Update();
            _exit.Flashing = (_currentRoom.keys.Length == 0);
        }

        private void ChangeState(GameStateType gameStateType)
        {
            switch (gameStateType)
            {
                case GameStateType.LevelDone:
                    if (_exit.Flashing)
                    {
                        DoLevelEnd();
                    }
                    break;
            }
        }

        private void DoLevelEnd()
        {
            _doEnd = true;
            MakeBlue(true);
            _pauseables.ForEach(p => p.Paused = true);
        }

        private void TrickleDownAir(float deltaTime)
        {
            _trickleDownTime += deltaTime;
            if (_trickleDownTime >= TRICKDOWN_COOLDOWN)
            {
                _trickleDownTime -= TRICKDOWN_COOLDOWN;
                _airMeter.AirLeft--;
                _score += 10;
                if (_score > _hiScore)
                {
                    _hiScore = _score;
                }
                _scoreRenderer.UpdateScore(_score, _hiScore);

                if (_airMeter.AirLeft <= 0)
                {
                    _doEnd = false;
                    _trickleDownTime = 0;
                    MakeBlue(false);
                    StateManager.ChangeState("game", _roomId + 1, true, _score, _hiScore, _lives.Lives);
                }
            }
        }

        private void MakeBlue(bool makeBlue)
        {
            _font.DrawColor = makeBlue ? DARK_COLOUR : Color.White;
            _roomRenderer.DrawColor = makeBlue ? DARK_COLOUR : Color.White;
            _lives.DrawColor = makeBlue ? DARK_COLOUR : Color.White;
            _baddieRenderer.DrawColor = makeBlue ? DARK_COLOUR : Color.White;
            _willy.DrawColor = makeBlue ? DARK_COLOUR : Color.White;
            _exit.DrawColor = makeBlue ? DARK_COLOUR : Color.White;
        }

        private void Score_Update(object sender, int points)
        {
            _pickUpKey.Play();
            _score += points;
            if (_score > _hiScore)
            {
                _hiScore = _score;
            }
            _scoreRenderer.UpdateScore(_score, _hiScore);
        }

        private void Willy_Death(object sender, System.EventArgs e)
        {
            _die.Play();
            _lives.Lives--;
            if (_lives.Lives < 0)
            {
                StateManager.ChangeState("gameover", _currentRoom.name, _score, _hiScore);
            }
        }

        private void ToggleQuit(bool showQuitConfirm)
        {
            _quitShowing = showQuitConfirm;
            _quitGameRenderer.Hidden = !_quitShowing;
            _pauseables.ForEach(p => p.Paused = showQuitConfirm);
        }
    }
}
