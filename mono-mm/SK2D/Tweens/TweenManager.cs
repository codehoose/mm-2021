using System;
using System.Collections.Generic;

namespace SK2D.Tweens
{
    public class TweenManager
    {
        private readonly List<Tween> _tweens = new List<Tween>();

        public Tween Add(float tick, Action action)
        {
            var tween = new Tween(tick, action);
            _tweens.Add(tween);
            return tween;
        }

        public Tween AddClamp(float tick, int min, int max, Action<int> setter)
        {
            var i = min;
            var tween = new Tween(tick, () =>
            {
                i++;
                i %= max;
                setter(i);
            });
            _tweens.Add(tween);
            return tween;
        }

        public void Remove(Tween tween)
        {
            if (_tweens.Contains(tween))
            {
                _tweens.Remove(tween);
            }
        }

        public void Tick(float deltaTime)
        {
            foreach (var tween in _tweens)
            {
                tween.Tick(deltaTime);
            }
        }
    }
}
