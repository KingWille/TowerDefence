using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence
{
    internal class IStateStartMenu : IStateHandler
    {
        private StartMenu menu;
        public IStateStartMenu() 
        {
            menu = new StartMenu(Assets.StartMenu, Vector2.Zero);
        }

        internal override void Update(Game1 game)
        {
            menu.Update();
        }
        internal override void Draw(Game1 game)
        {
            Globals.Device.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin();

            menu.Draw();

            Globals.SpriteBatch.End();
        }
    }
}