using SK2D.Graphics;

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

        public virtual void Run(float deltaTime)
        {

        }

        protected void AddImage(Image image, Layer layer)
        {
            StateManager.Game.Renderer.AddImage(image, layer);
        }

        protected void AddImage(Image image, Layer layer, int x, int y)
        {
            StateManager.Game.Renderer.AddImage(image, layer, x, y);
        }
    }
}
