namespace TowerDefence
{
    internal class IStateLevelEditor : IStateHandler
    {
        private LevelEditor LevelEdit;
        public IStateLevelEditor() 
        { 
            LevelEdit = new LevelEditor();
        }

        //Uppdaterar leveleditorn
        internal override void Update(Game1 game)
        {
            LevelEdit.Update();
        }

        //Ritar upp leveleditorn
        internal override void Draw(Game1 game)
        {
            Globals.Device.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack);

            LevelEdit.Draw();

            Globals.SpriteBatch.End();
        }
    }
}
