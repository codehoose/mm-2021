using System;

namespace SK2D.Tweens
{
    public class Tween : IPausable
    {
        float _time = 0f;
        float _tick;
        Action _action;

        public bool Paused { get; set; }

        public float TimeScale { get; set; } = 1f;

        internal Tween(float tick, Action action)
        {
            _tick = tick;
            _action = action;
        }

        public void Tick(float deltaTime)
        {
            if (Paused)
            {
                return;
            }

            _time += (deltaTime * TimeScale);
            if (_time > _tick)
            {
                _time -= _tick;
                _action.Invoke();
            }
        }
    }
}
