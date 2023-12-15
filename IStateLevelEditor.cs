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
        internal override void Update()
        {
            LevelEdit.Update();
            Tutorials.Update();
        }

        //Ritar upp leveleditorn
        internal override void Draw()
        {
            Globals.Device.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(SpriteSortMode.FrontToBack);

            LevelEdit.Draw();
            Tutorials.Draw();

            Globals.SpriteBatch.End();
        }
    }
}
