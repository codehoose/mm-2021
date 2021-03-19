using MonoManicMiner.States;
using SK2D;

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
            StateManager.Register<PauseState>("paused");
            StateManager.ChangeState("game", 0, true);
            //StateManager.ChangeState("title");
        }
    }
}
