global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Content;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
global using System;
global using System.Collections.Generic;
global using WinFormsApp1;

namespace TowerDefence
{
    internal class Globals
    {
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        public static GraphicsDevice Device { get; set; }
        public static float DeltaTime {  get; set; }
        public static float DeltaTimeMilli {  get; set; }

        public static Vector2 WindowSize {  get; set; }
        public static InputHandler Input {  get; set; }

        public static void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTimeMilli = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
