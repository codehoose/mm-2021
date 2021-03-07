using MonoManicMiner.States;
using SK2D;
using SK2D.Graphics;

namespace MonoManicMiner
{
    public class ManicMiner : SK2DGame
    {
        public ManicMiner() : base(512, 384, 2)
        {
        }

        protected override void Boot()
        {
            //var background = ContentManager.LoadImage("background.png");
            //Renderer.AddImage(background, Layer.Background);

            StateManager.Register<TitleScreenState>("title");
            StateManager.ChangeState("title");
        }
    }
}
