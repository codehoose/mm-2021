using Microsoft.Xna.Framework;

namespace SK2D.StateMachine
{
    public interface IState
    {
        void Enter();

        void Exit();

        void Run(GameTime gameTime);
    }
}
