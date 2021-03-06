﻿using System;
using Microsoft.Xna.Framework.Input;

namespace SK2D.Input
{
    public class KeyUp
    {
        private bool _wasPressed;
        private Keys _key;

        public event EventHandler KeyReleased;

        public KeyUp(Keys key, Action _action = null)
        {
            _key = key;

            if (_action != null)
            {
                KeyReleased += (o, e) => _action.Invoke();
            }
        }

        public void Update()
        {
            if (!_wasPressed && Keyboard.GetState().IsKeyDown(_key))
            {
                _wasPressed = true;
            } else if (_wasPressed && Keyboard.GetState().IsKeyUp(_key))
            {
                _wasPressed = false;
                KeyReleased?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
