using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence
{
    internal class IStateStartMenu : IStateHandler
    {
        private StartMenu menu;
        public IStateStartMenu() 
        {
            menu = new StartMenu();
        }

        internal override void Update()
        {
            menu.Update();
        }
        internal override void Draw()
        {
            Globals.Device.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin();

            menu.Draw();

            Globals.SpriteBatch.End();
        }
    }
}