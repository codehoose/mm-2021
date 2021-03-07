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
            StateManager.Register<TitleScreenState>("title");
            StateManager.Register<GameState>("game");
            StateManager.ChangeState("game", 0);
        }
    }
}
