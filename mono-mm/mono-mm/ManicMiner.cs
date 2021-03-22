using MonoManicMiner.States;
using SK2D;

namespace MonoManicMiner
{
    public class ManicMiner : SK2DGame
    {
        public ManicMiner() : base(1024, 768, 4)
        {
        }

        protected override void Boot()
        {
            StateManager.Register<TitleScreenState>("title");
            StateManager.Register<GameState>("game");
            StateManager.Register<PauseState>("paused");
            StateManager.Register<GameOverState>("gameover");
            StateManager.ChangeState("game", 0, true);
            //StateManager.ChangeState("gameover", " We must perform a quirkafleeg!", 0, 0);
        }
    }
}
