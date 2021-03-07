using Microsoft.Xna.Framework;

namespace SK2D.StateMachine
{
    public interface IStateManager
    {
        SK2DGame Game { get; }

        void Register<T>(string stateName) where T : BaseState;

        void ChangeState(string stateName);

        void Run(GameTime gameTime);
    }
}
