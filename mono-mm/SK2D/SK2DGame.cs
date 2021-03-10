using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SK2D
{
    public class SK2DGame : Game
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _scale;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Color _clearColour;

        public ContentManager.LocalContentManager ContentManager { get; }

        public Graphics.Renderer Renderer { get; private set; }

        public StateManager StateManager { get; }

        public Tweens.TweenManager Tweens { get; } = new Tweens.TweenManager();

        public SK2DGame(int width = 800, int height = 450, int scale = 1)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _clearColour = Color.Black;

            _width = width;
            _height = height;
            _scale = scale;

            ContentManager = new ContentManager.LocalContentManager(this);
            StateManager = new StateManager(this);
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _width;
            _graphics.PreferredBackBufferHeight = _height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Renderer = new Graphics.Renderer(_spriteBatch);
            Renderer.Scale = _scale;

            Boot();
        }

        protected virtual void Boot()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Tweens.Tick(deltaTime);
            Renderer.Update(deltaTime);
            StateManager.Run(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_clearColour);
            Renderer.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
