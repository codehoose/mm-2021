using Microsoft.Xna.Framework;

namespace SK2D.StateMachine
{
    public abstract class BaseState : IState
    {
        protected IStateManager StateManager { get; }

        public BaseState(IStateManager stateManager)
        {
            StateManager = stateManager;
        }

        public abstract void Enter(params object[] args);

        public virtual void Exit()
        {
            
        }

        public abstract void Run(GameTime gameTime);
    }
}
