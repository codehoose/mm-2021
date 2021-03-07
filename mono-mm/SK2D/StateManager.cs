using Microsoft.Xna.Framework;
using SK2D.StateMachine;
using System;
using System.Collections.Generic;

namespace SK2D
{
    public class StateManager : IStateManager
    {
        private Dictionary<string, IState> _states = new Dictionary<string, IState>();

        private IState _currentState;

        public SK2DGame Game { get; }

        public StateManager(SK2DGame game)
        {
            Game = game;
        }

        public void Register<T>(string stateName) where T: BaseState
        {
            var instance = Activator.CreateInstance(typeof(T), this) as IState;
            _states.Add(stateName, instance);
        }

        public void ChangeState(string stateName)
        {
            _currentState?.Exit();

            if (_states.ContainsKey(stateName))
            {
                _currentState = _states[stateName];
                _currentState.Enter();
            }
        }

        public void Run(GameTime gameTime)
        {
            _currentState?.Run(gameTime);
        }
    }
}
