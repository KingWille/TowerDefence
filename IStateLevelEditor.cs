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
            LevelEdit.Draw();
        }
    }
}
