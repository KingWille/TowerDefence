﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    internal class InputHandler
    {
        private KeyboardState currentPressedKey, wasPressedKey;
        internal MouseState currentMouseState, wasMouseState;


        public MouseState GetMouseState()
        {
            wasMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            return currentMouseState;
        }
        //Berättar vilken tangent som trycks ner
        public KeyboardState GetState()
        {
            wasPressedKey = currentPressedKey;
            currentPressedKey = Keyboard.GetState();
            return currentPressedKey;
        }

        public bool HasBeenClicked()
        {
            return wasMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released;
        }
        //Försäkrar programmet att man måste släppa tangenten och trycka igen för att något annat ska hända
        public bool HasBeenPressed(Keys key)
        {
            return currentPressedKey.IsKeyDown(key) && !wasPressedKey.IsKeyDown(key);
        }

        //Kollar om en tangent är nertryckt
        public bool IsPressed(Keys key)
        {
            return currentPressedKey.IsKeyDown(key);
        }
    }
}
