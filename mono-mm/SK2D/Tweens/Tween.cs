using System;

namespace SK2D.Tweens
{
    public class Tween
    {
        float _time = 0f;
        float _tick;
        Action _action;

        internal Tween(float tick, Action action)
        {
            _tick = tick;
            _action = action;

        }

        public void Tick(float deltaTime)
        {
            _time += deltaTime;
            if (_time > _tick)
            {
                _time -= _tick;
                _action.Invoke();
            }
        }
    }
}
