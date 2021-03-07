using Microsoft.Xna.Framework;

namespace SK2D.StateMachine
{
    public interface IState
    {
        void Enter(params object[] args);

        void Exit();

        void Run(GameTime gameTime);
    }
}
